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
    public partial class TeacherForm : Form
    {
        static MongoClient client;
        static IMongoDatabase db;
        static IMongoCollection<StudentInfo> studentInfo;
        static IMongoCollection<StudentPicture> studentPicture;
        static IMongoCollection<SubjectInfo> subjectInfo;
        static IMongoCollection<RequestInfo> requestInfo;
        StudentInfo student1,student2,student3;
        StudentPicture picture1,picture2,picture3;
        SubjectInfo subject1,subject2,subject3;
        RequestInfo request1 , request2 , request3 ;
        public TeacherForm()
        {
            InitializeComponent();
           
            RefreshForm();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch
            {

            }
        }

        void RefreshForm()
        {
            

            try
            {
                request1 = requestInfo.Find<RequestInfo>(n => true).First();
                student1 = new StudentInfo();
                picture1 = new StudentPicture();
                subject1 = new SubjectInfo();
                student1 = studentInfo.Find(n => n.UserName == request1.UserName).First();
                picture1 = studentPicture.Find(n => n.UserName == request1.UserName).First();
                subject1 = subjectInfo.Find(n => n.SubjectId == request1.SubjectId).First();
                label1.Text = student1.Name;
                label9.Text = subject1.SubjectName;
                pictureBox2.ImageLocation = picture1.ImageLocations[0];
                pictureBox3.ImageLocation = picture1.ImageLocations[1];
                pictureBox4.ImageLocation = picture1.ImageLocations[2];
                pictureBox5.ImageLocation = picture1.ImageLocations[3];
                
                 
            }
            catch
            {
               
                label1.Text = "NA";
                label9.Text = "NA";
                pictureBox2.ImageLocation = "NA";
                pictureBox3.ImageLocation = "NA";
                pictureBox4.ImageLocation = "NA";
                pictureBox5.ImageLocation = "NA";
            }
            try
            {
                request2 = requestInfo.Find<RequestInfo>(n => true).Skip(1).First();
                student2 = new StudentInfo();
                picture2 = new StudentPicture();
                subject2 = new SubjectInfo();
                student2 = studentInfo.Find(n => n.UserName == request2.UserName).First();
                picture2 = studentPicture.Find(n => n.UserName == request2.UserName).First();
                subject2 = subjectInfo.Find(n => n.SubjectId == request2.SubjectId).First();
                label2.Text = student2.Name;
                label10.Text = subject2.SubjectName;
                pictureBox6.ImageLocation = picture2.ImageLocations[0];
                pictureBox7.ImageLocation = picture2.ImageLocations[1];
                pictureBox8.ImageLocation = picture2.ImageLocations[2];
                pictureBox9.ImageLocation = picture2.ImageLocations[3];
            }
            catch
            {
                label2.Text = "NA";
                label10.Text = "NA";
                pictureBox6.ImageLocation = "NA";
                pictureBox7.ImageLocation = "NA";
                pictureBox8.ImageLocation = "NA";
                pictureBox9.ImageLocation = "NA";
            }
            try
            {
                request3 = requestInfo.Find<RequestInfo>(n => true).Skip(2).First();

                student3 = new StudentInfo();
                picture3 = new StudentPicture();
                subject3 = new SubjectInfo();
                student3 = studentInfo.Find(n => n.UserName == request3.UserName).First();
                picture3 = studentPicture.Find(n => n.UserName == request3.UserName).First();
                subject3 = subjectInfo.Find(n => n.SubjectId == request3.SubjectId).First();
                label3.Text = student3.Name;
                label11.Text = subject3.SubjectName;
                pictureBox10.ImageLocation = picture3.ImageLocations[0];
                pictureBox11.ImageLocation = picture3.ImageLocations[1];
                pictureBox12.ImageLocation = picture3.ImageLocations[2];
                pictureBox13.ImageLocation = picture3.ImageLocations[3];
            }
            catch {
                label3.Text = "NA";
                label11.Text = "NA";
                pictureBox10.ImageLocation = "NA";
                pictureBox11.ImageLocation = "NA";
                pictureBox12.ImageLocation = "NA";
                pictureBox13.ImageLocation = "NA";
            }
            
        }
        static TeacherForm()
        {
            client = new MongoClient();
            db = client.GetDatabase("ProxyKiller");
            studentInfo = db.GetCollection<StudentInfo>("students");
            studentPicture = db.GetCollection<StudentPicture>("studentPicture");
            subjectInfo = db.GetCollection<SubjectInfo>("subjectInfo");
            requestInfo = db.GetCollection<RequestInfo>("requestInfo");
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            UploadAttendance obj = new UploadAttendance("tmp");
            obj.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            UploadAttendance obj = new UploadAttendance("tmp");
            obj.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            UploadAttendance obj = new UploadAttendance("tmp");
            obj.Show();
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

        private void button8_Click(object sender, EventArgs e)
        {
            ListOfSubjects l = new ListOfSubjects();
            l.ShowDialog();
        }
    }
}
