using System;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace Lab3_Variant_21
{
    class Sort : ISort
    {
        private bool b_sorted = false;
        private int[] arr;
        private Graphics g;
        private int MaxVal;
        public void asc(int[] arr_In, Graphics g_In, int MaxVal_In)
        {
            arr = arr_In;
            g = g_In;
            MaxVal = MaxVal_In;

            while (!b_sorted)
            {
                for (int i = 0; i < arr.Count() - 1; i++)
                {
                    if (arr[i] > arr[i + 1])
                    {
                        int temp = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = temp;
                        Thread.Sleep(1);
                    }
                }
                b_sorted = IsSorted();
            }
        }
        public void desc(int[] arr_In, Graphics g_In, int MaxVal_In)
        {
            arr = arr_In;
            g = g_In;
            MaxVal = MaxVal_In;

            while (!b_sorted)
            {
                for (int i = arr.Count() - 1; i > 0; i--)
                {
                    if (arr[i] < arr[i - 1])
                    {
                        int temp = arr[i];
                        arr[i] = arr[i - 1];
                        arr[i - 1] = temp;
                        Thread.Sleep(1);
                    }
                }
                b_sorted = IsSorted();
            }
        }
        public void Draw()
        {
            while (!b_sorted)
            {
                g.Clear(Color.Black);
                for (int i = 0; i < 80; i++)
                {
                    g.FillRectangle(new SolidBrush(Color.White), i * 10, MaxVal - arr[i], 5, MaxVal);
                }
                Thread.Sleep(50);                
            }
        }
        private bool IsSorted()
        {
            for (int i = 0; i < arr.Count() - 1; i++)
            {
                if (arr[i] > arr[i + 1]) return false;
            }
            return true;
        }
    }
}
