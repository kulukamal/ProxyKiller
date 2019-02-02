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
    public partial class Subject : Form
    {
        static IMongoDatabase db;
        static IMongoCollection<SubjectAttendance> subjects;
        static MongoClient client;
        List<SubjectAttendance> list;
        SubjectAttendance subject;
        public Subject(string subjectId)
        {
            InitializeComponent();
            subjects = db.GetCollection<SubjectAttendance>(subjectId);
            list = subjects.Find<SubjectAttendance>(n=>true).ToList();
            var bindList = new BindingList<SubjectAttendance>(list);
            dataGridView1.DataSource = bindList;
            subject = new SubjectAttendance();
            label1.Text = "Subject : " + subjectId;
        }
        static Subject()
        {
            client = new MongoClient();
            db = client.GetDatabase("ProxyKiller");
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int index;
            try
            {
                index = dataGridView1.SelectedRows[0].Index;
                subject = subjects.Find(n => n.UserName == list[index].UserName).First();
                listBox1.DataSource = subject.listOfAbsents;

            }
            catch { }
            try
            {
                index = dataGridView1.CurrentCell.RowIndex;
                subject = subjects.Find(n => n.UserName == list[index].UserName).First();
                listBox1.DataSource = subject.listOfAbsents;
            }
            catch { }
        }
        
    }
}
