﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.Util;
using Emgu.CV.Structure;

namespace Main
{
    public partial class FindCenter : Form
    {
        Main mOriFromMain;
        Rectangle roi = new Rectangle(670, 330, 1915, 1457);
        public FindCenter(Main main)
        {
            mOriFromMain = main;
            InitializeComponent();
        }

        private void FindCenter_Load(object sender, EventArgs e)
        {

        }
        private phuongtrinhduongthang giahephuongtrinh(Point point1, Point point2)
        {
            phuongtrinhduongthang result = new phuongtrinhduongthang();
            /*
            y1 = ax1 + b
            y2 = ax2 + b
            => a = (y1-y2)/(x1-x2)
            => b = y1/a*x1
            
            */
            result.a = (double)(point1.Y - point2.Y) / (point1.X - point2.X);
            result.b = (double)point2.Y - (result.a * point2.X);
            // convert to ax+by+c = 0
            return result;
        }
        private void phuongtrinhvuonggoc(ref phuongtrinhduongthang dtvg, Point mid)
        {
            dtvg.a = -1 / dtvg.a;
            // y = ax+ b
            dtvg.b = mid.Y - dtvg.a * mid.X;
        }
        private Point GetCenter(phuongtrinhduongthang dt1,phuongtrinhduongthang dt2)
        {
            Point center = new Point();
            /*
            y = a1x + b1 => x = (b2-b1)/ (a1-a2)
            y = a2x + b2

            */
            center.X = (int)((dt2.b - dt1.b) / (dt1.a - dt2.a));
            center.Y = (int)( dt2.a * center.X + dt2.b );



            return center;
        }
        private Point CalculatorCenter(Point cRc1,Point cRc2,Point cRc3)
        {
            
            Point midPoint1 = new Point((int)(cRc1.X + cRc2.X) / 2, (int)(cRc1.Y + cRc2.Y) / 2);
            Point midPoint2 = new Point((int)(cRc3.X + cRc2.X) / 2, (int)(cRc3.Y + cRc2.Y) / 2);
            // y = ax+b
            phuongtrinhduongthang dt1 = giahephuongtrinh(cRc1,cRc2);
            phuongtrinhduongthang dt2 = giahephuongtrinh(cRc2,cRc3);
            phuongtrinhvuonggoc(ref dt1, midPoint1);
            phuongtrinhvuonggoc(ref dt2, midPoint2);
            Point result =  GetCenter(dt1, dt2);
            mOriFromMain.Center_text.Text = (result.X+roi.X).ToString() + "," + (result.Y+roi.Y).ToString();
            return result;
        }
        private Point FindcRect(Image<Gray, byte> img, int thresh_value)
        {
            Point cRect;
            using (VectorOfPoint cnt = ComputerVison.FindContours(img, thresh_value))
            {
                RotatedRect rec1 = CvInvoke.MinAreaRect(cnt);
                PointF cRec1F = rec1.Center;
                cRect = Point.Round(cRec1F);
                CvInvoke.Circle(img, cRect, 3, new MCvScalar(0, 0, 0), 3);
            }
            return cRect;
        }
        private void Get_Center_Click(object sender, EventArgs e)
        {
            using (Mat img1 = CvInvoke.Imread(@"E:\THIEU-FII\Projects\Locate_Label_Assembly_Machine\Main\bin\Debug\img\center\1.bmp", Emgu.CV.CvEnum.ImreadModes.Grayscale))
            {
                using (Mat img2 = CvInvoke.Imread(@"E:\THIEU-FII\Projects\Locate_Label_Assembly_Machine\Main\bin\Debug\img\center\2.bmp", Emgu.CV.CvEnum.ImreadModes.Grayscale))
                {
                    using (Mat img3 = CvInvoke.Imread(@"E:\THIEU-FII\Projects\Locate_Label_Assembly_Machine\Main\bin\Debug\img\center\3.bmp", Emgu.CV.CvEnum.ImreadModes.Grayscale))
                    {
                        Image<Gray, byte> iGray1 = img1.ToImage<Gray, byte>();
                        Image<Gray, byte> iGray2 = img2.ToImage<Gray, byte>();
                        Image<Gray, byte> iGray3 = img3.ToImage<Gray, byte>();
                        Point cRec1, cRec2, cRec3;
                        
                        iGray1 = ComputerVison.RoiImage(iGray1, roi);
                        iGray2 = ComputerVison.RoiImage(iGray2, roi);
                        iGray3 = ComputerVison.RoiImage(iGray3, roi);

                        cRec1 = FindcRect(iGray1, 150);
                        cRec2 = FindcRect(iGray2, 150);
                        cRec3 = FindcRect(iGray3, 150);
                        Point midPoint1 = new Point((int)(cRec1.X + cRec2.X) / 2, (int)(cRec1.Y + cRec2.Y) / 2);
                        Point midPoint2 = new Point((int)(cRec3.X + cRec2.X) / 2, (int)(cRec3.Y + cRec2.Y) / 2);
                        Point result =  CalculatorCenter(cRec1, cRec2, cRec3);
                        CvInvoke.Circle(iGray3, result, 5 , new MCvScalar(255, 0, 255), 5);
                        pictureBox1.Image = iGray3.ToBitmap();
                        iGray1.Dispose();
                        iGray2.Dispose();
                        iGray3.Dispose();
                    }
                }
            }
            //this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    struct phuongtrinhduongthang
    {
        public double a;
        public double b;
    }
}
