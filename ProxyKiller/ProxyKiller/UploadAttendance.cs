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
using System.IO;

namespace ProxyKiller
{
    public partial class UploadAttendance : Form
    {
        MongoClient client;
        IMongoDatabase db;
        IMongoCollection<SubjectAttendance> subjectAttendance;
        IMongoCollection<StudentAttendance> studentAttendance;
        IMongoCollection<Buffer> buffer;
        IMongoCollection<ImageBuffer> iBuffer;
        IMongoCollection<AttendanceMap> attendanceMap;

        SubjectAttendance subject;
        StudentMap map;
        StudentAttendance student;
        Buffer buff;
        ImageBuffer iBuff;
        string subjectId;
        AttendanceMap attendance;

        public UploadAttendance(string subjectId)
        {
            InitializeComponent();
            client = new MongoClient();
            db = client.GetDatabase("ProxyKiller");
            this.subjectId = subjectId;
            buffer = db.GetCollection<Buffer>("buffer");
            iBuffer = db.GetCollection<ImageBuffer>("imageBuffer");
            attendanceMap = db.GetCollection<AttendanceMap>("attendanceMap");
            subjectAttendance = db.GetCollection<SubjectAttendance>(subjectId);
            subject = new SubjectAttendance();
            attendance = new AttendanceMap();
            map = new StudentMap();
            student = new StudentAttendance();
            buff = new Buffer();
            iBuff = new ImageBuffer();
            buff.UserName = subjectId;
            label1.Text = "Subject : " + subjectId;
        }
        
        //browse button
        private void button1_Click(object sender, EventArgs e)
        {
            string filename = "C:\\ProxyKiller\\ProxyKiller\\ProxyKiller\\testPicture\\test.jpg";
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if(file.ShowDialog()==DialogResult.OK)
            {
                iBuff.ImageLocation = pictureBox2.ImageLocation = file.FileName;
            }
            else
            {
                return;
            }

            
        }
        //upload button
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
                    buffer.InsertOne(buff);
                    iBuffer.InsertOne(iBuff);
                    foreach(var ele in subjectAttendance.AsQueryable())
                    {
                        attendance.UserName = ele.UserName;
                        attendance.Buffer = 0;
                        attendanceMap.InsertOne(attendance);
                    }
                    string resultCmd;
                    ProcessStartInfo start = new ProcessStartInfo();
                    start.FileName = @"C:\Program Files (x86)\Python37-32\python.exe";
                    start.Arguments = @"C:\ProxyKiller\ProxyKiller\ProxyKiller\AddAttendance.py";
                    start.UseShellExecute = false;
                    start.RedirectStandardOutput = true;
                    using (Process process = Process.Start(start))
                    {
                        using (System.IO.StreamReader reader = process.StandardOutput)
                        {
                            resultCmd = reader.ReadToEnd();
                            Console.WriteLine(resultCmd);
                        }
                    }
                    subjectAttendance = db.GetCollection<SubjectAttendance>(subjectId);
                    buffer = db.GetCollection<Buffer>("buffer");
                    if (buffer.AsQueryable().Any())
                    {
                        MessageBox.Show("Couldn't upload please try another image!");
                        buffer.DeleteMany(n => true);
                        iBuffer.DeleteMany(n=>true);
                        attendanceMap.DeleteMany(nameof => true);
                    }
                    else
                    {
                        var list = attendanceMap.AsQueryable().ToList();
                        foreach (var l in list)
                        {
                             if(l.Buffer == 0)
                            {
                                var ele = subjectAttendance.Find(n => n.UserName == l.UserName).First();
                                subjectAttendance = db.GetCollection<SubjectAttendance>(subjectId);
                                subject.UserName = ele.UserName;
                                subject.Name = ele.Name;
                                subject.Absent = ele.Absent + 1;
                                subject.listOfAbsents = ele.listOfAbsents;
                                if (subject.listOfAbsents == null)
                                {
                                    subject.listOfAbsents = new List<DateTime>();
                                }
                                subject.listOfAbsents.Add(DateTime.Now);

                                studentAttendance = db.GetCollection<StudentAttendance>(l.UserName);
                                var tmp = studentAttendance.Find(n => n.SubjectId == subjectId).First();

                                student.SubjectId = tmp.SubjectId;
                                student.SubjectName = tmp.SubjectName;
                                student.Absent = tmp.Absent + 1;
                                student.listOfAbsents = tmp.listOfAbsents;
                                if (student.listOfAbsents == null)
                                {
                                    student.listOfAbsents = new List<DateTime>();
                                }
                                student.listOfAbsents.Add(DateTime.Now);

                                subjectAttendance.ReplaceOne(n => n.UserName == l.UserName, subject);
                                studentAttendance.ReplaceOne(n => n.SubjectId == subjectId, student);
                            }
                            else
                            {

                            }
                                
                           
                            
                        }
                        attendanceMap.DeleteMany(n => true);
                        MessageBox.Show("sucessful");
                        this.Close();
                    }
                }
            }
        }

     
    }
}
