using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class ListOfSubjects : Form
    {
        static MongoClient client;
        static IMongoDatabase db;
        static IMongoCollection<SubjectInfo> subjectInfo;
        public ListOfSubjects()
        {
            InitializeComponent();
        }
        static ListOfSubjects()
        {
            client = new MongoClient();
            db = client.GetDatabase("ProxyKiller");
            subjectInfo = db.GetCollection<SubjectInfo>("subjectInfo");
        }
        private void button25_Click(object sender, EventArgs e)
        {
            SubjectInfo subject = new SubjectInfo();
            if(textBox1.Text.Length == 0 || textBox2.Text.Length == 0 )
            {
                MessageBox.Show("Subject Id and Name are Compulsury :");
                return;
            }
            subject.SubjectId = textBox1.Text;
            subject.SubjectName = textBox2.Text;
            if(subjectInfo.Find(n=>n.SubjectId==subject.SubjectId).CountDocuments()!=0)
            {
                MessageBox.Show("Subjet Id Already Exists!");
                return;
            }

            subjectInfo.InsertOne(subject);
            MessageBox.Show("Sucessfuly Added");

        }
    }
}
