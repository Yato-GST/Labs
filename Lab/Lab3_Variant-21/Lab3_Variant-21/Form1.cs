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

namespace Lab3_Variant_21
{
    public partial class Form1 : Form
    {
        int[] arr;
        Graphics g;
        public Form1()
        {
            InitializeComponent();
        }
        private void PrintArray()
        {
            g.Clear(Color.Black);
            for (int i = 0; i < 80; i++)
            {
                g.FillRectangle(new SolidBrush(Color.White), i * 10, panel1.Height - arr[i], 5, panel1.Height);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            g = panel1.CreateGraphics();
            int NumEntries = 80;
            int MaxVal = panel1.Height;
            arr = new int[NumEntries];

            g.FillRectangle(new SolidBrush(Color.Black), 0, 0, panel1.Width, MaxVal);
            Random r = new Random();

            for (int i = 0; i < NumEntries; i++)
            {
                arr[i] = r.Next(0, MaxVal);
            }

            for (int i = 0; i < NumEntries; i++)
            {               
                g.FillRectangle(new SolidBrush(Color.White), i * 10, MaxVal - arr[i], 5, MaxVal);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ISort se = new Sort();

            Parallel.Invoke(() => se.asc(arr, g, panel1.Height),        //сортировка по возрастанию
                            () => se.desc(arr, g, panel1.Height),       //descending sorting
                            () => { Thread.Sleep(10); se.Draw(); });    //перерисовка массива

            Thread.Sleep(10);
            PrintArray();
        }        
    }
}
