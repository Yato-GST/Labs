using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Lab2_Variant_21
{
    class Program
    {
        static int[] arr;
        static int totalSum;
        static void getSum(object o)
        {
            int start = ((int[])o)[0];
            int end = ((int[])o)[1];
            int sum = 0;

            for (int i = start; i < end; i++)
            {
                try
                {
                    if (arr[0] != arr[i])
                    {
                        sum ++;
                    }
                }
                catch (DivideByZeroException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            totalSum += sum;
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            int arrNum = 1, threadNum = 1;

            try
            {                
                Console.Write("Кількість елементів масиву: ");
                arrNum = int.Parse(Console.ReadLine());
                if (arrNum <= 0)
                    throw new ArgumentOutOfRangeException("Від’ємне число не допускається");

                Console.Write("Кількість потоків: ");
                threadNum = int.Parse(Console.ReadLine());

                if (threadNum < 1)
                    throw new ArgumentOutOfRangeException("Від’ємне число не допускається");
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }

            Random rand = new Random();
            try
            {
                arr = new int[arrNum];
            }
            catch
            {
                Console.WriteLine("Error");
            }

            for (int i = 0; i < arr.Length; i++)
                arr[i] = rand.Next(0, 2);

            for (int i = 0; i < arr.Length; i++)
            {
                if(arr[0] == arr[i])
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(arr[i]+" ");
            }
            Console.ResetColor();
            Console.WriteLine();

            if (threadNum == 1)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                Thread thr = new Thread(() => { getSum(new int[] { 0, arrNum }); });
                thr.Start();
                thr.Join();

                sw.Stop();
                Console.WriteLine("Витрачено часу (мс): {0}", sw.ElapsedMilliseconds);
                Console.WriteLine("Сума елементів масиву, що відрізняються від першого елемента: {0}", totalSum);
            }
            else
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                int elemPerThread = arrNum / threadNum;
                int startIndex = -elemPerThread;
                int endIndex = 0;

                Thread[] arrThr = new Thread[threadNum];
                for (int i = 0; i < threadNum; i++)
                {
                    arrThr[i] = new Thread(() => { getSum(new int[] { startIndex += elemPerThread, endIndex += elemPerThread }); });
                    arrThr[i].Start();
                }

                for (int j = 0; j < threadNum; j++)
                    arrThr[j].Join();

                stopwatch.Stop();
                Console.WriteLine("Витрачено часу (мс): {0}", stopwatch.ElapsedMilliseconds);
                Console.WriteLine("Сума елементів масиву, що відрізняються від першого елемента: {0}", totalSum);
            }

            Console.ReadLine();
        }
    }
}
