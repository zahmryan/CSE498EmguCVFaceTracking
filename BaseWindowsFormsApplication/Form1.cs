using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using Accord.Vision.Detection;
using Accord.Vision.Detection.Cascades;
using Accord.Imaging.Filters;
using AForge.Imaging;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.UI;

namespace BaseWindowsFormsApplication
{
    public partial class Form1 : Form
    {
        private Capture cap;
        private HaarCascade haar;
        HaarObjectDetector detector;

        public Form1()
        {
            InitializeComponent();
        }

        PictureBox pictureBox1 = new PictureBox();
        ImageViewer imageViewer = new ImageViewer();

        private void timer1_Tick(object sender, EventArgs e)
        {
            Mat frame = new Mat();
            frame = cap.QueryFrame();
            Image<Bgr, byte> frameImage = frame.ToImage<Bgr, byte>();
            Image<Gray, byte> grayFrameImage = frameImage.Convert<Gray, byte>();

            if(frame != null){
                Rectangle[] faces = detector.ProcessFrame(grayFrameImage.ToBitmap());
                RectanglesMarker marker = new RectanglesMarker(faces, Color.Fuchsia);
                imageViewer.Image = new Image<Bgr, byte>(marker.Apply(frame.Bitmap));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OpenFileDialog OF = new OpenFileDialog();
            PictureBox pictureBox1 = new PictureBox();
            haar = new FaceHaarCascade();
            detector = new HaarObjectDetector(haar, 30);
            Timer timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            cap = new Capture("samplemp4.mp4");
            timer1.Start();
            imageViewer.ShowDialog();
        }
    }
}
