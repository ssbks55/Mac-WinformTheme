using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using xThemes;

namespace App
{
    public partial class Form1 : Form
    {
        ThemeManager mgr;
        public Form1()
        {
            InitializeComponent();
            mgr = new ThemeManager();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mgr.ApplyFormThemeSizable(this, true);
            button3.Enabled = false;
        }
    }
}
