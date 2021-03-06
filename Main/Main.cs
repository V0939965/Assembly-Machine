﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.Structure;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;

namespace Main
{
    public partial class Main : Form
    {
        //------------ PARAMETER VARIABLE -----------
        Origin ORG = new Origin();
        Configuration Config = new Configuration();
        Rectangle ORGRec = new Rectangle();

        HIKVision Camera = new HIKVision();
        private Thread HBB;
        private bool Run = false;
        PLCController.PLCController plc = new PLCController.PLCController("192.168.3.250",1996);
        PLCController.PLCController plc2 = new PLCController.PLCController("192.168.3.95", 1996);
        string linkimg = string.Empty;

        // ---------- PLC -------------
        string PLC_Flag = "M96", PLC_PulseX = "D96", PLC_PulseY = "D98", PLC_PulseZ = "D100",
            PLC_PulseXH = "D97", PLC_PulseYH = "D99", PLC_PulseZH = "D101";
        string[] Scanner = { "M76", "M166" };
        string[] LabelResut = new string[2];
        public Main()
        {
            InitializeComponent();
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadUI();
        }
        private void GetOriginImage()
        {
            try
            {
                using (Mat img = CvInvoke.Imread(@"img\origin.bmp", Emgu.CV.CvEnum.ImreadModes.Grayscale))
                {
                    Image<Gray, byte> _img = img.ToImage<Gray, byte>();
                    _img = ComputerVison.RoiImage(_img, Config.Parameter.ROI);
                    VectorOfPoint cnt = new VectorOfPoint();
                    cnt = ComputerVison.FindContours(_img, Config.Parameter.THRESHOLD_VALUE);
                    RotatedRect a = CvInvoke.MinAreaRect(cnt);
                    ORGRec = a.MinAreaRect();
                    ORGRec.X += Config.Parameter.ROI.X-10;
                    ORGRec.Y += Config.Parameter.ROI.Y-10;
                    ORGRec.Height += 20;
                    ORGRec.Width += 20;
                    using (Image<Bgr, byte> iBgr2 = _img.Convert<Bgr, byte>())
                    {
                        CvInvoke.Rectangle(iBgr2, ORGRec, new MCvScalar(0, 255, 0), 3);
                        CvInvoke.Imwrite("img\\originRoi.bmp", iBgr2);
                    }
                    Point[] p = ComputerVison.Search2Tip(cnt);
                    ORG.SetPointA(p[0]);
                    ORG.SetPointB(p[1]);
                    cnt.Dispose();
                    _img.Dispose();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
        private bool OpenCamera()
        {
            bool response = false;
            if (comboCamera.SelectedIndex != -1)
            {
                int nRet;
                nRet = Camera.OpenDevice(comboCamera.SelectedIndex);
                response = (nRet == HIKVision.HIK_OK);
                if (response == true)
                {
                    float Exposure;
                    Exposure = (float)Convert.ToDouble(exposure_time.Text);
                    Camera.SetExposureTime(Exposure);
                    Exposure = Camera.GetExposureTime();
                    exposure_time.Text = Math.Round(Exposure).ToString();
                }
            }
            else
                MessageBox.Show("Please select camera before execution!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return response;
        }
        private void LoadUI()
        {
            comboCamera.Items.Clear();
            string[] _ListDevice = Camera.SearchDevice();
            if(_ListDevice.Length > 0)
            {
                comboCamera.Items.AddRange(_ListDevice);
                comboCamera.SelectedIndex = 0;
            }
            else
            {
                _ListDevice = null;
                DialogResult result = MessageBox.Show("Camera not found!", "Information", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                if(result == DialogResult.Retry)
                {
                    Thread.Sleep(1000);
                    LoadUI();
                }
                else
                {
                    Start.Enabled = false;
                }
            }
            if (Serial_Light.IsOpen == false)
                Serial_Light.Open();
            Config.Init();
            UpdateParameter();
            modeCamera_CheckedChanged(null, null);
            ORG.SetPointO(Config.Parameter.ROTATION_CENTER);
            GetOriginImage();
            for (int i = 0; i < LabelResut.Length; i++)
            {
                LabelResut[i] = string.Empty;
            }

        }
        public void ShowLog(string stringstatus,Color color)
        {
            status.Text = stringstatus;
            status.ForeColor = color;
            status.Invalidate();
            status.Update();
            status.Refresh();
            Application.DoEvents();
        }
        private void Release()
        {
            Camera.CloseDevice();
            Run = false;
        }
        private bool OpenDevice()
        {
            ShowLog("Update parameter!", Color.Green);
            UpdateParameter(true); ;
            ShowLog("Connecting COM port for light!", Color.Green);
            if (!Serial_Light.IsOpen)
            {
                Serial_Light.Open();
            }
            ORG.SetPointO(Config.Parameter.ROTATION_CENTER);
            GetOriginImage();
            ShowLog("Connecting to Camera", Color.Green);
            bool response = OpenCamera();

            if (response)
            {
                ShowLog("Connecting to PLC 1", Color.Green);
                response = plc.Open(10000);
                if(response)
                {
                    ShowLog("Connecting to PLC 2", Color.Green);
                    response = plc2.Open(10000);
                    if (response)
                        ShowLog("Starting success", Color.Green);
                    else
                    {
                        MessageBox.Show("Can't Connecting to PLC 2 ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        ShowLog("Can't Connecting to PLC 2 ", Color.Red);
                    }
                }
                else
                {
                    MessageBox.Show("Can't Connecting to PLC 1 ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ShowLog("Can't Connecting to PLC 1 ", Color.Red);
                }
            }
            else
            {
                ShowLog("Can't open Camera", Color.Red);
                MessageBox.Show("Can't Connecting to Camera", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if(!response)
                Release();
            return response;

        }
        private Image<Bgr, byte> GetImage()
        {
            Image<Bgr, byte> img;
            if (modeCamera.Checked == true)
            {
                using (Bitmap Cap = Camera.GetFrame())
                {
                    img = new Image<Bgr, byte>(Cap);
                }
                return img;
            }
            else
            {
                using (Mat im = CvInvoke.Imread(linkimg))
                {
                    img = im.ToImage<Bgr, byte>();
                }
                return img;
            }  
        }

        private void ComputerVisionLabel()
        {
            if (modeCamera.Checked == true)
            {
                int Flag_Ready = 1;
                while (Run)
                {
                    int nRet_Locate=-1;
                    {
                        while(nRet_Locate == -1)
                        {
                            nRet_Locate = plc.GetDevice(PLC_Flag);
                        }
                    }
                    if (nRet_Locate == Flag_Ready)
                        Handling();
                    else
                    {
                        int[] nRet_Scanner = { -1, -1};
                         while (nRet_Scanner[0] == -1 && nRet_Scanner[1] == -1)
                        {
                            nRet_Scanner[0] = plc2.GetDevice(Scanner[0]);
                            nRet_Scanner[1] = plc2.GetDevice(Scanner[1]);
                        }
                        for (int i = 0;i< nRet_Scanner.Length;i++)
                        {
                            if (nRet_Scanner[i] == 1)
                                Scan_Label(i);
                        }  
                    }
                    SynchronizePLC();
                    Thread.Sleep(100);
                }
            }
        }
        private void PLCCommunicate(short x, short y, short z)
        {
            //lock(plc)
            {
                if (x < 0)
                    while(plc.SetDevice(PLC_PulseXH, -1)==-1);
                else
                    plc.SetDevice(PLC_PulseXH, 0);
                plc.SetDevice(PLC_PulseX, x);
                if (y < 0)
                    while (plc.SetDevice(PLC_PulseYH, -1)==-1);
                else
                    plc.SetDevice(PLC_PulseYH, 0);
                plc.SetDevice(PLC_PulseY, y);
                if (z < 0)
                    plc.SetDevice(PLC_PulseZH, -1);
                else
                    plc.SetDevice(PLC_PulseZH, 0);
                Thread.Sleep(1);
                plc.SetDevice(PLC_PulseZ, z);
                Thread.Sleep(1);
                plc.SetDevice(PLC_Flag, 0);
            }
            
        }
        private void Handling()
        {
            Response result = new Response();
            string name = DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString();
            Light_Mode(modeCamera.Checked);
            Thread.Sleep(500);
            Image<Bgr, byte> iBgr = GetImage();
            Light_Mode(false);
            Image<Gray, byte> iGray = iBgr.Convert<Gray, byte>();
            iGray = ComputerVison.RoiImage(iGray, Config.Parameter.ROI);
            VectorOfPoint cnt = new VectorOfPoint();
            Point[] p = new Point[2];
            cnt = ComputerVison.FindContours(iGray, Config.Parameter.THRESHOLD_VALUE);
            if(cnt != null)
            {
                p = ComputerVison.Search2Tip(cnt);
                ComputerVison.Calculator(ref result, ORG.PointA, ORG.PointB, p[0], p[1], Config.Parameter.LabelSize.Width, false);
                ComputerVison.RouPoint(ORG.PointO, ref p[0], result.ANGLE);
                ComputerVison.RouPoint(ORG.PointO, ref p[1], result.ANGLE);
                ComputerVison.Calculator(ref result, ORG.PointA, ORG.PointB, p[0], p[1], Config.Parameter.LabelSize.Width, true);
                short y = (short)Math.Round(result.X * Config.Parameter.PULSE_Y + 150);
                short x = (short)Math.Round(result.Y * Config.Parameter.PULSE_X + 20);
                short z = (short)Math.Round(-result.ANGLE * Config.Parameter.PULSE_Z / 360);
                if (modeCamera.Checked)
                {
                    PLCCommunicate(x, y, z);
                }
                //log.Invoke(new MethodInvoker(delegate ()
                //{
                //    log.Text = "X:" + x.ToString("F3") + " Y:" + y.ToString("F3") + " Angle:" + z.ToString();
                //}));
                iGray = iBgr.Convert<Gray, byte>();
                ComputerVison.RotationImage(ref iGray, Config.Parameter.ROTATION_CENTER, (float)result.ANGLE);
                cnt = ComputerVison.FindContours(iGray, Config.Parameter.THRESHOLD_VALUE);
                using (Image<Bgr, byte> iBgr2 = iGray.Convert<Bgr, byte>())
                {
                    if(cnt!= null)
                    {
                        RotatedRect r = CvInvoke.MinAreaRect(cnt);
                        CvInvoke.Rectangle(iBgr2, r.MinAreaRect(), new MCvScalar(0, 255, 0), 3);
                    }
                    if (modeCamera.Checked)
                    {
                        CvInvoke.Imwrite(@"backup\" + name + ".bmp", iBgr);
                        CvInvoke.Imwrite(@"backup_H\" + name + "trans.bmp", iBgr2);
                    }
                    p_imShow.Invoke(new MethodInvoker(delegate ()
                    {
                        p_imShow.Image = iBgr2.ToBitmap();
                    }));
                }
                cnt.Dispose();
            }
            else
            {
                using (Image<Bgr, byte> iBgr2 = iGray.Convert<Bgr, byte>())
                {
                    CvInvoke.Rectangle(iBgr2, ORGRec, new MCvScalar(0, 255, 0), 3);
                    p_imShow.Invoke(new MethodInvoker(delegate ()
                    {
                        p_imShow.Image = iBgr2.ToBitmap();
                    }));
                }
                DialogResult kq = MessageBox.Show("Not found label from images! You want to try again!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (kq == DialogResult.Yes)
                {
                    iGray.Dispose();
                    iBgr.Dispose();
                    Handling();
                }
                else
                {
                    result.ANGLE = 0;
                    result.X = 0;
                    result.Y = 0;
                }
            }
            iGray.Dispose();
            iBgr.Dispose();
        }
       
        private void Start_Click(object sender, EventArgs e)
        {
            if (this.Start.Text=="Start")
            {
                Start.Text = "Starting";
                Start.Enabled = false;
                this.Cursor = Cursors.WaitCursor;
                Start.Update();
                bool response = OpenDevice();
                if (response ==  true)
                {
                    G_setting.Enabled = false;
                    G_PLC.Enabled = false;
                    trackBar1.Enabled = false;
                    Run = true;
                    HBB = new Thread(new ThreadStart(ComputerVisionLabel));
                    HBB.Start();
                    this.Start.Text = "Stop";
                    Light_Mode(false);
                }
                else
                    Start.Text = "Start";
                this.Cursor = Cursors.Default;
                Start.Enabled = true;
            }
            else
            {
                Start.Enabled = false;
                this.Cursor = Cursors.WaitCursor;
                Release();
                this.Start.Text = "Start";
                Run = false;
                G_setting.Enabled = true;
                G_PLC.Enabled = true;
                trackBar1.Enabled = true;
                Light_Mode(true);
                this.Cursor = Cursors.Default;
                Start.Enabled = true;
            }
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (!Serial_Light.IsOpen)
                Serial_Light.Open();
            label9.Text = trackBar1.Value.ToString();
            byte[] a = { 0xab, 0xba, 0x03, 0x31, 0x00, 0x09 };
            a[5] = (byte)trackBar1.Value;
            Serial_Light.Write(a, 0, a.Length);
        }

        private void Light_Mode(bool state = false)
        {
            if (!Serial_Light.IsOpen)
                Serial_Light.Open();
            byte[] a = state ? new byte[] { 0xab, 0xba, 0x03, 0x32, 0x00, 0x01 } : new byte[] { 0xab, 0xba, 0x03, 0x32, 0x00, 0x00 };
            Serial_Light.Write(a, 0, a.Length);
        }

        private void Scan_Label(int index)
        {
            if (!Serial_Scanner.IsOpen)
                Serial_Scanner.Open();
            byte[] a = { 0x16, 0x54, 0x0d};
            Serial_Scanner.Write(a, 0, a.Length);
            List<char> recevie = new List<char>();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < 5000)
            {
                int r = Serial_Scanner.ReadChar();
                if (r == 0x0a)
                    break;
                else
                    recevie.Add((char)r);
            }
            sw.Stop();
            if(sw.ElapsedMilliseconds >5000)
            {
                MessageBox.Show("Scan Error");
            }
            else
            {
                string c = new string(recevie.ToArray());
                c = c.TrimEnd();
                LabelResut[index] = c;
                lb1.Invoke(new MethodInvoker(delegate ()
                {
                    lb1.Text = LabelResut[0];
                }));
                lb2.Invoke(new MethodInvoker(delegate ()
                {
                    lb2.Text = LabelResut[1];
                }));
                if (index == Scanner.Length - 1)
                    senIT();
                plc2.SetDevice(Scanner[index], 0);

            }
        }

        private void senIT()
        {
            Thread.Sleep(500);
            for(int i = 0; i < LabelResut.Length; i++)
            {
                LabelResut[i] = string.Empty;
            }
        }
        private void RecevieLabel()
        {

        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void rOIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateParameter();
            try
            {
                using (Mat Ori = CvInvoke.Imread(@"img\origin.bmp", Emgu.CV.CvEnum.ImreadModes.Grayscale))
                {
                    CvInvoke.Rectangle(Ori, Config.Parameter.ROI, new MCvScalar(255, 0, 255), 5);
                    p_imShow.Image = Ori.Bitmap;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void iChooseLink_Click(object sender, EventArgs e)
        {
            OpenFileDialog Folder = new OpenFileDialog();
            if(Folder.ShowDialog() == DialogResult.OK)
            {

                iLink.Text = linkimg = Folder.FileName;
                Handling();
            }
            Folder.Dispose();
        }

        private void modeCamera_CheckedChanged(object sender, EventArgs e)
        {
            iLink.Enabled = iChooseLink.Enabled = modeFolder.Checked;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult kq = MessageBox.Show("You want to exit?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                Release();
                Serial_Light.Close();
                Serial_Light.Dispose();
                Serial_Scanner.Close();
                Serial_Scanner.Dispose();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void cameraCaptureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p_imShow.Image = null;
            bool response = OpenCamera();
            if (response == true)
            {
                using (Image<Bgr, byte> iBgr = GetImage())
                {
                    p_imShow.Image = iBgr.ToBitmap();
                }
                Camera.CloseDevice();
            }
        }

        private void getOriginImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p_imShow.Image = null;
            UpdateParameter();
            bool response = OpenCamera();
            if (response == true)
            {
                using (Image<Bgr, byte> iBgr = GetImage())
                {
                    p_imShow.Image = iBgr.ToBitmap();
                    DialogResult kq = MessageBox.Show("You Sure create new origin image?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (kq == DialogResult.Yes)
                    {

                        CvInvoke.Imwrite(@"img\origin.bmp", iBgr);
                        Image<Gray, byte> iGray = ComputerVison.RoiImage(iBgr.Convert<Gray, byte>(), Config.Parameter.ROI);

                        using (VectorOfPoint cnt = ComputerVison.FindContours(iGray, Config.Parameter.THRESHOLD_VALUE))
                        {
                            RotatedRect a = CvInvoke.MinAreaRect(cnt);
                            ORGRec = a.MinAreaRect();
                            using (Image<Bgr, byte> iBgr2 = iGray.Convert<Bgr, byte>())
                            {
                                CvInvoke.Rectangle(iBgr2, ORGRec, new MCvScalar(0, 255, 0), 3);
                                CvInvoke.Imwrite("img\\originRoi.bmp", iBgr2);
                            }
                        }
                        iGray.Dispose();
                    }
                }
                Camera.CloseDevice();
            }
        }

        private void rotationCenterToolStripMenuItem_Click(object sender, EventArgs e)
        {
           UpdateParameter();
            try
            {
                using (Mat Ori = CvInvoke.Imread(@"img\origin.bmp", Emgu.CV.CvEnum.ImreadModes.Grayscale))
                {
                    CvInvoke.Circle(Ori, new Point(Config.Parameter.ROTATION_CENTER.X + Config.Parameter.ROI.X, Config.Parameter.ROTATION_CENTER.Y + Config.Parameter.ROI.Y), 10, new MCvScalar(0, 0, 0), 10);
                    p_imShow.Image = Ori.Bitmap;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getCenterRotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindCenter findcenter = new FindCenter(this);
            findcenter.Show();
        }
        private void SynchronizePLC()
        {
            string[,] Device1 = new string[,] { 
                {"M814","M114" },
                {"M802","M86" },
                {"M804","M84" }
            };
            string[,] Device2 = new string[,] {
                {"M115", "M810" },
                {"M90","M890" },
                {"M83","M803" },
                {"M516","M800" }
            };
            {
                short nRet;
                short nRet1 = 0;
                for (int i = 0; i < Device1.Length / 2; i++)
                {
                    if (!Run)
                        break;
                    nRet = plc.GetDevice(Device1[i, 0]);
                    if (nRet != -1)
                        nRet1 = plc2.SetDevice(Device1[i, 1], nRet);
                }
                for (int i = 0; i < Device2.Length / 2; i++)
                {
                    if (!Run)
                        break;
                    nRet = plc2.GetDevice(Device2[i, 0]);
                    if (nRet!= -1)
                        nRet1 = plc.SetDevice(Device2[i, 1], nRet);
                }
            }
        }
        public void UpdateParameter(bool write=false)
        {
            bool kq = !write;
            if(write == true)
            {
                kq = Config.Update(exposuretime: exposure_time.Text,
                        threshold: threshold_value.Text,
                        roi: roi_text.Text,
                        pulsex: pulse_x.Text,
                        pulsey: pulse_y.Text,
                        pulsez: pulse_z.Text,
                        Center: Center_text.Text,
                        Lightvalue: trackBar1.Value.ToString()
                );
            }
            this.exposure_time.Text = Config.save.ExposureTime;
            this.threshold_value.Text = Config.save.ThresholdValue;
            this.roi_text.Text = Config.save.Roi;
            this.Center_text.Text = Config.save.RotationCenter;
            this.pulse_x.Text = Config.save.PulseX;
            this.pulse_y.Text = Config.save.PulseY;
            this.pulse_z.Text = Config.save.PulseZ;
            this.trackBar1.Value = Config.Parameter.LightValue;
            trackBar1_Scroll(null, null);
        }
    }
    struct Origin
    {
        public Point PointA;
        public Point PointB;
        public Point PointO;
        public void SetPointA(Point A)
        {
            PointA = A;
        }
        public void SetPointB(Point B)
        {
            PointB = B;
        }
        public void SetPointO(Point O)
        {
            PointO = O;
        }
    }
    
    struct Response
    {
        public double X;
        public double Y;
        public double ANGLE;
    }
}
