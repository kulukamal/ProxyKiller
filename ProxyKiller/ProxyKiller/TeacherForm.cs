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
using System.Diagnostics;
using MongoDB.Driver.Core;
using System.Threading;

namespace ProxyKiller
{
    public partial class TeacherForm : Form
    {
        MongoClient client;
        IMongoDatabase db;
        IMongoCollection<StudentInfo> studentInfo;
        IMongoCollection<StudentPicture> studentPicture;
        IMongoCollection<SubjectInfo> subjectInfo;
        IMongoCollection<RequestInfo> requestInfo;
        IMongoCollection<SubjectAttendance> subjectAttendance1,subjectAttendance2,subjectAttendance3;
        IMongoCollection<StudentAttendance> studentAttendance1, studentAttendance2, studentAttendance3;
        IMongoCollection<Buffer> buffer;
        IMongoCollection<StudentMap> studentMap;
        StudentInfo student1,student2,student3;
        StudentPicture picture1,picture2,picture3;
        SubjectInfo subject1,subject2,subject3;
        RequestInfo request1 , request2 , request3 ;
        SubjectAttendance attendance1, attendance2, attendance3;
        StudentAttendance att1, att2, att3;
        StudentMap tmp;
        Buffer buff;

        //constructor
        public TeacherForm()
        {
            InitializeComponent();
            panel4.Hide();
            client = new MongoClient();
            db = client.GetDatabase("ProxyKiller");
            studentInfo = db.GetCollection<StudentInfo>("students");
            studentPicture = db.GetCollection<StudentPicture>("studentPicture");
            subjectInfo = db.GetCollection<SubjectInfo>("subjectInfo");
            requestInfo = db.GetCollection<RequestInfo>("requestInfo");
            buffer = db.GetCollection<Buffer>("buffer");
            studentMap = db.GetCollection<StudentMap>("studentMap");
            tmp = new StudentMap();
            buff = new Buffer();
            var list = subjectInfo.AsQueryable().ToList();
            List<string> subjectList = new List<string>();
            foreach (var ele in list)
            {
                subjectList.Add(ele.SubjectName + " ( " + ele.SubjectId + " ) ");
            }
            listBox1.DataSource = subjectList;
            RefreshForm();

        }

        //add person to Person Group in Face Api using python
        private void AddPerson(Object std)
        {
            StudentInfo student = (StudentInfo)std;
            buff.UserName = student.UserName;
            buffer.InsertOne(buff);
            string result;
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"C:\Program Files (x86)\Python37-32\python.exe";
            start.Arguments = @"C:\ProxyKiller\ProxyKiller\ProxyKiller\AddPerson.py";
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (System.IO.StreamReader reader = process.StandardOutput)
                {
                    result = reader.ReadToEnd();
                    Console.WriteLine(result);
                }
            }
            
            
        }

       

        //subject add button
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

        //upload attendance of selected subject button
        private void button4_Click_1(object sender, EventArgs e)
        {
            string tmpString=listBox1.Items[listBox1.SelectedIndex].ToString();
            int start = tmpString.IndexOf("( ")+2;
            int end = tmpString.IndexOf(" )");
            string subjectId = tmpString.Substring(start, end - start);


            UploadAttendance upload = new UploadAttendance(subjectId);
            upload.ShowDialog();
        }

        //view attendance of selected subject
        private void button5_Click(object sender, EventArgs e)
        {
            string tmpString = listBox1.Items[listBox1.SelectedIndex].ToString();
            int start = tmpString.IndexOf("( ") + 2;
            int end = tmpString.IndexOf(" )");
            string subjectId = tmpString.Substring(start, end - start);

            Subject subjectAttendance = new Subject(subjectId);
            subjectAttendance.ShowDialog();
        }

        //panel1 aprove button
        private void button1_Click(object sender, EventArgs e)
        {
           
            try
            {

                if (student1 == null) throw new Exception();

                int count = 0;
                try
                {
                    count = (int)studentMap.Find(n => n.UserName == student1.UserName).CountDocuments();
                }
                catch { }
                if (count == 0)
                {
                    Thread t1 = new Thread(AddPerson);
                    panel4.Show();
                        t1.Start(student1);
                        t1.Join();
                        buffer = db.GetCollection<Buffer>("buffer");
                        if (buffer.Find(n => true).CountDocuments() != 0)
                        {
                            buffer.DeleteMany(n => true);
                            throw new Exception();
                        }
                       
                    
                }

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

                MessageBox.Show("Operation Sucessfull!");
                panel4.Hide();
                RefreshForm();

            }
            catch
            {

            }
        }
        //panel1 reject button
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

        //panel2 aprove button
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (student2 == null) throw new Exception();
                int count = 0;
                try
                {
                    count = (int)studentMap.Find(n => n.UserName == student2.UserName).CountDocuments();
                }
                catch { }
                if (count == 0)
                {
                    Thread t1 = new Thread(AddPerson);
                    panel4.Show();
                        t1.Start(student2);
                        t1.Join();
                        buffer = db.GetCollection<Buffer>("buffer");
                        if (buffer.Find(n => true).CountDocuments() != 0)
                        {
                            buffer.DeleteMany(n => true);
                            throw new Exception();
                        }
                    
                    
                }
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
                MessageBox.Show("Operation Sucessfull!");
                panel4.Hide();
                RefreshForm();

            }
            catch
            {

            }
        }
        //panel2 reject button
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

        //panel3 aprove button
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (student3 == null) throw new Exception();
                int count = 0;
                try
                {
                    count = (int)studentMap.Find(n => n.UserName == student3.UserName).CountDocuments();
                }
                catch { }
                if (count == 0)
                {
                    Thread t1 = new Thread(AddPerson);
                    panel4.Show();
                        t1.Start(student3);
                        t1.Join();
                        buffer = db.GetCollection<Buffer>("buffer");
                        if (buffer.Find(n => true).CountDocuments() != 0)
                        {
                            buffer.DeleteMany(n => true);
                            throw new Exception();
                        }
                     
                }
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
                MessageBox.Show("Operation Sucessfull!");
                panel4.Hide();

                RefreshForm();

            }
            catch
            {

            }
        }
        //panel3 reject button
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

        //Refresh form
        void RefreshForm()
        {
            panel4.Show();

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
            panel4.Hide();
        }

        //logout link
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
