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
                pictureBox2.Image = img;
                pictureBox3.Image = img;
                pictureBox4.Image = img;
                pictureBox5.Image = img;
            }
        }
    }
}
