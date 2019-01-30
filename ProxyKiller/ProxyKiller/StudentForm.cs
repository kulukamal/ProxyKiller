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
        static IMongoCollection<SubjectInfo> subjectInfo;
        static IMongoCollection<RequestInfo> requestInfo;
        static IMongoCollection<StudentAttendance> studentAttendance;
        StudentInfo student;
        StudentPicture picture;
        SubjectInfo subject;
        StudentAttendance attendance;
        static StudentForm()
        {
            client = new MongoClient();
            db = client.GetDatabase("ProxyKiller");
            studentInfo = db.GetCollection<StudentInfo>("students");
            studentPicture = db.GetCollection<StudentPicture>("studentPicture");
            subjectInfo = db.GetCollection<SubjectInfo>("subjectInfo");
            requestInfo = db.GetCollection<RequestInfo>("requestInfo");
            
        }
        public StudentForm(string user)
        {
            InitializeComponent();
            studentAttendance = db.GetCollection<StudentAttendance>(user);
            student = new StudentInfo();
            picture = new StudentPicture();
            student = studentInfo.Find(n => n.UserName == user).First();
            picture = studentPicture.Find(n => n.UserName == user).First();
            pictureBox2.ImageLocation = picture.ImageLocations[0];
            label3.Text = student.Name;

            var list = subjectInfo.AsQueryable().Where(n=>true).ToList();
            List<string> subjectList = new List<string>();
            foreach (var ele in list)
            {
                if(studentAttendance.Find(n=>n.SubjectId==ele.SubjectId).CountDocuments()==0)
                    subjectList.Add(ele.SubjectId);
            }
            listBox1.DataSource = subjectList;

            student = new StudentInfo();
            subject = new SubjectInfo();
            attendance = new StudentAttendance();
            try
            {

                attendance = studentAttendance.AsQueryable().First();
                label1.Text = label1.Text + " " + attendance.SubjectName;
                label2.Text = label2.Text + " " + attendance.Absent.ToString();

                listBox2.DataSource = attendance.listOfAbsents; 
            }
            catch
            {
                label1.Text = label1.Text + " NA ";
                label2.Text = label2.Text + " NA ";
                listBox2.DataSource = null;
            }
            try
            {

                attendance = studentAttendance.AsQueryable().Skip(1).First();
                label5.Text = label5.Text + " " + attendance.SubjectName;
                label4.Text = label4.Text + " " + attendance.Absent.ToString();

                listBox3.DataSource = attendance.listOfAbsents;
            }
            catch
            {
                label5.Text = label5.Text + " NA ";
                label4.Text = label4.Text + " NA ";
                listBox3.DataSource = null;
            }
            try
            {

                attendance = studentAttendance.AsQueryable().Skip(2).First();
                label7.Text = label7.Text + " " + attendance.SubjectName;
                label6.Text = label6.Text + " " + attendance.Absent.ToString();

                listBox4.DataSource = attendance.listOfAbsents;
            }
            catch
            {
                label7.Text = label7.Text + " NA ";
                label6.Text = label6.Text + " NA ";
                listBox4.DataSource = null;
            }
            try
            {

                attendance = studentAttendance.AsQueryable().Skip(3).First();
                label9.Text = label9.Text + " " + attendance.SubjectName;
                label8.Text = label8.Text + " " + attendance.Absent.ToString();

                listBox5.DataSource = attendance.listOfAbsents;
            }
            catch
            {
                label9.Text = label9.Text + " NA ";
                label8.Text = label8.Text + " NA ";
                listBox5.DataSource = null;
            }
            try
            {

                attendance = studentAttendance.AsQueryable().Skip(4).First();
                label11.Text = label11.Text + " " + attendance.SubjectName;
                label10.Text = label10.Text + " " + attendance.Absent.ToString();

                listBox6.DataSource = attendance.listOfAbsents;
            }
            catch
            {
                label11.Text = label11.Text + " NA ";
                label10.Text = label10.Text + " NA ";
                listBox6.DataSource = null;
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            string subjectId = listBox1.Items[listBox1.SelectedIndex].ToString();
            RequestInfo request = new RequestInfo();
            request.SubjectId = subjectId;
            request.UserName = student.UserName;
            request.UserNameSubjectId = student.UserName + subjectId;
            try
            {
                requestInfo.InsertOne(request);
                MessageBox.Show("Sucessfully Added");
            }
            catch
            {
                MessageBox.Show("Already Requested or enrolled!!");
            }

            

        }
        
    }
}
