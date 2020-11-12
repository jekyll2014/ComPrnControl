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
        public int LinesLimit = 500;
        public int LineTimeLimit = 500;
        public string LogFileName = "";
        public bool AutoSave = false;
        public bool AutoScroll = true;
        public bool FilterZeroChar = true;
        public TextFormat DefaultTextFormat = TextFormat.AutoReplaceHex; //Text, HEX, Auto (change non-readable to <HEX>)
        public TimeFormat DefaultTimeFormat = TimeFormat.LongTime;
        public DateFormat DefaultDateFormat = DateFormat.ShortDate;

        private delegate void SetTextCallback(string text);

        private readonly Form mainForm;
        private readonly TextBox _textBox;
        private readonly List<string> _logBuffer = new List<string>();
        private StringBuilder _unfinishedString = new StringBuilder();
        private int _selStart;
        private volatile bool _textChanged;
        private Timer _formTimer;

        public List<string> Channels = new List<string> { "" };

        private byte _prevChannel;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged()
        {
            _textChanged = true;
            mainForm?.Invoke((MethodInvoker)delegate
           {
               PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Text"));
           });
        }

        public string Text => ToString();

        public TextLogger(Form mainForm, TextBox textBox = null)
        {
            this.mainForm = mainForm;
            _textBox = textBox;
        }

        public void TimerStart(int delay)
        {
            if (mainForm != null && _textBox != null)
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

            var tmpStr = new StringBuilder();
            if (channel != _prevChannel)
            {
                if (_unfinishedString.Length > 0)
                    AddText(Environment.NewLine, logTime, _prevChannel, textFormat, timeFormat);
                _prevChannel = channel;
            }

            if (timeFormat != TimeFormat.None && dateFormat != DateFormat.None) logTime = DateTime.Now;

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

            if (channel >= Channels.Count) channel = 0;

            if (!string.IsNullOrEmpty(Channels[channel])) tmpStr.Append(Channels[channel] + " ");

            if (textFormat == TextFormat.Default) textFormat = DefaultTextFormat;

            if (textFormat == TextFormat.PlainText)
            {
                tmpStr.Append(text);
            }
            else if (textFormat == TextFormat.Hex)
            {
                tmpStr.Append(Accessory.ConvertStringToHex(text));
                tmpStr.Append("\r\n");
            }
            else if (textFormat == TextFormat.AutoReplaceHex)
            {
                tmpStr.Append(ReplaceUnprintable(text));
                tmpStr.Append("\r\n");
            }

            var inputStrings = ConvertTextToStringArray(tmpStr.ToString(), ref _unfinishedString);
            return AddTextToBuffer(inputStrings);
        }

        private bool AddTextToBuffer(IList<string> text)
        {
            if (text == null || text.Count <= 0) return false;
            lock (_textOutThreadLock)
            {
                if (FilterZeroChar)
                    for (var i = 0; i < text.Count; i++)
                        text[i] = Accessory.FilterZeroChar(text[i]);

                if (AutoSave && !string.IsNullOrEmpty(LogFileName))
                {
                    var textBuffer = new StringBuilder();
                    foreach (var t in text)
                    {
                        textBuffer.Append(t + Environment.NewLine);
                    }
                    if (textBuffer.Length>0) File.AppendAllText(LogFileName, textBuffer.ToString());
                }

                if (noScreenOutput) return true;

                if (_textBox != null) _selStart = _textBox.SelectionStart;

                _logBuffer.AddRange(text);
                if (_logBuffer.Count >= LinesLimit)
                {
                    while (_logBuffer.Count >= LinesLimit)
                    {
                        if (_textBox != null) _selStart -= _logBuffer[0].Length;
                        _logBuffer.RemoveAt(0);
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
                mainForm?.Invoke((MethodInvoker)delegate
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
            var tmpTxt = new StringBuilder();
            foreach (var str in _logBuffer) tmpTxt.Append(str + Environment.NewLine);

            tmpTxt.Append(_unfinishedString);

            return tmpTxt.ToString();
        }

        private static string[] ConvertTextToStringArray(string data, ref StringBuilder nonComplete)
        {
            var divider = new HashSet<char>
            {
                '\r',
                '\n'
            };

            var stringCollection = new List<string>();

            foreach (var t in data)
                if (divider.Contains(t))
                {
                    if (nonComplete.Length > 0) stringCollection.Add(nonComplete.ToString());
                    nonComplete.Clear();
                }
                else
                {
                    if (!divider.Contains(t)) nonComplete.Append(t);
                }

            return stringCollection.ToArray();
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
            _logBuffer.Clear();
            _selStart = 0;
            _unfinishedString.Clear();
            OnPropertyChanged();
        }

        public void Dispose()
        {
            TimerStop();
        }
    }
}