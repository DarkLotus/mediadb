using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MediaDBClient
{
    public partial class Options : Form
    {
        Form1 main;
        public Options(Form1 main)
        {
            this.main = main;
            InitializeComponent();
            checkBox1.Checked = main.Options.MyConfig.scanSubFolders;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                main.Options.MyConfig.scanSubFolders = true;
            }
            else
            {
                main.Options.MyConfig.scanSubFolders = false;
            }
            
        }
    }
}
