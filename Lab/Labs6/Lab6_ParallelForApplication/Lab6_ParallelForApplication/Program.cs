using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6_ParallelForApplication
{
    class Program
    {
        static int Max = 1;
        static void MaxNumber(int c)
        {
            Random rnd = new Random();
            int v = rnd.Next(0, c);

            if (Max < v)
            {
                Max = v;
            }
        }
        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 1; i <= 10000000; i++)
            {
                MaxNumber(i);
            }

            long elapsed = sw.ElapsedMilliseconds;
            Console.WriteLine("Время выполнения алгоритма в миллисекундах: {0}", elapsed);
            Console.WriteLine("Максимальное число: {0} ", Max);

            Console.ReadLine();
        }
    }
}
