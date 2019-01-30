using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
namespace ProxyKiller
{
    public partial class UploadAttendance : Form
    {
        public UploadAttendance(string subject)
        {
            InitializeComponent();
            label1.Text = "Subject : " + subject;
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            string filename = "C:\\ProxyKiller\\ProxyKiller\\ProxyKiller\\testPicture\\test.jpg";
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if(file.ShowDialog()==DialogResult.OK)
            {
                pictureBox2.ImageLocation = file.FileName;
            }
            else
            {
                return;
            }

            File.Copy(pictureBox2.ImageLocation, filename, true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(pictureBox2.ImageLocation == "C:\\ProxyKiller\\ProxyKiller\\ProxyKiller\\PicturesUsed\\upload.png")
            {
                MessageBox.Show("Please upload a image");
                return;
            }
            else
            {
                var result = MessageBox.Show("Are you sure with the image?", "upload", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result == DialogResult.Yes)
                {
                    MessageBox.Show("Picture uploaded sucessfully");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"C:\Program Files (x86)\Python37-32\python.exe";//cmd is full path to python.exe
            start.Arguments = @"F:\pyMongo.py";//args is path to .py file and any cmd line args
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    Console.Write(result);
                }
            }
        }
    }
}
