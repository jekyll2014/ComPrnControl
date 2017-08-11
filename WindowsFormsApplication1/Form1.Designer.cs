using System;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button_refresh = new System.Windows.Forms.Button();
            this.checkBox_RI1 = new System.Windows.Forms.CheckBox();
            this.checkBox_CTS1 = new System.Windows.Forms.CheckBox();
            this.checkBox_RTS1 = new System.Windows.Forms.CheckBox();
            this.checkBox_DTR1 = new System.Windows.Forms.CheckBox();
            this.checkBox_DSR1 = new System.Windows.Forms.CheckBox();
            this.checkBox_CD1 = new System.Windows.Forms.CheckBox();
            this.label_ComStopBits = new System.Windows.Forms.Label();
            this.label_ComFlow = new System.Windows.Forms.Label();
            this.label_ComParity = new System.Windows.Forms.Label();
            this.label_ComPort = new System.Windows.Forms.Label();
            this.label_ComSpeed = new System.Windows.Forms.Label();
            this.label_ComDataBits = new System.Windows.Forms.Label();
            this.comboBox_stopbits1 = new System.Windows.Forms.ComboBox();
            this.comboBox_parity1 = new System.Windows.Forms.ComboBox();
            this.comboBox_databits1 = new System.Windows.Forms.ComboBox();
            this.comboBox_handshake1 = new System.Windows.Forms.ComboBox();
            this.comboBox_portspeed1 = new System.Windows.Forms.ComboBox();
            this.comboBox_portname1 = new System.Windows.Forms.ComboBox();
            this.button_closeport = new System.Windows.Forms.Button();
            this.button_openport = new System.Windows.Forms.Button();
            this.button_Clear = new System.Windows.Forms.Button();
            this.textBox_param = new System.Windows.Forms.TextBox();
            this.checkBox_hexParam = new System.Windows.Forms.CheckBox();
            this.checkBox_hexTerminal = new System.Windows.Forms.CheckBox();
            this.checkBox_autoscroll = new System.Windows.Forms.CheckBox();
            this.checkBox_hexCommand = new System.Windows.Forms.CheckBox();
            this.textBox_terminal = new System.Windows.Forms.TextBox();
            this.textBox_command = new System.Windows.Forms.TextBox();
            this.button_Send = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.textBox_saveTo = new System.Windows.Forms.TextBox();
            this.checkBox_saveTo = new System.Windows.Forms.CheckBox();
            this.button_openFile = new System.Windows.Forms.Button();
            this.textBox_fileName = new System.Windows.Forms.TextBox();
            this.checkBox_hexFileOpen = new System.Windows.Forms.CheckBox();
            this.button_sendFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.textBox_delay = new System.Windows.Forms.TextBox();
            this.textBox_sendNum = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_strDelay = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.radioButton_byString = new System.Windows.Forms.RadioButton();
            this.radioButton_byByte = new System.Windows.Forms.RadioButton();
            this.radioButton_stream = new System.Windows.Forms.RadioButton();
            this.textBox_strLimit = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_refresh
            // 
            this.button_refresh.Location = new System.Drawing.Point(500, 52);
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(72, 25);
            this.button_refresh.TabIndex = 9;
            this.button_refresh.Text = "Refresh";
            this.button_refresh.UseVisualStyleBackColor = true;
            this.button_refresh.Click += new System.EventHandler(this.button_refresh_Click);
            // 
            // checkBox_RI1
            // 
            this.checkBox_RI1.AutoSize = true;
            this.checkBox_RI1.Enabled = false;
            this.checkBox_RI1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_RI1.Location = new System.Drawing.Point(443, 57);
            this.checkBox_RI1.Name = "checkBox_RI1";
            this.checkBox_RI1.Size = new System.Drawing.Size(37, 17);
            this.checkBox_RI1.TabIndex = 29;
            this.checkBox_RI1.Text = "RI";
            this.checkBox_RI1.UseVisualStyleBackColor = true;
            // 
            // checkBox_CTS1
            // 
            this.checkBox_CTS1.AutoSize = true;
            this.checkBox_CTS1.Enabled = false;
            this.checkBox_CTS1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_CTS1.Location = new System.Drawing.Point(390, 57);
            this.checkBox_CTS1.Name = "checkBox_CTS1";
            this.checkBox_CTS1.Size = new System.Drawing.Size(47, 17);
            this.checkBox_CTS1.TabIndex = 28;
            this.checkBox_CTS1.Text = "CTS";
            this.checkBox_CTS1.UseVisualStyleBackColor = true;
            // 
            // checkBox_RTS1
            // 
            this.checkBox_RTS1.AutoSize = true;
            this.checkBox_RTS1.Enabled = false;
            this.checkBox_RTS1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_RTS1.Location = new System.Drawing.Point(336, 57);
            this.checkBox_RTS1.Name = "checkBox_RTS1";
            this.checkBox_RTS1.Size = new System.Drawing.Size(48, 17);
            this.checkBox_RTS1.TabIndex = 27;
            this.checkBox_RTS1.Text = "RTS";
            this.checkBox_RTS1.UseVisualStyleBackColor = true;
            this.checkBox_RTS1.CheckedChanged += new System.EventHandler(this.checkBox_RTS1_CheckedChanged);
            // 
            // checkBox_DTR1
            // 
            this.checkBox_DTR1.AutoSize = true;
            this.checkBox_DTR1.Enabled = false;
            this.checkBox_DTR1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_DTR1.Location = new System.Drawing.Point(281, 57);
            this.checkBox_DTR1.Name = "checkBox_DTR1";
            this.checkBox_DTR1.Size = new System.Drawing.Size(49, 17);
            this.checkBox_DTR1.TabIndex = 26;
            this.checkBox_DTR1.Text = "DTR";
            this.checkBox_DTR1.UseVisualStyleBackColor = true;
            this.checkBox_DTR1.CheckedChanged += new System.EventHandler(this.checkBox_DTR1_CheckedChanged);
            // 
            // checkBox_DSR1
            // 
            this.checkBox_DSR1.AutoSize = true;
            this.checkBox_DSR1.Enabled = false;
            this.checkBox_DSR1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox_DSR1.Location = new System.Drawing.Point(226, 57);
            this.checkBox_DSR1.Name = "checkBox_DSR1";
            this.checkBox_DSR1.Size = new System.Drawing.Size(49, 17);
            this.checkBox_DSR1.TabIndex = 25;
            this.checkBox_DSR1.Text = "DSR";
            this.checkBox_DSR1.UseVisualStyleBackColor = true;
            // 
            // checkBox_CD1
            // 
            this.checkBox_CD1.AutoSize = true;
            this.checkBox_CD1.Enabled = false;
            this.checkBox_CD1.Location = new System.Drawing.Point(179, 57);
            this.checkBox_CD1.Name = "checkBox_CD1";
            this.checkBox_CD1.Size = new System.Drawing.Size(41, 17);
            this.checkBox_CD1.TabIndex = 24;
            this.checkBox_CD1.Text = "CD";
            this.checkBox_CD1.UseVisualStyleBackColor = true;
            // 
            // label_ComStopBits
            // 
            this.label_ComStopBits.AutoSize = true;
            this.label_ComStopBits.Location = new System.Drawing.Point(497, 9);
            this.label_ComStopBits.Name = "label_ComStopBits";
            this.label_ComStopBits.Size = new System.Drawing.Size(48, 13);
            this.label_ComStopBits.TabIndex = 69;
            this.label_ComStopBits.Text = "Stop bits";
            // 
            // label_ComFlow
            // 
            this.label_ComFlow.AutoSize = true;
            this.label_ComFlow.Location = new System.Drawing.Point(176, 9);
            this.label_ComFlow.Name = "label_ComFlow";
            this.label_ComFlow.Size = new System.Drawing.Size(64, 13);
            this.label_ComFlow.TabIndex = 70;
            this.label_ComFlow.Text = "Flow control";
            // 
            // label_ComParity
            // 
            this.label_ComParity.AutoSize = true;
            this.label_ComParity.Location = new System.Drawing.Point(413, 9);
            this.label_ComParity.Name = "label_ComParity";
            this.label_ComParity.Size = new System.Drawing.Size(33, 13);
            this.label_ComParity.TabIndex = 68;
            this.label_ComParity.Text = "Parity";
            // 
            // label_ComPort
            // 
            this.label_ComPort.AutoSize = true;
            this.label_ComPort.Location = new System.Drawing.Point(12, 9);
            this.label_ComPort.Name = "label_ComPort";
            this.label_ComPort.Size = new System.Drawing.Size(53, 13);
            this.label_ComPort.TabIndex = 67;
            this.label_ComPort.Text = "COM Port";
            // 
            // label_ComSpeed
            // 
            this.label_ComSpeed.AutoSize = true;
            this.label_ComSpeed.Location = new System.Drawing.Point(85, 9);
            this.label_ComSpeed.Name = "label_ComSpeed";
            this.label_ComSpeed.Size = new System.Drawing.Size(58, 13);
            this.label_ComSpeed.TabIndex = 66;
            this.label_ComSpeed.Text = "Port speed";
            // 
            // label_ComDataBits
            // 
            this.label_ComDataBits.AutoSize = true;
            this.label_ComDataBits.Location = new System.Drawing.Point(331, 9);
            this.label_ComDataBits.Name = "label_ComDataBits";
            this.label_ComDataBits.Size = new System.Drawing.Size(49, 13);
            this.label_ComDataBits.TabIndex = 65;
            this.label_ComDataBits.Text = "Data bits";
            // 
            // comboBox_stopbits1
            // 
            this.comboBox_stopbits1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_stopbits1.FormattingEnabled = true;
            this.comboBox_stopbits1.Location = new System.Drawing.Point(500, 25);
            this.comboBox_stopbits1.Name = "comboBox_stopbits1";
            this.comboBox_stopbits1.Size = new System.Drawing.Size(72, 21);
            this.comboBox_stopbits1.Sorted = true;
            this.comboBox_stopbits1.TabIndex = 6;
            // 
            // comboBox_parity1
            // 
            this.comboBox_parity1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_parity1.FormattingEnabled = true;
            this.comboBox_parity1.Location = new System.Drawing.Point(416, 25);
            this.comboBox_parity1.Name = "comboBox_parity1";
            this.comboBox_parity1.Size = new System.Drawing.Size(78, 21);
            this.comboBox_parity1.Sorted = true;
            this.comboBox_parity1.TabIndex = 5;
            // 
            // comboBox_databits1
            // 
            this.comboBox_databits1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_databits1.FormattingEnabled = true;
            this.comboBox_databits1.Items.AddRange(new object[] {
            "8",
            "7",
            "6",
            "5"});
            this.comboBox_databits1.Location = new System.Drawing.Point(334, 25);
            this.comboBox_databits1.Name = "comboBox_databits1";
            this.comboBox_databits1.Size = new System.Drawing.Size(76, 21);
            this.comboBox_databits1.TabIndex = 4;
            // 
            // comboBox_handshake1
            // 
            this.comboBox_handshake1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_handshake1.FormattingEnabled = true;
            this.comboBox_handshake1.Location = new System.Drawing.Point(179, 25);
            this.comboBox_handshake1.Name = "comboBox_handshake1";
            this.comboBox_handshake1.Size = new System.Drawing.Size(149, 21);
            this.comboBox_handshake1.Sorted = true;
            this.comboBox_handshake1.TabIndex = 3;
            // 
            // comboBox_portspeed1
            // 
            this.comboBox_portspeed1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_portspeed1.FormattingEnabled = true;
            this.comboBox_portspeed1.Items.AddRange(new object[] {
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
            "300"});
            this.comboBox_portspeed1.Location = new System.Drawing.Point(88, 25);
            this.comboBox_portspeed1.Name = "comboBox_portspeed1";
            this.comboBox_portspeed1.Size = new System.Drawing.Size(85, 21);
            this.comboBox_portspeed1.TabIndex = 2;
            // 
            // comboBox_portname1
            // 
            this.comboBox_portname1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_portname1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBox_portname1.FormattingEnabled = true;
            this.comboBox_portname1.Location = new System.Drawing.Point(12, 25);
            this.comboBox_portname1.Name = "comboBox_portname1";
            this.comboBox_portname1.Size = new System.Drawing.Size(70, 21);
            this.comboBox_portname1.Sorted = true;
            this.comboBox_portname1.TabIndex = 1;
            this.comboBox_portname1.SelectedIndexChanged += new System.EventHandler(this.comboBox_portname1_SelectedIndexChanged);
            // 
            // button_closeport
            // 
            this.button_closeport.Enabled = false;
            this.button_closeport.Location = new System.Drawing.Point(89, 52);
            this.button_closeport.MinimumSize = new System.Drawing.Size(70, 25);
            this.button_closeport.Name = "button_closeport";
            this.button_closeport.Size = new System.Drawing.Size(70, 25);
            this.button_closeport.TabIndex = 8;
            this.button_closeport.Text = "Close";
            this.button_closeport.UseVisualStyleBackColor = true;
            this.button_closeport.Click += new System.EventHandler(this.button_closeport_Click);
            // 
            // button_openport
            // 
            this.button_openport.Location = new System.Drawing.Point(12, 52);
            this.button_openport.MinimumSize = new System.Drawing.Size(70, 25);
            this.button_openport.Name = "button_openport";
            this.button_openport.Size = new System.Drawing.Size(70, 25);
            this.button_openport.TabIndex = 7;
            this.button_openport.Text = "Open";
            this.button_openport.UseVisualStyleBackColor = true;
            this.button_openport.Click += new System.EventHandler(this.button_openport_Click);
            // 
            // button_Clear
            // 
            this.button_Clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Clear.Location = new System.Drawing.Point(502, 275);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(70, 25);
            this.button_Clear.TabIndex = 23;
            this.button_Clear.Text = "Clear";
            this.button_Clear.UseVisualStyleBackColor = true;
            this.button_Clear.Click += new System.EventHandler(this.button_Clear_Click);
            // 
            // textBox_param
            // 
            this.textBox_param.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_param.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBox_param.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox_param.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_param.Location = new System.Drawing.Point(186, 110);
            this.textBox_param.Name = "textBox_param";
            this.textBox_param.Size = new System.Drawing.Size(386, 20);
            this.textBox_param.TabIndex = 13;
            this.textBox_param.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_command_KeyUp);
            this.textBox_param.Leave += new System.EventHandler(this.textBox_param_Leave);
            // 
            // checkBox_hexParam
            // 
            this.checkBox_hexParam.AutoSize = true;
            this.checkBox_hexParam.Checked = true;
            this.checkBox_hexParam.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_hexParam.Location = new System.Drawing.Point(88, 112);
            this.checkBox_hexParam.Name = "checkBox_hexParam";
            this.checkBox_hexParam.Size = new System.Drawing.Size(93, 17);
            this.checkBox_hexParam.TabIndex = 12;
            this.checkBox_hexParam.Text = "hex parameter";
            this.checkBox_hexParam.UseVisualStyleBackColor = true;
            this.checkBox_hexParam.CheckedChanged += new System.EventHandler(this.checkBox_hexParam_CheckedChanged);
            // 
            // checkBox_hexTerminal
            // 
            this.checkBox_hexTerminal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_hexTerminal.AutoSize = true;
            this.checkBox_hexTerminal.Checked = true;
            this.checkBox_hexTerminal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_hexTerminal.Location = new System.Drawing.Point(90, 283);
            this.checkBox_hexTerminal.Name = "checkBox_hexTerminal";
            this.checkBox_hexTerminal.Size = new System.Drawing.Size(48, 17);
            this.checkBox_hexTerminal.TabIndex = 20;
            this.checkBox_hexTerminal.Text = "Hex;";
            this.checkBox_hexTerminal.UseVisualStyleBackColor = true;
            // 
            // checkBox_autoscroll
            // 
            this.checkBox_autoscroll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_autoscroll.AutoSize = true;
            this.checkBox_autoscroll.Checked = true;
            this.checkBox_autoscroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_autoscroll.Enabled = false;
            this.checkBox_autoscroll.Location = new System.Drawing.Point(12, 283);
            this.checkBox_autoscroll.Name = "checkBox_autoscroll";
            this.checkBox_autoscroll.Size = new System.Drawing.Size(75, 17);
            this.checkBox_autoscroll.TabIndex = 19;
            this.checkBox_autoscroll.Text = "Autoscroll;";
            this.checkBox_autoscroll.UseVisualStyleBackColor = true;
            // 
            // checkBox_hexCommand
            // 
            this.checkBox_hexCommand.AutoSize = true;
            this.checkBox_hexCommand.Checked = true;
            this.checkBox_hexCommand.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_hexCommand.Location = new System.Drawing.Point(88, 85);
            this.checkBox_hexCommand.Name = "checkBox_hexCommand";
            this.checkBox_hexCommand.Size = new System.Drawing.Size(92, 17);
            this.checkBox_hexCommand.TabIndex = 10;
            this.checkBox_hexCommand.Text = "hex command";
            this.checkBox_hexCommand.UseVisualStyleBackColor = true;
            this.checkBox_hexCommand.CheckedChanged += new System.EventHandler(this.checkBox_hexCommand_CheckedChanged);
            // 
            // textBox_terminal
            // 
            this.textBox_terminal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_terminal.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_terminal.Location = new System.Drawing.Point(12, 198);
            this.textBox_terminal.Multiline = true;
            this.textBox_terminal.Name = "textBox_terminal";
            this.textBox_terminal.ReadOnly = true;
            this.textBox_terminal.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_terminal.Size = new System.Drawing.Size(560, 71);
            this.textBox_terminal.TabIndex = 18;
            // 
            // textBox_command
            // 
            this.textBox_command.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_command.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBox_command.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox_command.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_command.Location = new System.Drawing.Point(186, 83);
            this.textBox_command.Name = "textBox_command";
            this.textBox_command.Size = new System.Drawing.Size(386, 20);
            this.textBox_command.TabIndex = 11;
            this.textBox_command.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_command_KeyUp);
            this.textBox_command.Leave += new System.EventHandler(this.textBox_command_Leave);
            // 
            // button_Send
            // 
            this.button_Send.Enabled = false;
            this.button_Send.Location = new System.Drawing.Point(12, 83);
            this.button_Send.Name = "button_Send";
            this.button_Send.Size = new System.Drawing.Size(70, 47);
            this.button_Send.TabIndex = 87;
            this.button_Send.Text = "Send";
            this.button_Send.UseVisualStyleBackColor = true;
            this.button_Send.Click += new System.EventHandler(this.button_Send_Click);
            // 
            // serialPort1
            // 
            this.serialPort1.WriteBufferSize = 20480;
            this.serialPort1.WriteTimeout = 500;
            this.serialPort1.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.serialPort1_ErrorReceived);
            this.serialPort1.PinChanged += new System.IO.Ports.SerialPinChangedEventHandler(this.serialPort1_PinChanged);
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // textBox_saveTo
            // 
            this.textBox_saveTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_saveTo.Location = new System.Drawing.Point(211, 281);
            this.textBox_saveTo.Name = "textBox_saveTo";
            this.textBox_saveTo.Size = new System.Drawing.Size(70, 20);
            this.textBox_saveTo.TabIndex = 22;
            this.textBox_saveTo.Text = "com_rx.txt";
            // 
            // checkBox_saveTo
            // 
            this.checkBox_saveTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_saveTo.AutoSize = true;
            this.checkBox_saveTo.Checked = true;
            this.checkBox_saveTo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_saveTo.Location = new System.Drawing.Point(141, 283);
            this.checkBox_saveTo.Name = "checkBox_saveTo";
            this.checkBox_saveTo.Size = new System.Drawing.Size(64, 17);
            this.checkBox_saveTo.TabIndex = 21;
            this.checkBox_saveTo.Text = "save to:";
            this.checkBox_saveTo.UseVisualStyleBackColor = true;
            this.checkBox_saveTo.CheckedChanged += new System.EventHandler(this.checkBox_saveTo_CheckedChanged);
            // 
            // button_openFile
            // 
            this.button_openFile.Location = new System.Drawing.Point(12, 136);
            this.button_openFile.MinimumSize = new System.Drawing.Size(70, 25);
            this.button_openFile.Name = "button_openFile";
            this.button_openFile.Size = new System.Drawing.Size(70, 25);
            this.button_openFile.TabIndex = 16;
            this.button_openFile.Text = "Select file:";
            this.button_openFile.UseVisualStyleBackColor = true;
            this.button_openFile.Click += new System.EventHandler(this.button_openFile_Click);
            // 
            // textBox_fileName
            // 
            this.textBox_fileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_fileName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBox_fileName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox_fileName.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_fileName.Location = new System.Drawing.Point(186, 136);
            this.textBox_fileName.Name = "textBox_fileName";
            this.textBox_fileName.Size = new System.Drawing.Size(308, 20);
            this.textBox_fileName.TabIndex = 15;
            this.textBox_fileName.TextChanged += new System.EventHandler(this.textBox_fileName_TextChanged);
            // 
            // checkBox_hexFileOpen
            // 
            this.checkBox_hexFileOpen.AutoSize = true;
            this.checkBox_hexFileOpen.Checked = true;
            this.checkBox_hexFileOpen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_hexFileOpen.Location = new System.Drawing.Point(88, 141);
            this.checkBox_hexFileOpen.Name = "checkBox_hexFileOpen";
            this.checkBox_hexFileOpen.Size = new System.Drawing.Size(87, 17);
            this.checkBox_hexFileOpen.TabIndex = 14;
            this.checkBox_hexFileOpen.Text = "hex text data";
            this.checkBox_hexFileOpen.UseVisualStyleBackColor = true;
            this.checkBox_hexFileOpen.CheckedChanged += new System.EventHandler(this.checkBox_hexFileOpen_CheckedChanged);
            // 
            // button_sendFile
            // 
            this.button_sendFile.Enabled = false;
            this.button_sendFile.Location = new System.Drawing.Point(12, 167);
            this.button_sendFile.MinimumSize = new System.Drawing.Size(70, 25);
            this.button_sendFile.Name = "button_sendFile";
            this.button_sendFile.Size = new System.Drawing.Size(70, 25);
            this.button_sendFile.TabIndex = 17;
            this.button_sendFile.Text = "Send file";
            this.button_sendFile.UseVisualStyleBackColor = true;
            this.button_sendFile.Click += new System.EventHandler(this.button_sendFile_ClickAsync);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // textBox_delay
            // 
            this.textBox_delay.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBox_delay.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox_delay.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_delay.Location = new System.Drawing.Point(475, 170);
            this.textBox_delay.MaxLength = 5;
            this.textBox_delay.Name = "textBox_delay";
            this.textBox_delay.Size = new System.Drawing.Size(40, 20);
            this.textBox_delay.TabIndex = 101;
            this.textBox_delay.Text = "1000";
            // 
            // textBox_sendNum
            // 
            this.textBox_sendNum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBox_sendNum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox_sendNum.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_sendNum.Location = new System.Drawing.Point(389, 170);
            this.textBox_sendNum.MaxLength = 5;
            this.textBox_sendNum.Name = "textBox_sendNum";
            this.textBox_sendNum.Size = new System.Drawing.Size(40, 20);
            this.textBox_sendNum.TabIndex = 102;
            this.textBox_sendNum.Text = "001";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(521, 173);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(54, 13);
            this.label24.TabIndex = 103;
            this.label24.Text = "ms. delay;";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(435, 173);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(34, 13);
            this.label23.TabIndex = 104;
            this.label23.Text = "times;";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(332, 173);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 103;
            this.label1.Text = "ms. delay;";
            // 
            // textBox_strDelay
            // 
            this.textBox_strDelay.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBox_strDelay.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox_strDelay.Enabled = false;
            this.textBox_strDelay.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_strDelay.Location = new System.Drawing.Point(286, 170);
            this.textBox_strDelay.MaxLength = 5;
            this.textBox_strDelay.Name = "textBox_strDelay";
            this.textBox_strDelay.Size = new System.Drawing.Size(40, 20);
            this.textBox_strDelay.TabIndex = 101;
            this.textBox_strDelay.Text = "0";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(500, 136);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(72, 20);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 105;
            // 
            // radioButton_byString
            // 
            this.radioButton_byString.AutoSize = true;
            this.radioButton_byString.Location = new System.Drawing.Point(150, 171);
            this.radioButton_byString.Name = "radioButton_byString";
            this.radioButton_byString.Size = new System.Drawing.Size(67, 17);
            this.radioButton_byString.TabIndex = 106;
            this.radioButton_byString.TabStop = true;
            this.radioButton_byString.Text = "by string;";
            this.radioButton_byString.UseVisualStyleBackColor = true;
            // 
            // radioButton_byByte
            // 
            this.radioButton_byByte.AutoSize = true;
            this.radioButton_byByte.Location = new System.Drawing.Point(220, 171);
            this.radioButton_byByte.Name = "radioButton_byByte";
            this.radioButton_byByte.Size = new System.Drawing.Size(62, 17);
            this.radioButton_byByte.TabIndex = 106;
            this.radioButton_byByte.TabStop = true;
            this.radioButton_byByte.Text = "by byte;";
            this.radioButton_byByte.UseVisualStyleBackColor = true;
            // 
            // radioButton_stream
            // 
            this.radioButton_stream.AutoSize = true;
            this.radioButton_stream.Checked = true;
            this.radioButton_stream.Location = new System.Drawing.Point(88, 171);
            this.radioButton_stream.Name = "radioButton_stream";
            this.radioButton_stream.Size = new System.Drawing.Size(59, 17);
            this.radioButton_stream.TabIndex = 106;
            this.radioButton_stream.TabStop = true;
            this.radioButton_stream.Text = "stream;";
            this.radioButton_stream.UseVisualStyleBackColor = true;
            this.radioButton_stream.CheckedChanged += new System.EventHandler(this.radioButton_stream_CheckedChanged);
            // 
            // textBox_strLimit
            // 
            this.textBox_strLimit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_strLimit.Location = new System.Drawing.Point(395, 281);
            this.textBox_strLimit.Name = "textBox_strLimit";
            this.textBox_strLimit.Size = new System.Drawing.Size(34, 20);
            this.textBox_strLimit.TabIndex = 22;
            this.textBox_strLimit.Text = "1000";
            this.textBox_strLimit.TextChanged += new System.EventHandler(this.textBox_strLimit_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(287, 284);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 103;
            this.label2.Text = "combine strings, ms:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 312);
            this.Controls.Add(this.radioButton_stream);
            this.Controls.Add(this.radioButton_byByte);
            this.Controls.Add(this.radioButton_byString);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.textBox_strDelay);
            this.Controls.Add(this.textBox_delay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_sendNum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.button_openFile);
            this.Controls.Add(this.textBox_fileName);
            this.Controls.Add(this.checkBox_hexFileOpen);
            this.Controls.Add(this.button_sendFile);
            this.Controls.Add(this.textBox_strLimit);
            this.Controls.Add(this.textBox_saveTo);
            this.Controls.Add(this.checkBox_saveTo);
            this.Controls.Add(this.button_Clear);
            this.Controls.Add(this.textBox_param);
            this.Controls.Add(this.checkBox_hexParam);
            this.Controls.Add(this.checkBox_hexTerminal);
            this.Controls.Add(this.checkBox_autoscroll);
            this.Controls.Add(this.checkBox_hexCommand);
            this.Controls.Add(this.textBox_terminal);
            this.Controls.Add(this.textBox_command);
            this.Controls.Add(this.button_Send);
            this.Controls.Add(this.button_refresh);
            this.Controls.Add(this.checkBox_RI1);
            this.Controls.Add(this.checkBox_CTS1);
            this.Controls.Add(this.checkBox_RTS1);
            this.Controls.Add(this.checkBox_DTR1);
            this.Controls.Add(this.checkBox_DSR1);
            this.Controls.Add(this.checkBox_CD1);
            this.Controls.Add(this.label_ComStopBits);
            this.Controls.Add(this.label_ComFlow);
            this.Controls.Add(this.label_ComParity);
            this.Controls.Add(this.label_ComPort);
            this.Controls.Add(this.label_ComSpeed);
            this.Controls.Add(this.label_ComDataBits);
            this.Controls.Add(this.comboBox_stopbits1);
            this.Controls.Add(this.comboBox_parity1);
            this.Controls.Add(this.comboBox_databits1);
            this.Controls.Add(this.comboBox_handshake1);
            this.Controls.Add(this.comboBox_portspeed1);
            this.Controls.Add(this.comboBox_portname1);
            this.Controls.Add(this.button_closeport);
            this.Controls.Add(this.button_openport);
            this.MinimumSize = new System.Drawing.Size(600, 300);
            this.Name = "Form1";
            this.Text = "ComPrnControl (c) Andrey Kalugin (jekyll@mail.ru), 2016";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_refresh;
        private System.Windows.Forms.CheckBox checkBox_RI1;
        private System.Windows.Forms.CheckBox checkBox_CTS1;
        private System.Windows.Forms.CheckBox checkBox_RTS1;
        private System.Windows.Forms.CheckBox checkBox_DTR1;
        private System.Windows.Forms.CheckBox checkBox_DSR1;
        private System.Windows.Forms.CheckBox checkBox_CD1;
        private System.Windows.Forms.Label label_ComStopBits;
        private System.Windows.Forms.Label label_ComFlow;
        private System.Windows.Forms.Label label_ComParity;
        private System.Windows.Forms.Label label_ComPort;
        private System.Windows.Forms.Label label_ComSpeed;
        private System.Windows.Forms.Label label_ComDataBits;
        private System.Windows.Forms.ComboBox comboBox_stopbits1;
        private System.Windows.Forms.ComboBox comboBox_parity1;
        private System.Windows.Forms.ComboBox comboBox_databits1;
        private System.Windows.Forms.ComboBox comboBox_handshake1;
        private System.Windows.Forms.ComboBox comboBox_portspeed1;
        private System.Windows.Forms.ComboBox comboBox_portname1;
        private System.Windows.Forms.Button button_closeport;
        private System.Windows.Forms.Button button_openport;
        private System.Windows.Forms.Button button_Clear;
        private System.Windows.Forms.TextBox textBox_param;
        private System.Windows.Forms.CheckBox checkBox_hexParam;
        private System.Windows.Forms.CheckBox checkBox_hexTerminal;
        private System.Windows.Forms.CheckBox checkBox_autoscroll;
        private System.Windows.Forms.CheckBox checkBox_hexCommand;
        private System.Windows.Forms.TextBox textBox_terminal;
        private System.Windows.Forms.TextBox textBox_command;
        private System.Windows.Forms.Button button_Send;
        private System.Windows.Forms.TextBox textBox_saveTo;
        private System.Windows.Forms.CheckBox checkBox_saveTo;
        private System.Windows.Forms.Button button_openFile;
        private System.Windows.Forms.TextBox textBox_fileName;
        private System.Windows.Forms.CheckBox checkBox_hexFileOpen;
        private System.Windows.Forms.Button button_sendFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox textBox_delay;
        private System.Windows.Forms.TextBox textBox_sendNum;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_strDelay;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.RadioButton radioButton_byString;
        private System.Windows.Forms.RadioButton radioButton_byByte;
        private System.Windows.Forms.RadioButton radioButton_stream;
        private System.Windows.Forms.TextBox textBox_strLimit;
        private System.Windows.Forms.Label label2;
        public SerialPort serialPort1;

        bool o_cd1, o_dsr1, o_dtr1, o_rts1, o_cts1;
        int SendComing = 0, txtOutState = 0;
        long oldTicks = DateTime.Now.Ticks, limitTick = 0;

        delegate void SetTextCallback1(string text);
        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            //if (this.textBox_terminal1.InvokeRequired)
            if (this.textBox_terminal.InvokeRequired)
            {
                SetTextCallback1 d = new SetTextCallback1(SetText);
                this.BeginInvoke(d, new object[] { text });
            }
            else
            {
                //this.textBox_terminal.Text += text;
                this.textBox_terminal.SelectionStart = this.textBox_terminal.TextLength;
                this.textBox_terminal.SelectedText = text;
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

        public static string ConvertByteArrToHex(byte[] byteArr, int arrLength)
        {
            string hexStr = "";
            int i = 0;
            for (i = 0; i < arrLength; i++)
            {
                hexStr += byteArr[i].ToString("X2") + " ";
            }
            return hexStr;
        }

        public static string ConvertStringToHex(string utfString)
        {
            byte[] encodedBytes = System.Text.Encoding.GetEncoding(ComPrnControl.Properties.Settings.Default.CodePage).GetBytes(utfString);
            string hexStr = "";
            foreach (System.Char c in encodedBytes)
            {
                hexStr += ((int)c).ToString("X2") + " ";
            }
            return hexStr;
        }

        public static string ConvertHexToString(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if (hexString.Length % 2 == 1) hexString = hexString + "0";
            byte[] StrValue = new byte[hexString.Length / 2];
            int i = 0;
            while (hexString.Length > 1)
            {
                StrValue[i] = System.Convert.ToByte(System.Convert.ToUInt32(hexString.Substring(0, 2), 16));
                hexString = hexString.Substring(2, hexString.Length - 2);
                i++;
            }
            return System.Text.Encoding.GetEncoding(ComPrnControl.Properties.Settings.Default.CodePage).GetString(StrValue, 0, i);
        }

        public static byte[] ConvertHexToByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if (hexString.Length % 2 == 1) hexString = hexString + "0";
            byte[] byteValue = new byte[hexString.Length / 2];
            int i = 0;
            while (hexString.Length > 1)
            {
                byteValue[i] = System.Convert.ToByte(System.Convert.ToUInt32(hexString.Substring(0, 2), 16));
                hexString = hexString.Substring(2, hexString.Length - 2);
                i++;
            }
            return byteValue;
        }

        string checkHexString(string inStr)
        {
            string outStr = "";
            if (inStr != "")
            {
                char[] str = inStr.ToCharArray(0, inStr.Length);
                string tmpStr = "";
                for (int i = 0; i < inStr.Length; i++)
                {
                    if ((str[i] >= 'A' && str[i] <= 'F') || (str[i] >= 'a' && str[i] <= 'f') || (str[i] >= '0' && str[i] <= '9'))
                    {
                        tmpStr += str[i].ToString();
                    }
                    else if (str[i] == ' ' && tmpStr.Length > 0)
                    {
                        for (int i1 = 0; i1 < 2 - tmpStr.Length; i1++) outStr += "0";
                        outStr += tmpStr + " ";
                        tmpStr = "";
                    }
                    if (tmpStr.Length == 2)
                    {
                        outStr += tmpStr + " ";
                        tmpStr = "";
                    }
                }
                if (tmpStr != "")
                {
                    for (int i = 0; i < 2 - tmpStr.Length; i++) outStr += "0";
                    outStr += tmpStr + " ";
                }
                return outStr.ToUpperInvariant();
            }
            else return ("");
        }

        delegate void SetPinCallback1(bool setPin);
        private void SetPinCD1(bool setPin)
        {
            if (this.checkBox_CD1.InvokeRequired)
            {
                SetPinCallback1 d = new SetPinCallback1(SetPinCD1);
                this.BeginInvoke(d, new object[] { setPin });
            }
            else
            {
                this.checkBox_CD1.Checked = setPin;
            }
        }

        private void SetPinDSR1(bool setPin)
        {
            if (this.checkBox_DSR1.InvokeRequired)
            {
                SetPinCallback1 d = new SetPinCallback1(SetPinDSR1);
                this.BeginInvoke(d, new object[] { setPin });
            }
            else
            {
                this.checkBox_DSR1.Checked = setPin;
            }
        }

        private void SetPinCTS1(bool setPin)
        {
            if (this.checkBox_CTS1.InvokeRequired)
            {
                SetPinCallback1 d = new SetPinCallback1(SetPinCTS1);
                this.BeginInvoke(d, new object[] { setPin });
            }
            else
            {
                this.checkBox_CTS1.Checked = setPin;
            }
        }

        private void SetPinRING1(bool setPin)
        {
            if (this.checkBox_RI1.InvokeRequired)
            {
                SetPinCallback1 d = new SetPinCallback1(SetPinRING1);
                this.BeginInvoke(d, new object[] { setPin });
            }
            else
            {
                this.checkBox_RI1.Checked = setPin;
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
            lock (threadLock)
            {
                if (txtOutState == state && (DateTime.Now.Ticks - oldTicks) < limitTick && state != 12 && state != 22)
                {
                    SetText(tmpBuffer);
                    oldTicks = DateTime.Now.Ticks;
                }
                else
                {
                    if (state == Port1DataIn) tmpBuffer = "\r\n<< " + tmpBuffer;         //sending data
                    else if (state == Port1DataOut) tmpBuffer = "\r\n>> " + tmpBuffer;    //receiving data
                    else if (state == Port1SignalIn) tmpBuffer = "\r\n<< " + tmpBuffer;    //pin change received
                    else if (state == Port1SignalOut) tmpBuffer = "\r\n>> " + tmpBuffer;    //pin changed by user
                    else if (state == Port1Error) tmpBuffer = "\r\n!! " + tmpBuffer;    //error occured
                    SetText(tmpBuffer);
                    txtOutState = state;
                    oldTicks = DateTime.Now.Ticks;
                }
                if (checkBox_saveTo.Checked == true)
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
            }
        }
    }
}

