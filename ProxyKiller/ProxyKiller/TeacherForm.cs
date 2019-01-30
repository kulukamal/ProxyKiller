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
        IMongoCollection<SubjectAttendance> subjectAttendance1,subjectAttendance2,subjectAttendance3;
        IMongoCollection<StudentAttendance> studentAttendance1, studentAttendance2, studentAttendance3;
        StudentInfo student1,student2,student3;
        StudentPicture picture1,picture2,picture3;
        SubjectInfo subject1,subject2,subject3;
        RequestInfo request1 , request2 , request3 ;
        SubjectAttendance attendance1, attendance2, attendance3;
        StudentAttendance att1, att2, att3;
        
        public TeacherForm()
        {
            InitializeComponent();
            var list = subjectInfo.AsQueryable().ToList();
            List<string> subjectList = new List<string>();
            foreach (var ele in list)
            {
                subjectList.Add(ele.SubjectName + " ( " + ele.SubjectId + " ) ");
            }
            listBox1.DataSource = subjectList;
            RefreshForm();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                attendance3 = new SubjectAttendance();
                attendance3.UserName = student3.UserName;
                attendance3.Name = student3.Name;

                att3 = new StudentAttendance();
                att3.SubjectId = subject3.SubjectId;
                att3.SubjectName = subject3.SubjectName;

                subjectAttendance3 = db.GetCollection<SubjectAttendance>(subject3.SubjectId);
                subjectAttendance3.InsertOne(attendance3);

                studentAttendance3 = db.GetCollection<StudentAttendance>(student3.UserName);
                studentAttendance3.InsertOne(att3);

                requestInfo.DeleteOne(n => n.UserNameSubjectId == request3.UserNameSubjectId);
                RefreshForm();

            }
            catch
            {

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                requestInfo.DeleteOne(n => n.UserNameSubjectId == request3.UserNameSubjectId);
                RefreshForm();
            }
            catch
            { }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            SubjectInfo subject = new SubjectInfo();
            if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0)
            {
                MessageBox.Show("Subject Id and Name are Compulsury :");
                return;
            }
            subject.SubjectId = textBox1.Text;
            subject.SubjectName = textBox2.Text;
            if (subjectInfo.Find(n => n.SubjectId == subject.SubjectId).CountDocuments() != 0)
            {
                MessageBox.Show("Subjet Id Already Exists!");
                return;
            }

            subjectInfo.InsertOne(subject);
            MessageBox.Show("Sucessfuly Added");

            textBox1.Text = textBox2.Text = String.Empty;
            var list = subjectInfo.AsQueryable().ToList();
            List<string> subjectList = new List<string>();
            foreach (var ele in list)
            {
                subjectList.Add(ele.SubjectName + " ( " + ele.SubjectId + " ) ");
            }
            listBox1.DataSource = subjectList;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            string tmpString=listBox1.Items[listBox1.SelectedIndex].ToString();
            int start = tmpString.IndexOf("( ")+2;
            int end = tmpString.IndexOf(" )");
            string subjectId = tmpString.Substring(start, end - start);


            UploadAttendance upload = new UploadAttendance(subjectId);
            upload.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string tmpString = listBox1.Items[listBox1.SelectedIndex].ToString();
            int start = tmpString.IndexOf("( ") + 2;
            int end = tmpString.IndexOf(" )");
            string subjectId = tmpString.Substring(start, end - start);

            Subject subjectAttendance = new Subject(subjectId);
            subjectAttendance.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                attendance1 = new SubjectAttendance();
                attendance1.UserName = student1.UserName;
                attendance1.Name = student1.Name;

                att1 = new StudentAttendance();
                att1.SubjectId = subject1.SubjectId;
                att1.SubjectName = subject1.SubjectName;
                
                subjectAttendance1 = db.GetCollection<SubjectAttendance>(subject1.SubjectId);
                subjectAttendance1.InsertOne(attendance1);

                studentAttendance1 = db.GetCollection<StudentAttendance>(student1.UserName);
                studentAttendance1.InsertOne(att1);

                requestInfo.DeleteOne(n => n.UserNameSubjectId == request1.UserNameSubjectId);
                RefreshForm();

            }
            catch
            {

            }
        }
        private void button9_Click_1(object sender, EventArgs e)
        {
            try
            {
                requestInfo.DeleteOne(n => n.UserNameSubjectId == request1.UserNameSubjectId);
                RefreshForm();
            }
            catch
            { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                attendance2 = new SubjectAttendance();
                attendance2.UserName = student2.UserName;
                attendance2.Name = student2.Name;

                att2 = new StudentAttendance();
                att2.SubjectId = subject2.SubjectId;
                att2.SubjectName = subject2.SubjectName;

                subjectAttendance2 = db.GetCollection<SubjectAttendance>(subject2.SubjectId);
                subjectAttendance2.InsertOne(attendance2);

                studentAttendance2 = db.GetCollection<StudentAttendance>(student2.UserName);
                studentAttendance2.InsertOne(att2);

                requestInfo.DeleteOne(n => n.UserNameSubjectId == request2.UserNameSubjectId);
                RefreshForm();

            }
            catch
            {

            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                requestInfo.DeleteOne(n => n.UserNameSubjectId == request2.UserNameSubjectId);
                RefreshForm();
            }
            catch
            { }
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
        
    }
}
