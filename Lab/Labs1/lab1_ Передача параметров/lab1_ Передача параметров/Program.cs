using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace lab1__Передача_параметров
{
    class Program
    {
        static long Factorial(long n)
        {
            long res = 1;
            do
            {
                res = res * n;
            } while (--n > 0);
            return res;
        }

        static double res;
        static void ThreadWork(object state)
        {
            string sTitle = ((object[])state)[0] as string;
            double d = (double)(((object[])state)[1]);
            Console.WriteLine(sTitle);
            res = Math.Sqrt(d);
        }

        static void Main()
        {
            Console.WriteLine("Выбирите пример(1...3)?");
            string namber_example = Console.ReadLine();

            switch (namber_example)
            {
                case "1":
                    long res1 = 0, res2 = 0;
                    long n1 = 5, n2 = 10;
                    Thread t1 = new Thread(() =>
                    {
                        res1 = Factorial(n1);
                    });
                    Thread t2 = new Thread(() =>
                    {
                        res2 = Factorial(n2);
                    });
                    // Start threads 
                    t1.Start(); t2.Start();
                    // Waiting for the threads to complete 
                    t1.Join(); t2.Join();
                    Console.WriteLine("Factorial of {0} equals {1}",
                                 n1, res1);
                    Console.WriteLine("Factorial of {0} equals {1}",
                                n2, res2);
                    Console.Read();
                    break;
                case "2":
                    Thread thr1 = new Thread(ThreadWork);
                    thr1.Start(new object[] { "Thread #1", 3.14 });
                    thr1.Join();
                    Console.WriteLine("Result: {0}", res);
                    Console.Read();
                    break;
               case "3":
                    for (int i = 0; i < 10; i++)
                    {
                        Thread t = new Thread(() =>
                            Console.Write("ABCDEFGHIJK"[i])); 
                        t.Start();
                    }
                    Console.WriteLine();
                    Console.WriteLine("с дополнительной переменной");
                    for (int i = 0; i < 10; i++)
                    {
                        int i_copy = i;
                        Thread t = new Thread(() =>
                            Console.Write("ABCDEFGHIJK"[i_copy]));
                        t.Start();
                    }
                    Console.Read();
                    break;
            }
        }
    }
}