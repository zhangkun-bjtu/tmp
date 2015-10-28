using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlockJam
{
    public partial class MainWindow : Form
    {
        struct point
        {
            public int posi, posj, kind;
            public string name;
        }
        public static int showansai = 0, showansstep = 0;
        public static MainWindow mymain = null;
        public bool havemoved = false;
        int SingleGird = 60;
        public int cntstep = 0;
        public int blank1posi, blank1posj, blank2posi, blank2posj;
        object showupbase, showleftbase, showdownbase, showrightbase;//带base的是为了确定多方向移动时点击方向按钮对应的参数
        //string upbase, downbase, leftbase, rightbase;
        string[] name;
        Queue<int[,]> ansinmatrix;
        int showupblankkind, showleftblankkind, showdownblankkind, showrightblankkind;
        struct direction
        {
            public int up, down, left, right;
            public int upblankkind, downblankkind, leftblankkind, rightblankkind;
        }
        private void getcenterpos(PictureBox cnt, out int posi, out int posj)
        {
            posi = (cnt.Top - Frame.Top) / SingleGird + 1;
            posj = (cnt.Left - Frame.Left) / SingleGird + 1;
        }
        public void status_stored_init()
        {
            status_name.Left = Frame.Left + 250;
            status_name.Top = Frame.Top + 10;
            stepname.Left = Frame.Left + 250;
            stepname.Top = Frame.Top + 60;
            step.Left = Frame.Left + 270;
            step.Top = Frame.Top + stepname.Height + 90;
            stepname.BackColor = Color.Transparent;
            stepname.Parent = Frame;
            step.BackColor = Color.Transparent;
            step.Parent = Frame;
            step.Text = "0";
            status_name.Parent = Frame;
            status_name.BackColor = Color.Transparent;
            Cao.Top = -100;
            Guan_Heng.Top = -1000;
            Guan_Zong.Top = -1000;
            Zhang_Heng.Top = -1000;
            Zhang_Zong.Top = -1000;
            Zhao_Heng.Top = -1000;
            Zhao_Zong.Top = -1000;
            Huang_Heng.Top = -1000;
            Huang_Zong.Top = -1000;
            Ma_Heng.Top = -1000;
            Ma_Zong.Top = -1000;
            logo.Visible = false;
            clicktosearch.Left = Frame.Left + 245;
            clicktosearch.Top = step.Top + step.Height + 70;
            havemoved = false;
            enablepic();
            showansai = showansstep = 0;
            nextstep.Top = -1000;
            prestep.Top = -1000;
            stoporstart.Enabled = true;
            stoporstart.Top = -1000;
            cntstep = 0;
            this.Height =415;
        }
        public void unablepic()
        {
            Cao.Enabled = false;
            Huang_Heng.Enabled = false;
            Huang_Zong.Enabled=false;
            Guan_Heng.Enabled = false;
            Guan_Zong.Enabled = false;
            Zhang_Zong.Enabled = false;
            Zhang_Heng.Enabled = false;
            Zhao_Heng.Enabled = false;
            Zhao_Zong.Enabled = false;
            Ma_Heng.Enabled = false;
            Ma_Zong.Enabled = false;
        }
        public void enablepic()
        {
            Cao.Enabled = true;
            Huang_Heng.Enabled = true;
            Huang_Zong.Enabled = true;
            Guan_Heng.Enabled = true;
            Guan_Zong.Enabled = true;
            Zhang_Zong.Enabled = true;
            Zhang_Heng.Enabled = true;
            Zhao_Heng.Enabled = true;
            Zhao_Zong.Enabled = true;
            Ma_Heng.Enabled = true;
            Ma_Zong.Enabled = true;
        }
        public void showinit()
        {
            showdown.Top = -100;
            showup.Top = -100;
            showleft.Top = -100;
            showright.Top = -100;
        }
        void getposiblemove(PictureBox cnt, ref direction ans)
        {
            int posi, posj;
            getcenterpos(cnt, out posi, out posj);
            if (cnt.Width == SingleGird && cnt.Height == SingleGird)
            {
                if ((posj == blank1posj && posi - 1 == blank1posi))
                {
                    ans.up = 1;
                    ans.upblankkind = 1;
                }
                else if ((posj == blank2posj && posi - 1 == blank2posi))
                {
                    ans.up = 1;
                    ans.upblankkind = 2;
                }
                if ((posj == blank1posj && posi + 1 == blank1posi))
                {
                    ans.down = 1;
                    ans.downblankkind = 1;
                }
                else if ((posj == blank2posj && posi + 1 == blank2posi))
                {
                    ans.down = 1;
                    ans.downblankkind = 2;
                }
                if ((posi == blank1posi && posj - 1 == blank1posj))
                {
                    ans.left = 1;
                    ans.leftblankkind = 1;
                }
                else if ((posi == blank2posi && posj - 1 == blank2posj))
                {
                    ans.left = 1;
                    ans.leftblankkind = 2;
                }
                if ((posi == blank1posi && posj + 1 == blank1posj))
                {
                    ans.right = 1;
                    ans.rightblankkind = 1;
                }
                else if ((posi == blank2posi && posj + 1 == blank2posj))
                {
                    ans.right = 1;
                    ans.rightblankkind = 2;
                }
            }
            else if (cnt.Width == 2 * SingleGird && cnt.Height == SingleGird)
            {
                if (blank1posi == blank2posi && blank1posi == posi - 1 && ((blank2posj == posj && blank1posj == posj + 1) || (blank1posj == posj && blank2posj == posj + 1)))
                {
                    ans.up = 1;
                    ans.upblankkind = 3;
                }
                if (blank1posi == blank2posi && blank1posi == posi + 1 && ((blank2posj == posj && blank1posj == posj + 1) || (blank1posj == posj && blank2posj == posj + 1)))
                {
                    ans.down = 1;
                    ans.downblankkind = 3;
                }
                if ((posi == blank1posi && posj - 1 == blank1posj))
                {
                    ans.left = 1;
                    ans.leftblankkind = 1;
                }
                else if ((posi == blank2posi && posj - 1 == blank2posj))
                {
                    ans.left = 1;
                    ans.leftblankkind = 2;
                }
                if ((posi == blank1posi && posj + 2 == blank1posj))
                {
                    ans.right = 1;
                    ans.rightblankkind = 1;
                }
                else if ((posi == blank2posi && posj + 2 == blank2posj))
                {
                    ans.right = 1;
                    ans.rightblankkind = 2;
                }
            }
            else if (cnt.Width == SingleGird && cnt.Height == 2 * SingleGird)
            {
                if ((posj == blank1posj && posi - 1 == blank1posi))
                {
                    ans.up = 1;
                    ans.upblankkind = 1;
                }
                else if ((posj == blank2posj && posi - 1 == blank2posi))
                {
                    ans.up = 1;
                    ans.upblankkind = 2;
                }
                if ((posj == blank1posj && posi + 2 == blank1posi))
                {
                    ans.down = 1;
                    ans.downblankkind = 1;
                }
                else if ((posj == blank2posj && posi + 2 == blank2posi))
                {
                    ans.down = 1;
                    ans.downblankkind = 2;
                }
                if (blank1posj == blank2posj && posj - 1 == blank2posj && ((blank1posi == posi && blank2posi == posi + 1) || (blank2posi == posi && blank1posi == posi + 1)))
                {
                    ans.left = 1;
                    ans.leftblankkind = 3;
                }
                if (blank1posj == blank2posj && posj + 1 == blank2posj && ((blank1posi == posi && blank2posi == posi + 1) || (blank2posi == posi && blank1posi == posi + 1)))
                {
                    ans.right = 1;
                    ans.rightblankkind = 3;
                }
            }
            else
            {
                if (blank1posi == blank2posi && blank1posi == posi - 1 && ((blank2posj == posj && blank1posj == posj + 1) || (blank1posj == posj && blank2posj == posj + 1)))
                {
                    ans.up = 1;
                    ans.upblankkind = 3;
                }
                if (blank1posi == blank2posi && blank1posi == posi + 2 && ((blank2posj == posj && blank1posj == posj + 1) || (blank1posj == posj && blank2posj == posj + 1)))
                {
                    ans.down = 1;
                    ans.downblankkind = 3;
                }
                if (blank1posj == blank2posj && posj - 1 == blank2posj && ((blank1posi == posi && blank2posi == posi + 1) || (blank2posi == posi && blank1posi == posi + 1)))
                {
                    ans.left = 1;
                    ans.leftblankkind = 3;
                }
                if (blank1posj == blank2posj && posj + 2 == blank2posj && ((blank1posi == posi && blank2posi == posi + 1) || (blank2posi == posi && blank1posi == posi + 1)))
                {
                    ans.right = 1;
                    ans.rightblankkind = 3;
                }
            }
        }
        private void moveinpic(PictureBox cnt, direction posdir)
        {
            if (showansstep == 1 || showansai == 1)
            {
                MessageBox.Show("演示过程中不可以手动移动");
                return;
            }
            int flag = posdir.up + posdir.down + posdir.left + posdir.right;
            if (flag == 1)
            {
                havemoved = true;
                if (posdir.up == 1)
                {
                    cnt.Top -= SingleGird;
                    if (posdir.upblankkind == 1)
                    {
                        Blank1.Top += SingleGird;
                        blank1posi++;
                        if (cnt.Height == 2 * SingleGird)
                        {
                            Blank1.Top += SingleGird;
                            blank1posi++;
                        }
                    }
                    else if (posdir.upblankkind == 2)
                    {
                        Blank2.Top += SingleGird;
                        blank2posi++;
                        if (cnt.Height == 2 * SingleGird)
                        {
                            Blank2.Top += SingleGird;
                            blank2posi++;
                        }
                    }
                    else
                    {
                        Blank1.Top += SingleGird;
                        Blank2.Top += SingleGird;
                        blank1posi++;
                        blank2posi++;
                        if (cnt.Height == 2 * SingleGird)
                        {
                            Blank1.Top += SingleGird;
                            blank1posi++;
                            Blank2.Top += SingleGird;
                            blank2posi++;
                        }
                    }
                }
                else if (posdir.down == 1)
                {
                    cnt.Top += SingleGird;
                    if (posdir.downblankkind == 1)
                    {
                        Blank1.Top -= SingleGird;
                        blank1posi--;
                        if (cnt.Height == 2 * SingleGird)
                        {
                            Blank1.Top -= SingleGird;
                            blank1posi--;
                        }
                    }
                    else if (posdir.downblankkind == 2)
                    {
                        Blank2.Top -= SingleGird;
                        blank2posi--;
                        if (cnt.Height == 2 * SingleGird)
                        {
                            Blank2.Top -= SingleGird;
                            blank2posi--;
                        }
                    }
                    else
                    {
                        Blank1.Top -= SingleGird;
                        Blank2.Top -= SingleGird;
                        blank1posi--;
                        blank2posi--;
                        if (cnt.Height == 2 * SingleGird)
                        {
                            Blank1.Top -= SingleGird;
                            blank1posi--;
                            Blank2.Top -= SingleGird;
                            blank2posi--;
                        }
                    }
                }
                else if (posdir.left == 1)
                {
                    cnt.Left -= SingleGird;
                    if (posdir.leftblankkind == 1)
                    {
                        Blank1.Left += SingleGird;
                        blank1posj++;
                        if (cnt.Width == 2 * SingleGird)
                        {
                            Blank1.Left += SingleGird;
                            blank1posj++;
                        }
                    }
                    else if (posdir.leftblankkind == 2)
                    {
                        Blank2.Left += SingleGird;
                        blank2posj++;
                        if (cnt.Width == 2 * SingleGird)
                        {
                            Blank2.Left += SingleGird;
                            blank2posj++;
                        }
                    }
                    else
                    {
                        Blank1.Left += SingleGird;
                        Blank2.Left += SingleGird;
                        blank1posj++;
                        blank2posj++;
                        if (cnt.Width == 2 * SingleGird)
                        {
                            Blank1.Left += SingleGird;
                            blank1posj++;
                            Blank2.Left += SingleGird;
                            blank2posj++;
                        }
                    }
                }
                else
                {
                    cnt.Left += SingleGird;
                    if (posdir.rightblankkind == 1)
                    {
                        Blank1.Left -= SingleGird;
                        blank1posj--;
                        if (cnt.Width == 2 * SingleGird)
                        {
                            Blank1.Left -= SingleGird;
                            blank1posj--;
                        }
                    }
                    else if (posdir.rightblankkind == 2)
                    {
                        Blank2.Left -= SingleGird;
                        blank2posj--;
                        if (cnt.Width == 2 * SingleGird)
                        {
                            Blank2.Left -= SingleGird;
                            blank2posj--;
                        }
                    }
                    else
                    {
                        Blank1.Left -= SingleGird;
                        Blank2.Left -= SingleGird;
                        blank1posj--;
                        blank2posj--;
                        if (cnt.Width == 2 * SingleGird)
                        {
                            Blank1.Left -= SingleGird;
                            blank1posj--;
                            Blank2.Left -= SingleGird;
                            blank2posj--;
                        }
                    }
                }
                step.Text = Convert.ToString(Convert.ToInt32(step.Text) + 1);
            }
            else
            {
                if (posdir.up == 1)
                {
                    showup.Top = cnt.Top;
                    showup.Left = cnt.Left + (cnt.Width - showup.Width) / 2;
                    showupbase = new object();
                    showupbase = cnt;
                    showupblankkind = posdir.upblankkind;
                }
                if (posdir.down == 1)
                {
                    showdown.Top = cnt.Top + cnt.Height - showdown.Height;
                    showdown.Left = cnt.Left + (cnt.Width - showdown.Width) / 2;
                    showdownbase = new object();
                    showdownbase = cnt;
                    showdownblankkind = posdir.downblankkind;
                }
                if (posdir.left == 1)
                {
                    showleft.Left = cnt.Left;
                    showleft.Top = cnt.Top + (cnt.Height - showleft.Height) / 2;
                    showleftbase = new object();
                    showleftbase = cnt;
                    showleftblankkind = posdir.leftblankkind;
                }
                if (posdir.right == 1)
                {
                    showright.Left = cnt.Left + cnt.Width - showright.Width;
                    showright.Top = cnt.Top + (cnt.Height - showright.Height) / 2;
                    showrightbase = new object();
                    showrightbase = cnt;
                    showrightblankkind = posdir.rightblankkind;
                }
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            mymain = this;
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
            logo.BackColor = Color.Transparent;
            logo.Parent = Frame;
            this.Width = Frame.Width + 20;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void 已有局面ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        public void creat_state(PictureBox cnt, int ilength, int jlength, int posi, int posj)
        {
            cnt.Width = ilength * SingleGird;
            cnt.Height = jlength * SingleGird;
            cnt.Top = posi;
            cnt.Left = posj;
        }
        private void 一夫当关ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showinit();
            status_stored_init();
            status_name.Text = "一夫当关";
            creat_state(Cao, 2, 2, Frame.Top, 0);
            creat_state(Zhang_Heng, 2, 1, Frame.Top, 2 * SingleGird);
            creat_state(Zhao_Heng, 2, 1, SingleGird + Frame.Top, 2 * SingleGird);
            creat_state(Ma_Heng, 2, 1, SingleGird * 2 + Frame.Top, 2 * SingleGird);
            creat_state(Huang_Heng, 2, 1, 3 * SingleGird + Frame.Top, 2 * SingleGird);
            creat_state(Guan_Zong, 1, 2, 2 * SingleGird + Frame.Top, SingleGird);
            creat_state(Soldier1, 1, 1, SingleGird * 2 + Frame.Top, 0);
            creat_state(Soldier2, 1, 1, SingleGird * 3 + Frame.Top, 2 * 0);
            creat_state(Soldier3, 1, 1, SingleGird * 4 + Frame.Top, 2 * 0);
            creat_state(Soldier4, 1, 1, SingleGird * 4 + Frame.Top, SingleGird);
            creat_state(Blank1, 1, 1, SingleGird * 4 + Frame.Top, 2 * SingleGird);
            creat_state(Blank2, 1, 1, SingleGird * 4 + Frame.Top, 3 * SingleGird);
            blank1posi = 5; blank1posj = 3; blank2posi = 5; blank2posj = 4;
        }

        private void Huang_Heng_Click(object sender, EventArgs e)
        {
            showinit();
            direction posdir;
            posdir.up = posdir.down = posdir.left = posdir.right = 0;
            posdir.upblankkind = posdir.downblankkind = posdir.leftblankkind = posdir.rightblankkind = 0;
            getposiblemove(Huang_Heng, ref posdir);
            moveinpic(Huang_Heng, posdir);
        }

        private void Frame_Click(object sender, EventArgs e)
        {

        }

        private void Soldier4_Click(object sender, EventArgs e)
        {
            showinit();
            direction posdir;
            posdir.up = posdir.down = posdir.left = posdir.right = 0;
            posdir.upblankkind = posdir.downblankkind = posdir.leftblankkind = posdir.rightblankkind = 0;
            getposiblemove(Soldier4, ref posdir);
            moveinpic(Soldier4, posdir);
        }

        private void showdown_Click(object sender, EventArgs e)
        {
            havemoved = true;
            ((PictureBox)showdownbase).Top += SingleGird;
            if (showdownblankkind == 1)
            {
                Blank1.Top -= SingleGird;
                blank1posi--;
                if (((PictureBox)showdownbase).Height == 2 * SingleGird)
                {
                    Blank1.Top -= SingleGird;
                    blank1posi--;
                }
            }
            else if (showdownblankkind == 2)
            {
                Blank2.Top -= SingleGird;
                blank2posi--;
                if (((PictureBox)showdownbase).Height == 2 * SingleGird)
                {
                    Blank2.Top -= SingleGird;
                    blank2posi--;
                }
            }
            else
            {
                Blank1.Top -= SingleGird;
                blank1posi--;
                Blank2.Top -= SingleGird;
                blank2posi--;
                if (((PictureBox)showdownbase).Height == 2 * SingleGird)
                {
                    Blank1.Top -= SingleGird;
                    blank1posi--;
                    Blank2.Top -= SingleGird;
                    blank2posi--;
                }
            }
            showinit();
            step.Text = Convert.ToString(Convert.ToInt32(step.Text) + 1);
        }

        private void showleft_Click(object sender, EventArgs e)
        {
            havemoved = true;
            ((PictureBox)showleftbase).Left -= SingleGird;
            if (showleftblankkind == 1)
            {
                Blank1.Left += SingleGird;
                blank1posj++;
                if (((PictureBox)showleftbase).Width == 2 * SingleGird)
                {
                    Blank1.Left += SingleGird;
                    blank1posj++;
                }
            }
            else if (showleftblankkind == 2)
            {
                Blank2.Left += SingleGird;
                blank2posj++;
                if (((PictureBox)showleftbase).Width == 2 * SingleGird)
                {
                    Blank2.Left += SingleGird;
                    blank2posj++;
                }
            }
            else
            {
                Blank1.Left += SingleGird;
                blank1posj++;
                Blank2.Left += SingleGird;
                blank2posj++;
                if (((PictureBox)showleftbase).Width == 2 * SingleGird)
                {
                    Blank1.Left += SingleGird;
                    blank1posj++;
                    Blank2.Left += SingleGird;
                    blank2posj++;
                }
            }
            showinit();
            step.Text = Convert.ToString(Convert.ToInt32(step.Text) + 1);
        }

        private void showright_Click(object sender, EventArgs e)
        {
            havemoved = true;
            ((PictureBox)showrightbase).Left += SingleGird;
            if (showrightblankkind == 1)
            {
                Blank1.Left -= SingleGird;
                blank1posj--;
                if (((PictureBox)showrightbase).Width == 2 * SingleGird)
                {
                    Blank1.Left -= SingleGird;
                    blank1posj--;
                }
            }
            else if (showrightblankkind == 2)
            {
                Blank2.Left -= SingleGird;
                blank2posj--;
                if (((PictureBox)showrightbase).Width == 2 * SingleGird)
                {
                    Blank2.Left -= SingleGird;
                    blank2posj--;
                }
            }
            else
            {
                Blank1.Left -= SingleGird;
                blank1posj--;
                Blank2.Left -= SingleGird;
                blank2posj--;
                if (((PictureBox)showrightbase).Width == 2 * SingleGird)
                {
                    Blank1.Left -= SingleGird;
                    blank1posj--;
                    Blank2.Left -= SingleGird;
                    blank2posj--;
                }
            }
            showinit();
            step.Text = Convert.ToString(Convert.ToInt32(step.Text) + 1);
        }

        private void showup_Click(object sender, EventArgs e)
        {
            havemoved = true;
            ((PictureBox)showupbase).Top -= SingleGird;
            if (showupblankkind == 1)
            {
                Blank1.Top += SingleGird;
                blank1posi++;
                if (((PictureBox)showupbase).Height == 2 * SingleGird)
                {
                    Blank1.Top += SingleGird;
                    blank1posi++;
                }
            }
            else if (showupblankkind == 2)
            {
                Blank2.Top += SingleGird;
                blank2posi++;
                if (((PictureBox)showupbase).Height == 2 * SingleGird)
                {
                    Blank2.Left += SingleGird;
                    blank2posi++;
                }
            }
            else
            {
                Blank1.Top += SingleGird;
                blank1posi++;
                Blank2.Top += SingleGird;
                blank2posi++;
                if (((PictureBox)showupbase).Height == 2 * SingleGird)
                {
                    Blank1.Left += SingleGird;
                    blank1posi++;
                    Blank2.Left += SingleGird;
                    blank2posi++;
                }
            }
            showinit();
            step.Text = Convert.ToString(Convert.ToInt32(step.Text) + 1);
        }

        private void Guan_Zong_Click(object sender, EventArgs e)
        {
            showinit();
            direction posdir;
            posdir.up = posdir.down = posdir.left = posdir.right = 0;
            posdir.upblankkind = posdir.downblankkind = posdir.leftblankkind = posdir.rightblankkind = 0;
            getposiblemove(Guan_Zong, ref posdir);
            moveinpic(Guan_Zong, posdir);
        }

        private void Guan_Heng_Click(object sender, EventArgs e)
        {
            showinit();
            direction posdir;
            posdir.up = posdir.down = posdir.left = posdir.right = 0;
            posdir.upblankkind = posdir.downblankkind = posdir.leftblankkind = posdir.rightblankkind = 0;
            getposiblemove(Guan_Heng, ref posdir);
            moveinpic(Guan_Heng, posdir);
        }

        private void Zhang_Zong_Click(object sender, EventArgs e)
        {
            showinit();
            direction posdir;
            posdir.up = posdir.down = posdir.left = posdir.right = 0;
            posdir.upblankkind = posdir.downblankkind = posdir.leftblankkind = posdir.rightblankkind = 0;
            getposiblemove(Zhang_Zong, ref posdir);
            moveinpic(Zhang_Zong, posdir);
        }

        private void Zhang_Heng_Click(object sender, EventArgs e)
        {
            showinit();
            direction posdir;
            posdir.up = posdir.down = posdir.left = posdir.right = 0;
            posdir.upblankkind = posdir.downblankkind = posdir.leftblankkind = posdir.rightblankkind = 0;
            getposiblemove(Zhang_Heng, ref posdir);
            moveinpic(Zhang_Heng, posdir);
        }

        private void Zhao_Zong_Click(object sender, EventArgs e)
        {
            showinit();
            direction posdir;
            posdir.up = posdir.down = posdir.left = posdir.right = 0;
            posdir.upblankkind = posdir.downblankkind = posdir.leftblankkind = posdir.rightblankkind = 0;
            getposiblemove(Zhao_Zong, ref posdir);
            moveinpic(Zhao_Zong, posdir);
        }

        private void Zhao_Heng_Click(object sender, EventArgs e)
        {
            showinit();
            direction posdir;
            posdir.up = posdir.down = posdir.left = posdir.right = 0;
            posdir.upblankkind = posdir.downblankkind = posdir.leftblankkind = posdir.rightblankkind = 0;
            getposiblemove(Zhao_Heng, ref posdir);
            moveinpic(Zhao_Heng, posdir);
        }

        private void Ma_Zong_Click(object sender, EventArgs e)
        {
            showinit();
            direction posdir;
            posdir.up = posdir.down = posdir.left = posdir.right = 0;
            posdir.upblankkind = posdir.downblankkind = posdir.leftblankkind = posdir.rightblankkind = 0;
            getposiblemove(Ma_Zong, ref posdir);
            moveinpic(Ma_Zong, posdir);
        }

        private void Ma_Heng_Click(object sender, EventArgs e)
        {
            showinit();
            direction posdir;
            posdir.up = posdir.down = posdir.left = posdir.right = 0;
            posdir.upblankkind = posdir.downblankkind = posdir.leftblankkind = posdir.rightblankkind = 0;
            getposiblemove(Ma_Heng, ref posdir);
            moveinpic(Ma_Heng, posdir);
        }

        private void Soldier1_Click(object sender, EventArgs e)
        {
            showinit();
            direction posdir;
            posdir.up = posdir.down = posdir.left = posdir.right = 0;
            posdir.upblankkind = posdir.downblankkind = posdir.leftblankkind = posdir.rightblankkind = 0;
            getposiblemove(Soldier1, ref posdir);
            moveinpic(Soldier1, posdir);
        }

        private void Soldier2_Click(object sender, EventArgs e)
        {
            showinit();
            direction posdir;
            posdir.up = posdir.down = posdir.left = posdir.right = 0;
            posdir.upblankkind = posdir.downblankkind = posdir.leftblankkind = posdir.rightblankkind = 0;
            getposiblemove(Soldier2, ref posdir);
            moveinpic(Soldier2, posdir);
        }

        private void Soldier3_Click(object sender, EventArgs e)
        {
            showinit();
            direction posdir;
            posdir.up = posdir.down = posdir.left = posdir.right = 0;
            posdir.upblankkind = posdir.downblankkind = posdir.leftblankkind = posdir.rightblankkind = 0;
            getposiblemove(Soldier3, ref posdir);
            moveinpic(Soldier3, posdir);
        }

        private void Blank1_Click(object sender, EventArgs e)
        {

        }

        private void Cao_Click(object sender, EventArgs e)
        {
            showinit();
            direction posdir;
            posdir.up = posdir.down = posdir.left = posdir.right = 0;
            posdir.upblankkind = posdir.downblankkind = posdir.leftblankkind = posdir.rightblankkind = 0;
            getposiblemove(Cao, ref posdir);
            moveinpic(Cao, posdir);
            if (Cao.Top - Frame.Top == 3 * SingleGird && Cao.Left - Frame.Left == SingleGird)
            {
                MessageBox.Show("恭喜您过关！");
            }
        }

        private void Huang_Zong_Click(object sender, EventArgs e)
        {
            showinit();
            direction posdir;
            posdir.up = posdir.down = posdir.left = posdir.right = 0;
            posdir.upblankkind = posdir.downblankkind = posdir.leftblankkind = posdir.rightblankkind = 0;
            getposiblemove(Huang_Zong, ref posdir);
            moveinpic(Huang_Zong, posdir);
        }

        private void step_Click(object sender, EventArgs e)
        {

        }

        private void 一字长蛇ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showinit();
            status_stored_init();
            status_name.Text = "一字长蛇";
            creat_state(Cao, 2, 2, Frame.Top, 0);
            creat_state(Zhang_Heng, 2, 1, Frame.Top + 3 * SingleGird, 0);
            creat_state(Zhao_Heng, 2, 1, 2 * SingleGird + Frame.Top, 0);
            creat_state(Ma_Heng, 2, 1, Frame.Top, 2 * SingleGird);
            creat_state(Huang_Heng, 2, 1, SingleGird + Frame.Top, 2 * SingleGird);
            creat_state(Guan_Zong, 1, 2, 2 * SingleGird + Frame.Top, 2 * SingleGird);
            creat_state(Soldier1, 1, 1, SingleGird * 2 + Frame.Top, 3 * SingleGird);
            creat_state(Soldier2, 1, 1, SingleGird * 3 + Frame.Top, 3 * SingleGird);
            creat_state(Soldier3, 1, 1, SingleGird * 4 + Frame.Top, 3 * SingleGird);
            creat_state(Soldier4, 1, 1, SingleGird * 4 + Frame.Top, 2 * SingleGird);
            creat_state(Blank1, 1, 1, SingleGird * 4 + Frame.Top, 0);
            creat_state(Blank2, 1, 1, SingleGird * 4 + Frame.Top, SingleGird);
            blank1posi = 5; blank1posj = 1; blank2posi = 5; blank2posj = 2;
        }

        private void 天罗地网ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showinit();
            status_stored_init();
            status_name.Text = "天罗地网";
            creat_state(Cao, 2, 2, Frame.Top, SingleGird);
            creat_state(Zhang_Heng, 2, 1, Frame.Top + 2 * SingleGird, 0);
            creat_state(Zhao_Zong, 1, 2, Frame.Top, 0);
            creat_state(Ma_Zong, 1, 2, Frame.Top + 3 * SingleGird, 0);
            creat_state(Huang_Zong, 1, 2, 3 * SingleGird + Frame.Top, 3 * SingleGird);
            creat_state(Guan_Heng, 2, 1, 3 * SingleGird + Frame.Top, SingleGird);
            creat_state(Soldier1, 1, 1, Frame.Top, 3 * SingleGird);
            creat_state(Soldier2, 1, 1, SingleGird + Frame.Top, 3 * SingleGird);
            creat_state(Soldier3, 1, 1, SingleGird * 2 + Frame.Top, 3 * SingleGird);
            creat_state(Soldier4, 1, 1, SingleGird * 2 + Frame.Top, 2 * SingleGird);
            creat_state(Blank1, 1, 1, SingleGird * 4 + Frame.Top, SingleGird);
            creat_state(Blank2, 1, 1, SingleGird * 4 + Frame.Top, SingleGird * 2);
            blank1posi = 5; blank1posj = 2; blank2posi = 5; blank2posj = 3;
        }

        private void 铜墙铁壁ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showinit();
            status_stored_init();
            status_name.Text = "铜墙铁壁";
            creat_state(Cao, 2, 2, Frame.Top, SingleGird);
            creat_state(Zhang_Heng, 2, 1, Frame.Top + 3 * SingleGird, SingleGird);
            creat_state(Zhao_Heng, 2, 1, Frame.Top + 2 * SingleGird, SingleGird);
            creat_state(Ma_Zong, 1, 2, Frame.Top, 3 * SingleGird);
            creat_state(Huang_Zong, 1, 2, Frame.Top, 0);
            creat_state(Guan_Heng, 2, 1, 4 * SingleGird + Frame.Top, SingleGird);
            creat_state(Soldier1, 1, 1, 2 * SingleGird + Frame.Top, 0);
            creat_state(Soldier2, 1, 1, 3 * SingleGird + Frame.Top, 0);
            creat_state(Soldier3, 1, 1, 2 * SingleGird + Frame.Top, 3 * SingleGird);
            creat_state(Soldier4, 1, 1, SingleGird * 3 + Frame.Top, 3 * SingleGird);
            creat_state(Blank1, 1, 1, SingleGird * 4 + Frame.Top, 0);
            creat_state(Blank2, 1, 1, SingleGird * 4 + Frame.Top, SingleGird * 3);
            blank1posi = 5; blank1posj = 1; blank2posi = 5; blank2posj = 4;
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Version：1.0\r\nQQ:   1214690378");
        }
        private void get_heng_zong_kind(PictureBox zong, PictureBox heng, out int i, out int j, out int kind)
        {
            int posi, posj;
            getcenterpos(zong, out posi, out posj);
            if (posi >= 1 && posj >= 1)
            {
                kind = 1;//1表示纵
            }
            else
            {
                getcenterpos(heng, out posi, out posj);
                kind = 2;//2表示横
            }
            i = posi;
            j = posj;
        }
        bool judge(point a, point b)
        {
            if (a.kind == 2 && b.kind == 1) return true;
            else if (a.kind == 1 && b.kind == 2) return false;
            else
            {
                if (a.posi < b.posi || (a.posi == b.posi && a.posj < b.posj)) return false;
                else return true;
            }
        }
        void point_sort(point[] cnt, int num)
        {
            point temp = new point();
            for (int i = 0; i < num; i++)
            {
                for (int j = i + 1; j < num; j++)
                {
                    if (judge(cnt[i], cnt[j]))
                    {
                        temp = cnt[i];
                        cnt[i] = cnt[j];
                        cnt[j] = temp;
                    }
                }
            }
        }
        private void pic_to_matrix_and_getname(int[,] matrix, string[] name)
        {
            point[] wuhujiang = new point[5];
            int caoposi, caoposj, s1posi, s1posj, s2posi, s2posj, s3posi, s3posj, s4posi, s4posj;
            wuhujiang[0].name = "关羽";
            wuhujiang[1].name = "张飞";
            wuhujiang[2].name = "赵云";
            wuhujiang[3].name = "黄忠";
            wuhujiang[4].name = "马超";
            getcenterpos(Cao, out caoposi, out caoposj);
            getcenterpos(Cao, out caoposi, out caoposj);
            getcenterpos(Soldier1, out s1posi, out  s1posj);
            getcenterpos(Soldier2, out  s2posi, out  s2posj);
            getcenterpos(Soldier3, out  s3posi, out  s3posj);
            getcenterpos(Soldier4, out s4posi, out s4posj);
            get_heng_zong_kind(Guan_Zong, Guan_Heng, out  wuhujiang[0].posi, out  wuhujiang[0].posj, out  wuhujiang[0].kind);
            get_heng_zong_kind(Zhang_Zong, Zhang_Heng, out wuhujiang[1].posi, out  wuhujiang[1].posj, out  wuhujiang[1].kind);
            get_heng_zong_kind(Zhao_Zong, Zhao_Heng, out wuhujiang[2].posi, out wuhujiang[2].posj, out wuhujiang[2].kind);
            get_heng_zong_kind(Huang_Zong, Huang_Heng, out wuhujiang[3].posi, out wuhujiang[3].posj, out wuhujiang[3].kind);
            get_heng_zong_kind(Ma_Zong, Ma_Heng, out  wuhujiang[4].posi, out  wuhujiang[4].posj, out wuhujiang[4].kind);
            /*textBox1.Text += Convert.ToString(wuhujiang[0].posi) + Convert.ToString(wuhujiang[0].posj) + Convert.ToString(wuhujiang[0].kind)+wuhujiang[0].name+"\r\n";
            textBox1.Text += Convert.ToString(wuhujiang[1].posi) + Convert.ToString(wuhujiang[1].posj) + Convert.ToString(wuhujiang[1].kind) + wuhujiang[1].name + "\r\n";
            textBox1.Text += Convert.ToString(wuhujiang[2].posi) + Convert.ToString(wuhujiang[2].posj) + Convert.ToString(wuhujiang[2].kind) + wuhujiang[2].name + "\r\n";
            textBox1.Text += Convert.ToString(wuhujiang[3].posi) + Convert.ToString(wuhujiang[3].posj) + Convert.ToString(wuhujiang[3].kind) + wuhujiang[3].name + "\r\n";
            textBox1.Text += Convert.ToString(wuhujiang[4].posi) + Convert.ToString(wuhujiang[4].posj) + Convert.ToString(wuhujiang[4].kind) + wuhujiang[4].name + "\r\n";*/
            point_sort(wuhujiang, 5);
            //textBox1.Text += wuhujiang[0].name + "  " + wuhujiang[1].name + "  " + wuhujiang[2].name + "  " + wuhujiang[3].name + "  " + wuhujiang[4].name;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    matrix[i, j] = 0;
                }
            }
            for (int i = 0; i < 5; i++)
            {
                name[i + 1] = wuhujiang[i].name;
                if (wuhujiang[i].kind == 2)
                {
                    matrix[wuhujiang[i].posi-1, wuhujiang[i].posj - 1] = matrix[wuhujiang[i].posi-1, wuhujiang[i].posj] = i + 1;
                }
                else if (wuhujiang[i].kind == 1)
                {
                    matrix[wuhujiang[i].posi - 1, wuhujiang[i].posj-1] = matrix[wuhujiang[i].posi, wuhujiang[i].posj-1] = i + 1;
                }
            }
            matrix[s1posi - 1, s1posj - 1] = matrix[s2posi - 1, s2posj - 1] = matrix[s3posi - 1, s3posj - 1] = matrix[s4posi - 1, s4posj - 1] = 7;
            matrix[caoposi - 1, caoposj - 1] = matrix[caoposi, caoposj - 1] = matrix[caoposi - 1, caoposj] = matrix[caoposi, caoposj] = 6;
          
        }
        private void matrix_pic(int[,] matrix,string[] name)
        {
            int i=0,j=0,s_num=1,blank_num=1,flag=0;
            for (i = 0; i < 5; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    if (matrix[i, j] == 6)
                    {
                        if (flag == 0) { creat_state(Cao, 2, 2, i * SingleGird + Frame.Top, j * SingleGird); flag = 1; }
                    }
                    else if (matrix[i, j] == 7)
                    {
                        if (s_num == 1) { creat_state(Soldier1, 1, 1, i * SingleGird + Frame.Top, j * SingleGird); s_num++; }
                        else if (s_num == 2) { creat_state(Soldier2, 1, 1, i * SingleGird + Frame.Top, j * SingleGird); s_num++; }
                        else if (s_num == 3) { creat_state(Soldier3, 1, 1, i * SingleGird + Frame.Top, j * SingleGird); s_num++; }
                        else if (s_num == 4) { creat_state(Soldier4, 1, 1, i * SingleGird + Frame.Top, j * SingleGird); s_num++; }
                    }
                    else if (matrix[i, j] == 0)
                    {
                        if (blank_num == 1) { creat_state(Blank1, 1, 1, i * SingleGird + Frame.Top, j * SingleGird); blank_num++; }
                        else if (blank_num == 2) { creat_state(Blank2, 1, 1, i * SingleGird + Frame.Top, j * SingleGird); blank_num++; }
                    }
                }
            }
            for (int wuhu = 1; wuhu <= 5; wuhu++)
            {
                for (i = 0; i < 5; i++)
                {
                    for (j = 0; j < 4; j++)
                    {
                        if (wuhu == matrix[i, j]) break;
                    }
                    if (j >= 4) continue;
                    else if(wuhu == matrix[i, j]) break;
                }
                int kind = 0;
                if (i < 4 && matrix[i + 1, j] == wuhu) kind = 1;//纵着放置的
                else kind = 2;//横着放置的
                if (name[wuhu] == "关羽")
                {
                    if (kind == 1) creat_state(Guan_Zong, 1, 2, Frame.Top + i * SingleGird, j * SingleGird);
                    else creat_state(Guan_Heng, 2, 1, Frame.Top + i * SingleGird, j * SingleGird);
                }
                else if (name[wuhu] == "张飞")
                {
                    if (kind == 1) creat_state(Zhang_Zong, 1, 2, Frame.Top + i * SingleGird, j * SingleGird);
                    else creat_state(Zhang_Heng, 2, 1, Frame.Top + i * SingleGird, j * SingleGird);
                }
                else if (name[wuhu] == "赵云")
                {
                    if (kind == 1) creat_state(Zhao_Zong, 1, 2, Frame.Top + i * SingleGird, j * SingleGird);
                    else creat_state(Zhao_Heng, 2, 1, Frame.Top + i * SingleGird, j * SingleGird);
                }
                else if (name[wuhu] == "马超")
                {
                    if (kind == 1) creat_state(Ma_Zong, 1, 2, Frame.Top + i * SingleGird, j * SingleGird);
                    else creat_state(Ma_Heng, 2, 1, Frame.Top + i * SingleGird, j * SingleGird);
                }
                else if (name[wuhu] == "黄忠")
                {
                    if (kind == 1) creat_state(Huang_Zong, 1, 2, Frame.Top + i * SingleGird, j * SingleGird);
                    else creat_state(Huang_Heng, 2, 1, Frame.Top + i * SingleGird, j * SingleGird);
                }
            }
        }
        private void clicktosearch_Click(object sender, EventArgs e)
        {
            if(SearchPath.search.ansform!=null)  SearchPath.search.ansform.Close();
            int[,] matrix = new int[5, 4];
            name = new string[10];
            ansinmatrix = new Queue<int[,]>();
            pic_to_matrix_and_getname(matrix, name);
            SearchPath.search.zong_num = SearchPath.search.get_zong_num(matrix);
            int judge= SearchPath.search.bfs(matrix,name,ref ansinmatrix);
            if (judge == 0)
            {
                MessageBox.Show("There is no answer for this situation！");
            }
            else if (judge == -1)
            {
                MessageBox.Show("Out of memory");
            }
            havemoved = false;
            step.Text = Convert.ToString(0);
        }
        private void nextstep_Click(object sender, EventArgs e)
        {
            if (cntstep < ansinmatrix.Count() - 1)
            {
                cntstep++;
                matrix_pic(ansinmatrix.ElementAt(cntstep), name);
                if (cntstep == ansinmatrix.Count()) { MessageBox.Show("OK！"); }
            }
            else MessageBox.Show("OK！");
        }

        private void prestep_Click(object sender, EventArgs e)
        {
            if (cntstep > 0)
            {
                cntstep--;
                matrix_pic(ansinmatrix.ElementAt(cntstep), name);
            }
        }

        private void stoporstart_Click(object sender, EventArgs e)
        {
            useinshow.Enabled = true;
        }

        private void useinshow_Tick(object sender, EventArgs e)
        {
            if (cntstep < ansinmatrix.Count())
            {
                matrix_pic(ansinmatrix.ElementAt(cntstep), name);
                step.Text = Convert.ToString(cntstep);
                cntstep++;
                if (cntstep == ansinmatrix.Count()) { MessageBox.Show("OK!"); useinshow.Enabled = false; stoporstart.Enabled = false; }
            }
        }

        private void 输入局面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchPath.inputstatus input = new SearchPath.inputstatus();
            input.Show();
            //this.Hide();
        }

        private void Guan_Zong_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
