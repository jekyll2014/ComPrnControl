using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        bool o_cd1, o_dsr1, o_dtr1, o_rts1, o_cts1;
        int SendComing = 0, txtOutState = 0;
        long oldTicks = DateTime.Now.Ticks, limitTick = 200;
        int LogLinesLimit = 100;

        delegate void SetTextCallback1(string text);
        private void SetText(string text)
        {
            text = Accessory.FilterZeroChar(text);
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            //if (this.textBox_terminal1.InvokeRequired)
            if (textBox_terminal.InvokeRequired)
            {
                SetTextCallback1 d = new SetTextCallback1(SetText);
                BeginInvoke(d, new object[] { text });
            }
            else
            {
                int pos = textBox_terminal.SelectionStart;
                textBox_terminal.AppendText(text);
                if (textBox_terminal.Lines.Length > LogLinesLimit)
                {
                    StringBuilder tmp = new StringBuilder();
                    for (int i = textBox_terminal.Lines.Length - LogLinesLimit; i < textBox_terminal.Lines.Length; i++)
                    {
                        tmp.Append(textBox_terminal.Lines[i]);
                        tmp.Append("\r\n");
                    }
                    textBox_terminal.Text = tmp.ToString();
                }
                if (checkBox_autoscroll.Checked)
                {
                    textBox_terminal.SelectionStart = textBox_terminal.Text.Length;
                    textBox_terminal.ScrollToCaret();
                }
                else
                {
                    textBox_terminal.SelectionStart = pos;
                    textBox_terminal.ScrollToCaret();
                }
            }
        }

        void SerialPopulate()
        {
            comboBox_portname1.Items.Clear();
            comboBox_handshake1.Items.Clear();
            comboBox_parity1.Items.Clear();
            comboBox_stopbits1.Items.Clear();
            //Serial settings populate
            comboBox_portname1.Items.Add("-None-");
            //Add ports
            foreach (string s in SerialPort.GetPortNames())
            {
                comboBox_portname1.Items.Add(s);
            }
            //Add handshake methods
            foreach (string s in Enum.GetNames(typeof(Handshake)))
            {
                comboBox_handshake1.Items.Add(s);
            }
            //Add parity
            foreach (string s in Enum.GetNames(typeof(Parity)))
            {
                comboBox_parity1.Items.Add(s);
            }
            //Add stopbits
            foreach (string s in Enum.GetNames(typeof(StopBits)))
            {
                comboBox_stopbits1.Items.Add(s);
            }
            if (comboBox_portname1.Items.Count == 1)
            {
                comboBox_portname1.SelectedIndex = 0;
                button_openport.Enabled = false;
            }
            else
            {
                comboBox_portname1.SelectedIndex = 1;
            }
            comboBox_portspeed1.SelectedIndex = 0;
            comboBox_handshake1.SelectedIndex = 0;
            comboBox_databits1.SelectedIndex = 0;
            comboBox_parity1.SelectedIndex = 2;
            comboBox_stopbits1.SelectedIndex = 1;
            if (comboBox_portname1.SelectedIndex == 0) button_openport.Enabled = false;
            else button_openport.Enabled = true;
        }

        delegate void SetPinCallback1(bool setPin);
        private void SetPinCD1(bool setPin)
        {
            if (checkBox_CD1.InvokeRequired)
            {
                SetPinCallback1 d = new SetPinCallback1(SetPinCD1);
                BeginInvoke(d, new object[] { setPin });
            }
            else
            {
                checkBox_CD1.Checked = setPin;
            }
        }

        private void SetPinDSR1(bool setPin)
        {
            if (checkBox_DSR1.InvokeRequired)
            {
                SetPinCallback1 d = new SetPinCallback1(SetPinDSR1);
                BeginInvoke(d, new object[] { setPin });
            }
            else
            {
                checkBox_DSR1.Checked = setPin;
            }
        }

        private void SetPinCTS1(bool setPin)
        {
            if (checkBox_CTS1.InvokeRequired)
            {
                SetPinCallback1 d = new SetPinCallback1(SetPinCTS1);
                BeginInvoke(d, new object[] { setPin });
            }
            else
            {
                checkBox_CTS1.Checked = setPin;
            }
        }

        private void SetPinRING1(bool setPin)
        {
            if (checkBox_RI1.InvokeRequired)
            {
                SetPinCallback1 d = new SetPinCallback1(SetPinRING1);
                BeginInvoke(d, new object[] { setPin });
            }
            else
            {
                checkBox_RI1.Checked = setPin;
            }
        }

        public const byte Port1DataIn = 11;
        public const byte Port1DataOut = 12;
        public const byte Port1SignalIn = 13;
        public const byte Port1SignalOut = 14;
        public const byte Port1Error = 15;

        private object threadLock = new object();
        public void collectBuffer(string tmpBuffer, int state)
        {
            if (tmpBuffer != "")
            {
                string time = DateTime.Today.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "." + DateTime.Now.Millisecond.ToString("D3");
                lock (threadLock)
                {
                    if (!(txtOutState == state && (DateTime.Now.Ticks - oldTicks) < limitTick && state != Port1DataOut))
                    {
                        if (state == Port1DataIn) tmpBuffer = "<< " + tmpBuffer;         //sending data
                        else if (state == Port1DataOut) tmpBuffer = ">> " + tmpBuffer;    //receiving data
                        else if (state == Port1SignalIn) tmpBuffer = "<< " + tmpBuffer;    //pin change received
                        else if (state == Port1SignalOut) tmpBuffer = ">> " + tmpBuffer;    //pin changed by user
                        else if (state == Port1Error) tmpBuffer = "!! " + tmpBuffer;    //error occured

                        if (checkBox_saveTime.Checked == true) tmpBuffer = time + " " + tmpBuffer;
                        tmpBuffer = "\r\n" + tmpBuffer;
                        txtOutState = state;
                    }
                    SetText(tmpBuffer);

                    if ((checkBox_saveInput.Checked == true && (state == Port1DataIn || state == Port1SignalIn)) || (checkBox_saveOutput.Checked == true && (state == Port1DataOut || state == Port1SignalOut)))
                    {
                        try
                        {
                            File.AppendAllText(textBox_saveTo.Text, tmpBuffer, Encoding.GetEncoding(ComPrnControl.Properties.Settings.Default.CodePage));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("\r\nError opening file " + textBox_saveTo.Text + ": " + ex.Message);
                        }
                    }
                    oldTicks = DateTime.Now.Ticks;
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            checkBox_hexCommand.Checked = ComPrnControl.Properties.Settings.Default.checkBox_hexCommand;
            textBox_command.Text = ComPrnControl.Properties.Settings.Default.textBox_command;
            checkBox_hexParam.Checked = ComPrnControl.Properties.Settings.Default.checkBox_hexParam;
            textBox_param.Text = ComPrnControl.Properties.Settings.Default.textBox_param;
            limitTick = ComPrnControl.Properties.Settings.Default.LineBreakTimeout;
            limitTick *= 10000;
            LogLinesLimit = ComPrnControl.Properties.Settings.Default.LogLinesLimit;
            serialPort1.Encoding = Encoding.GetEncoding(ComPrnControl.Properties.Settings.Default.CodePage);
            SerialPopulate();
        }

        private void button_refresh_Click(object sender, EventArgs e)
        {
            SerialPopulate();
        }

        private void comboBox_portname1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_portname1.SelectedIndex == 0)
            {
                comboBox_portspeed1.Enabled = false;
                comboBox_handshake1.Enabled = false;
                comboBox_databits1.Enabled = false;
                comboBox_parity1.Enabled = false;
                comboBox_stopbits1.Enabled = false;
            }
            else
            {
                comboBox_portspeed1.Enabled = true;
                comboBox_handshake1.Enabled = true;
                comboBox_databits1.Enabled = true;
                comboBox_parity1.Enabled = true;
                comboBox_stopbits1.Enabled = true;
            }
            if (comboBox_portname1.SelectedIndex == 0) button_openport.Enabled = false;
            else button_openport.Enabled = true;
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            List<byte> rx = new List<byte>();
            try
            {
                while (serialPort1.BytesToRead > 0) rx.Add((byte)serialPort1.ReadByte());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading port " + serialPort1.PortName + ": " + ex.Message);
            }
            string outStr1;
            if (checkBox_hexTerminal.Checked) outStr1 = Accessory.ConvertByteArrayToHex(rx.ToArray());
            else outStr1 = Encoding.GetEncoding(ComPrnControl.Properties.Settings.Default.CodePage).GetString(rx.ToArray(), 0, rx.Count);
            collectBuffer(outStr1, Port1DataIn);
        }

        private void serialPort1_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            MessageBox.Show("Port1 error: " + e.EventType);
        }

        private void serialPort1_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            SetPinCD1(serialPort1.CDHolding);
            SetPinDSR1(serialPort1.DsrHolding);
            SetPinCTS1(serialPort1.CtsHolding);
            string outStr = "";
            if (serialPort1.CDHolding && !o_cd1)
            {
                o_cd1 = true;
                outStr += "<DCD1^>";
            }
            else if (!serialPort1.CDHolding && o_cd1)
            {
                o_cd1 = false;
                outStr += "<DCD1v>";
            }
            if (serialPort1.DsrHolding && !o_dsr1)
            {
                o_dsr1 = true;
                outStr += "<DSR1^>";
            }
            else if (!serialPort1.DsrHolding && o_dsr1)
            {
                o_dsr1 = false;
                outStr += "<DSR1v>";
            }
            if (serialPort1.CtsHolding && !o_cts1)
            {
                o_cts1 = true;
                outStr += "<CTS1^>";
            }
            else if (!serialPort1.CtsHolding && o_cts1)
            {
                o_cts1 = false;
                outStr += "<CTS1v>";
            }
            if (e.EventType.Equals(SerialPinChange.Ring))
            {
                SetPinRING1(true);
                outStr += "<RING1v>";
                SetPinRING1(false);
            }
            collectBuffer(outStr, Port1SignalIn);
        }

        private void checkBox_hexCommand_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_hexCommand.Checked) textBox_command.Text = Accessory.ConvertStringToHex(textBox_command.Text);
            else textBox_command.Text = Accessory.ConvertHexToString(textBox_command.Text);
        }

        private void checkBox_hexParam_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_hexParam.Checked) textBox_param.Text = Accessory.ConvertStringToHex(textBox_param.Text);
            else textBox_param.Text = Accessory.ConvertHexToString(textBox_param.Text);
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            textBox_terminal.Clear();
        }

        private void textBox_command_Leave(object sender, EventArgs e)
        {
            if (checkBox_hexCommand.Checked) textBox_command.Text = Accessory.CheckHexString(textBox_command.Text);
        }

        private void textBox_param_Leave(object sender, EventArgs e)
        {
            if (checkBox_hexParam.Checked) textBox_param.Text = Accessory.CheckHexString(textBox_param.Text);
        }

        private void checkBox_DTR1_CheckedChanged(object sender, EventArgs e)
        {
            serialPort1.DtrEnable = checkBox_DTR1.Checked;
            string outStr = "";
            if (serialPort1.DtrEnable && !o_dtr1)
            {
                o_dtr1 = true;
                outStr = "<DTR1^>";
            }
            else if (!serialPort1.DtrEnable && o_dtr1)
            {
                o_dtr1 = false;
                outStr = "<DTR1v>";
            }
            collectBuffer(outStr, Port1SignalOut);
        }

        private void checkBox_RTS1_CheckedChanged(object sender, EventArgs e)
        {
            serialPort1.RtsEnable = checkBox_RTS1.Checked;
            string outStr = "";
            if (serialPort1.RtsEnable && !o_rts1 && serialPort1.Handshake != Handshake.RequestToSend && serialPort1.Handshake != Handshake.RequestToSendXOnXOff)
            {
                o_rts1 = true;
                outStr += "<RTS1^>";
            }
            else if (!serialPort1.RtsEnable && o_rts1)
            {
                o_rts1 = false;
                outStr += "<RTS1v>";
            }
            collectBuffer(outStr, Port1SignalOut);
        }

        private void button_openport_Click(object sender, EventArgs e)
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
                serialPort1.ReadTimeout = ComPrnControl.Properties.Settings.Default.ReceiveTimeOut;
                serialPort1.WriteTimeout = ComPrnControl.Properties.Settings.Default.SendTimeOut;
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
                serialPort1.PinChanged += serialPort1_PinChanged;
                serialPort1.DataReceived += serialPort1_DataReceived;
                button_refresh.Enabled = false;
                button_closeport.Enabled = true;
                button_openport.Enabled = false;
                button_Send.Enabled = true;
                //button_sendFile.Enabled = true;
                textBox_fileName_TextChanged(this, EventArgs.Empty);
                o_cd1 = serialPort1.CDHolding;
                checkBox_CD1.Checked = o_cd1;
                o_dsr1 = serialPort1.DsrHolding;
                checkBox_DSR1.Checked = o_dsr1;
                o_dtr1 = serialPort1.DtrEnable;
                checkBox_DTR1.Checked = o_dtr1;
                o_cts1 = serialPort1.CtsHolding;
                checkBox_CTS1.Checked = o_cts1;
                checkBox_DTR1.Enabled = true;

                if (serialPort1.Handshake == Handshake.RequestToSend || serialPort1.Handshake == Handshake.RequestToSendXOnXOff)
                {
                    checkBox_RTS1.Enabled = false;
                }
                else
                {
                    o_rts1 = serialPort1.RtsEnable;
                    checkBox_RTS1.Checked = o_rts1;
                    checkBox_RTS1.Enabled = true;
                }
            }
            //else if (serialPort1.IsOpen == true) button_Send.Enabled = true;
        }

        private void button_Send_Click(object sender, EventArgs e)
        {
            if (textBox_command.Text + textBox_param.Text != "")
            {
                string outStr = "";
                string sendStrHex = "";
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
                if (checkBox_hexTerminal.Checked) outStr = sendStrHex;
                else outStr = Accessory.ConvertHexToString(sendStrHex);
                collectBuffer(outStr, Port1DataOut);
            }
        }

        private void button_closeport_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error closing port " + serialPort1.PortName + ": " + ex.Message);
            }
            serialPort1.DataReceived -= serialPort1_DataReceived;
            serialPort1.PinChanged -= serialPort1_PinChanged;
            comboBox_portname1.Enabled = true;
            comboBox_portspeed1.Enabled = true;
            comboBox_handshake1.Enabled = true;
            comboBox_databits1.Enabled = true;
            comboBox_parity1.Enabled = true;
            comboBox_stopbits1.Enabled = true;
            button_Send.Enabled = false;
            button_sendFile.Enabled = false;
            button_refresh.Enabled = true;
            button_openport.Enabled = true;
            button_closeport.Enabled = false;
            checkBox_RTS1.Enabled = false;
            checkBox_DTR1.Enabled = false;
            checkBox_DTR1.Checked = false;
            checkBox_RTS1.Checked = false;
        }

        private void checkBox_saveTo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_saveTo.Checked) textBox_saveTo.Enabled = false;
            else textBox_saveTo.Enabled = true;
        }

        private void button_openFile_Click(object sender, EventArgs e)
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

        private void checkBox_hexFileOpen_CheckedChanged(object sender, EventArgs e)
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

        private void textBox_command_KeyUp(object sender, KeyEventArgs e)
        {
            if (button_Send.Enabled)
            {
                if (e.KeyData == Keys.Return)
                {
                    button_Send_Click(textBox_command, EventArgs.Empty);
                }
            }
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            textBox_fileName.Text = openFileDialog1.FileName;
        }

        private async void button_sendFile_ClickAsync(object sender, EventArgs e)
        {
            if (SendComing > 0)
            {
                SendComing++;
            }
            else if (SendComing == 0)
            {
                UInt16 repeat = 1, delay = 0, strDelay = 0;

                if (textBox_fileName.Text != "" && textBox_sendNum.Text != "" && UInt16.TryParse(textBox_sendNum.Text, out repeat) && UInt16.TryParse(textBox_delay.Text, out delay) && UInt16.TryParse(textBox_strDelay.Text, out strDelay))
                {
                    SendComing = 1;
                    button_Send.Enabled = false;
                    button_closeport.Enabled = false;
                    button_openFile.Enabled = false;
                    button_sendFile.Text = "Stop";
                    textBox_fileName.Enabled = false;
                    textBox_sendNum.Enabled = false;
                    textBox_delay.Enabled = false;
                    textBox_strDelay.Enabled = false;
                    for (int n = 0; n < repeat; n++)
                    {
                        string outStr;
                        long length = 0;
                        if (repeat > 1) outStr = "\r\nSend cycle " + (n + 1).ToString() + "/" + repeat.ToString() + "\r\n";
                        else outStr = "";
                        collectBuffer(outStr, Port1DataOut);
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
                                byte[] tmpBuffer = new byte[length];
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
                                    for (int m = 0; m < tmpBuffer.Length; m++)
                                    {
                                        serialPort1.Write(tmpBuffer, m, 1);
                                        progressBar1.Value = (n * tmpBuffer.Length + m) * 100 / (repeat * tmpBuffer.Length);
                                        if (strDelay > 0) await TaskEx.Delay(strDelay);
                                        if (SendComing > 1) m = tmpBuffer.Length;
                                    }
                                    if (checkBox_hexTerminal.Checked) outStr = Accessory.ConvertByteArrayToHex(tmpBuffer);
                                    else outStr = Accessory.ConvertHexToString(Accessory.ConvertByteArrayToHex(tmpBuffer));
                                    collectBuffer(outStr, Port1DataOut);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error sending to port " + serialPort1.PortName + ": " + ex.Message);
                                }
                            }
                            else //stream
                            {
                                byte[] tmpBuffer = new byte[length];
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
                                    for (int m = 0; m < tmpBuffer.Length; m++)
                                    {
                                        serialPort1.Write(tmpBuffer, m, 1);
                                        progressBar1.Value = (n * tmpBuffer.Length + m) * 100 / (repeat * tmpBuffer.Length);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error sending to port " + serialPort1.PortName + ": " + ex.Message);
                                }
                                progressBar1.Value = 100;
                                if (checkBox_hexTerminal.Checked) outStr = Accessory.ConvertByteArrayToHex(tmpBuffer);
                                else outStr = Accessory.ConvertHexToString(Accessory.ConvertByteArrayToHex(tmpBuffer));
                                collectBuffer(outStr, Port1DataOut);
                            }
                        }
                        else  //hex text read
                        {
                            if (radioButton_byString.Checked) //String-by-string
                            {
                                String[] tmpBuffer = { };
                                try
                                {
                                    length = new FileInfo(textBox_fileName.Text).Length;
                                    //tmpBuffer = File.ReadAllText(textBox_fileName.Text).Replace("\n", "").Split('\r');
                                    tmpBuffer = File.ReadAllLines(textBox_fileName.Text);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("\r\nError reading file " + textBox_fileName.Text + ": " + ex.Message);
                                }
                                for (int m = 0; m < tmpBuffer.Length; m++)
                                {
                                    tmpBuffer[m] = Accessory.CheckHexString(tmpBuffer[m]);
                                }
                                try
                                {
                                    for (int m = 0; m < tmpBuffer.Length; m++)
                                    {
                                        byte[] s = Accessory.ConvertHexToByteArray(tmpBuffer[m]);
                                        serialPort1.Write(s, 0, s.Length);
                                        if (checkBox_hexTerminal.Checked) outStr = tmpBuffer[m];
                                        else outStr = Accessory.ConvertHexToString(tmpBuffer[m].ToString());
                                        collectBuffer(outStr, Port1DataOut);
                                        progressBar1.Value = (n * tmpBuffer.Length + m) * 100 / (repeat * tmpBuffer.Length);
                                        if (strDelay > 0) await TaskEx.Delay(strDelay);
                                        if (SendComing > 1) m = tmpBuffer.Length;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error sending to port " + serialPort1.PortName + ": " + ex.Message);
                                }
                                //if (checkBox_hexTerminal.Checked) outStr += tmpBuffer;
                                //else outStr += ConvertHexToString(tmpBuffer.ToString());
                            }
                            else if (radioButton_byByte.Checked) //byte-by-byte
                            {
                                String tmpBuffer = "";
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
                                    for (int m = 0; m < tmpBuffer.Length; m += 3)
                                    {
                                        serialPort1.Write(Accessory.ConvertHexToByteArray(tmpBuffer.Substring(m, 3)), 0, 1);
                                        progressBar1.Value = (n * tmpBuffer.Length + m) * 100 / (repeat * tmpBuffer.Length);
                                        if (strDelay > 0) await TaskEx.Delay(strDelay);
                                        if (SendComing > 1) m = tmpBuffer.Length;
                                    }
                                    if (checkBox_hexTerminal.Checked) outStr = tmpBuffer;
                                    else outStr = Accessory.ConvertHexToString(tmpBuffer);
                                    collectBuffer(outStr, Port1DataOut);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error sending to port " + serialPort1.PortName + ": " + ex.Message);
                                }
                            }
                            else //raw stream
                            {
                                String tmpBuffer = "";
                                progressBar1.Value = 0;
                                try
                                {
                                    length = new FileInfo(textBox_fileName.Text).Length;
                                    tmpBuffer = Accessory.CheckHexString(File.ReadAllText(textBox_fileName.Text));
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("\r\nError reading file " + textBox_fileName.Text + ": " + ex.Message);
                                }
                                try
                                {
                                    for (int m = 0; m < tmpBuffer.Length; m += 3)
                                    {
                                        serialPort1.Write(Accessory.ConvertHexToByteArray(tmpBuffer.Substring(m, 3)), 0, 1);
                                        progressBar1.Value = (n * tmpBuffer.Length + m) * 100 / (repeat * tmpBuffer.Length);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error sending to port " + serialPort1.PortName + ": " + ex.Message);
                                }
                                progressBar1.Value = 100;
                                if (checkBox_hexTerminal.Checked) outStr = tmpBuffer;
                                else outStr = Accessory.ConvertHexToString(tmpBuffer);
                                collectBuffer(outStr, Port1DataOut);
                            }
                        }
                        if (repeat > 1 && delay > 0) await TaskEx.Delay(delay);
                        if (SendComing > 1) n = repeat;
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
                SendComing = 0;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ComPrnControl.Properties.Settings.Default.checkBox_hexCommand = checkBox_hexCommand.Checked;
            ComPrnControl.Properties.Settings.Default.textBox_command = textBox_command.Text;
            ComPrnControl.Properties.Settings.Default.checkBox_hexParam = checkBox_hexParam.Checked;
            ComPrnControl.Properties.Settings.Default.textBox_param = textBox_param.Text;
            ComPrnControl.Properties.Settings.Default.Save();
        }

        private void radioButton_stream_CheckedChanged(object sender, EventArgs e)
        {
            textBox_strDelay.Enabled = !radioButton_stream.Checked;
        }

        private void textBox_fileName_TextChanged(object sender, EventArgs e)
        {
            if (textBox_fileName.Text != "" && button_closeport.Enabled == true) button_sendFile.Enabled = true;
            else button_sendFile.Enabled = false;
        }
    }
}
