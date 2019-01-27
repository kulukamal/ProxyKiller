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
    public partial class LoginForm : Form
    {
        static IMongoDatabase db;
        static IMongoCollection<LoginCredential> loginCredential;
        static MongoClient client;
        static LoginForm()
        {
            client = new MongoClient();
            db = client.GetDatabase("ProxyKiller");
            loginCredential = db.GetCollection<LoginCredential>("loginCredential");
            
        }
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            Int64 password = textBox2.Text.GetHashCode();
            var userList = loginCredential.AsQueryable().Where(n => n.UserName == username);
            if(userList.Count() != 0)
            {
                var user = userList.First();
                if (user.Password == password)
                {
                    if(user.Type == "teacher")
                    {
                        TeacherForm t = new TeacherForm();
                        this.Hide();
                        t.Show();
                    }
                    else
                    {
                        StudentForm s = new StudentForm(user.UserName);
                        this.Hide();
                        s.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password!", "Unauthorized acess", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            else
            {
                MessageBox.Show("Invalid Username or Password!","Unauthorized acess",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
            }
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StudentSignUp s = new StudentSignUp();
            this.Hide();
            s.Show();
        }
    }
}
