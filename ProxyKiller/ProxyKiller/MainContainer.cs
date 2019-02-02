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
    public partial class MainContainer : Form
    {
        static MainContainer _instance;
        static public MainContainer GetInstance()
        {
            if (_instance == null) _instance = new MainContainer();
            return _instance;
        }
        public static void LoadForm(Form form)
        {
            form.TopLevel = false;
            _instance = MainContainer.GetInstance();
            MainContainer._instance.mainPanel.Controls.Add(form);
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            if(!form.Visible)
            {
                form.Show();
                form.BringToFront();
            }
            else
            {
                form.BringToFront();
            }
            
        }
        public MainContainer()
        {
            InitializeComponent();
            
        }
    }
}
