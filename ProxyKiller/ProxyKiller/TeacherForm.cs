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
    public partial class TeacherForm : Form
    {
        public TeacherForm()
        {
            InitializeComponent();
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
