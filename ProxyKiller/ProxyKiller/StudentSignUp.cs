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
        static MongoClient client;
        static IMongoDatabase db;
        static IMongoCollection<StudentInfo> studentInfo;
        static IMongoCollection<LoginCredential> loginCredential;
        public StudentSignUp()
        {
            InitializeComponent();
            student = new StudentInfo();
            user = new LoginCredential();

        }
        static StudentSignUp()
        {
            client = new MongoClient();
            db = client.GetDatabase("ProxyKiller");
            studentInfo = db.GetCollection<StudentInfo>("students");
            loginCredential = db.GetCollection<LoginCredential>("loginCredential");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog img = new OpenFileDialog();
            img.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            student.ImageLocation = pictureBox1.ImageLocation;
            if(img.ShowDialog()==DialogResult.OK)
            {
                student.ImageLocation = img.FileName;
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
            
            student.Name = textBox2.Text;
            student.Type = "student";
            student.MobileNo = textBox3.Text;

            if(textBox4.Text!=textBox5.Text)
            {
                MessageBox.Show("Password not mathcing!");
                return;
            }
            
            if(loginCredential.Find(n=>n.UserName == textBox1.Text).CountDocuments()!=0)
            {
                MessageBox.Show("Username already exists!");
                return;
            }
            user.UserName = student.UserName = textBox1.Text;
            user.Password = textBox4.Text.GetHashCode();
            string filename =String.Format(@"C:\ProxyKiller\ProxyKiller\ProxyKiller\userPictures\{0}.jpg", user.UserName);
            File.Copy(student.ImageLocation, filename, true);
            studentInfo.InsertOne(student);
            loginCredential.InsertOne(user);

            StudentForm s = new StudentForm(user.UserName);
            this.Hide();
            s.Show();


        }
    }
}
