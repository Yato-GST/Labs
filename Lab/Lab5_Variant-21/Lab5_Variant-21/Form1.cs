using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;

namespace Lab5_Variant_21
{
    public partial class Form1 : Form
    {
        static int[] arr1 = new int[1000];
        static int[] arr2 = new int[2000];
        static int[] arr3 = new int[5000];
        static int[] arr4 = new int[10000];
        static int[] arr5 = new int[20000];
        Thread thr1;
        Thread thr2;
        Thread thr3;
        Thread thr4;
        Thread thr5;

        Random rand = new Random();
        public Form1()
        {
            InitializeComponent();
            thr1 = new Thread(Thr1);
            thr2 = new Thread(Thr2);
            thr3 = new Thread(Thr3);
            thr4 = new Thread(Thr4);
            thr5 = new Thread(Thr5);
        }
        static void Merge(int[] array, int lowIndex, int middleIndex, int highIndex)
        {
            var left = lowIndex;
            var right = middleIndex + 1;
            var tempArray = new int[highIndex - lowIndex + 1];
            var index = 0;

            while ((left <= middleIndex) && (right <= highIndex))
            {
                if (array[left] < array[right])
                {
                    tempArray[index] = array[left];
                    left++;
                }
                else
                {
                    tempArray[index] = array[right];
                    right++;
                }

                index++;
            }

            for (var i = left; i <= middleIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }

            for (var i = right; i <= highIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }

            for (var i = 0; i < tempArray.Length; i++)
            {
                array[lowIndex + i] = tempArray[i];
            }
        }

        //сортировка слиянием
        static int[] MergeSort(int[] array, int lowIndex, int highIndex)
        {
            if (lowIndex < highIndex)
            {
                var middleIndex = (lowIndex + highIndex) / 2;
                MergeSort(array, lowIndex, middleIndex);
                MergeSort(array, middleIndex + 1, highIndex);
                Merge(array, lowIndex, middleIndex, highIndex);
            }

            return array;
        }

        static int[] MergeSort(int[] array)
        {
            return MergeSort(array, 0, array.Length - 1);
        }
        private void Form1_Load(object sender, EventArgs e)
        {            
            for (int i = 0; i < arr1.Length; i++)
            {
                arr1[i] = rand.Next(1, 100);
            }
            for (int i = 0; i < arr2.Length; i++)
            {
                arr2[i] = rand.Next(1, 100);
            }
            for (int i = 0; i < arr3.Length; i++)
            {
                arr3[i] = rand.Next(1, 100);
            }
            for (int i = 0; i < arr4.Length; i++)
            {
                arr4[i] = rand.Next(1, 100);
            }
            for (int i = 0; i < arr5.Length; i++)
            {
                arr5[i] = rand.Next(1, 100);
            }

            // Форматировать диаграмму
            chart1.BackColor = Color.Gray;
            chart1.BackSecondaryColor = Color.WhiteSmoke;
            chart1.BackGradientStyle = GradientStyle.DiagonalRight;

            chart1.BorderlineDashStyle = ChartDashStyle.Solid;
            chart1.BorderlineColor = Color.Gray;
            chart1.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;

            // Форматировать область диаграммы
            chart1.ChartAreas[0].BackColor = Color.White;
            chart1.Series.Clear();

            chart1.Series.Add(new Series("LineSeries")
            {
                ChartType = SeriesChartType.Line
            });

            int a = 4, m;
            for (int i = 1; i < 50; i++)
            {
                m = (((2 * a) - i) / 2);
                if (m == 0)
                {
                    m = 1;
                }
                chart1.Series[0].Points.AddXY((i), (i * i * i) / m);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var s = textBox1.Text.Split(new[] { " ", ",", ";" }, StringSplitOptions.RemoveEmptyEntries);
            var array = new int[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                array[i] = Convert.ToInt32(s[i]);
            }
            textBox2.Text = string.Join(", ", MergeSort(array));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //textBox2.Text = string.Join(", ", MergeSort(arr1));
            thr1.Start();            
            thr2.Start();
            thr3.Start();
            thr4.Start();
            thr5.Start();
        }

        static void Thr1()
        {
            Form1 f1 = new Form1();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            f1.textBox2.Text = string.Join(", ", MergeSort(arr1));
            sw.Stop();
            f1.label4.Text = string.Join("Витрачено часу (мс): {0}", sw.ElapsedMilliseconds);
            f1.Refresh();
        }
        static void Thr2()
        {
            Form1 f1 = new Form1();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            f1.textBox2.Text = string.Join(", ", MergeSort(arr2));
            sw.Stop();
            f1.label5.Text = string.Join("Витрачено часу (мс): {0}", sw.ElapsedMilliseconds);
        }
        static void Thr3()
        {
            Form1 f1 = new Form1();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            f1.textBox2.Text = string.Join(", ", MergeSort(arr3));
            sw.Stop();
            f1.label6.Text = string.Join("Витрачено часу (мс): {0}", sw.ElapsedMilliseconds);
        }
        static void Thr4()
        {
            Form1 f1 = new Form1();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            f1.textBox2.Text = string.Join(", ", MergeSort(arr4));
            sw.Stop();
            f1.label7.Text = string.Join("Витрачено часу (мс): {0}", sw.ElapsedMilliseconds);
        }
        static void Thr5()
        {
            Form1 f1 = new Form1();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            f1.textBox2.Text = string.Join(", ", MergeSort(arr5));
            sw.Stop();
            f1.label8.Text = string.Join("Витрачено часу (мс): {0}", sw.ElapsedMilliseconds);
        }
    }
}
