using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.Structure;
using System.Threading;
using System.Net.Sockets;
namespace Main
{
    public partial class Main : Form
    {
        
        //------------ PARAMETER VARIABLE -----------
        Origin ORG = new Origin();
        Setting Parameter = new Setting();
        Rectangle ORGRec = new Rectangle();

        HIKVision Camera = new HIKVision();
        private Thread HBB;
        private bool Run = false;
        PLCController.PLCController plc = new PLCController.PLCController("192.168.3.250",1996);


        // ---------- PLC -------------
        string PLC_Flag = "M96", PLC_PulseX = "D96", PLC_PulseY = "D98", PLC_PulseZ = "D100",
            PLC_PulseXH = "D97", PLC_PulseYH = "D99", PLC_PulseZH = "D101";


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
                    _img = ComputerVison.RoiImage(_img, Parameter.ROI);
                    VectorOfPoint cnt = new VectorOfPoint();
                    cnt = ComputerVison.FindContours(_img, Parameter.THRESHOLD_VALUE);
                    RotatedRect a = CvInvoke.MinAreaRect(cnt);
                    ORGRec = a.MinAreaRect();
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
            Parameter.SetSetting(exposure_time.Text, threshold_value.Text, roi_text.Text,
                                                 pulse_x.Text, pulse_y.Text, pulse_z.Text, Center_text.Text);

