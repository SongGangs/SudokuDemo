using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuDemo
{
    public partial class Form1 : Form
    {
        private int RandomNull =25;
        private  int[,]SudoNull=new int[9,9];
        public Form1()
        {
            InitializeComponent();
            SudoNull = GetOrginArray(Generate9Array(Generate3Aaary()));
            ShowResultInSceen(SudoNull);
        }

        /// <summary>
        /// 生成3个长度为3的数值 由1-9无重复组成
        /// </summary>
        private int[,] Generate3Aaary()
        {
            int[,] result = new int[3,3];
            int[] array=new int[9];
            Random random=new Random();
            List<int> list = new List<int>();
            for (int i = 1; i < 10; i++)
                list.Add(i);
            while (list.Count>0)
            {
                int k = random.Next(list.Count);
                array[9 - list.Count] = list[k];
                list.RemoveAt(k);
            }
            for (int j = 0; j < 9; j++)
            {
                result[j/3,j%3] = array[j];
                
            }
            return result;
        }


        /// <summary>
        /// 生成一个9*9的数组  
        /// 个人觉得算法可以优化
        /// </summary>
        /// <param name="array">3*3的数组</param>
        /// <returns></returns>
        private int[,] Generate9Array(int[,] array)
        {
            int[,]result=new int[9,9];
                for (int j = 0; j < 9; j++)
                {
                    result[0, j] = array[j/3, j%3];
                }
            for (int i = 1; i < 3; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (j<6)
                        result[i, j] = result[i-1, j + 3];
                    else
                        result[i, j] = result[i-1, j - 6];
                }
            }
            for (int j = 0; j < 9; j++)
            {
                for (int i = 3; i < 9; i++)
                {
                    if (j%3 == 0)
                    {
                        if (i < 6)
                            result[i, j] = result[i - 3, j + 1];
                        else
                            result[i, j] = result[i - 6, j + 2];
                    }
                    else  if (j % 3 == 1)
                    {
                        if (i < 6)
                            result[i, j] = result[i - 3, j + 1];
                        else
                            result[i, j] = result[i - 6, j -1];
                    }
                    else if (j%3 == 2)
                    {
                        if (i < 6)
                            result[i, j] = result[i - 3, j - 2];
                        else
                            result[i, j] = result[i - 6, j - 1];
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 在屏幕上展示
        /// </summary>
        /// <param name="array"></param>
        private void ShowResultInSceen(int[,] array)
        {
            for (int i = 0; i <9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    TextBox textBox = this.panel10.Controls[8-i].Controls[j] as TextBox;
                     textBox.Text=String.Empty;
                     textBox.Enabled = true;
                    if (array[i, j] != 0)
                    {
                        textBox.Text = array[i, j].ToString();
                        //textBox.ReadOnly = true;
                        textBox.Enabled = false;
                    }
                }
            }
        }

     
        /// <summary>
        /// 使用回溯算法求解
        /// </summary>
        /// <param name="n"></param>
        private void GetAnswer(int n,int[,]array)
        {
            if (n == 81)
            {//是否已经是最后一个格子
                Show();
                return;
            }

            int i = n / 9, j = n % 9;

            if (array[i, j] != 0)
            {//如果当前格子不需要填数字，就跳到下一个格子
                GetAnswer(n + 1,array);
                return;
            }

            for (int k = 0; k < 9; k++)
            {
                array[i, j]++;//当前格子进行尝试所有解
                if (IsValid(i, j,array))
                    GetAnswer(n + 1,array);//验证通过，就继续下一个
            }

            array[i, j] = 0;  //如果上面的单元无解，还原，就回溯
            return;
        }

        /// <summary>
        /// 生成一些空位的数组
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private int[,] GetOrginArray(int[,] array)
        {
            for (int i = 0; i < RandomNull; i++)
            {
                Random random = new Random();
                int k = random.Next(81);
                if (array[k / 9, k % 9]==0)
                    i--;
                else
                    array[k/9, k%9] = 0;
            }
            return array;
        }

        /// <summary>
        /// 验证函数
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private bool IsValid(int i, int j,int[,]array)
        {
            int n = array[i, j];
            int[] query = new int[9] { 0, 0, 0, 3, 3, 3, 6, 6, 6 };

            int t, u;
            //每一行每一列是否重复
            for (t = 0; t < 9; t++)
            {
                if ((t != i && array[t, j] == n) || (t != j && array[i, t] == n))
                    return false;
            }
            //每个九宫格是否重复
            for (t = query[i]; t < query[i] + 3; t++)
            {
                for (u = query[j]; u < query[j] + 3; u++)
                {
                    if ((t != i || u != j) && array[t, u] == n)
                        return false;
                }
            }
            return true;

        }

        private void 初级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomNull = 25;
        }

        private void 中级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomNull = 36;
        }

        private void 高级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomNull = 64;
        }

        private void 检验ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetAnswer(0, SudoNull);
        }

        private void 重新开始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SudoNull = GetOrginArray(Generate9Array(Generate3Aaary()));
            ShowResultInSceen(SudoNull);
        }
    }
}
