using Emgu.CV;
using Emgu.CV.Structure;

//using OpenCvSharp;
//using OpenCvSharp.Extensions;
using System.CodeDom.Compiler;
namespace WinFormsApp1
{
    
    public partial class Form1 : Form
    {
        private Bitmap? img;
        public Form1()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            openFileDialog1.ShowDialog();

            var file = openFileDialog1.FileName;
            if (file != null)
            {
                img = new Bitmap(file);
                pictureBox1.Image = img;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (img != null)
            {
                Thread[] boxThreads = new Thread[4];
                boxThreads[0] = new Thread(() =>
                {
                    pictureBox2.Image = Edging(img);
                });
                boxThreads[1] = new Thread(() =>
                {
                    pictureBox3.Image = Graying(img);
                });
                boxThreads[2] = new Thread(() =>
                {
                    pictureBox4.Image = BlueToPink(img);
                });
                boxThreads[3] = new Thread(() =>
                {
                    pictureBox5.Image = GreenMask(img);
                });

                foreach (Thread t in boxThreads) t.Start();
                foreach (Thread t in boxThreads) t.Join();
            }
        }

        public Bitmap Edging(Bitmap img)
        {
            Mat pic = new Mat();
            lock (img)
            {
                pic = img.ToMat();
            }
            Mat gaussianBlur = new Emgu.CV.Mat();
            Mat sobelXy = new Emgu.CV.Mat();


            CvInvoke.GaussianBlur(pic, gaussianBlur, new System.Drawing.Size(3, 3), 5.0);
            CvInvoke.Sobel(gaussianBlur, sobelXy, Emgu.CV.CvEnum.DepthType.Default, 1, 1, 5);

            return sobelXy.ToBitmap();
        }

        public Bitmap Graying(Bitmap img)
        {
            Mat pic = new Mat();
            lock (img)
            {
                pic = img.ToMat();
            }
            Image<Gray, byte> convertPic = pic.ToImage<Gray, byte>();
            return convertPic.ToBitmap();
        }
        
        public Bitmap GreenMask(Bitmap img)
        {
            Mat pic = new Mat();
            lock (img)
            {
                pic = img.ToMat();
            }
            Image<Bgr, byte> convertPic = pic.ToImage<Bgr, byte>();
            
            for (int i = 0; i < convertPic.Rows; i++)
            {
                for (int j = 0; j < convertPic.Cols; j++)
                {
                    convertPic[i, j] = new Bgr(convertPic[i, j].MCvScalar.V0 - 100, convertPic[i, j].MCvScalar.V1 + 100, convertPic[i, j].MCvScalar.V0 - 100);
                }
            }
            return convertPic.ToBitmap();
        }

        public Bitmap BlueToPink(Bitmap img)
        {
            Mat pic = new Mat();
            lock (img)
            {
                pic = img.ToMat();
            }
            Image<Bgr, byte> convertPic = pic.ToImage<Bgr, byte>();
            var image = convertPic.InRange(new Bgr(30, 0, 0), new Bgr(255, 255, 190));
            for (int i = 0; i < image.Rows; i++)
            {
                for (int j = 0; j < image.Cols; j++)
                {
                    var num = image[i, j];

                    if (num.Intensity > 0)
                    {
                        convertPic[i, j] = new Bgr(convertPic[i, j].MCvScalar.V0, convertPic[i, j].MCvScalar.V1, convertPic[i, j].MCvScalar.V0);
                    }
                }
            }
            return convertPic.ToBitmap();
        }
    }
}
