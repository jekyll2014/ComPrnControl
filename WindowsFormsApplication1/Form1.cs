using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComPrnControl
{
    public partial class Form1 : Form
    {
        private bool _oCd1, _oDsr1, _oDtr1, _oRts1, _oCts1;
        private int _sendComing;

        private bool SerialPopulate(ComboBox portNameCombo, ComboBox portSpeedCombo = null, ComboBox portDataBitsCombo = null, ComboBox portHandShakeCombo = null, ComboBox portParityCombo = null, ComboBox portStopBitsCombo = null)
        {
            if (portNameCombo == null) return false;

            //Add ports
            var oldPortName = portNameCombo.SelectedItem?.ToString() ?? "";
            portNameCombo.Items.Clear();
            portNameCombo.Items.Add("-None-");
            portNameCombo.Items.AddRange(SerialPort.GetPortNames());
            if (portNameCombo.Items.Contains(oldPortName)) portNameCombo.SelectedItem = oldPortName;
            else portNameCombo.SelectedIndex = 1;

            if (portNameCombo.Items.Count == 1)
            {
                portNameCombo.SelectedIndex = 0;
                return false;
            }

            //Add speeds
            if (portSpeedCombo != null)
            {
                string[] portSpeeds =
                {
                    "250000",
                    "230400",
                    "115200",
                    "57600",
                    "38400",
                    "19200",
                    "9600",
                    "4800",
                    "2400",
                    "1200",
                    "600",
                    "300"
                };
                var oldPortSpeed = portSpeedCombo.SelectedItem?.ToString() ?? "";
                portSpeedCombo.Items.Clear();
                portSpeedCombo.Items.AddRange(portSpeeds);
                if (portSpeedCombo.Items.Contains(oldPortSpeed)) portSpeedCombo.SelectedItem = oldPortSpeed;
                else portSpeedCombo.SelectedIndex = 0;
            }

            //Add DataBits
            if (portDataBitsCombo != null)
            {
                string[] portDataBits =
                {
                    "8",
                    "7",
                    "6",
                    "5"
                };

                var oldPortDataBits = portDataBitsCombo.SelectedItem?.ToString() ?? "";
                portDataBitsCombo.Items.Clear();
                portDataBitsCombo.Items.AddRange(portDataBits);
                if (portDataBitsCombo.Items.Contains(oldPortDataBits)) portDataBitsCombo.SelectedItem = oldPortDataBits;
                else portDataBitsCombo.SelectedIndex = 0;

            }

            //Add handshake methods
            if (portHandShakeCombo != null)
            {
                var oldPortHandShake = portHandShakeCombo.SelectedItem?.ToString() ?? "";
                portHandShakeCombo.Items.Clear();
                portHandShakeCombo.Items.AddRange(Enum.GetNames(typeof(Handshake)));
                if (portHandShakeCombo.Items.Contains(oldPortHandShake)) portHandShakeCombo.SelectedItem = oldPortHandShake;
                else portHandShakeCombo.SelectedIndex = 0;
            }

            //Add parity
            if (portParityCombo != null)
            {
                var oldPortParity = portParityCombo.SelectedItem?.ToString() ?? "";
                portParityCombo.Items.Clear();
                portParityCombo.Items.AddRange(Enum.GetNames(typeof(Parity)));
                if (portParityCombo.Items.Contains(oldPortParity)) portParityCombo.SelectedItem = oldPortParity;
                else portParityCombo.SelectedIndex = 2;
            }

            //Add stopbits
            if (portStopBitsCombo != null)
            {
                var oldPortStopBits = portStopBitsCombo.SelectedItem?.ToString() ?? "";
                portStopBitsCombo.Items.Clear();
                portStopBitsCombo.Items.AddRange(Enum.GetNames(typeof(StopBits)));
                if (portStopBitsCombo.Items.Contains(oldPortStopBits)) portStopBitsCombo.SelectedItem = oldPortStopBits;
                else portStopBitsCombo.SelectedIndex = 1;
            }

            return true;
        }

        private delegate void SetPinCallback1(bool setPin);

        private void SetPinCd1(bool setPin)
        {
            if (checkBox_CD1.InvokeRequired)
            {
                var d = new SetPinCallback1(SetPinCd1);
                BeginInvoke(d, new object[] { setPin });
            }
            else
            {
                checkBox_CD1.Checked = setPin;
            }
        }

        private void SetPinDsr1(bool setPin)
        {
            if (checkBox_DSR1.InvokeRequired)
            {
                var d = new SetPinCallback1(SetPinDsr1);
                BeginInvoke(d, new object[] { setPin });
            }
            else
            {
                checkBox_DSR1.Checked = setPin;
            }
        }

        private void SetPinCts1(bool setPin)
        {
            if (checkBox_CTS1.InvokeRequired)
            {
                var d = new SetPinCallback1(SetPinCts1);
                BeginInvoke(d, new object[] { setPin });
            }
            else
            {
                checkBox_CTS1.Checked = setPin;
            }
        }

        private void SetPinRing1(bool setPin)
        {
            if (checkBox_RI1.InvokeRequired)
            {
                var d = new SetPinCallback1(SetPinRing1);
                BeginInvoke(d, new object[] { setPin });
            }
            else
            {
                checkBox_RI1.Checked = setPin;
            }
        }

        private TextLogger.TextLogger _logger;

        private enum DataDirection
        {
            Received,
            Sent,
            SignalIn,
            SignalOut,
            Error
        }

        private readonly Dictionary<byte, string> _directions = new Dictionary<byte, string>()
        {
            {(byte)DataDirection.Received, "<<"},
            {(byte)DataDirection.Sent,">>"},
            {(byte)DataDirection.SignalIn,"*<"},
            {(byte)DataDirection.SignalOut,"*>"},
            {(byte)DataDirection.Error,"!!"}
        };

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            checkBox_hexCommand.Checked = Properties.Settings.Default.checkBox_hexCommand;
            textBox_command.Text = Properties.Settings.Default.textBox_command;
            checkBox_hexParam.Checked = Properties.Settings.Default.checkBox_hexParam;
            textBox_param.Text = Properties.Settings.Default.textBox_param;
            serialPort1.Encoding = Encoding.GetEncoding(Properties.Settings.Default.CodePage);
            button_openport.Enabled = SerialPopulate(comboBox_portname1, comboBox_portspeed1, comboBox_databits1, comboBox_handshake1, comboBox_parity1, comboBox_stopbits1);

            _logger = new TextLogger.TextLogger(this)
            {
                Channels = _directions,
                FilterZeroChar = false,
            };
            textBox_terminal.DataBindings.Add("Text", _logger, "Text", false, DataSourceUpdateMode.OnPropertyChanged);

            _logger.LineTimeLimit = Properties.Settings.Default.LineBreakTimeout;
            _logger.LineLimit = Properties.Settings.Default.LogLinesLimit;
            _logger.AutoSave = checkBox_saveTo.Checked;
            _logger.LogFileName = textBox_saveTo.Text;
            _logger.DefaultTextFormat = checkBox_hexTerminal.Checked
                ? TextLogger.TextLogger.TextFormat.Hex
                : TextLogger.TextLogger.TextFormat.AutoReplaceHex;
            _logger.DefaultTimeFormat =
                checkBox_saveTime.Checked ? TextLogger.TextLogger.TimeFormat.LongTime : TextLogger.TextLogger.TimeFormat.None;
            _logger.DefaultDateFormat =
                checkBox_saveTime.Checked ? TextLogger.TextLogger.DateFormat.ShortDate : TextLogger.TextLogger.DateFormat.None;
            _logger.AutoScroll = checkBox_autoscroll.Checked;
            CheckBox_autoscroll_CheckedChanged(null, EventArgs.Empty);
        }

        private void ComboBox_portname1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_portname1.SelectedIndex == 0)
            {
                comboBox_portspeed1.Enabled = false;
                comboBox_handshake1.Enabled = false;
                comboBox_databits1.Enabled = false;
                comboBox_parity1.Enabled = false;
                comboBox_stopbits1.Enabled = false;
                button_openport.Enabled = false;
            }
            else
            {
                comboBox_portspeed1.Enabled = true;
                comboBox_handshake1.Enabled = true;
                comboBox_databits1.Enabled = true;
                comboBox_parity1.Enabled = true;
                comboBox_stopbits1.Enabled = true;
                button_openport.Enabled = true;
            }
        }

        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var rx = new List<byte>();
            try
            {
                while (serialPort1.BytesToRead > 0) rx.Add((byte)serialPort1.ReadByte());
            }
            catch (Exception ex)
            {
                _logger.AddText("Error reading port " + serialPort1.PortName + ": " + ex.Message, (byte)DataDirection.Error, DateTime.Now, TextLogger.TextLogger.TextFormat.PlainText);
            }
            var outStr1 = Encoding.GetEncoding(ComPrnControl.Properties.Settings.Default.CodePage).GetString(rx.ToArray(), 0, rx.Count);
            _logger.AddText(outStr1, (byte)DataDirection.Received, DateTime.Now);
        }

        private void SerialPort1_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            _logger.AddText("Port1 error: " + e.EventType, (byte)DataDirection.Error, DateTime.Now, TextLogger.TextLogger.TextFormat.PlainText);
        }

        private void SerialPort1_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            SetPinCd1(serialPort1.CDHolding);
            SetPinDsr1(serialPort1.DsrHolding);
            SetPinCts1(serialPort1.CtsHolding);
            var outStr = "";
            if (serialPort1.CDHolding && !_oCd1)
            {
                _oCd1 = true;
                outStr += "<DCD1^>";
            }
            else if (!serialPort1.CDHolding && _oCd1)
            {
                _oCd1 = false;
                outStr += "<DCD1v>";
            }
            if (serialPort1.DsrHolding && !_oDsr1)
            {
                _oDsr1 = true;
                outStr += "<DSR1^>";
            }
            else if (!serialPort1.DsrHolding && _oDsr1)
            {
                _oDsr1 = false;
                outStr += "<DSR1v>";
            }
            if (serialPort1.CtsHolding && !_oCts1)
            {
                _oCts1 = true;
                outStr += "<CTS1^>";
            }
            else if (!serialPort1.CtsHolding && _oCts1)
            {
                _oCts1 = false;
                outStr += "<CTS1v>";
            }
            if (e.EventType.Equals(SerialPinChange.Ring))
            {
                SetPinRing1(true);
                outStr += "<RING1v>";
                SetPinRing1(false);
            }
            _logger.AddText(outStr, (byte)DataDirection.SignalIn, DateTime.Now, TextLogger.TextLogger.TextFormat.PlainText);
        }

        private void CheckBox_hexCommand_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_hexCommand.Checked) textBox_command.Text = Accessory.ConvertStringToHex(textBox_command.Text);
            else textBox_command.Text = Accessory.ConvertHexToString(textBox_command.Text);
        }

        private void CheckBox_hexParam_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_hexParam.Checked) textBox_param.Text = Accessory.ConvertStringToHex(textBox_param.Text);
            else textBox_param.Text = Accessory.ConvertHexToString(textBox_param.Text);
        }

        private void Button_Clear_Click(object sender, EventArgs e)
        {
            _logger.Clear();
        }

        private void TextBox_command_Leave(object sender, EventArgs e)
        {
            if (checkBox_hexCommand.Checked) textBox_command.Text = Accessory.CheckHexString(textBox_command.Text);
        }

        private void TextBox_param_Leave(object sender, EventArgs e)
        {
            if (checkBox_hexParam.Checked) textBox_param.Text = Accessory.CheckHexString(textBox_param.Text);
        }

        private void CheckBox_DTR1_CheckedChanged(object sender, EventArgs e)
        {
            serialPort1.DtrEnable = checkBox_DTR1.Checked;
            var outStr = "";
            if (serialPort1.DtrEnable && !_oDtr1)
            {
                _oDtr1 = true;
                outStr = "<DTR1^>";
            }
            else if (!serialPort1.DtrEnable && _oDtr1)
            {
                _oDtr1 = false;
                outStr = "<DTR1v>";
            }
            _logger.AddText(outStr, (byte)DataDirection.SignalOut, DateTime.Now, TextLogger.TextLogger.TextFormat.PlainText);
        }

        private void CheckBox_RTS1_CheckedChanged(object sender, EventArgs e)
        {
            serialPort1.RtsEnable = checkBox_RTS1.Checked;
            var outStr = "";
            if (serialPort1.RtsEnable && !_oRts1 && serialPort1.Handshake != Handshake.RequestToSend && serialPort1.Handshake != Handshake.RequestToSendXOnXOff)
            {
                _oRts1 = true;
                outStr += "<RTS1^>";
            }
            else if (!serialPort1.RtsEnable && _oRts1)
            {
                _oRts1 = false;
                outStr += "<RTS1v>";
            }
            _logger.AddText(outStr, (byte)DataDirection.SignalOut, DateTime.Now, TextLogger.TextLogger.TextFormat.PlainText);
        }

        private void Button_openport_Click(object sender, EventArgs e)
        {
            if (comboBox_portname1.SelectedIndex != 0)
            {
                comboBox_portname1.Enabled = false;
                comboBox_portspeed1.Enabled = false;
                comboBox_handshake1.Enabled = false;
                comboBox_databits1.Enabled = false;
                comboBox_parity1.Enabled = false;
                comboBox_stopbits1.Enabled = false;
                serialPort1.PortName = comboBox_portname1.Text;
                serialPort1.BaudRate = Convert.ToInt32(comboBox_portspeed1.Text);
                serialPort1.DataBits = Convert.ToUInt16(comboBox_databits1.Text);
                serialPort1.Handshake = (Handshake)Enum.Parse(typeof(Handshake), comboBox_handshake1.Text);
                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), comboBox_parity1.Text);
                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), comboBox_stopbits1.Text);
                serialPort1.ReadTimeout = Properties.Settings.Default.ReceiveTimeOut;
                serialPort1.WriteTimeout = Properties.Settings.Default.SendTimeOut;
                serialPort1.ReadBufferSize = 8192;
                try
                {
                    serialPort1.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening port " + serialPort1.PortName + ": " + ex.Message);
                    comboBox_portname1.Enabled = true;
                    comboBox_portspeed1.Enabled = true;
                    comboBox_handshake1.Enabled = true;
                    comboBox_databits1.Enabled = true;
                    comboBox_parity1.Enabled = true;
                    comboBox_stopbits1.Enabled = true;
                    return;
                }
                serialPort1.PinChanged += SerialPort1_PinChanged;
                serialPort1.DataReceived += SerialPort1_DataReceived;
                button_closeport.Enabled = true;
                button_openport.Enabled = false;
                button_Send.Enabled = true;
                TextBox_fileName_TextChanged(this, EventArgs.Empty);
                _oCd1 = serialPort1.CDHolding;
                checkBox_CD1.Checked = _oCd1;
                _oDsr1 = serialPort1.DsrHolding;
                checkBox_DSR1.Checked = _oDsr1;
                _oDtr1 = serialPort1.DtrEnable;
                checkBox_DTR1.Checked = _oDtr1;
                _oCts1 = serialPort1.CtsHolding;
                checkBox_CTS1.Checked = _oCts1;
                checkBox_DTR1.Enabled = true;

                if (serialPort1.Handshake == Handshake.RequestToSend || serialPort1.Handshake == Handshake.RequestToSendXOnXOff)
                {
                    checkBox_RTS1.Enabled = false;
                }
                else
                {
                    _oRts1 = serialPort1.RtsEnable;
                    checkBox_RTS1.Checked = _oRts1;
                    checkBox_RTS1.Enabled = true;
                }
            }
        }

        private void Button_Send_Click(object sender, EventArgs e)
        {
            if (textBox_command.Text + textBox_param.Text != "")
            {
                String sendStrHex;
                if (checkBox_hexCommand.Checked) sendStrHex = textBox_command.Text;
                else sendStrHex = Accessory.ConvertStringToHex(textBox_command.Text);
                if (checkBox_hexParam.Checked) sendStrHex += textBox_param.Text;
                else sendStrHex += Accessory.ConvertStringToHex(textBox_param.Text);
                try
                {
                    serialPort1.Write(Accessory.ConvertHexToByteArray(sendStrHex), 0, sendStrHex.Length / 3);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error sending to port " + serialPort1.PortName + ": " + ex.Message);
                }
                var outStr = Accessory.ConvertHexToString(sendStrHex);
                _logger.AddText(outStr, (byte)DataDirection.Sent, DateTime.Now);
            }
        }

        private void Button_closeport_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error closing port " + serialPort1.PortName + ": " + ex.Message);
            }
            serialPort1.DataReceived -= SerialPort1_DataReceived;
            serialPort1.PinChanged -= SerialPort1_PinChanged;
            comboBox_portname1.Enabled = true;
            comboBox_portspeed1.Enabled = true;
            comboBox_handshake1.Enabled = true;
            comboBox_databits1.Enabled = true;
            comboBox_parity1.Enabled = true;
            comboBox_stopbits1.Enabled = true;
            button_Send.Enabled = false;
            button_sendFile.Enabled = false;
            button_openport.Enabled = true;
            button_closeport.Enabled = false;
            checkBox_RTS1.Enabled = false;
            checkBox_DTR1.Enabled = false;
            checkBox_DTR1.Checked = false;
            checkBox_RTS1.Checked = false;
        }

        private void CheckBox_saveTo_CheckedChanged(object sender, EventArgs e)
        {
            textBox_saveTo.Enabled = !checkBox_saveTo.Checked;
            _logger.AutoSave = checkBox_saveTo.Checked;
        }

        private void Button_openFile_Click(object sender, EventArgs e)
        {
            if (checkBox_hexFileOpen.Checked)
            {
                openFileDialog1.FileName = "";
                openFileDialog1.Title = "Open file";
                openFileDialog1.DefaultExt = "txt";
                openFileDialog1.Filter = "HEX files|*.hex|Text files|*.txt|All files|*.*";
                openFileDialog1.ShowDialog();
            }
            else
            {
                openFileDialog1.FileName = "";
                openFileDialog1.Title = "Open file";
                openFileDialog1.DefaultExt = "bin";
                openFileDialog1.Filter = "BIN files|*.bin|PRN files|*.prn|All files|*.*";
                openFileDialog1.ShowDialog();
            }
        }

        private void CheckBox_hexFileOpen_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox_hexFileOpen.Checked)
            {
                radioButton_byString.Enabled = false;
                if (radioButton_byString.Checked) radioButton_byByte.Checked = true;
                checkBox_hexFileOpen.Text = "binary data";
            }
            else
            {
                radioButton_byString.Enabled = true;
                checkBox_hexFileOpen.Text = "hex text data";
            }
        }

        private void TextBox_fileName_KeyUp(object sender, KeyEventArgs e)
        {
            if (button_Send.Enabled)
                if (e.KeyData == Keys.Return) Button_Send_Click(textBox_command, EventArgs.Empty);
        }

        private void TextBox_command_KeyUp(object sender, KeyEventArgs e)
        {
            if (button_Send.Enabled)
                if (e.KeyData == Keys.Return) Button_Send_Click(textBox_command, EventArgs.Empty);
        }

        private void CheckBox_hexTerminal_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_hexTerminal.Checked)
                _logger.DefaultTextFormat = TextLogger.TextLogger.TextFormat.Hex;
            else
                _logger.DefaultTextFormat = TextLogger.TextLogger.TextFormat.AutoReplaceHex;
        }

        private void CheckBox_saveTime_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_saveTime.Checked)
            {
                _logger.DefaultDateFormat = TextLogger.TextLogger.DateFormat.ShortDate;
                _logger.DefaultTimeFormat = TextLogger.TextLogger.TimeFormat.LongTime;
            }
            else
            {
                _logger.DefaultDateFormat = TextLogger.TextLogger.DateFormat.None;
                _logger.DefaultTimeFormat = TextLogger.TextLogger.TimeFormat.None;
            }
        }

        private void TextBox_saveTo_Leave(object sender, EventArgs e)
        {
            _logger.LogFileName = textBox_saveTo.Text;
        }

        private void CheckBox_autoscroll_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_autoscroll.Checked)
            {
                _logger.AutoScroll = true;
                textBox_terminal.TextChanged += TextBox_terminal_TextChanged;
            }
            else
            {
                _logger.AutoScroll = false;
                textBox_terminal.TextChanged -= TextBox_terminal_TextChanged;
            }
        }

        private void TextBox_terminal_TextChanged(object sender, EventArgs e)
        {
            textBox_terminal.SelectionStart = textBox_terminal.Text.Length;
            textBox_terminal.ScrollToCaret();
        }

        private void OpenFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            textBox_fileName.Text = openFileDialog1.FileName;
        }

        private void ComboBox_portname1_DropDown(object sender, EventArgs e)
        {
            button_openport.Enabled = SerialPopulate(comboBox_portname1, comboBox_portspeed1, comboBox_databits1, comboBox_handshake1, comboBox_parity1, comboBox_stopbits1);
        }

        private async void Button_sendFile_ClickAsync(object sender, EventArgs e)
        {
            if (_sendComing > 0)
            {
                _sendComing++;
            }
            else if (_sendComing == 0)
            {

                if (textBox_fileName.Text != "" && textBox_sendNum.Text != "" && ushort.TryParse(textBox_sendNum.Text, out ushort repeat) && ushort.TryParse(textBox_delay.Text, out ushort delay) && ushort.TryParse(textBox_strDelay.Text, out ushort strDelay))
                {
                    _sendComing = 1;
                    button_Send.Enabled = false;
                    button_closeport.Enabled = false;
                    button_openFile.Enabled = false;
                    button_sendFile.Text = "Stop";
                    textBox_fileName.Enabled = false;
                    textBox_sendNum.Enabled = false;
                    textBox_delay.Enabled = false;
                    textBox_strDelay.Enabled = false;
                    for (var n = 0; n < repeat; n++)
                    {
                        string outStr;
                        long length = 0;
                        if (repeat > 1) outStr = "\r\nSend cycle " + (n + 1).ToString() + "/" + repeat.ToString() + "\r\n";
                        else outStr = "";
                        _logger.AddText(outStr, (byte)DataDirection.Sent, DateTime.Now, TextLogger.TextLogger.TextFormat.PlainText);
                        try
                        {
                            length = new FileInfo(textBox_fileName.Text).Length;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("\r\nError opening file " + textBox_fileName.Text + ": " + ex.Message);
                        }

                        if (!checkBox_hexFileOpen.Checked)  //binary data read
                        {
                            if (radioButton_byByte.Checked) //byte-by-byte
                            {
                                var tmpBuffer = new byte[length];
                                try
                                {
                                    tmpBuffer = File.ReadAllBytes(textBox_fileName.Text);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("\r\nError reading file " + textBox_fileName.Text + ": " + ex.Message);
                                }
                                try
                                {
                                    for (var m = 0; m < tmpBuffer.Length; m++)
                                    {
                                        serialPort1.Write(tmpBuffer, m, 1);
                                        progressBar1.Value = (n * tmpBuffer.Length + m) * 100 / (repeat * tmpBuffer.Length);
                                        if (strDelay > 0) await TaskEx.Delay(strDelay);
                                        if (_sendComing > 1) m = tmpBuffer.Length;
                                        byte[] outByte = new[] { tmpBuffer[m] };
                                        outStr = Accessory.ConvertByteArrayToString(tmpBuffer);
                                        _logger.AddText(outStr, (byte)DataDirection.Sent, DateTime.Now);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.AddText("Error sending to port " + serialPort1.PortName + ": " + ex.Message, (byte)DataDirection.Error, DateTime.Now, TextLogger.TextLogger.TextFormat.PlainText);
                                }
                            }
                            else //stream
                            {
                                var tmpBuffer = new byte[length];
                                progressBar1.Value = 0;
                                try
                                {
                                    tmpBuffer = File.ReadAllBytes(textBox_fileName.Text);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("\r\nError reading file " + textBox_fileName.Text + ": " + ex.Message);
                                }
                                try
                                {
                                    for (var m = 0; m < tmpBuffer.Length; m++)
                                    {
                                        serialPort1.Write(tmpBuffer, m, 1);
                                        progressBar1.Value = (n * tmpBuffer.Length + m) * 100 / (repeat * tmpBuffer.Length);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.AddText("Error sending to port " + serialPort1.PortName + ": " + ex.Message, (byte)DataDirection.Error, DateTime.Now, TextLogger.TextLogger.TextFormat.PlainText);
                                }
                                progressBar1.Value = 100;
                                outStr = Accessory.ConvertByteArrayToString(tmpBuffer);
                                _logger.AddText(outStr, (byte)DataDirection.Sent, DateTime.Now);
                            }
                        }
                        else  //hex text read
                        {
                            if (radioButton_byString.Checked) //String-by-string
                            {
                                string[] tmpBuffer = { };
                                try
                                {
                                    tmpBuffer = File.ReadAllLines(textBox_fileName.Text);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("\r\nError reading file " + textBox_fileName.Text + ": " + ex.Message);
                                }
                                for (var m = 0; m < tmpBuffer.Length; m++) tmpBuffer[m] = Accessory.CheckHexString(tmpBuffer[m]);
                                try
                                {
                                    for (var m = 0; m < tmpBuffer.Length; m++)
                                    {
                                        var s = Accessory.ConvertHexToByteArray(tmpBuffer[m]);
                                        serialPort1.Write(s, 0, s.Length);
                                        outStr = Accessory.ConvertHexToString(tmpBuffer[m]);
                                        _logger.AddText(outStr, (byte)DataDirection.Sent, DateTime.Now);
                                        progressBar1.Value = (n * tmpBuffer.Length + m) * 100 / (repeat * tmpBuffer.Length);
                                        if (strDelay > 0) await TaskEx.Delay(strDelay);
                                        if (_sendComing > 1) m = tmpBuffer.Length;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.AddText("Error sending to port " + serialPort1.PortName + ": " + ex.Message, (byte)DataDirection.Error, DateTime.Now, TextLogger.TextLogger.TextFormat.PlainText);
                                }
                            }
                            else if (radioButton_byByte.Checked) //byte-by-byte
                            {
                                var tmpBuffer = "";
                                try
                                {
                                    length = new FileInfo(textBox_fileName.Text).Length;
                                    tmpBuffer = File.ReadAllText(textBox_fileName.Text);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("\r\nError reading file " + textBox_fileName.Text + ": " + ex.Message);
                                }
                                tmpBuffer = Accessory.CheckHexString(tmpBuffer);
                                try
                                {
                                    for (var m = 0; m < tmpBuffer.Length; m += 3)
                                    {
                                        var outByte = tmpBuffer.Substring(m, 3);
                                        serialPort1.Write(Accessory.ConvertHexToByteArray(outByte), 0, 1);
                                        progressBar1.Value = (n * tmpBuffer.Length + m) * 100 / (repeat * tmpBuffer.Length);
                                        if (strDelay > 0) await TaskEx.Delay(strDelay);
                                        if (_sendComing > 1) m = tmpBuffer.Length;
                                        outStr = Accessory.ConvertHexToString(outByte);
                                        _logger.AddText(outStr, (byte)DataDirection.Sent, DateTime.Now);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error sending to port " + serialPort1.PortName + ": " + ex.Message);
                                }
                            }
                            else //raw stream
                            {
                                var tmpBuffer = "";
                                progressBar1.Value = 0;
                                try
                                {
                                    tmpBuffer = Accessory.CheckHexString(File.ReadAllText(textBox_fileName.Text));
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("\r\nError reading file " + textBox_fileName.Text + ": " + ex.Message);
                                }
                                try
                                {
                                    for (var m = 0; m < tmpBuffer.Length; m += 3)
                                    {
                                        serialPort1.Write(Accessory.ConvertHexToByteArray(tmpBuffer.Substring(m, 3)), 0, 1);
                                        progressBar1.Value = (n * tmpBuffer.Length + m) * 100 / (repeat * tmpBuffer.Length);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.AddText("Error sending to port " + serialPort1.PortName + ": " + ex.Message, (byte)DataDirection.Error, DateTime.Now, TextLogger.TextLogger.TextFormat.PlainText);
                                }
                                progressBar1.Value = 100;
                                outStr = Accessory.ConvertHexToString(tmpBuffer);
                                _logger.AddText(outStr, (byte)DataDirection.Sent, DateTime.Now);
                            }
                        }
                        if (repeat > 1 && delay > 0) await TaskEx.Delay(delay);
                        if (_sendComing > 1) n = repeat;
                    }
                    button_Send.Enabled = true;
                    button_closeport.Enabled = true;
                    button_openFile.Enabled = true;
                    button_sendFile.Text = "Send file";
                    textBox_fileName.Enabled = true;
                    textBox_sendNum.Enabled = true;
                    textBox_delay.Enabled = true;
                    textBox_strDelay.Enabled = true;
                }
                _sendComing = 0;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _logger.Dispose();
            ComPrnControl.Properties.Settings.Default.checkBox_hexCommand = checkBox_hexCommand.Checked;
            ComPrnControl.Properties.Settings.Default.textBox_command = textBox_command.Text;
            ComPrnControl.Properties.Settings.Default.checkBox_hexParam = checkBox_hexParam.Checked;
            ComPrnControl.Properties.Settings.Default.textBox_param = textBox_param.Text;
            ComPrnControl.Properties.Settings.Default.Save();
        }

        private void RadioButton_stream_CheckedChanged(object sender, EventArgs e)
        {
            textBox_strDelay.Enabled = !radioButton_stream.Checked;
        }

        private void TextBox_fileName_TextChanged(object sender, EventArgs e)
        {
            if (textBox_fileName.Text != "" && button_closeport.Enabled) button_sendFile.Enabled = true;
            else button_sendFile.Enabled = false;
        }
    }
}
