using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SearchPath
{
    public partial class progress : Form
    {
        public progress()
        {
            InitializeComponent();
        }

        private void progress_Load(object sender, EventArgs e)
        {
            this.Top = BlockJam.MainWindow.mymain.Top+BlockJam.MainWindow.mymain.Height/2;
            this.Left = BlockJam.MainWindow.mymain.Left+BlockJam.MainWindow.mymain.Width/2-60;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Text == "搜索中，请稍候") this.Text += ".";
            else if (this.Text == "搜索中，请稍候.") this.Text += ".";
            else if (this.Text == "搜索中，请稍候..") this.Text += ".";
            else if (this.Text == "搜索中，请稍候...") this.Text += ".";
            else this.Text = "搜索中，请稍候";

        }
    }
}
