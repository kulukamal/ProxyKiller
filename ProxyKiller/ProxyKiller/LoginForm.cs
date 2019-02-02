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
        static LoginForm _instance;
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

        public static LoginForm GetInstance()
        {
            if (_instance == null) _instance = new LoginForm();
            return _instance;
        }

        //login button
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
                    textBox1.Text = textBox2.Text = String.Empty;
                    if(user.Type == "teacher")
                    {
                        TeacherForm t = TeacherForm.GetInstance();
                        MainContainer.LoadForm(t);
                    }
                    else
                    {
                        StudentForm s = StudentForm.GetInstance(user.UserName);
                        MainContainer.LoadForm(s);
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

        //sign up link
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StudentSignUp s = StudentSignUp.GetInstnce();
            MainContainer.LoadForm(s);
        }
    }
}
