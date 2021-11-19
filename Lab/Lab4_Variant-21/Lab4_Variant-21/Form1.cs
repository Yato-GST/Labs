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
using System.Drawing.Drawing2D;

namespace Lab4_Variant_21
{
    public partial class Form1 : Form
    {
        Thread thread1;
        Thread thread2;
        Thread thread3;
        Thread thread4;
        Thread thr_right;
        Thread thr_left;
        Thread thr_down;
        Graphics g;
        Rectangle r = new Rectangle(20, 20, 100, 50);
        Pen blackPen = new Pen(Color.Black, 3);
        static object locker = new object();
        Random rand = new Random();
        //GraphicsState transState;
        public Form1()
        {
            InitializeComponent();
            thread1 = new Thread(new ThreadStart(Rect1));
            thread2 = new Thread(new ThreadStart(Rect2));
            thread3 = new Thread(new ThreadStart(Rect3));
            thread4 = new Thread(new ThreadStart(Rect4));
            thr_right = new Thread(new ThreadStart(R));
            thr_left = new Thread(new ThreadStart(L));
            thr_down = new Thread(new ThreadStart(D));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            g = pictureBox1.CreateGraphics();            
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();
            //transState = g.Save();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            r.X = 380;
            g.DrawRectangle(blackPen, r);
            thr_right.Start();
            thr_left.Start();
            thr_down.Start();
        }
        private void Rect1()
        {
            while (true)
            {
                lock (locker)
                {
                    //Bitmap btm;
                    r.X = rand.Next(10, pictureBox1.Width - 100);
                    r.Y = rand.Next(10, pictureBox1.Height - 50);
                    //g.Restore(transState);
                    g.DrawRectangle(blackPen, r);
                    //transState = g.Save();
                    Thread.Sleep(500);
                }
            }
        }
        private void Rect2()
        {
            while (true)
            {
                lock (locker)
                {
                    r.X = rand.Next(10, pictureBox1.Width - 100);
                    r.Y = rand.Next(10, pictureBox1.Height - 50);
                    //g.Restore(transState);
                    g.DrawRectangle(blackPen, r);
                    //transState = g.Save();
                    Thread.Sleep(500);
                   // g.Clear(Color.White);
                }
            }
        }
        private void Rect3()
        {
            while (true)
            {
                lock (locker)
                {
                    r.X = rand.Next(10, pictureBox1.Width - 100);
                    r.Y = rand.Next(10, pictureBox1.Height - 50);
                    //g.Restore(transState);
                    g.DrawRectangle(blackPen, r);
                    //transState = g.Save();
                    Thread.Sleep(500);
                    //g.Clear(Color.White);
                }
            }
        }
        private void Rect4()
        {
            while (true)
            {
                lock (locker)
                {
                    r.X = rand.Next(10, pictureBox1.Width - 100);
                    r.Y = rand.Next(10, pictureBox1.Height - 50);
                    //g.Restore(transState);
                    g.DrawRectangle(blackPen, r);
                    //transState = g.Save();
                    Thread.Sleep(500);
                    g.Clear(Color.White);
                }
            }
        }
        //поток для движения вправо
        private void R()
        {           
            MutexS.mtx.WaitOne();
            r.X += 10;            
            pictureBox1.Invoke((MethodInvoker)delegate ()
            {
                pictureBox1.Refresh();
            });
            g.DrawRectangle(blackPen, r);
            Thread.Sleep(10);
            MutexS.mtx.ReleaseMutex();
            R();
        }
        //поток для движения влево
        private void L()
        {
            MutexS.mtx.WaitOne();            
            r.X -= 10;                            
            pictureBox1.Invoke((MethodInvoker)delegate ()
            {
                pictureBox1.Refresh();
            });
            g.DrawRectangle(blackPen, r);
            Thread.Sleep(10);
            MutexS.mtx.ReleaseMutex();
            L();
        }
        //поток для движения вниз
        private void D()
        {
            while (r.Y <= pictureBox1.Height - 2 * r.Height)
            {
                r.Y += 5;
                Thread.Sleep(10);
            }
            if (r.Y >= pictureBox1.Height - 100)
                r.Y = 10; 
            D();
        }
        class MutexS
        {
            public static Mutex mtx = new Mutex();
        }
    }
}
