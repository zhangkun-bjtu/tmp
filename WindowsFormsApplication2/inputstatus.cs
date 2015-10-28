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
    public partial class inputstatus : Form
    {
        struct point
        {
            public int top, left;
        }
        int SingleGird = 60;
        bool[] people_selected = new bool[20];
        object mouse_select = null;
        int select_kind = 0;
        object select_cancel=new object();
        bool[,] selected = new bool[10, 10];
        point cao, guan_heng, guan_zong, zhang_heng, zhang_zong, zhao_heng, zhao_zong, ma_heng, ma_zong, huang_zong, huang_heng, s1, s2, s3, s4;
        object[,] small_frame = new object[10, 10];
        public inputstatus()
        {
            InitializeComponent();
        }
        void pic_move(PictureBox cnt, int top, int left, int height, int width)
        {
            cnt.Width = width;
            cnt.Height = height;
            cnt.Top = top;
            cnt.Left = left;
        }
        void init_back_color()
        {
            for (int i = 1; i <= 5; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    (((PictureBox)small_frame[i, j]).BackColor) = this.BackColor;
                }
            }
        }
        void status_init()//formload时的init
        {
            for (int i = 1; i <= 5; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    pic_move((PictureBox)small_frame[i, j], (i - 1) * SingleGird, (j - 1) * SingleGird, SingleGird, SingleGird);
                }
            }
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    selected[i, j] = false;
                }
            }
            for (int i = 1; i < 20; i++)
            {
                people_selected[i] = false;
            }
            select_cancel=0;
        }
        void get_pos(PictureBox cnt, out int anstop, out int ansleft)
        {
            anstop = cnt.Top;
            ansleft = cnt.Left;
        }
        void get_all_pos()
        {
            get_pos(Cao, out cao.top, out cao.left);
            get_pos(Guan_Heng, out  guan_heng.top, out  guan_heng.left);
            get_pos(Guan_Zong, out  guan_zong.top, out  guan_zong.left);
            get_pos(Zhang_Heng, out  zhang_heng.top, out zhang_heng.left);
            get_pos(Zhang_Zong, out  zhang_zong.top, out zhang_zong.left);
            get_pos(Ma_Heng, out  ma_heng.top, out  ma_heng.left);
            get_pos(Ma_Zong, out ma_zong.top, out  ma_zong.left);
            get_pos(Zhao_Heng, out zhao_heng.top, out  zhao_heng.left);
            get_pos(Zhao_Zong, out zhao_zong.top, out zhao_zong.left);
            get_pos(Huang_Heng, out huang_heng.top, out huang_heng.left);
            get_pos(Huang_Zong, out huang_zong.top, out huang_zong.left);
            get_pos(Soldier1, out s1.top, out s1.left);
            get_pos(Soldier2, out s2.top, out s2.left);
            get_pos(Soldier3, out s3.top, out s3.left);
            get_pos(Soldier4, out s4.top, out s4.left);
        }
        void init_pic()//恢复到原位置
        {
            Cao.Left = cao.left; Cao.Top = cao.top;
            Guan_Heng.Left = guan_heng.left; Guan_Heng.Top = guan_heng.top;
            Guan_Zong.Left = guan_zong.left; Guan_Zong.Top = guan_zong.top;
            Zhang_Zong.Left = zhang_zong.left; Zhang_Zong.Top = zhang_zong.top;
            Zhang_Heng.Left = zhang_heng.left; Zhang_Heng.Top = zhang_heng.top;
            Zhao_Heng.Left = zhao_heng.left; Zhao_Heng.Top = zhao_heng.top;
            Zhao_Zong.Left = zhao_zong.left; Zhao_Zong.Top = zhao_zong.top;
            Ma_Heng.Left = ma_heng.left; Ma_Heng.Top = ma_heng.top;
            Ma_Zong.Left = ma_zong.left; Ma_Zong.Top = ma_zong.top;
            Huang_Heng.Left = huang_heng.left; Huang_Heng.Top = huang_heng.top;
            Huang_Zong.Left = huang_zong.left; Huang_Zong.Top = huang_zong.top;
            Soldier1.Left = s1.left; Soldier1.Top = s1.top;
            Soldier2.Left = s2.left; Soldier2.Top = s2.top;
            Soldier3.Left = s3.left; Soldier3.Top = s3.top;
            Soldier4.Left = s4.left; Soldier4.Top = s4.top;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    selected[i, j] = false;
                }
            }
            for (int i = 0; i < 20; i++)
            {
                people_selected[i] = false;
            }
            mouse_select = null;
            select_kind = 0;
            select_cancel = 0; ;
        }
        void init_style()
        {
            Cao.BorderStyle = BorderStyle.None;
            Huang_Heng.BorderStyle = BorderStyle.None;
            Huang_Zong.BorderStyle = BorderStyle.None;
            Guan_Heng.BorderStyle = BorderStyle.None;
            Guan_Zong.BorderStyle = BorderStyle.None;
            Zhang_Heng.BorderStyle = BorderStyle.None;
            Zhang_Zong.BorderStyle = BorderStyle.None;
            Zhao_Heng.BorderStyle = BorderStyle.None;
            Zhao_Zong.BorderStyle = BorderStyle.None;
            Ma_Heng.BorderStyle = BorderStyle.None;
            Ma_Zong.BorderStyle = BorderStyle.None;
            Soldier1.BorderStyle = BorderStyle.None;
            Soldier2.BorderStyle = BorderStyle.None;
            Soldier3.BorderStyle = BorderStyle.None;
            Soldier4.BorderStyle = BorderStyle.None;
        }
        private void inputstatus_Load(object sender, EventArgs e)
        {
            small_frame[1, 1] = frame11;
            small_frame[1, 2] = frame12;
            small_frame[1, 3] = frame13;
            small_frame[1, 4] = frame14;
            small_frame[2, 1] = frame21;
            small_frame[2, 2] = frame22;
            small_frame[2, 3] = frame23;
            small_frame[2, 4] = frame24;
            small_frame[3, 1] = frame31;
            small_frame[3, 2] = frame32;
            small_frame[3, 3] = frame33;
            small_frame[3, 4] = frame34;
            small_frame[4, 1] = frame41;
            small_frame[4, 2] = frame42;
            small_frame[4, 3] = frame43;
            small_frame[4, 4] = frame44;
            small_frame[5, 1] = frame51;
            small_frame[5, 2] = frame52;
            small_frame[5, 3] = frame53;
            small_frame[5, 4] = frame54;
            get_all_pos();
            status_init();
        }
        private void frame11_MouseMove(object sender, MouseEventArgs e)
        {
            init_back_color();
            if (mouse_select == null) return;
            int posi;
            int posj;
            posi = Convert.ToInt32(((PictureBox)sender).Name.Substring(5, 1));
            posj = Convert.ToInt32(((PictureBox)sender).Name.Substring(6, 1));
            if (selected[posi, posj]) return;
            if (select_kind == 6)
            {
                if (posi <= 4 && !selected[posi, posj] && !selected[posi + 1, posj] && posj <= 3 && !selected[posi, posj + 1] && !selected[posi + 1, posj + 1])
                {
                    ((PictureBox)small_frame[posi, posj]).BackColor = Color.Green;
                    ((PictureBox)small_frame[posi + 1, posj]).BackColor = Color.Green;
                    ((PictureBox)small_frame[posi, posj + 1]).BackColor = Color.Green;
                    ((PictureBox)small_frame[posi + 1, posj + 1]).BackColor = Color.Green;
                }
                else
                {
                    ((PictureBox)small_frame[posi, posj]).BackColor = Color.Red;
                    if (posi <= 4 && !selected[posi + 1, posj]) ((PictureBox)small_frame[posi + 1, posj]).BackColor = Color.Red;
                    if (posj <= 3 && !selected[posi, posj + 1]) ((PictureBox)small_frame[posi, posj + 1]).BackColor = Color.Red;
                    if (posi <= 4 && posj <= 3 && !selected[posi + 1, posj + 1]) ((PictureBox)small_frame[posi + 1, posj + 1]).BackColor = Color.Red;
                }
            }
            else if (select_kind == 1)
            {
                if (posi <= 4 && !selected[posi + 1, posj])
                {
                    ((PictureBox)small_frame[posi, posj]).BackColor = Color.Green;
                    ((PictureBox)small_frame[posi + 1, posj]).BackColor = Color.Green;
                }
                else ((PictureBox)small_frame[posi, posj]).BackColor = Color.Red;
            }
            else if (select_kind == 2)
            {
                if (posj <= 3 && !selected[posi, posj + 1])
                {
                    ((PictureBox)small_frame[posi, posj]).BackColor = Color.Green;
                    ((PictureBox)small_frame[posi, posj + 1]).BackColor = Color.Green;
                }
                else ((PictureBox)small_frame[posi, posj]).BackColor = Color.Red;
            }
            else if (select_kind == 7)
            {
                ((PictureBox)small_frame[posi, posj]).BackColor = Color.Green;
            }
        }

        private void Cao_Click(object sender, EventArgs e)
        {
            if (people_selected[6]) return;
            init_style();
            select_kind = 6;
            mouse_select = sender;
            ((PictureBox)sender).BorderStyle = BorderStyle.Fixed3D;
        }

        private void Guan_Zong_Click(object sender, EventArgs e)
        {
            if (people_selected[1]) return;
            init_style();
            select_kind = 1;
            mouse_select = Guan_Zong;
            ((PictureBox)sender).BorderStyle = BorderStyle.Fixed3D;
        }

        private void Zhang_Zong_Click(object sender, EventArgs e)
        {
            if (people_selected[2]) return;
            init_style();
            select_kind = 1;
            mouse_select = Zhang_Zong;
            ((PictureBox)sender).BorderStyle = BorderStyle.Fixed3D;
        }

        private void Zhao_Zong_Click(object sender, EventArgs e)
        {
            if (people_selected[3]) return;
            init_style();
            select_kind = 1;
            mouse_select = Zhao_Zong;
            ((PictureBox)sender).BorderStyle = BorderStyle.Fixed3D;
        }

        private void Huang_Zong_Click(object sender, EventArgs e)
        {
            if (people_selected[5]) return;
            init_style();
            select_kind = 1;
            mouse_select = Huang_Zong;
            ((PictureBox)sender).BorderStyle = BorderStyle.Fixed3D;
        }

        private void Ma_Zong_Click(object sender, EventArgs e)
        {
            if (people_selected[4]) return;
            init_style();
            select_kind = 1;
            mouse_select = Ma_Zong;
            ((PictureBox)sender).BorderStyle = BorderStyle.Fixed3D;
        }

        private void Guan_Heng_Click(object sender, EventArgs e)
        {
            if (people_selected[1]) return;
            init_style();
            select_kind = 2;
            mouse_select = Guan_Heng;
            ((PictureBox)sender).BorderStyle = BorderStyle.Fixed3D;
        }

        private void Zhang_Heng_Click(object sender, EventArgs e)
        {
            if (people_selected[2]) return;
            init_style();
            select_kind = 2;
            mouse_select = Zhang_Heng;
            ((PictureBox)sender).BorderStyle = BorderStyle.Fixed3D;
        }

        private void Zhao_Heng_Click(object sender, EventArgs e)
        {
            if (people_selected[3]) return;
            init_style();
            select_kind = 2;
            mouse_select = Zhao_Heng;
            ((PictureBox)sender).BorderStyle = BorderStyle.Fixed3D;
        }

        private void Ma_Heng_Click(object sender, EventArgs e)
        {
            if (people_selected[4]) return;
            init_style();
            select_kind = 2;
            mouse_select = Ma_Heng;
            ((PictureBox)sender).BorderStyle = BorderStyle.Fixed3D;
        }

        private void Huang_Heng_Click(object sender, EventArgs e)
        {
            if (people_selected[5]) return;
            init_style();
            select_kind = 2;
            mouse_select = Huang_Heng;
            ((PictureBox)sender).BorderStyle = BorderStyle.Fixed3D;
        }

        private void Soldier3_Click(object sender, EventArgs e)
        {
            if (people_selected[9]) return;
            init_style();
            select_kind = 7;
            mouse_select = Soldier3;
            ((PictureBox)sender).BorderStyle = BorderStyle.Fixed3D;
        }

        private void Soldier1_Click(object sender, EventArgs e)
        {
            if (people_selected[7]) return;
            init_style();
            select_kind = 7;
            mouse_select = Soldier1;
            ((PictureBox)sender).BorderStyle = BorderStyle.Fixed3D;
        }

        private void Soldier2_Click(object sender, EventArgs e)
        {
            if (people_selected[8]) return;
            init_style();
            select_kind = 7;
            mouse_select = Soldier2;
            ((PictureBox)sender).BorderStyle = BorderStyle.Fixed3D;
        }

        private void Soldier4_Click(object sender, EventArgs e)
        {
            if (people_selected[10]) return;
            init_style();
            select_kind = 7;
            mouse_select = Soldier4;
            ((PictureBox)sender).BorderStyle = BorderStyle.Fixed3D;
        }

        private void frame11_Click(object sender, EventArgs e)
        {
            init_style();
            if (((PictureBox)sender).BackColor != Color.Green) return;
            int height, width;
            if (select_kind == 6) height = width = 2 * SingleGird;
            else if (select_kind == 7) height = width = SingleGird;
            else if (select_kind == 1) { height = 2 * SingleGird; width = SingleGird; }
            else { height = SingleGird; width = 2 * SingleGird; }
            for (int i = 1; i <= 5; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    if (((PictureBox)small_frame[i, j]).BackColor == Color.Green) selected[i, j] = true;
                }
            }
            init_back_color();
            pic_move((PictureBox)mouse_select, ((PictureBox)sender).Top, ((PictureBox)sender).Left, height, width);
            if (((PictureBox)mouse_select).Name == "Cao") people_selected[6] = true;
            else if (((PictureBox)mouse_select).Name == "Guan_Zong" || ((PictureBox)mouse_select).Name == "Guan_Heng") people_selected[1] = true;
            else if (((PictureBox)mouse_select).Name == "Zhang_Zong" || ((PictureBox)mouse_select).Name == "Zhang_Heng") people_selected[2] = true;
            else if (((PictureBox)mouse_select).Name == "Zhao_Zong" || ((PictureBox)mouse_select).Name == "Zhao_Heng") people_selected[3] = true;
            else if (((PictureBox)mouse_select).Name == "Ma_Zong" || ((PictureBox)mouse_select).Name == "Ma_Heng") people_selected[4] = true;
            else if (((PictureBox)mouse_select).Name == "Huang_Zong" || ((PictureBox)mouse_select).Name == "Huang_Heng") people_selected[5] = true;
            else if (((PictureBox)mouse_select).Name == "Soldier1") people_selected[7] = true;
            else if (((PictureBox)mouse_select).Name == "Soldier2") people_selected[8] = true;
            else if (((PictureBox)mouse_select).Name == "Soldier3") people_selected[9] = true;
            else if (((PictureBox)mouse_select).Name == "Soldier4") people_selected[10] = true;
            ((PictureBox)sender).BorderStyle = BorderStyle.None;
            if ((PictureBox)mouse_select == Guan_Heng) Guan_Zong.Top = -1000;
            else if ((PictureBox)mouse_select == Guan_Zong) Guan_Heng.Top = -1000;
            else if ((PictureBox)mouse_select == Zhang_Heng) Zhang_Zong.Top = -1000;
            else if ((PictureBox)mouse_select == Zhang_Zong) Zhang_Heng.Top = -1000;
            else if ((PictureBox)mouse_select == Zhao_Zong) Zhao_Heng.Top = -1000;
            else if ((PictureBox)mouse_select == Zhao_Heng) Zhao_Zong.Top = -1000;
            else if ((PictureBox)mouse_select == Huang_Heng) Huang_Zong.Top = -1000;
            else if ((PictureBox)mouse_select == Huang_Zong) Huang_Heng.Top = -1000;
            else if ((PictureBox)mouse_select == Ma_Heng) Ma_Zong.Top = -1000;
            else if ((PictureBox)mouse_select == Ma_Zong) Ma_Heng.Top = -1000;
            select_kind = 0; mouse_select = null;
        }

        private void frame_Click(object sender, EventArgs e)
        {

        }

        private void frame12_Click(object sender, EventArgs e)
        {

        }

        private void frame12_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void frame13_Click(object sender, EventArgs e)
        {

        }

        private void frame14_Click(object sender, EventArgs e)
        {

        }

        private void frame22_Click(object sender, EventArgs e)
        {

        }

        private void frame23_Click(object sender, EventArgs e)
        {

        }

        private void frame24_Click(object sender, EventArgs e)
        {

        }

        private void frame51_Click(object sender, EventArgs e)
        {

        }

        private void frame32_Click(object sender, EventArgs e)
        {

        }

        private void frame33_Click(object sender, EventArgs e)
        {

        }

        private void frame34_Click(object sender, EventArgs e)
        {

        }

        private void frame42_Click(object sender, EventArgs e)
        {

        }

        private void frame43_Click(object sender, EventArgs e)
        {

        }

        private void frame44_Click(object sender, EventArgs e)
        {

        }

        private void frame52_Click(object sender, EventArgs e)
        {

        }

        private void frame53_Click(object sender, EventArgs e)
        {

        }

        private void frame54_Click(object sender, EventArgs e)
        {

        }

        private void frame31_Click(object sender, EventArgs e)
        {

        }

        private void frame21_Click(object sender, EventArgs e)
        {

        }

        private void frame41_Click(object sender, EventArgs e)
        {

        }

        private void frame13_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void frame14_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void frame22_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void frame23_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void frame24_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void frame32_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void frame33_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void frame34_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void frame51_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void frame42_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void frame43_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void frame44_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void frame41_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void frame52_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void frame53_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void frame54_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void frame31_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void frame21_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Soldier4_MouseMove(object sender, MouseEventArgs e)
        {
            init_back_color();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            init_pic();
            init_style();
            status_init();
            for (int i = 1; i <= 5; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    ((PictureBox)(small_frame[i, j])).BorderStyle = BorderStyle.Fixed3D;
                }
            }
        }

        private void yes_Click(object sender, EventArgs e)
        {
            int i;
            for (i = 1; i <= 10; i++)
            {
                if (!people_selected[i]) break;
            }
            if (i <= 10) { MessageBox.Show("您还没有安排好局面！"); return; }
            BlockJam.MainWindow.mymain.logo.BackColor = Color.Transparent;
            BlockJam.MainWindow.mymain.logo.Parent = BlockJam.MainWindow.mymain.Frame;
            BlockJam.MainWindow.mymain.Width = BlockJam.MainWindow.mymain.Frame.Width + 10;
            BlockJam.MainWindow.mymain.FormBorderStyle = FormBorderStyle.FixedSingle;
            BlockJam.MainWindow.mymain.showinit();
            BlockJam.MainWindow.mymain.status_stored_init();
            BlockJam.MainWindow.mymain.status_name.Text = "新的局面";
            BlockJam.MainWindow.mymain.creat_state(BlockJam.MainWindow.mymain.Cao, 2, 2, BlockJam.MainWindow.mymain.Frame.Top + (this.Cao.Top - this.frame.Top)+3, this.Cao.Left - this.frame.Left+3);
            BlockJam.MainWindow.mymain.creat_state(BlockJam.MainWindow.mymain.Guan_Heng, 2, 1, BlockJam.MainWindow.mymain.Frame.Top + (this.Guan_Heng.Top - this.frame.Top) + 3, this.Guan_Heng.Left - this.frame.Left + 3);
            BlockJam.MainWindow.mymain.creat_state(BlockJam.MainWindow.mymain.Zhang_Heng, 2, 1, BlockJam.MainWindow.mymain.Frame.Top + (this.Zhang_Heng.Top - this.frame.Top) + 3, this.Zhang_Heng.Left - this.frame.Left + 3);
            BlockJam.MainWindow.mymain.creat_state(BlockJam.MainWindow.mymain.Zhao_Heng, 2, 1, BlockJam.MainWindow.mymain.Frame.Top + (this.Zhao_Heng.Top - this.frame.Top) + 3, this.Zhao_Heng.Left - this.frame.Left + 3);
            BlockJam.MainWindow.mymain.creat_state(BlockJam.MainWindow.mymain.Ma_Heng, 2, 1, BlockJam.MainWindow.mymain.Frame.Top + (this.Ma_Heng.Top - this.frame.Top) + 3, this.Ma_Heng.Left - this.frame.Left + 3);
            BlockJam.MainWindow.mymain.creat_state(BlockJam.MainWindow.mymain.Huang_Heng, 2, 1, BlockJam.MainWindow.mymain.Frame.Top + (this.Huang_Heng.Top - this.frame.Top) + 3, this.Huang_Heng.Left - this.frame.Left + 3);
            BlockJam.MainWindow.mymain.creat_state(BlockJam.MainWindow.mymain.Guan_Zong, 1, 2, BlockJam.MainWindow.mymain.Frame.Top + (this.Guan_Zong.Top - this.frame.Top) + 3, this.Guan_Zong.Left - this.frame.Left + 3);
            BlockJam.MainWindow.mymain.creat_state(BlockJam.MainWindow.mymain.Zhang_Zong, 1, 2, BlockJam.MainWindow.mymain.Frame.Top + (this.Zhang_Zong.Top - this.frame.Top) + 3, this.Zhang_Zong.Left - this.frame.Left + 3);
            BlockJam.MainWindow.mymain.creat_state(BlockJam.MainWindow.mymain.Zhao_Zong, 1, 2, BlockJam.MainWindow.mymain.Frame.Top + (this.Zhao_Zong.Top - this.frame.Top) + 3, this.Zhao_Zong.Left - this.frame.Left + 3);
            BlockJam.MainWindow.mymain.creat_state(BlockJam.MainWindow.mymain.Ma_Zong, 1, 2, BlockJam.MainWindow.mymain.Frame.Top + (this.Ma_Zong.Top - this.frame.Top) + 3, this.Ma_Zong.Left - this.frame.Left + 3);
            BlockJam.MainWindow.mymain.creat_state(BlockJam.MainWindow.mymain.Huang_Zong, 1, 2, BlockJam.MainWindow.mymain.Frame.Top + (this.Huang_Zong.Top - this.frame.Top) + 3, this.Huang_Zong.Left - this.frame.Left + 3);
            BlockJam.MainWindow.mymain.creat_state(BlockJam.MainWindow.mymain.Soldier1, 1, 1, BlockJam.MainWindow.mymain.Frame.Top + (this.Soldier1.Top - this.frame.Top) + 3, this.Soldier1.Left - this.frame.Left + 3);
            BlockJam.MainWindow.mymain.creat_state(BlockJam.MainWindow.mymain.Soldier2, 1, 1, BlockJam.MainWindow.mymain.Frame.Top + (this.Soldier2.Top - this.frame.Top) + 3, this.Soldier2.Left - this.frame.Left + 3);
            BlockJam.MainWindow.mymain.creat_state(BlockJam.MainWindow.mymain.Soldier3, 1, 1, BlockJam.MainWindow.mymain.Frame.Top + (this.Soldier3.Top - this.frame.Top) + 3, this.Soldier3.Left - this.frame.Left + 3);
            BlockJam.MainWindow.mymain.creat_state(BlockJam.MainWindow.mymain.Soldier4, 1, 1, BlockJam.MainWindow.mymain.Frame.Top + (this.Soldier4.Top - this.frame.Top) + 3, this.Soldier4.Left - this.frame.Left + 3);
            int num = 1;
            for (i = 1; i <= 5; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    if (!selected[i, j])
                    {
                        if (num == 1)
                        {
                            BlockJam.MainWindow.mymain.blank1posi = i;
                            BlockJam.MainWindow.mymain.blank1posj = j;
                            num++;
                        }
                        else
                        {
                            BlockJam.MainWindow.mymain.blank2posi = i;
                            BlockJam.MainWindow.mymain.blank2posj = j;
                        }
                    }
                }
            }
            BlockJam.MainWindow.mymain.creat_state(BlockJam.MainWindow.mymain.Blank1, 1, 1, (BlockJam.MainWindow.mymain.blank1posi - 1) * SingleGird+BlockJam.MainWindow.mymain.Frame.Top, (BlockJam.MainWindow.mymain.blank1posj - 1) * SingleGird);
            BlockJam.MainWindow.mymain.creat_state(BlockJam.MainWindow.mymain.Blank2, 1, 1, (BlockJam.MainWindow.mymain.blank2posi - 1) * SingleGird + BlockJam.MainWindow.mymain.Frame.Top, (BlockJam.MainWindow.mymain.blank2posj - 1) * SingleGird);

            /*
            blank1posi = 5; blank1posj = 3; blank2posi = 5; blank2posj = 4; */
            this.Close();
            BlockJam.MainWindow.mymain.Show();
        }

        private void 还原ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (select_cancel!=null && ((PictureBox)select_cancel).Name == "Cao")
            {
                int i = ((PictureBox)select_cancel).Top / SingleGird + 1;
                int j = ((PictureBox)select_cancel).Left / SingleGird + 1;
                if (people_selected[6])
                {
                    selected[i, j] = selected[i + 1, j] = selected[i, j + 1] = selected[i + 1, j + 1] = false;
                }
                people_selected[6] = false;
                pic_move((PictureBox)select_cancel, cao.top, cao.left, 2 * SingleGird, 2 * SingleGird);
            }
            else if (select_cancel != null && ((PictureBox)select_cancel).Name == "Guan_Heng")
            {
                int i = ((PictureBox)select_cancel).Top / SingleGird + 1;
                int j = ((PictureBox)select_cancel).Left / SingleGird + 1;
                if (people_selected[1])
                {
                    selected[i, j] =  selected[i, j + 1] = false;
                }
                people_selected[1] = false;
                pic_move((PictureBox)Guan_Heng, guan_heng.top, guan_heng.left, SingleGird, 2 * SingleGird);
                pic_move((PictureBox)Guan_Zong, guan_zong.top, guan_zong.left,2*SingleGird,SingleGird);
            }
            else if (select_cancel != null && ((PictureBox)select_cancel).Name == "Zhang_Heng")
            {
                int i = ((PictureBox)select_cancel).Top / SingleGird + 1;
                int j = ((PictureBox)select_cancel).Left / SingleGird + 1;
                if (people_selected[2])
                {
                    selected[i, j] = selected[i, j + 1] = false;
                }
                people_selected[2] = false;
                pic_move((PictureBox)Zhang_Heng, zhang_heng.top, zhang_heng.left, SingleGird, 2 * SingleGird);
                pic_move((PictureBox)Zhang_Zong, zhang_zong.top, zhang_zong.left, 2*SingleGird,  SingleGird);
            }
            else if (select_cancel != null && ((PictureBox)select_cancel).Name == "Zhao_Heng")
            {
                int i = ((PictureBox)select_cancel).Top / SingleGird + 1;
                int j = ((PictureBox)select_cancel).Left / SingleGird + 1;
                if (people_selected[3])
                {
                    selected[i, j] = selected[i, j + 1] = false;
                }
                people_selected[3] = false;
                pic_move((PictureBox)Zhao_Heng, zhao_heng.top, zhao_heng.left, SingleGird, 2 * SingleGird);
                pic_move((PictureBox)Zhang_Zong, zhao_zong.top, zhao_zong.left, 2*SingleGird, SingleGird);
            }
            else if (select_cancel != null && ((PictureBox)select_cancel).Name == "Ma_Heng")
            {
                int i = ((PictureBox)select_cancel).Top / SingleGird + 1;
                int j = ((PictureBox)select_cancel).Left / SingleGird + 1;
                if (people_selected[4])
                {
                    selected[i, j] = selected[i, j + 1] = false;
                }
                people_selected[4] = false;
                pic_move((PictureBox)Ma_Heng, ma_heng.top, ma_heng.left, SingleGird, 2 * SingleGird);
                pic_move((PictureBox)Ma_Zong, ma_zong.top, ma_zong.left, 2*SingleGird, SingleGird);
            }
            else if (select_cancel != null && ((PictureBox)select_cancel).Name == "Huang_Heng")
            {
                int i = ((PictureBox)select_cancel).Top / SingleGird + 1;
                int j = ((PictureBox)select_cancel).Left / SingleGird + 1;
                if (people_selected[5])
                {
                    selected[i, j] = selected[i, j + 1] = false;
                }
                people_selected[5] = false;
                pic_move((PictureBox)Huang_Heng, huang_heng.top, huang_heng.left, SingleGird, 2 * SingleGird);
                pic_move((PictureBox)Huang_Zong, huang_zong.top, huang_zong.left, 2*SingleGird,  SingleGird);
            }
            else if (select_cancel != null && ((PictureBox)select_cancel).Name == "Guan_Zong")
            {
                int i = ((PictureBox)select_cancel).Top / SingleGird + 1;
                int j = ((PictureBox)select_cancel).Left / SingleGird + 1;
                if (people_selected[1])
                {
                    selected[i, j] = selected[i+1, j ] = false;
                }
                people_selected[1] = false;
                pic_move((PictureBox)Guan_Heng, guan_heng.top, guan_heng.left, SingleGird, 2 * SingleGird);
                pic_move((PictureBox)Guan_Zong, guan_zong.top, guan_zong.left, 2 * SingleGird, SingleGird);
            }
            else if (select_cancel != null && ((PictureBox)select_cancel).Name == "Zhang_Zong")
            {
                int i = ((PictureBox)select_cancel).Top / SingleGird + 1;
                int j = ((PictureBox)select_cancel).Left / SingleGird + 1;
                if (people_selected[2])
                {
                    selected[i, j] = selected[i+1, j ] = false;
                }
                people_selected[2] = false;
                pic_move((PictureBox)Zhang_Heng, zhang_heng.top, zhang_heng.left, SingleGird, 2 * SingleGird);
                pic_move((PictureBox)Zhang_Zong, zhang_zong.top, zhang_zong.left, 2 * SingleGird, SingleGird);
            }
            else if (select_cancel != null && ((PictureBox)select_cancel).Name == "Zhao_Zong")
            {
                int i = ((PictureBox)select_cancel).Top / SingleGird + 1;
                int j = ((PictureBox)select_cancel).Left / SingleGird + 1;
                if (people_selected[3])
                {
                    selected[i, j] = selected[i+1,j] = false;
                }
                people_selected[3] = false;
                pic_move((PictureBox)Zhao_Heng, zhao_heng.top, zhao_heng.left, SingleGird, 2 * SingleGird);
                pic_move((PictureBox)Zhao_Zong, zhao_zong.top, zhao_zong.left, 2 * SingleGird, SingleGird);
            }
            else if (select_cancel != null && ((PictureBox)select_cancel).Name == "Ma_Zong")
            {
                int i = ((PictureBox)select_cancel).Top / SingleGird + 1;
                int j = ((PictureBox)select_cancel).Left / SingleGird + 1;
                if (people_selected[4])
                {
                    selected[i, j] = selected[i+1, j ] = false;
                }
                people_selected[4] = false;
                pic_move((PictureBox)Ma_Heng, ma_heng.top, ma_heng.left, SingleGird, 2 * SingleGird);
                pic_move((PictureBox)Ma_Zong, ma_zong.top, ma_zong.left, 2 * SingleGird, SingleGird);
            }
            else if (select_cancel != null && ((PictureBox)select_cancel).Name == "Huang_Zong")
            {
                int i = ((PictureBox)select_cancel).Top / SingleGird + 1;
                int j = ((PictureBox)select_cancel).Left / SingleGird + 1;
                if (people_selected[5])
                {
                    selected[i, j] = selected[i+1, j] = false;
                }
                people_selected[5] = false;
                pic_move((PictureBox)Huang_Heng, huang_heng.top, huang_heng.left, SingleGird, 2 * SingleGird);
                pic_move((PictureBox)Huang_Zong, huang_zong.top, huang_zong.left, 2 * SingleGird, SingleGird);
            }
            else if (select_cancel != null && ((PictureBox)select_cancel).Name == "Soldier1")
            {
                int i = ((PictureBox)select_cancel).Top / SingleGird + 1;
                int j = ((PictureBox)select_cancel).Left / SingleGird + 1;
                if (people_selected[7])
                {
                    selected[i, j] =false;
                }
                people_selected[7] = false;
                pic_move((PictureBox)select_cancel, s1.top, s1.left, SingleGird, SingleGird);
            }
            else if (select_cancel != null && ((PictureBox)select_cancel).Name == "Soldier2")
            {
                int i = ((PictureBox)select_cancel).Top / SingleGird + 1;
                int j = ((PictureBox)select_cancel).Left / SingleGird + 1;
                if (people_selected[8])
                {
                    selected[i, j] = false;
                }
                people_selected[8] = false;
                pic_move((PictureBox)select_cancel, s2.top, s2.left, SingleGird, SingleGird);
            }
            else if (select_cancel != null && ((PictureBox)select_cancel).Name == "Soldier3")
            {
                int i = ((PictureBox)select_cancel).Top / SingleGird + 1;
                int j = ((PictureBox)select_cancel).Left / SingleGird + 1;
                if (people_selected[9])
                {
                    selected[i, j] = false;
                }
                people_selected[9] = false;
                pic_move((PictureBox)select_cancel, s3.top, s3.left, SingleGird, SingleGird);
            }
            else if (select_cancel != null && ((PictureBox)select_cancel).Name == "Soldier4")
            {
                int i = ((PictureBox)select_cancel).Top / SingleGird + 1;
                int j = ((PictureBox)select_cancel).Left / SingleGird + 1;
                if (people_selected[10])
                {
                    selected[i, j] = false;
                }
                people_selected[10] = false;
                pic_move((PictureBox)select_cancel, s4.top, s4.left, SingleGird, SingleGird);
            }
            select_cancel = null;
            for (int i = 1; i <= 5; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    ((PictureBox)small_frame[i,j]).BorderStyle = BorderStyle.Fixed3D;
                }
            }
        }

        private void Cao_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) select_cancel = sender;
        }
    }
}
