namespace Main
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.g_imShow = new System.Windows.Forms.GroupBox();
            this.p_imShow = new System.Windows.Forms.PictureBox();
            this.G_setting = new System.Windows.Forms.GroupBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.comboCamera = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.Center_text = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.threshold_value = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.roi_text = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.exposure_time = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Start = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pulse_x = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pulse_y = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pulse_z = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.G_PLC = new System.Windows.Forms.GroupBox();
            this.log = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.Serial_Light = new System.IO.Ports.SerialPort(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.modeCamera = new System.Windows.Forms.RadioButton();
            this.modeFolder = new System.Windows.Forms.RadioButton();
            this.panel9 = new System.Windows.Forms.Panel();
            this.iLink = new System.Windows.Forms.RichTextBox();
            this.iChooseLink = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cameraCaptureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rOIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotationCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getCenterRotationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getOriginImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.algorithm = new System.Windows.Forms.CheckBox();
            this.Serial_Scanner = new System.IO.Ports.SerialPort(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.g_imShow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.p_imShow)).BeginInit();
            this.G_setting.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.G_PLC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.panel9.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // g_imShow
            // 
            this.g_imShow.Controls.Add(this.p_imShow);
            this.g_imShow.Location = new System.Drawing.Point(12, 27);
            this.g_imShow.Name = "g_imShow";
            this.g_imShow.Size = new System.Drawing.Size(869, 608);
            this.g_imShow.TabIndex = 0;
            this.g_imShow.TabStop = false;
            this.g_imShow.Text = "Image Capture";
            // 
            // p_imShow
            // 
            this.p_imShow.BackColor = System.Drawing.SystemColors.ControlDark;
            this.p_imShow.Location = new System.Drawing.Point(6, 19);
            this.p_imShow.Name = "p_imShow";
            this.p_imShow.Size = new System.Drawing.Size(855, 583);
            this.p_imShow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.p_imShow.TabIndex = 0;
            this.p_imShow.TabStop = false;
            // 
            // G_setting
            // 
            this.G_setting.Controls.Add(this.panel8);
            this.G_setting.Controls.Add(this.panel7);
            this.G_setting.Controls.Add(this.panel6);
            this.G_setting.Controls.Add(this.panel2);
            this.G_setting.Controls.Add(this.panel1);
            this.G_setting.Location = new System.Drawing.Point(887, 27);
            this.G_setting.Name = "G_setting";
            this.G_setting.Size = new System.Drawing.Size(191, 186);
            this.G_setting.TabIndex = 1;
            this.G_setting.TabStop = false;
            this.G_setting.Text = "Setting";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.comboCamera);
            this.panel8.Controls.Add(this.label8);
            this.panel8.Location = new System.Drawing.Point(3, 19);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(182, 27);
            this.panel8.TabIndex = 2;
            // 
            // comboCamera
            // 
            this.comboCamera.FormattingEnabled = true;
            this.comboCamera.Location = new System.Drawing.Point(77, 3);
            this.comboCamera.Name = "comboCamera";
            this.comboCamera.Size = new System.Drawing.Size(102, 21);
            this.comboCamera.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Camera:";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.Center_text);
            this.panel7.Controls.Add(this.label7);
            this.panel7.Location = new System.Drawing.Point(3, 121);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(182, 27);
            this.panel7.TabIndex = 4;
            // 
            // Center_text
            // 
            this.Center_text.Location = new System.Drawing.Point(104, 3);
            this.Center_text.Name = "Center_text";
            this.Center_text.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Center_text.Size = new System.Drawing.Size(75, 20);
            this.Center_text.TabIndex = 1;
            this.Center_text.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Center Rotation:";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.threshold_value);
            this.panel6.Controls.Add(this.label6);
            this.panel6.Location = new System.Drawing.Point(3, 154);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(182, 27);
            this.panel6.TabIndex = 3;
            // 
            // threshold_value
            // 
            this.threshold_value.Location = new System.Drawing.Point(104, 3);
            this.threshold_value.Name = "threshold_value";
            this.threshold_value.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.threshold_value.Size = new System.Drawing.Size(75, 20);
            this.threshold_value.TabIndex = 1;
            this.threshold_value.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Threshold Value:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.roi_text);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(3, 88);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(182, 27);
            this.panel2.TabIndex = 2;
            // 
            // roi_text
            // 
            this.roi_text.Location = new System.Drawing.Point(80, 3);
            this.roi_text.Name = "roi_text";
            this.roi_text.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.roi_text.Size = new System.Drawing.Size(99, 20);
            this.roi_text.TabIndex = 1;
            this.roi_text.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "ROI (x,y,w,h):";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.exposure_time);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(3, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(182, 27);
            this.panel1.TabIndex = 0;
            // 
            // exposure_time
            // 
            this.exposure_time.Location = new System.Drawing.Point(104, 3);
            this.exposure_time.Name = "exposure_time";
            this.exposure_time.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.exposure_time.Size = new System.Drawing.Size(75, 20);
            this.exposure_time.TabIndex = 1;
            this.exposure_time.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ExposureTime:";
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(890, 610);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(188, 40);
            this.Start.TabIndex = 2;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pulse_x);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(3, 19);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(182, 27);
            this.panel3.TabIndex = 3;
            // 
            // pulse_x
            // 
            this.pulse_x.Location = new System.Drawing.Point(104, 3);
            this.pulse_x.Name = "pulse_x";
            this.pulse_x.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.pulse_x.Size = new System.Drawing.Size(75, 20);
            this.pulse_x.TabIndex = 1;
            this.pulse_x.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Pulse X:";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pulse_y);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Location = new System.Drawing.Point(3, 52);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(182, 27);
            this.panel4.TabIndex = 4;
            // 
            // pulse_y
            // 
            this.pulse_y.Location = new System.Drawing.Point(104, 3);
            this.pulse_y.Name = "pulse_y";
            this.pulse_y.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.pulse_y.Size = new System.Drawing.Size(75, 20);
            this.pulse_y.TabIndex = 1;
            this.pulse_y.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Pulse Y:";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.pulse_z);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Location = new System.Drawing.Point(3, 85);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(182, 27);
            this.panel5.TabIndex = 4;
            // 
            // pulse_z
            // 
            this.pulse_z.Location = new System.Drawing.Point(104, 3);
            this.pulse_z.Name = "pulse_z";
            this.pulse_z.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.pulse_z.Size = new System.Drawing.Size(75, 20);
            this.pulse_z.TabIndex = 1;
            this.pulse_z.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Pulse Z:";
            // 
            // G_PLC
            // 
            this.G_PLC.Controls.Add(this.panel4);
            this.G_PLC.Controls.Add(this.panel5);
            this.G_PLC.Controls.Add(this.panel3);
            this.G_PLC.Location = new System.Drawing.Point(887, 219);
            this.G_PLC.Name = "G_PLC";
            this.G_PLC.Size = new System.Drawing.Size(191, 119);
            this.G_PLC.TabIndex = 5;
            this.G_PLC.TabStop = false;
            this.G_PLC.Text = "PLC configuration";
            // 
            // log
            // 
            this.log.Location = new System.Drawing.Point(15, 640);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(546, 13);
            this.log.TabIndex = 7;
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.Location = new System.Drawing.Point(932, 343);
            this.trackBar1.Maximum = 255;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(128, 24);
            this.trackBar1.TabIndex = 17;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // Serial_Light
            // 
            this.Serial_Light.BaudRate = 19200;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1059, 354);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label9.Size = new System.Drawing.Size(13, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(893, 354);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Light:";
            // 
            // modeCamera
            // 
            this.modeCamera.AutoSize = true;
            this.modeCamera.Checked = true;
            this.modeCamera.Location = new System.Drawing.Point(3, 4);
            this.modeCamera.Name = "modeCamera";
            this.modeCamera.Size = new System.Drawing.Size(87, 17);
            this.modeCamera.TabIndex = 19;
            this.modeCamera.TabStop = true;
            this.modeCamera.Text = "From Camera";
            this.modeCamera.UseVisualStyleBackColor = true;
            this.modeCamera.CheckedChanged += new System.EventHandler(this.modeCamera_CheckedChanged);
            // 
            // modeFolder
            // 
            this.modeFolder.AutoSize = true;
            this.modeFolder.Location = new System.Drawing.Point(3, 23);
            this.modeFolder.Name = "modeFolder";
            this.modeFolder.Size = new System.Drawing.Size(80, 17);
            this.modeFolder.TabIndex = 20;
            this.modeFolder.Text = "From Folder";
            this.modeFolder.UseVisualStyleBackColor = true;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.iLink);
            this.panel9.Controls.Add(this.iChooseLink);
            this.panel9.Controls.Add(this.modeFolder);
            this.panel9.Controls.Add(this.modeCamera);
            this.panel9.Location = new System.Drawing.Point(890, 376);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(188, 69);
            this.panel9.TabIndex = 21;
            // 
            // iLink
            // 
            this.iLink.Location = new System.Drawing.Point(6, 45);
            this.iLink.Multiline = false;
            this.iLink.Name = "iLink";
            this.iLink.Size = new System.Drawing.Size(140, 19);
            this.iLink.TabIndex = 24;
            this.iLink.Text = "";
            // 
            // iChooseLink
            // 
            this.iChooseLink.Location = new System.Drawing.Point(152, 42);
            this.iChooseLink.Name = "iChooseLink";
            this.iChooseLink.Size = new System.Drawing.Size(30, 24);
            this.iChooseLink.TabIndex = 23;
            this.iChooseLink.Text = "...";
            this.iChooseLink.UseVisualStyleBackColor = true;
            this.iChooseLink.Click += new System.EventHandler(this.iChooseLink_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1094, 24);
            this.menuStrip1.TabIndex = 22;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitToolStripMenuItem.Image")));
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cameraCaptureToolStripMenuItem,
            this.rOIToolStripMenuItem,
            this.rotationCenterToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // cameraCaptureToolStripMenuItem
            // 
            this.cameraCaptureToolStripMenuItem.Image = global::Main.Properties.Resources.capture;
            this.cameraCaptureToolStripMenuItem.Name = "cameraCaptureToolStripMenuItem";
            this.cameraCaptureToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.cameraCaptureToolStripMenuItem.Text = "Camera Capture";
            this.cameraCaptureToolStripMenuItem.Click += new System.EventHandler(this.cameraCaptureToolStripMenuItem_Click);
            // 
            // rOIToolStripMenuItem
            // 
            this.rOIToolStripMenuItem.Image = global::Main.Properties.Resources.testroi1;
            this.rOIToolStripMenuItem.Name = "rOIToolStripMenuItem";
            this.rOIToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.rOIToolStripMenuItem.Text = "ROI";
            this.rOIToolStripMenuItem.Click += new System.EventHandler(this.rOIToolStripMenuItem_Click);
            // 
            // rotationCenterToolStripMenuItem
            // 
            this.rotationCenterToolStripMenuItem.Image = global::Main.Properties.Resources.cencter;
            this.rotationCenterToolStripMenuItem.Name = "rotationCenterToolStripMenuItem";
            this.rotationCenterToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.rotationCenterToolStripMenuItem.Text = "Rotation Center";
            this.rotationCenterToolStripMenuItem.Click += new System.EventHandler(this.rotationCenterToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getCenterRotationToolStripMenuItem,
            this.getOriginImageToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // getCenterRotationToolStripMenuItem
            // 
            this.getCenterRotationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("getCenterRotationToolStripMenuItem.Image")));
            this.getCenterRotationToolStripMenuItem.Name = "getCenterRotationToolStripMenuItem";
            this.getCenterRotationToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.getCenterRotationToolStripMenuItem.Text = "Get Rotation Center";
            this.getCenterRotationToolStripMenuItem.Click += new System.EventHandler(this.getCenterRotationToolStripMenuItem_Click);
            // 
            // getOriginImageToolStripMenuItem
            // 
            this.getOriginImageToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("getOriginImageToolStripMenuItem.Image")));
            this.getOriginImageToolStripMenuItem.Name = "getOriginImageToolStripMenuItem";
            this.getOriginImageToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.getOriginImageToolStripMenuItem.Text = "Get Origin Image";
            this.getOriginImageToolStripMenuItem.Click += new System.EventHandler(this.getOriginImageToolStripMenuItem_Click);
            // 
            // algorithm
            // 
            this.algorithm.AutoSize = true;
            this.algorithm.Location = new System.Drawing.Point(890, 451);
            this.algorithm.Name = "algorithm";
            this.algorithm.Size = new System.Drawing.Size(69, 17);
            this.algorithm.TabIndex = 23;
            this.algorithm.Text = "Algorithm";
            this.algorithm.UseVisualStyleBackColor = true;
            // 
            // Serial_Scanner
            // 
            this.Serial_Scanner.BaudRate = 115200;
            this.Serial_Scanner.PortName = "COM10";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(988, 451);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 31);
            this.button1.TabIndex = 24;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(944, 526);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(50, 23);
            this.button2.TabIndex = 25;
            this.button2.Text = "Set";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(893, 526);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(40, 20);
            this.textBox1.TabIndex = 26;
            this.textBox1.Text = "M0";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(890, 552);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(49, 20);
            this.textBox2.TabIndex = 27;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(945, 552);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(49, 20);
            this.textBox3.TabIndex = 28;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1094, 662);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.algorithm);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.log);
            this.Controls.Add(this.G_PLC);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.G_setting);
            this.Controls.Add(this.g_imShow);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Tag = "Connet to Camera";
            this.Text = "Locate Label For Assembly Machine";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.g_imShow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.p_imShow)).EndInit();
            this.G_setting.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.G_PLC.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox g_imShow;
        private System.Windows.Forms.PictureBox p_imShow;
        private System.Windows.Forms.GroupBox G_setting;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox exposure_time;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox roi_text;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox threshold_value;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox pulse_y;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox pulse_z;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox pulse_x;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox G_PLC;
        private System.Windows.Forms.Label log;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.ComboBox comboCamera;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.IO.Ports.SerialPort Serial_Light;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton modeCamera;
        private System.Windows.Forms.RadioButton modeFolder;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getCenterRotationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        public System.Windows.Forms.TextBox Center_text;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cameraCaptureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rOIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotationCenterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getOriginImageToolStripMenuItem;
        private System.Windows.Forms.RichTextBox iLink;
        private System.Windows.Forms.Button iChooseLink;
        private System.Windows.Forms.CheckBox algorithm;
        private System.IO.Ports.SerialPort Serial_Scanner;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
    }
}

