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
        public Form1()
        {
            InitializeComponent();
            ShowResultInSceen(Generate9Array(Generate3Aaary()));
        }

        /// <summary>
        /// 生成3个长度为3的数值 由1-9无重复组成
        /// </summary>
        private int[,] Generate3Aaary()
        {
            int[,] result = new int[3,3];
            int[] array=new int[9];
            Random random=new Random();
            List<int>list=new List<int>();
            for (int i = 1; i < 10; i++)
            {
                list.Add(i);
            }
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

        private int[,] Generate9Array(int[,] array)
        {
            int[,]result=new int[9,9];
           // for (int i = 0; i < 9; i++)
            //{
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
            //}
            return result;
        }

        private void ShowResultInSceen(int[,] array)
        {
            for (int i = 0; i <9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    TextBox textBox = this.panel10.Controls[8-i].Controls[j] as TextBox;
                    textBox.Text = array[i, j].ToString();
                }
            }
        }
    }
}
