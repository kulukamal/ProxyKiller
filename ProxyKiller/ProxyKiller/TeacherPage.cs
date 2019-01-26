using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProxyKiller
{
    public partial class TeacherPage : Form
    {
        public TeacherPage(string user)
        {
            InitializeComponent();
            label1.Text = "WelCome " + user;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                LoginForm l = new LoginForm();
                l.Show();
                this.Hide();
            }

        }
    }
}
