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
        public Subject(string str)
        {
            InitializeComponent();
            subjects = db.GetCollection<SubjectAttendance>(str);
            var list = subjects.Find<SubjectAttendance>(n=>true).ToList();
            var bindList = new BindingList<SubjectAttendance>(list);
            dataGridView1.DataSource = bindList;
        }
        static Subject()
        {
            client = new MongoClient();
            db = client.GetDatabase("ProxyKiller");
        }
    }
}
