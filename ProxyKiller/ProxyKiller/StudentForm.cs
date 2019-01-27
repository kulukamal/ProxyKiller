using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;

namespace ProxyKiller
{
    public partial class StudentForm : Form
    {

        static MongoClient client;
        static IMongoDatabase db;
        static IMongoCollection<StudentInfo> studentInfo;
        static IMongoCollection<StudentPicture> studentPicture;
        StudentInfo student;
        StudentPicture picture;
        static StudentForm()
        {
            client = new MongoClient();
            db = client.GetDatabase("ProxyKiller");
            studentInfo = db.GetCollection<StudentInfo>("students");
            studentPicture = db.GetCollection<StudentPicture>("studentPicture");
        }
        public StudentForm(string user)
        {
            InitializeComponent();
            student = new StudentInfo();
            picture = new StudentPicture();
            student = studentInfo.Find(n => n.UserName == user).First();
            picture = studentPicture.Find(n => n.UserName == user).First();
            pictureBox2.ImageLocation = picture.ImageLocations[0];
            label3.Text = student.Name;
            label1.Text = "Welcome " + student.UserName;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                LoginForm l = new LoginForm();
                l.Show();
                this.Hide();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
