using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public class TextLogger : IDisposable, INotifyPropertyChanged
    {
        // ring buffer for strings
        // string complete timeout
        // string length limit

        private object _textOutThreadLock = new object();

        public bool noScreenOutput = false;
        public int LineLimit = 0;
        public int CharLimit = 0;
        public int LineTimeLimit = 0;
        public string LogFileName = "";
        public bool AutoSave = false;
        public bool AutoScroll = true;
        public bool FilterZeroChar = true;
        public TextFormat DefaultTextFormat = TextFormat.AutoReplaceHex; //Text, HEX, Auto (change non-readable to <HEX>)
        public TimeFormat DefaultTimeFormat = TimeFormat.LongTime;
        public DateFormat DefaultDateFormat = DateFormat.ShortDate;

        private delegate void SetTextCallback(string text);

        private readonly Form _mainForm;
        private readonly TextBox _textBox;
        private string _logBuffer = "";
        private int _selStart;
        private volatile bool _textChanged;
        private Timer _formTimer;

        public Dictionary<int, string> Channels = new Dictionary<int, string>();

        private byte _prevChannel;
        private DateTime _lastEvent = DateTime.Now;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged()
        {
            _textChanged = true;
            _mainForm?.Invoke((MethodInvoker)delegate
           {
               PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Text"));
           });
        }

        public string Text => _logBuffer;

        public TextLogger(Form mainForm, TextBox textBox = null)
        {
            this._mainForm = mainForm;
            _textBox = textBox;
        }

        public void TimerStart(int delay)
        {
            if (_mainForm != null && _textBox != null)
            {
                _formTimer = new Timer();
                _formTimer.Tick += FormTimer_Tick;
                _formTimer.Interval = delay;
                _formTimer.Start();
            }
        }

        public void TimerStop()
        {
            _formTimer?.Stop();
            _formTimer?.Dispose();
        }

        private void FormTimer_Tick(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        public enum TextFormat
        {
            Default,
            PlainText,
            Hex,
            AutoReplaceHex
        }

        public enum TimeFormat
        {
            None,
            Default,
            ShortTime,
            LongTime
        }

        public enum DateFormat
        {
            None,
            Default,
            ShortDate,
            LongDate,
        }

        public bool AddText(string text, byte channel = 0)
        {
            return AddText(text, DateTime.MinValue, channel, TextFormat.Default, TimeFormat.Default);
        }

        public bool AddText(string text, byte channel, TimeFormat timeFormat = TimeFormat.Default)
        {
            return AddText(text, DateTime.MinValue, channel, TextFormat.Default, timeFormat);
        }

        public bool AddText(string text, byte channel, TextFormat textTextFormat = TextFormat.Default)
        {
            return AddText(text, DateTime.MinValue, channel, textTextFormat, TimeFormat.Default);
        }

        public bool AddText(string text, byte channel, TextFormat textTextFormat, TimeFormat timeFormat)
        {
            return AddText(text, DateTime.MinValue, channel, textTextFormat, timeFormat);
        }

        public bool AddText(string text, DateTime logTime, byte channel = 0, TextFormat textFormat = TextFormat.Default,
            TimeFormat timeFormat = TimeFormat.Default, DateFormat dateFormat = DateFormat.Default)
        {
            if (text == null || text.Length <= 0) return true;

            if (timeFormat != TimeFormat.None && dateFormat != DateFormat.None) logTime = DateTime.Now;

            var tmpStr = new StringBuilder();
            var continueString = false;
            if (channel != _prevChannel)
            {
                _prevChannel = channel;
            }
            else if (LineTimeLimit > 0)
            {
                var t = (int)logTime.Subtract(_lastEvent).TotalMilliseconds;
                if (t <= LineTimeLimit)
                    continueString = true;

                _lastEvent = logTime;
            }

            if (!continueString)
            {
                tmpStr.Append(Environment.NewLine);
                if (logTime != DateTime.MinValue)
                {
                    if (dateFormat == DateFormat.Default)
                        dateFormat = DefaultDateFormat;

                    if (timeFormat == TimeFormat.Default)
                        timeFormat = DefaultTimeFormat;

                    if (dateFormat == DateFormat.LongDate)
                        tmpStr.Append(logTime.ToLongDateString() + " ");
                    else if (dateFormat == DateFormat.ShortDate)
                        tmpStr.Append(logTime.ToShortDateString() + " ");

                    if (timeFormat == TimeFormat.LongTime)
                        tmpStr.Append(logTime.ToLongTimeString() + "." + logTime.Millisecond.ToString("D3") + " ");

                    else if (timeFormat == TimeFormat.ShortTime)
                        tmpStr.Append(logTime.ToShortTimeString() + " ");
                }

                if (Channels.ContainsKey(channel))
                {
                    tmpStr.Append(Channels[channel] + " ");
                }
            }

            if (textFormat == TextFormat.Default) textFormat = DefaultTextFormat;

            if (FilterZeroChar)
                text = Accessory.FilterZeroChar(text);

            if (textFormat == TextFormat.PlainText)
            {
                tmpStr.Append(text);
            }
            else if (textFormat == TextFormat.Hex)
            {
                tmpStr.Append(Accessory.ConvertStringToHex(text));
            }
            else if (textFormat == TextFormat.AutoReplaceHex)
            {
                tmpStr.Append(ReplaceUnprintable(text));
            }

            return AddTextToBuffer(tmpStr.ToString());
        }

        private bool AddTextToBuffer(string text)
        {
            if (text == null || text.Length <= 0) return false;
            lock (_textOutThreadLock)
            {
                if (AutoSave && !string.IsNullOrEmpty(LogFileName))
                {
                    File.AppendAllText(LogFileName, text);
                }

                if (noScreenOutput) return true;

                if (_textBox != null)
                {
                    _mainForm?.Invoke((MethodInvoker)delegate { _selStart = _textBox.SelectionStart; });
                }

                _logBuffer += text;

                if (CharLimit > 0 && _logBuffer.Length > CharLimit)
                {
                    _logBuffer = _logBuffer.Substring(_logBuffer.Length - CharLimit);
                }

                if (LineLimit > 0)
                {
                    if (GetLinesCount(_logBuffer, LineLimit, out var pos))
                    {
                        _logBuffer = _logBuffer.Substring(pos);
                    }
                }

                if (_textBox != null && _selStart < 0) _selStart = 0;

                OnPropertyChanged();
            }

            return true;
        }

        private void UpdateDisplay()
        {
            if (_textBox != null && _textChanged)
                _mainForm?.Invoke((MethodInvoker)delegate
               {
                   //var posLength = _textBox.SelectionLength;
                   _textBox.Text = ToString();
                   if (AutoScroll)
                   {
                       _textBox.SelectionStart = _textBox.Text.Length;
                       _textBox.ScrollToCaret();
                   }
                   else
                   {
                       _textBox.SelectionStart = _selStart;
                       //_textBox.SelectionLength = posLength;
                       _textBox.ScrollToCaret();
                   }

                   _textChanged = false;
               });
        }

        public override string ToString()
        {
            return _logBuffer;
        }

        private static bool GetLinesCount(string data, int lineLimit, out int pos)
        {
            var divider = new HashSet<char>
            {
                '\r',
                '\n'
            };

            var lineCount = 0;
            pos = 0;
            for (var i = data.Length - 1; i >= 0; i--)
            {
                if (divider.Contains(data[i])) // check 2 divider 
                {
                    lineCount++;
                    if (i - 1 >= 0 && divider.Contains(data[i - 1])) i--;
                }

                if (lineCount >= lineLimit)
                {
                    pos = i + 1;
                    return true;
                }
            }
            return false;
        }

        private static string ReplaceUnprintable(string text, bool leaveCrLf = true)
        {
            var str = new StringBuilder();

            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];
                if (char.IsControl(c) && !(leaveCrLf && (c == '\r' || c == '\n')))
                {
                    str.Append("<" + Accessory.ConvertStringToHex(c.ToString()) + ">");
                    if (c == '\n') str.Append("\n");
                }
                else
                {
                    str.Append(c);
                }
            }

            return str.ToString();
        }

        public void Clear()
        {
            _logBuffer = "";
            _selStart = 0;
            OnPropertyChanged();
        }

        public void Dispose()
        {
            TimerStop();
        }
    }
}