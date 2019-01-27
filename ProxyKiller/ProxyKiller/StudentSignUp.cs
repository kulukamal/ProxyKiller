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
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;

namespace ProxyKiller
{
    public partial class StudentSignUp : Form
    {
        StudentInfo student;
        LoginCredential user;
        StudentPicture picture;
        string defaultImage = @"C:\ProxyKiller\ProxyKiller\ProxyKiller\PicturesUsed\male.jpg";
        static MongoClient client;
        static IMongoDatabase db;
        static IMongoCollection<StudentInfo> studentInfo;
        static IMongoCollection<LoginCredential> loginCredential;
        static IMongoCollection<StudentPicture> studentPicture;
        public StudentSignUp()
        {
            InitializeComponent();
            student = new StudentInfo();
            user = new LoginCredential();
            picture = new StudentPicture();
            pictureBox1.ImageLocation = defaultImage;
            pictureBox2.ImageLocation = defaultImage;
            pictureBox3.ImageLocation = defaultImage;
            pictureBox4.ImageLocation = defaultImage;
        }
        static StudentSignUp()
        {
            client = new MongoClient();
            db = client.GetDatabase("ProxyKiller");
            studentInfo = db.GetCollection<StudentInfo>("students");
            loginCredential = db.GetCollection<LoginCredential>("loginCredential");
            studentPicture = db.GetCollection<StudentPicture>("studentPicture");
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog img = new OpenFileDialog();
            img.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if(img.ShowDialog()==DialogResult.OK)
            {
                picture.ImageLocations[0]= img.FileName;
                pictureBox1.ImageLocation = img.FileName;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach(Control c in Controls)
            {
                if(c is TextBox)
                {
                    if(c.Text.Length == 0)
                    {
                        MessageBox.Show("All feilds are mandatory");
                        return;
                    }
                }
            }
            foreach(PictureBox c in Controls.OfType<PictureBox>())
            {
                if(c is PictureBox)
                {
                    if(c.ImageLocation == defaultImage)
                    {
                        MessageBox.Show("all images are mandatory");
                        return;
                    }
                }
            }
            
            student.Name = textBox2.Text;
            student.Type = "student";
            student.MobileNo = textBox3.Text;

            if(textBox4.Text!=textBox5.Text)
            {
                MessageBox.Show("Password not mathcing!");
                return;
            }
            if(textBox4.Text.Length<8)
            {
                MessageBox.Show("Password length must be of 8 or more!");
                return;
            }
            if(loginCredential.Find(n=>n.UserName == textBox1.Text).CountDocuments()!=0)
            {
                MessageBox.Show("Username already exists!");
                return;
            }
            student.Gender = Controls.OfType<RadioButton>().First().Text;
            
            picture.UserName = user.UserName = student.UserName = textBox1.Text;
            user.Password = textBox4.Text.GetHashCode();
            Directory.CreateDirectory(String.Format(@"C:\ProxyKiller\ProxyKiller\ProxyKiller\userPictures\{0}", user.UserName));
            for (int i = 0; i < 4; i++)
            {
                string filename = String.Format(@"C:\ProxyKiller\ProxyKiller\ProxyKiller\userPictures\{0}\{1}{2}.jpg", user.UserName, user.UserName,i);
                File.Copy(picture.ImageLocations[i], filename, true);
                picture.ImageLocations[i] = filename;
            }
            
            studentInfo.InsertOne(student);
            loginCredential.InsertOne(user);
            studentPicture.InsertOne(picture);

            StudentForm s = new StudentForm(user.UserName);
            this.Hide();
            s.Show();
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog img = new OpenFileDialog();
            img.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (img.ShowDialog() == DialogResult.OK)
            {
                picture.ImageLocations[1] = img.FileName;
                pictureBox2.ImageLocation = img.FileName;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog img = new OpenFileDialog();
            img.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (img.ShowDialog() == DialogResult.OK)
            {
                picture.ImageLocations[2] = img.FileName;
                pictureBox3.ImageLocation = img.FileName;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog img = new OpenFileDialog();
            img.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (img.ShowDialog() == DialogResult.OK)
            {
                picture.ImageLocations[3] = img.FileName;
                pictureBox4.ImageLocation = img.FileName;
            }
        }
    }
}
