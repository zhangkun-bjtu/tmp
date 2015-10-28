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
    public partial class showans : Form
    {
        public showans()
        {
            InitializeComponent();
        }
        void textboxinit()
        {
            textBox1.Top = 0;
            textBox1.Left = 0;
            textBox1.Width = this.Width;
            textBox1.Height = this.Height-100;
            clicktoshowai.Top = textBox1.Top + textBox1.Height;
            clicktoshowai.Height = 70;
            clicktoshowai.Left = 0;
            clicktoshowai.Width = this.Width/2-20;
            clicktoshowstep.Top = clicktoshowai.Top;
            clicktoshowstep.Left = clicktoshowai.Left + clicktoshowai.Width;
            clicktoshowstep.Width = this.Width / 2;
            clicktoshowstep.Height = 70;
        }
        private void showans_Load(object sender, EventArgs e)
        {
            //hangrongdao.MainWindow.mymain.unablepic();
            textboxinit();
        }

        private void showans_Resize(object sender, EventArgs e)
        {
            textboxinit();
        }
        public void write_step(int i)
        {
            textBox1.Text+="Step"+Convert.ToString(i)+": ";
        }
        public void write_word(string cnt)
        {
            textBox1.Text += cnt;
        }
        public void write_word_line(string cnt)
        {
            textBox1.Text += cnt;
            textBox1.Text += "\r\n";
        }
        public void write_soilder(int i, int j,string move)
        {
            textBox1.Text += "第" +Convert.ToString(i)+"行，第"+ Convert.ToString(j)+ "列的兵" + move+"\r\n";
        }
        private void clicktoshowstep_Click(object sender, EventArgs e)
        {
            BlockJam.MainWindow.mymain.cntstep = 0;
            if (BlockJam.MainWindow.mymain.havemoved == true)
            {
                MessageBox.Show("You have moved, this answer now is not appropriate\r\n you can search for a new answer");
                return;
            }
            BlockJam.MainWindow.showansstep = 1;
            BlockJam.MainWindow.mymain.prestep.Top = BlockJam.MainWindow.mymain.clicktosearch.Top - 20;
            BlockJam.MainWindow.mymain.prestep.Left = BlockJam.MainWindow.mymain.clicktosearch.Left;
            BlockJam.MainWindow.mymain.prestep.Width = BlockJam.MainWindow.mymain.clicktosearch.Width-10;
            BlockJam.MainWindow.mymain.prestep.Height = BlockJam.MainWindow.mymain.clicktosearch.Height / 2+10;
            BlockJam.MainWindow.mymain.nextstep.Top = BlockJam.MainWindow.mymain.clicktosearch.Top+BlockJam.MainWindow.mymain.prestep.Height-10;
            BlockJam.MainWindow.mymain.nextstep.Left = BlockJam.MainWindow.mymain.clicktosearch.Left;
            BlockJam.MainWindow.mymain.nextstep.Width = BlockJam.MainWindow.mymain.clicktosearch.Width-10;
            BlockJam.MainWindow.mymain.nextstep.Height = BlockJam.MainWindow.mymain.clicktosearch.Height / 2+10;
            BlockJam.MainWindow.mymain.clicktosearch.Top = -1000;
            this.Close();
        }
        private void clicktoshowai_Click(object sender, EventArgs e)
        {
            BlockJam.MainWindow.showansai = 1;
            BlockJam.MainWindow.mymain.stoporstart.Top = BlockJam.MainWindow.mymain.clicktosearch.Top;
            BlockJam.MainWindow.mymain.stoporstart.Left = BlockJam.MainWindow.mymain.clicktosearch.Left;
            BlockJam.MainWindow.mymain.stoporstart.Width = BlockJam.MainWindow.mymain.clicktosearch.Width;
            BlockJam.MainWindow.mymain.stoporstart.Height = BlockJam.MainWindow.mymain.clicktosearch.Height;
            BlockJam.MainWindow.mymain.clicktosearch.Top = -1000;
            BlockJam.MainWindow.mymain.stoporstart.Enabled = true;
            this.Close();
        }
    }
}
