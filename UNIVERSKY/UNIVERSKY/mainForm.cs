using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;
using System.Net.Sockets;

using Emgu.CV;
using Emgu.CV.Cvb;
using Emgu.CV.UI;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.VideoSurveillance;
using Emgu.Util;
using Emgu.CV.Cuda;
using Emgu.CV.Features2D;
using System.Threading;

namespace UNIVERSKY
{
    public partial class mainForm : Form
    {
        #region 支持改变窗体大小
        private const int Guying_HTLEFT = 10;
        private const int Guying_HTRIGHT = 11;
        private const int Guying_HTTOP = 12;
        private const int Guying_HTTOPLEFT = 13;
        private const int Guying_HTTOPRIGHT = 14;
        private const int Guying_HTBOTTOM = 15;
        private const int Guying_HTBOTTOMLEFT = 0x10;
        private const int Guying_HTBOTTOMRIGHT = 17;
        // 窗体大小改变
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0084:
                    base.WndProc(ref m);
                    Point vPoint = new Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (vPoint.X <= 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)Guying_HTTOPLEFT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)Guying_HTBOTTOMLEFT;
                        else
                            m.Result = (IntPtr)Guying_HTLEFT;
                    else if (vPoint.X >= ClientSize.Width - 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)Guying_HTTOPRIGHT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)Guying_HTBOTTOMRIGHT;
                        else
                            m.Result = (IntPtr)Guying_HTRIGHT;
                    else if (vPoint.Y <= 5)
                        m.Result = (IntPtr)Guying_HTTOP;
                    else if (vPoint.Y >= ClientSize.Height - 5)
                        m.Result = (IntPtr)Guying_HTBOTTOM;
                    break;
                case 0x0201://鼠标左键按下的消息
                    m.Msg = 0x00A1;//更改消息为非客户区按下鼠标
                    m.LParam = IntPtr.Zero; //默认值
                    m.WParam = new IntPtr(2);//鼠标放在标题栏内
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        #endregion
        public mainForm()
        {
            InitializeComponent();
            //Model3D = new Unity3D(panel1, "");
            //Model3D.Start("E:\\项目\\UNIVERSEA\\UNIVERSKY\\UNIVERSKY_MISSION\\UNIVERSKY\\UNIVERSKY\\Unity3D\\universky.exe");
            for (int i=0;i<200;i++)
            {
                RollWave.Series[0].Points.Add(SkyLink.Roll);
                PitchWave.Series[0].Points.Add(SkyLink.Pitch);
                YawWave.Series[0].Points.Add(SkyLink.Yaw);
                HeightWave.Series[0].Points.Add(4);
            }
            SerialInit();
            SensorsWave_Init();
        }

        private void LoginClose_Click(object sender, EventArgs e)
        {
            //Model3D.Stop();
            this.Close();
        }

        private void LoginMin_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        private void LoginClose_MouseHover(object sender, EventArgs e)
        {
            LoginClose.Image = Properties.Resources.关闭2;
        }

        private void LoginClose_MouseLeave(object sender, EventArgs e)
        {
            LoginClose.Image = Properties.Resources.关闭;
        }

        private void LoginMin_MouseHover(object sender, EventArgs e)
        {
            LoginMin.Image = Properties.Resources.最小化2;
        }

        private void LoginMin_MouseLeave(object sender, EventArgs e)
        {
            LoginMin.Image = Properties.Resources.最小化;
        }
        private void Title_Panel_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
        Unity3D Model3D = null;//Unity3D 类
        // 窗口 大小化
        bool Window_State = false;
        private void WindowMaxMin_Click(object sender, EventArgs e)
        {
            Window_State = !Window_State;
            if (Window_State)
            {
                this.WindowState = FormWindowState.Maximized;
                WindowMaxMin.Image = Properties.Resources.窗口化;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                WindowMaxMin.Image = Properties.Resources.最大化;
            }
        }

        private void 主页按钮_Click(object sender, EventArgs e)
        {
            mainPages.SelectedIndex = 0;
            
        }

        private void 航线按钮_Click(object sender, EventArgs e)
        {
            mainPages.SelectedIndex = 1;
        }

        private void 编队按钮_Click(object sender, EventArgs e)
        {
            mainPages.SelectedIndex = 2;
        }

        private void 波形按钮_Click(object sender, EventArgs e)
        {
            mainPages.SelectedIndex = 3;
        }

        private void 视觉按钮_Click(object sender, EventArgs e)
        {
            mainPages.SelectedIndex = 4;
        }
        private void 参数按钮_Click(object sender, EventArgs e)
        {
            mainPages.SelectedIndex = 5;
        }
        private void 设置按钮_Click(object sender, EventArgs e)
        {
            mainPages.SelectedIndex = 6;
        }

        private static VideoCapture _cameraCapture;
        private void videoOpen_Click(object sender, EventArgs e)
        {
            Run();
        }

        void Run()
        {
            try
            {
                _cameraCapture = new VideoCapture();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
            //_disOpticalFlow = new DISOpticalFlow();
            Application.Idle += ProcessFrame;
        }

        Mat curgray = new Mat();    // 当前图片  
        Mat pregray = new Mat();    // 预测图片 
        Mat Flow = new Mat();    // 光流图片
        Mat KeyPointPic = new Mat();
        VectorOfKeyPoint feature = new VectorOfKeyPoint();// 检测的特征  
        VectorOfPointF[] points = new VectorOfPointF[2]; //point0为特征点的原来位置，point1为特征点的新位置
        PointF[] currFeature = new PointF[2];//point0为特征点的原来位置
        PointF[] prevFeature = new PointF[2];//point1为特征点的新位置
        PointF[] initial = new PointF[2];    // 初始化跟踪点的位置
        int AddNewPoint = 0;
        VectorOfPointF initPoint = new VectorOfPointF();    // 初始化跟踪点的位置  
        int maxCount = 266;     // 检测的最大特征数  
        double qLevel = 0.01;   // 特征检测的等级  
        double minDist = 10.0;  // 两特征点之间的最小距离  

        byte[] status; // 跟踪特征的状态，特征的流发现为1，否则为0  
        float[] err;

        private GFTTDetector goodFeaturesToTrack;
        void ProcessFrame(object sender, EventArgs e)
        {

            Mat frame = _cameraCapture.QueryFrame();
            Mat smoothedFrame = new Mat();
            CvInvoke.GaussianBlur(frame, smoothedFrame, new Size(3, 3), 1);//高斯滤波    
            CvInvoke.CvtColor(frame, curgray, ColorConversion.Bgr2Gray);//灰度图
            goodFeaturesToTrack = new GFTTDetector(maxCount, qLevel, minDist);//关键点检测初始化
            frame.CopyTo(KeyPointPic);

            MKeyPoint[] keypoint = goodFeaturesToTrack.Detect(curgray);//关键点检测
            for (int i = 0; i < keypoint.Count(); i++)
            {
                System.Drawing.Point point = System.Drawing.Point.Truncate(keypoint[i].Point);//获得关键点的坐标位置，以 Point 类型。
                CvInvoke.Circle(KeyPointPic, point, 3, new MCvScalar(0, 0, 255), 1);
            }

            if (prevFeature.Count() < 10) //特征点太少了，重新检测特征点  
            {
                MKeyPoint[] keypoints = goodFeaturesToTrack.Detect(curgray);//关键点检测
                AddNewPoint = keypoints.Count();
                Array.Resize(ref prevFeature, keypoints.Count());
                Array.Resize(ref initial, keypoints.Count());
                for (int i = 0; i < keypoints.Count(); i++)
                {
                    System.Drawing.Point point = System.Drawing.Point.Truncate(keypoints[i].Point);//获得关键点的坐标位置，以 Point 类型。
                    prevFeature[i] = point;
                    initial[i] = point;
                    CvInvoke.Circle(curgray, point, 3, new MCvScalar(0, 0, 255), 1);
                }
            }
            if (pregray.Size.IsEmpty) curgray.CopyTo(pregray);//第一帧

            MCvTermCriteria termcrit = new MCvTermCriteria(6);
            CvInvoke.CalcOpticalFlowPyrLK(pregray, curgray, prevFeature, curgray.Size, 2, termcrit, out currFeature, out status, out err, 0, 0.0001);
            AddNewPoint = prevFeature.Count();
            // 去掉一些不好的特征点
            int k = 0;
            for (int i = 0; i < currFeature.Count(); i++)
            {
                try
                {
                    if (acceptTrackedPoint(i))
                    {
                        initial[k] = initial[i];
                        currFeature[k++] = currFeature[i];
                    }
                }
                catch { }
            }

            Array.Resize(ref currFeature, k);
            Array.Resize(ref initial, k);

            frame.CopyTo(Flow);
            for (int i = 0; i < currFeature.Count(); i++)
            {
                //CvInvoke.Circle(Flow, Point.Truncate(currFeature[i]), 3, new MCvScalar(0, 0, 255),1);
                CvInvoke.Line(Flow, Point.Truncate(initial[i]), Point.Truncate(currFeature[i]), new Bgr(Color.DarkOrange).MCvScalar, 2);
            }



            imageBox1.Image = frame;
            imageBox2.Image = KeyPointPic;
            imageBox3.Image = Flow;

            curgray.CopyTo(pregray);
            Array.Resize(ref prevFeature, currFeature.Count());
            for (int i = 0; i < currFeature.Count(); i++)
            {
                prevFeature[i] = currFeature[i];
            }
            //Thread t = new Thread(() =>
            //{
            //    this.mainPages.Invoke(new Action(delegate ()
            //    {


            //    }));
            //});
            //t.Start();
        }

        //  检测新点是否应该被添加
        // return: 是否被添加标志
        bool addNewPoints()
        {
            return prevFeature.Count() <= 10;
        }
        //决定哪些跟踪点被接受
        bool acceptTrackedPoint(int i)
        {
            return ((status[i] > 0) && ((Math.Abs(prevFeature[i].X - currFeature[i].X) + Math.Abs(prevFeature[i].Y - currFeature[i].Y)) > 2));
        }

    }
}
