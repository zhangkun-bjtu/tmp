using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchPath
{
    class search
    {
        struct point
        {
            public int x, y;
        }
        struct status
        {
            public int cao;
            public int[] zong;//纵着放置的五虎将
            public int[] heng;//横着放置的五虎将
            public int[] bing;//下标从0开始
            public int[] universe;
            public int zongcntnum, hengcntnum;
            public void init()
            {
                cao = 0;
                zong = new int[5];
                heng = new int[5];
                bing = new int[4];
                universe = new int[6];
                zongcntnum = hengcntnum = 0;
            }
            public void zongclear()
            {
                zongcntnum = 0;
            }
            public void hengclear()
            {
                hengcntnum = 0;
            }
        }
        struct zk
        {
            public int pre;
            public int cnt, cnt_universe;
            public bool vis;
        }
        public static int zong_num;//纵将的个数
        public static showans ansform;
        public static int get_zong_num(int[,] matrix)
        {
            int ans = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (matrix[i + 1, j] == matrix[i, j] && matrix[i, j] != 7 && matrix[i, j] != 0 && matrix[i, j] != 6) ans++;
                }
            }
            return ans;
        }
        static void sort(int[] cnt, int cntnum)
        {
            for (int i = 0; i < cntnum; i++)
            {
                for (int j = i + 1; j < cntnum; j++)
                {
                    if (cnt[j] < cnt[i])
                    {
                        int temp = cnt[j];
                        cnt[j] = cnt[i];
                        cnt[i] = temp;
                    }
                }
            }
        }

        static void num_to_status(int num, int pre_universe, ref status zk)
        {
            int[] reference = { 1, 2, 3, 4, 5, 12, 13, 14, 15, 23, 24, 25, 34, 35, 45 };
            int i, j;
            int temp = num % 15; num /= 15;
            for (i = 0; i < 15; i++)
            {
                if (i == temp) break;
            }
            int temp1=reference[i]/10;
            int temp2=reference[i]%10;
            i = 0; j = 0;
            for (; j < 6; j++) if (j != temp1 && j != temp2) zk.bing[i++] = j;
            for (i = 1; i <= 5 - zong_num; i++) { zk.heng[zk.hengcntnum++] = (num % 12); num /= 12; }
            sort(zk.heng, zk.hengcntnum);
            for (i = 1; i <= zong_num; i++) { zk.zong[zk.zongcntnum++] = (num % 12); num /= 12; }
            sort(zk.zong, zk.zongcntnum);
            zk.cao = num;
            for (i = 5; i >= 1; i--) { zk.universe[i] = pre_universe % 5 + 1; pre_universe /= 5; }
        }
        static void status_to_num(status zk, ref int num, ref int pre_universe)
        {
            int[] reference = { 1, 2, 3, 4, 5, 12, 13, 14, 15, 23, 24, 25, 34, 35, 45 };
            num = pre_universe = 0;
            num += zk.cao;
            int[] temp = { 0, 0, 0, 0, 0, 0 };
            int i, j;
            for (i = 0; i < zk.zongcntnum; i++) { num *= 12; num += zk.zong[i]; };
            for (i = 0; i < zk.hengcntnum; i++) { num *= 12; num += zk.heng[i]; };
            for (i = 0; i < 4; i++) temp[zk.bing[i]] = 1;
            for (i = 0; i < 5; i++) if (temp[i] == 0) break;
            for (j = i + 1; j < 5; j++) if (temp[j]==0) break;
            int need = i * 10 + j;
            for (i = 0; i < 15; i++)
            {
                if (reference[i] == need) break;
            }
            num *= 15;
            num += i;
            pre_universe += (zk.universe[1] - 1);
            for (i = 2; i <= 5; i++) { pre_universe *= 5; pre_universe += (zk.universe[i] - 1); }
        }
        static void status_print(status cnt)
        {
            Console.WriteLine(cnt.cao);
            for (int i = 0; i < cnt.zongcntnum; i++)
            {
                Console.Write("{0} ", cnt.zong[i]);
            }
            Console.WriteLine();
            for (int i = 0; i < cnt.hengcntnum; i++)
            {
                Console.Write("{0} ", cnt.heng[i]);
            }
            Console.WriteLine();
            for (int i = 0; i < 4; i++)
            {
                Console.Write("{0} ", cnt.bing[i]);
            }
            Console.WriteLine();
            for (int i = 1; i < 6; i++)
            {
                Console.Write("{0} ", cnt.universe[i]);
            }
            Console.WriteLine();
        }
        static void matrix_print(int[,] matrix)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(" {0}", matrix[i, j]);
                }
                Console.WriteLine();
            }
        }
        static void status_to_matrix(status cnt, int[,] matrix)
        {
            int i, j, k;
            for (i = 0; i < 5; i++) for (j = 0; j < 4; j++) matrix[i, j] = 0;
            int caoposi = cnt.cao / 3, caoposj = cnt.cao % 3, temp = 0;
            matrix[caoposi, caoposj] = matrix[caoposi + 1, caoposj] = matrix[caoposi, caoposj + 1] = matrix[caoposi + 1, caoposj + 1] = 6;
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    if (matrix[i, j] != 6 && matrix[i + 1, j] != 6)
                    {
                        for (k = 0; k < cnt.zongcntnum; k++) if (cnt.zong[k] == temp) matrix[i, j] = matrix[i + 1, j] = k + 1;
                        temp++;
                    }
                }
            }
            temp = 0;
            for (i = 0; i < 5; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    if (!(((1 <= matrix[i, j] && matrix[i, j] <= cnt.zongcntnum) || matrix[i, j] == 6) || ((1 <= matrix[i, j + 1] && matrix[i, j + 1] <= cnt.zongcntnum) || matrix[i, j + 1] == 6)))
                    {
                        for (k = 0; k < cnt.hengcntnum; k++) if (cnt.heng[k] == temp) matrix[i, j] = matrix[i, j + 1] = cnt.zongcntnum + k + 1;
                        temp++;
                    }
                }
            }
            temp = 0;
            for (i = 0; i < 5; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        for (k = 0; k < 4; k++) if (cnt.bing[k] == temp) matrix[i, j] = 7;
                        temp++;
                    }
                }
            }
        }
        static void matrix_to_status(int[] universe, int[,] matrix, ref status ans)
        {
            int temp = 0, bing_num = 0, i, j;
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    if (matrix[i, j] == 6) break;
                    temp++;
                }
                if (matrix[i, j] == 6) break;
            }
            ans.cao = temp;
            temp = 0;
            ans.zongclear();
            ans.hengclear();
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    if (matrix[i, j] != 6 && matrix[i + 1, j] != 6)
                    {
                        if (matrix[i, j] != 0 && matrix[i, j] != 7 && matrix[i, j] == matrix[i + 1, j])
                        {
                            ans.zong[ans.zongcntnum++] = temp;
                        }
                        temp++;
                    }
                }
            }
            temp = 0;
            for (i = 0; i < 5; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    if (!(matrix[i, j] == 6 || matrix[i, j + 1] == 6 || (1 <= matrix[i, j] && matrix[i, j] <= ans.zongcntnum) || (1 <= matrix[i, j + 1] && matrix[i, j + 1] <= ans.zongcntnum)))
                    {
                        if (matrix[i, j] == matrix[i, j + 1] && matrix[i, j] != 0 && matrix[i, j] != 7) ans.heng[ans.hengcntnum++] = temp;
                        temp++;
                    }
                }
            }
            temp = 0;
            for (i = 0; i < 5; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    if (matrix[i, j] == 7 || matrix[i, j] == 0)
                    {
                        if (matrix[i, j] == 7) ans.bing[bing_num++] = temp;
                        temp++;
                    }
                }
            }
            int[,] cnt_matrix = new int[5, 4];
            status_to_matrix(ans, cnt_matrix);
            for (i = 1; i <= 5; i++) ans.universe[i] = universe[i];
            for (i = 0; i < 5; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    if (matrix[i, j] != cnt_matrix[i, j]) ans.universe[cnt_matrix[i, j]] = universe[matrix[i, j]];
                }
            }
        }
        static bool left_move(status cnt, int posi, int posj, ref status ans)//空格左边的东西过来
        {
            int[,] matrix = new int[5, 4];
            status_to_matrix(cnt, matrix);
            if (posj == 0) return false;//没有左边
            if (matrix[posi, posj - 1] == 0) return false;
            else if (matrix[posi, posj - 1] == 7)
            {//左边是兵
                matrix[posi, posj] = 7;
                matrix[posi, posj - 1] = 0;
                matrix_to_status(cnt.universe, matrix, ref ans);
                return true;
            }
            else if (cnt.zongcntnum < matrix[posi, posj - 1] && matrix[posi, posj - 1] <= 5)
            {//左边是横着放置的将{
                matrix[posi, posj] = matrix[posi, posj - 1];
                matrix[posi, posj - 2] = 0;
                matrix_to_status(cnt.universe, matrix, ref ans);
                return true;

            }
            else
            {//左边放着的是纵着的将或者是曹操
                if (posi > 0 && matrix[posi - 1, posj] == 0)
                {
                    if (matrix[posi, posj - 1] == 6 && matrix[posi - 1, posj - 1] == 6)
                    {//左边是曹操{
                        matrix[posi, posj] = matrix[posi - 1, posj] = 6;
                        matrix[posi, posj - 2] = matrix[posi - 1, posj - 2] = 0;
                        matrix_to_status(cnt.universe, matrix, ref ans);
                        return true;
                    }
                    else if (1 <= matrix[posi, posj - 1] && matrix[posi, posj - 1] <= cnt.zongcntnum && matrix[posi, posj - 1] == matrix[posi - 1, posj - 1])
                    {
                        matrix[posi, posj] = matrix[posi - 1, posj] = matrix[posi, posj - 1];
                        matrix[posi, posj - 1] = matrix[posi - 1, posj - 1] = 0;
                        matrix_to_status(cnt.universe, matrix, ref ans);
                        return true;
                    }
                    return false;
                }
                else if (posi < 4 && matrix[posi + 1, posj] == 0)
                {
                    if (matrix[posi, posj - 1] == 6 && matrix[posi + 1, posj - 1] == 6)
                    {//左边是曹操{
                        matrix[posi, posj] = matrix[posi + 1, posj] = 6;
                        matrix[posi, posj - 2] = matrix[posi + 1, posj - 2] = 0;
                        matrix_to_status(cnt.universe, matrix, ref ans);
                        return true;
                    }
                    else if (1 <= matrix[posi, posj - 1] && matrix[posi, posj - 1] <= cnt.zongcntnum && matrix[posi, posj - 1] == matrix[posi + 1, posj - 1])
                    {
                        matrix[posi, posj] = matrix[posi + 1, posj] = matrix[posi, posj - 1];
                        matrix[posi, posj - 1] = matrix[posi + 1, posj - 1] = 0;
                        matrix_to_status(cnt.universe, matrix, ref ans);
                        return true;
                    }
                    return false;
                }
                return false;
            }
        }
        static bool right_move(status cnt, int posi, int posj, ref status ans)//空格右边的东西过来
        {
            int[,] matrix = new int[5, 4];
            status_to_matrix(cnt, matrix);
            if (posj == 3) return false;//没有右边
            if (matrix[posi, posj + 1] == 0) return false;
            else if (matrix[posi, posj + 1] == 7)
            {//右边是兵
                matrix[posi, posj] = 7;
                matrix[posi, posj + 1] = 0;
                matrix_to_status(cnt.universe, matrix, ref ans);
                return true;
            }
            else if (cnt.zongcntnum < matrix[posi, posj + 1] && matrix[posi, posj + 1] <= 5)
            {//左边是横着放置的将{
                matrix[posi, posj] = matrix[posi, posj + 1];
                matrix[posi, posj + 2] = 0;
                matrix_to_status(cnt.universe, matrix, ref ans);
                return true;

            }
            else
            {//左边放着的是纵着的将或者是曹操
                if (posi > 0 && matrix[posi - 1, posj] == 0)
                {
                    if (matrix[posi, posj + 1] == 6 && matrix[posi - 1, posj + 1] == 6)
                    {//左边是曹操{
                        matrix[posi, posj] = matrix[posi - 1, posj] = 6;
                        matrix[posi, posj + 2] = matrix[posi - 1, posj + 2] = 0;
                        matrix_to_status(cnt.universe, matrix, ref ans);
                        return true;
                    }
                    else if (1 <= matrix[posi, posj + 1] && matrix[posi, posj + 1] <= cnt.zongcntnum && matrix[posi, posj + 1] == matrix[posi - 1, posj + 1])
                    {
                        matrix[posi, posj] = matrix[posi - 1, posj] = matrix[posi, posj + 1];
                        matrix[posi, posj + 1] = matrix[posi - 1, posj + 1] = 0;
                        matrix_to_status(cnt.universe, matrix, ref ans);
                        return true;
                    }
                    return false;
                }
                else if (posi < 4 && matrix[posi + 1, posj] == 0)
                {
                    if (matrix[posi, posj + 1] == 6 && matrix[posi + 1, posj + 1] == 6)
                    {//左边是曹操
                        matrix[posi, posj] = matrix[posi + 1, posj] = 6;
                        matrix[posi, posj + 2] = matrix[posi + 1, posj + 2] = 0;
                        matrix_to_status(cnt.universe, matrix, ref ans);
                        return true;
                    }
                    else if (1 <= matrix[posi, posj + 1] && matrix[posi, posj + 1] <= cnt.zongcntnum && matrix[posi, posj + 1] == matrix[posi + 1, posj + 1])
                    {
                        matrix[posi, posj] = matrix[posi + 1, posj] = matrix[posi, posj + 1];
                        matrix[posi, posj + 1] = matrix[posi + 1, posj + 1] = 0;
                        matrix_to_status(cnt.universe, matrix, ref ans);
                        return true;
                    }
                    return false;
                }
                return false;
            }
        }
        static bool up_move(status cnt, int posi, int posj, ref status ans)//空格上面的东西下来
        {
            int[,] matrix = new int[5, 4];
            status_to_matrix(cnt, matrix);
            if (posi == 0) return false;//没有上面
            if (matrix[posi - 1, posj] == 0) return false;
            else if (matrix[posi - 1, posj] == 7)
            {//上边是兵
                matrix[posi, posj] = 7;
                matrix[posi - 1, posj] = 0;
                matrix_to_status(cnt.universe, matrix, ref ans);
                return true;
            }
            else if (1 <= matrix[posi - 1, posj] && matrix[posi - 1, posj] <= cnt.zongcntnum)
            {//上边是纵着放置的将
                matrix[posi, posj] = matrix[posi - 1, posj];
                matrix[posi - 2, posj] = 0;
                matrix_to_status(cnt.universe, matrix, ref ans);
                return true;
            }
            else
            {//上边放着的是横着的将或者是曹操
                if (posj > 0 && matrix[posi, posj - 1] == 0)
                {
                    if (matrix[posi - 1, posj] == 6 && matrix[posi - 1, posj - 1] == 6)
                    {//上面边是曹操{
                        matrix[posi, posj] = matrix[posi, posj - 1] = 6;
                        matrix[posi - 2, posj] = matrix[posi - 2, posj - 1] = 0;
                        matrix_to_status(cnt.universe, matrix, ref ans);
                        return true;
                    }
                    else if (cnt.zongcntnum < matrix[posi - 1, posj] && matrix[posi - 1, posj - 1] <= 5 && matrix[posi - 1, posj] == matrix[posi - 1, posj - 1])
                    {
                        matrix[posi, posj] = matrix[posi, posj - 1] = matrix[posi - 1, posj];
                        matrix[posi - 1, posj] = matrix[posi - 1, posj - 1] = 0;
                        matrix_to_status(cnt.universe, matrix, ref ans);
                        return true;
                    }
                    return false;
                }
                else if (posj < 3 && matrix[posi, posj + 1] == 0)
                {
                    if (matrix[posi - 1, posj] == 6 && matrix[posi - 1, posj + 1] == 6)
                    {//上面边是曹操
                        matrix[posi, posj] = matrix[posi, posj + 1] = 6;
                        matrix[posi - 2, posj] = matrix[posi - 2, posj + 1] = 0;
                        matrix_to_status(cnt.universe, matrix, ref ans);
                        return true;
                    }
                    else if (cnt.zongcntnum < matrix[posi - 1, posj + 1] && matrix[posi - 1, posj + 1] <= 5 && matrix[posi - 1, posj] == matrix[posi - 1, posj + 1])
                    {
                        matrix[posi, posj] = matrix[posi, posj + 1] = matrix[posi - 1, posj];
                        matrix[posi - 1, posj] = matrix[posi - 1, posj + 1] = 0;
                        matrix_to_status(cnt.universe, matrix, ref ans);
                        return true;
                    }
                    return false;
                }
                return false;
            }
        }
        static bool down_move(status cnt, int posi, int posj, ref status ans)//空格下面的东西上来
        {
            int[,] matrix = new int[5, 4];
            status_to_matrix(cnt, matrix);
            if (posi == 4) return false;//没有下面
            if (matrix[posi + 1, posj] == 0) return false;
            else if (matrix[posi + 1, posj] == 7)
            {//上边是兵
                matrix[posi, posj] = 7;
                matrix[posi + 1, posj] = 0;
                matrix_to_status(cnt.universe, matrix, ref ans);
                return true;
            }
            else if (1 <= matrix[posi + 1, posj] && matrix[posi + 1, posj] <= cnt.zongcntnum)
            {//上边是纵着放置的将
                matrix[posi, posj] = matrix[posi + 1, posj];
                matrix[posi + 2, posj] = 0;
                matrix_to_status(cnt.universe, matrix, ref ans);
                return true;
            }
            else
            {//上边放着的是横着的将或者是曹操
                if (posj > 0 && matrix[posi, posj - 1] == 0)
                {
                    if (matrix[posi + 1, posj] == 6 && matrix[posi + 1, posj - 1] == 6)
                    {//上面边是曹操{
                        matrix[posi, posj] = matrix[posi, posj - 1] = 6;
                        matrix[posi + 2, posj] = matrix[posi + 2, posj - 1] = 0;
                        matrix_to_status(cnt.universe, matrix, ref ans);
                        return true;
                    }
                    else if (cnt.zongcntnum < matrix[posi + 1, posj] && matrix[posi + 1, posj - 1] <= 5 && matrix[posi + 1, posj] == matrix[posi + 1, posj - 1])
                    {
                        matrix[posi, posj] = matrix[posi, posj - 1] = matrix[posi + 1, posj];
                        matrix[posi + 1, posj] = matrix[posi + 1, posj - 1] = 0;
                        matrix_to_status(cnt.universe, matrix, ref ans);
                        return true;
                    }
                    return false;
                }
                else if (posj < 3 && matrix[posi, posj + 1] == 0)
                {
                    if (matrix[posi + 1, posj] == 6 && matrix[posi + 1, posj + 1] == 6)
                    {//上面边是曹操
                        matrix[posi, posj] = matrix[posi, posj + 1] = 6;
                        matrix[posi + 2, posj] = matrix[posi + 2, posj + 1] = 0;
                        matrix_to_status(cnt.universe, matrix, ref ans);
                        return true;
                    }
                    else if (cnt.zongcntnum < matrix[posi + 1, posj + 1] && matrix[posi + 1, posj + 1] <= 5 && matrix[posi + 1, posj] == matrix[posi + 1, posj + 1])
                    {
                        matrix[posi, posj] = matrix[posi, posj + 1] = matrix[posi + 1, posj];
                        matrix[posi + 1, posj] = matrix[posi + 1, posj + 1] = 0;
                        matrix_to_status(cnt.universe, matrix, ref ans);
                        return true;
                    }
                    return false;
                }
                return false;
            }
        }
        static bool move(status cnt, int num, int kind, ref status ans)
        {
            int posi1 = -1, posi2 = -1, posj1 = -1, posj2 = -1;
            int[,] matrix = new int[5, 4];
            status_to_matrix(cnt, matrix);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (matrix[i, j] == 0 && posi1 == -1) { posi1 = i; posj1 = j; }
                    else if (matrix[i, j] == 0) { posi2 = i; posj2 = j; }
                }
            }
            if (kind == 1)
            {
                if (num == 1 && up_move(cnt, posi1, posj1, ref ans)) return true;
                else if (num == 2 && up_move(cnt, posi2, posj2, ref ans)) return true;
            }
            else if (kind == 2)
            {
                if (num == 1 && down_move(cnt, posi1, posj1, ref ans)) return true;
                else if (num == 2 && down_move(cnt, posi2, posj2, ref ans)) return true;
            }
            else if (kind == 3)
            {
                if (num == 1 && left_move(cnt, posi1, posj1, ref ans)) return true;
                else if (num == 2 && left_move(cnt, posi2, posj2, ref ans)) return true;
            }
            else if (kind == 4)
            {
                if (num == 1 && right_move(cnt, posi1, posj1, ref ans)) return true;
                else if (num == 2 && right_move(cnt, posi2, posj2, ref ans)) return true;
            }
            return false;
        }
        static bool judge(status temp)
        {
            int[,] matrix = new int[5, 4];
            status_to_matrix(temp, matrix);
            if (matrix[4, 1] == 6 && matrix[4, 2] == 6) return true;
            else return false;
        }
        public static int bfs(int[,] matrix, string[] name, ref Queue<int[,]> ansinmatrix)
        {
            progress haha = new progress();
            haha.Show();
            zk[] data;
            try
            {
                data = new zk[45000000];
            }
            catch (System.OutOfMemoryException ex)
            {
                haha.Close();
                return -1;
            }
            int i, j;
            for (i = 0; i < 45000000; i++) data[i].vis = false;
            status init = new status();
            init.init();
            int[] universe = { 0, 1, 2, 3, 4, 5 };
            int flag = 0;
            matrix_to_status(universe, matrix, ref init);
            zk start;
            start.cnt = start.cnt_universe = start.pre = 0;
            start.vis = false;
            start.pre = -1; start.vis = true; status_to_num(init, ref start.cnt, ref start.cnt_universe);
            Queue<zk> key = new Queue<zk>();
            key.Enqueue(start);
            int end = start.cnt;
            while (key.Count != 0)
            {
                zk zk_temp = key.ElementAt(0);
                key.Dequeue();
                status cnt = new status();
                cnt.init();
                num_to_status(zk_temp.cnt, zk_temp.cnt_universe, ref cnt);
                for (i = 1; i <= 2; i++)
                {
                    for (j = 1; j <= 4; j++)
                    {
                        status temp = new status();
                        temp.init();
                        if (move(cnt, i, j, ref temp))
                        {
                            int temp_num = 0, temp_universe = 0;
                            status_to_num(temp, ref temp_num, ref temp_universe);
                            if (!data[temp_num].vis)
                            {
                                data[temp_num].vis = true;
                                data[temp_num].cnt = temp_num; data[temp_num].cnt_universe = temp_universe; data[temp_num].pre = zk_temp.cnt;
                                zk zk_temp1;
                                zk_temp1.cnt = temp_num; zk_temp1.cnt_universe = temp_universe; zk_temp1.pre = zk_temp.cnt;
                                zk_temp1.vis = true;
                                key.Enqueue(zk_temp1);
                                if (judge(temp)) { flag = zk_temp1.cnt; break; }
                            }
                        } if (flag != 0) break;
                    } if (flag != 0) break;
                } if (flag != 0) break;
            }
            haha.Close();
            if (flag == 0)
            {
                return 0;
            }
            Queue<int> yes = new Queue<int>();
            yes.Enqueue(flag);
            while (true)
            {
                flag = data[flag].pre;
                yes.Enqueue(flag);
                if (flag == end) break;
            }
            status getansmaxtrix = new status();
            getansmaxtrix.init();
            for (i = yes.Count() - 1; i >= 0; i--)
            {
                int[,] ansmatrix = { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
                getansmaxtrix.init();
                num_to_status(data[yes.ElementAt(i)].cnt, data[yes.ElementAt(i)].cnt_universe, ref getansmaxtrix);
                status_to_matrix(getansmaxtrix, ansmatrix);
                for (int ii = 0; ii < 5; ii++)
                {
                    for (int jj = 0; jj < 4; jj++)
                    {
                        if (1 <= ansmatrix[ii, jj] && ansmatrix[ii, jj] <= 5)
                        {
                            ansmatrix[ii, jj] = getansmaxtrix.universe[ansmatrix[ii, jj]];
                        }
                    }
                }
                ansinmatrix.Enqueue(ansmatrix);
            }
            /*inputstatus haha = new inputstatus();
            haha.Show();
            for (i = 0; i <ansinmatrix.Count(); i++)
            {
                haha.show_num(i);
                haha.show_matrix(ansinmatrix.ElementAt(i));
            }*/
            ansform = new showans();
            ansform.Show();
            int[,] pre_matrix = new int[5, 4];
            int[,] cnt_matrix = new int[5, 4];
            status prestatus = new status();
            status cntstatus = new status();
            prestatus.init();
            cntstatus.init();
            name[6] = "曹操";
            for (int fuck = yes.Count() - 1; fuck >= 1; fuck--)
            {
                prestatus.init();
                cntstatus.init();
                num_to_status(data[yes.ElementAt(fuck)].cnt, data[yes.ElementAt(fuck)].cnt_universe, ref prestatus);
                num_to_status(data[yes.ElementAt(fuck - 1)].cnt, data[yes.ElementAt(fuck - 1)].cnt_universe, ref cntstatus);
                ansform.write_step(yes.Count() - fuck);
                status_to_matrix(prestatus, pre_matrix);
                for (i = 0; i < 5; i++)
                    for (j = 0; j < 4; j++)
                        if (1 <= pre_matrix[i, j] && pre_matrix[i, j] <= 5) pre_matrix[i, j] = prestatus.universe[pre_matrix[i, j]];
                status_to_matrix(cntstatus, cnt_matrix);
                for (i = 0; i < 5; i++)
                    for (j = 0; j < 4; j++)
                        if (1 <= cnt_matrix[i, j] && cnt_matrix[i, j] <= 5) cnt_matrix[i, j] = cntstatus.universe[cnt_matrix[i, j]];
                int[,] pre_pos = new int[11, 2];
                int[,] cnt_pos = new int[11, 2];
                for (i = 0; i < 11; i++) { for (j = 0; j < 2; j++) { pre_pos[i, j] = 0; } }
                for (i = 0; i < 11; i++) { for (j = 0; j < 2; j++) { cnt_pos[i, j] = 0; } }
                Queue<point> pre_bing = new Queue<point>();
                Queue<point> cnt_bing = new Queue<point>();
                point temp = new point();
                for (i = 0; i < 5; i++)
                {
                    for (j = 0; j < 4; j++)
                    {
                        if (pre_matrix[i, j] == 7) { temp.x = i + 1; temp.y = j + 1; pre_bing.Enqueue(temp); }
                        else if (pre_pos[pre_matrix[i, j], 0] == 0) { pre_pos[pre_matrix[i, j], 0] = i + 1; pre_pos[pre_matrix[i, j], 1] = j + 1; }
                        if (cnt_matrix[i, j] == 7) { temp.x = i + 1; temp.y = j + 1; cnt_bing.Enqueue(temp); }
                        else if (cnt_pos[cnt_matrix[i, j], 0] == 0) { cnt_pos[cnt_matrix[i, j], 0] = i + 1; cnt_pos[cnt_matrix[i, j], 1] = j + 1; }
                    }
                }
                int move_flag = 0;
                for (i = 1; i <= 6; i++)
                {
                    if (pre_pos[i, 0] - cnt_pos[i, 0] == 1) { ansform.write_word(name[i]); ansform.write_word_line("向上移动一格"); move_flag = 1; }
                    else if (pre_pos[i, 0] - cnt_pos[i, 0] == -1) { ansform.write_word(name[i]); ansform.write_word_line("向下移动一格"); move_flag = 1; }
                    if (pre_pos[i, 1] - cnt_pos[i, 1] == 1) { ansform.write_word(name[i]); ansform.write_word_line("向左移动一格"); move_flag = 1; }
                    if (pre_pos[i, 1] - cnt_pos[i, 1] == -1) { ansform.write_word(name[i]); ansform.write_word_line("向右移动一格"); move_flag = 1; }
                }
                if (move_flag == 1) continue;
                int[] pre_bing_vis = { 0, 0, 0, 0 };
                int[] cnt_bing_vis = { 0, 0, 0, 0 };
                for (i = 0; i < 4; i++)
                {
                    for (j = 0; j < 4; j++)
                    {
                        if (pre_bing.ElementAt(i).x == cnt_bing.ElementAt(j).x && pre_bing.ElementAt(i).y == cnt_bing.ElementAt(j).y) { pre_bing_vis[i] = cnt_bing_vis[j] = 1; }
                    }
                }
                int posi, posj;
                for (posi = 0; posi < 4; posi++) if (pre_bing_vis[posi] == 0) break;
                for (posj = 0; posj < 4; posj++) if (cnt_bing_vis[posj] == 0) break;
                if (pre_bing.ElementAt(posi).x - cnt_bing.ElementAt(posj).x == 1) { ansform.write_soilder(pre_bing.ElementAt(posi).x, pre_bing.ElementAt(posi).y, "向上移一位"); continue; }
                else if (pre_bing.ElementAt(posi).x - cnt_bing.ElementAt(posj).x == -1) { ansform.write_soilder(pre_bing.ElementAt(posi).x, pre_bing.ElementAt(posi).y, "向下移一位"); continue; }
                else if (pre_bing.ElementAt(posi).y - cnt_bing.ElementAt(posj).y == 1) { ansform.write_soilder(pre_bing.ElementAt(posi).x, pre_bing.ElementAt(posi).y, "向左移一位"); continue; }
                else if (pre_bing.ElementAt(posi).y - cnt_bing.ElementAt(posj).y == -1) { ansform.write_soilder(pre_bing.ElementAt(posi).x, pre_bing.ElementAt(posi).y, "向右移一位"); continue; }
                prestatus.init();
                cntstatus.init();
            }
            return 1;
        }
    }
}