            if (Serial_Light.IsOpen == false)
                Serial_Light.Open();
            trackBar1_Scroll(null, null);
            Thread a = new Thread(new ThreadStart(fixbug));
            a.Start();
        }
        private void Release()
        {
            Camera.CloseDevice();
            plc.Close();
            Run = false;
        }
        private bool OpenDevice()
        {
            bool response = Parameter.SetSetting(exposure_time.Text, threshold_value.Text, roi_text.Text,
                                                pulse_x.Text, pulse_y.Text, pulse_z.Text, Center_text.Text);
            if (!Serial_Light.IsOpen)
            {
                Serial_Light.Open();
            }
            if (response == true)
            {
                ORG.SetPointO(Parameter.ROTATION_CENTER);
                GetOriginImage();
                response = OpenCamera();
                if (response == true)
                {
                    try
                    {
                        plc.Open();
                        return response;
                    }
                    catch (SocketException er)
                    {
                        Camera.CloseDevice();
                        response = false;
                        MessageBox.Show("[" + er.ErrorCode + "]" + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Can't open Camera!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            return response;

        }
        private Image<Bgr, byte> GetImage()
        {
            Image<Bgr, byte> img;
            using (Bitmap Cap = Camera.GetFrame())
            {
                img = new Image<Bgr, byte>(Cap);
            }
            return img;
        }

        private void ComputerVisionLabel()
        {
            int Flag_Ready = 1;
            while(true)
            {
                if (Run == false)
                    break;
                else
                {
                    int nRet = plc.GetDevice(PLC_Flag);
                    if (nRet == Flag_Ready)
                    {
                        Response result = Handling();
                        Light_Mode(false);
                        short y = (short) Math.Round(result.X * Parameter.PULSE_Y) ;
                        short x = (short)Math.Round(result.Y * Parameter.PULSE_X);
                        short z = (short)Math.Round(-result.ANGLE * Parameter.PULSE_Z/360);
                        PLCCommunicate(x, y, z);
                        log.Invoke(new MethodInvoker(delegate ()
                        {
                            log.Text = "X:" + x.ToString("F3") + " Y:" + y.ToString("F3") + " Angle:" + z.ToString();
                        }));
                    }
                }
                Thread.Sleep(100);
            }
        }
        private void PLCCommunicate(short x, short y, short z)
        {
            if(x<0)
                plc.SetDevice(PLC_PulseXH,-1);
            else
                plc.SetDevice(PLC_PulseXH, 0);
            Thread.Sleep(1);
            plc.SetDevice(PLC_PulseX, x);
            Thread.Sleep(1);
            if (y < 0)
                plc.SetDevice(PLC_PulseYH, -1);
            else
                plc.SetDevice(PLC_PulseYH, 0);
            Thread.Sleep(1);
            
            plc.SetDevice(PLC_PulseY, y);
            Thread.Sleep(1);
            if (z < 0)
                plc.SetDevice(PLC_PulseZH, -1);
            else
                plc.SetDevice(PLC_PulseZH, 0);
            Thread.Sleep(1);
            plc.SetDevice(PLC_PulseZ, z);
            Thread.Sleep(1);
            plc.SetDevice(PLC_Flag, 0);
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label9.Text = trackBar1.Value.ToString();
            byte[] a = { 0xab, 0xba, 0x03, 0x31, 0x00, 0x09 };
            a[5] = (byte)trackBar1.Value;
            Serial_Light.Write(a, 0, a.Length);
        }

        private void Light_Mode(bool state = false)
        {
            byte[] a = state ?  new byte[]{ 0xab, 0xba, 0x03, 0x32, 0x00, 0x01 } : new byte[]  { 0xab, 0xba, 0x03, 0x32, 0x00, 0x00 };
            Serial_Light.Write(a, 0, a.Length);
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void rOIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Parameter.SetSetting(exposure_time.Text, threshold_value.Text, roi_text.Text,
                                                 pulse_x.Text, pulse_y.Text, pulse_z.Text, Center_text.Text);
            try
            {
                using (Mat Ori = CvInvoke.Imread(@"img\origin.bmp", Emgu.CV.CvEnum.ImreadModes.Grayscale))
                {
                    CvInvoke.Rectangle(Ori, Parameter.ROI, new MCvScalar(255, 0, 255), 5);
                    p_imShow.Image = Ori.Bitmap;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult kq = MessageBox.Show("You want to exit?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                Release();
                Serial_Light.Close();
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
            Parameter.SetSetting(exposure_time.Text, threshold_value.Text, roi_text.Text,
                                                 pulse_x.Text, pulse_y.Text, pulse_z.Text, Center_text.Text);
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
                        Image<Gray, byte> iGray = ComputerVison.RoiImage(iBgr.Convert<Gray, byte>(), Parameter.ROI);

                        using (VectorOfPoint cnt = ComputerVison.FindContours(iGray, Parameter.THRESHOLD_VALUE))
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
            Parameter.SetSetting(exposure_time.Text, threshold_value.Text, roi_text.Text,
                                                 pulse_x.Text, pulse_y.Text, pulse_z.Text, Center_text.Text);
            try
            {
                using (Mat Ori = CvInvoke.Imread(@"img\origin.bmp", Emgu.CV.CvEnum.ImreadModes.Grayscale))
                {
                    CvInvoke.Circle(Ori, new Point(Parameter.ROTATION_CENTER.X + Parameter.ROI.X, Parameter.ROTATION_CENTER.Y + Parameter.ROI.Y), 10, new MCvScalar(0, 0, 0), 10);
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



        private Response Handling()
        {
            Response result = new Response();
            string name = DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString();
            Light_Mode(true);
            Thread.Sleep(500);
            Image<Bgr, byte> iBgr = GetImage();
            Light_Mode(false);
            CvInvoke.Imwrite(@"backup\" + name + ".bmp", iBgr);
            Image<Gray, byte> iGray = iBgr.Convert<Gray, byte>();
            iGray = ComputerVison.RoiImage(iGray, Parameter.ROI);
            VectorOfPoint cnt = new VectorOfPoint();
            cnt = ComputerVison.FindContours(iGray, Parameter.THRESHOLD_VALUE);
            Point[] p = ComputerVison.Search2Tip(cnt);
            CvInvoke.Line(iBgr, p[0], p[1], new MCvScalar(0, 255, 0), 3);
            CvInvoke.Line(iBgr, ORG.PointA, ORG.PointB, new MCvScalar(0, 0, 255), 3);

            ComputerVison.Calculator(ref result, ORG.PointA, ORG.PointB, p[0], p[1], Parameter.LabelSize.Height,false);
            ComputerVison.RotationImage(ref iGray, ORG.PointO, (float)result.ANGLE);
            cnt = ComputerVison.FindContours(iGray, Parameter.THRESHOLD_VALUE);
            p = ComputerVison.Search2Tip(cnt);
            ComputerVison.Calculator(ref result, ORG.PointA, ORG.PointB, p[0], p[1], Parameter.LabelSize.Height,true);
            double Pixel_Per_Mm = Math.Abs(p[0].Y - p[1].Y) / Parameter.LabelSize.Width;
            ComputerVison.TransformImage(ref iGray, -(int)(result.X*Pixel_Per_Mm),-(int)(result.Y*Pixel_Per_Mm));
            cnt = ComputerVison.FindContours(iGray, Parameter.THRESHOLD_VALUE);
            using (Image<Bgr, byte> iBgr2 = iGray.Convert<Bgr, byte>())
            {
                RotatedRect r = CvInvoke.MinAreaRect(cnt);
                
                CvInvoke.Rectangle(iBgr2, r.MinAreaRect(), new MCvScalar(0, 0, 255), 3);
                CvInvoke.Rectangle(iBgr2, ORGRec, new MCvScalar(0, 255, 0), 3);
                p_imShow.Invoke(new MethodInvoker(delegate ()
                {
                    p_imShow.Image = iBgr2.ToBitmap();
                }));
                CvInvoke.Imwrite(@"backup_H\" + name + "trans.bmp", iBgr2);
            }
           
            
            cnt.Dispose();
            iGray.Dispose();
            iBgr.Dispose();
            return result;
        }
       
        void fixbug()
        {
            try
            {
                plc.Open();
            }
            catch (Exception)
            {

            }
        }
        private void Start_Click(object sender, EventArgs e)
        {
            
            if (this.Start.Text=="Start")
            {
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
            }
            else
            {
                Release();
                log.Text = "Reddy";
                this.Start.Text = "Start";
                Run = false;
                G_setting.Enabled = true;
                G_PLC.Enabled = true;
                trackBar1.Enabled = true;
                Light_Mode(true);
            }
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
    struct Setting
    {
        public int THRESHOLD_VALUE;
        public Rectangle ROI;
        public int EXPOSURE_TIME;
        public int PULSE_X;
        public int PULSE_Y;
        public int PULSE_Z;
        public Rectangle LabelSize;
        public Point ROTATION_CENTER;
        public bool SetSetting(string exposuretime, string threshold, string roi, string pulsex, string pulsey, string pulsez,string Center)
        {
            try
            {
                THRESHOLD_VALUE = Convert.ToInt16(threshold);
                EXPOSURE_TIME = Convert.ToInt16(exposuretime);
                PULSE_X = Convert.ToInt32(pulsex);
                PULSE_Y = Convert.ToInt32(pulsey);
                PULSE_Z = Convert.ToInt32(pulsez);
                string split = null;
                int count = 1;
                for (int i=0;i < roi.Length;i++)
                {
                    char item = roi[i];
                    if (item.ToString() == "," || i == roi.Length-1)
                    {
                        switch (count)
                        {
                            case 1:
                            {
                                ROI.X = Convert.ToInt16(split);
                                break;
                            }
                            case 2:
                            {
                                ROI.Y = Convert.ToInt16(split);
                                break;
                            }
                            case 3:
                            {
                                ROI.Width = Convert.ToInt16(split);
                                break;
                            }
                            default:
                            {
                                split += item.ToString();
                                ROI.Height = Convert.ToInt16(split);
                                break;
                            }
                        }
                        count++;
                        split = null;
                    }
                    else
                    {
                        split += item.ToString();
                    }
                }
                count = 1;
                for (int i = 0; i < Center.Length; i++)
                {
                    if (Center[i].ToString() == "," || i == Center.Length - 1)
                    {
                        switch (count)
                        {
                            case 1:
                                {
                                    ROTATION_CENTER.X = Convert.ToInt16(split);
                                    ROTATION_CENTER.X -= ROI.X;
                                    break;
                                }
                            default:
                                {
                                    split += Center[i].ToString();
                                    ROTATION_CENTER.Y = Convert.ToInt16(split);
                                    ROTATION_CENTER.Y -= ROI.Y;
                                    break;
                                }
                        }
                        count++;
                        split = null;
                    }
                    else
                        split += Center[i].ToString();
                }
                LabelSize = new Rectangle(0, 0, 50, 20);
                return true;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
    struct Response
    {
        public double X;
        public double Y;
        public double ANGLE;
        public double DEVIATION;
    }


}
