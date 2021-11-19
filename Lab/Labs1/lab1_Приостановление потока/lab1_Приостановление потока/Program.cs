using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace lab1_Приостановление_потока
{
    class Program
    {
        static void ThreadFunc(object o)
        {
            for (int i = 0; i < 20; i++)
                Console.Write(o);
        }
        static void ThreadFunc_S(object o)
        {
            for (int i = 0; i < 20; i++)
            {
                Console.Write(o);
                Thread.Sleep(0);
            }
        }

        static void Main(string[] args)
        {
            Thread[] t = new Thread[4];
            bool s = false;
            do
            {
                if (s == false)
                {
                    for (int i = 0; i < 4; i++)
                        t[i] = new Thread(ThreadFunc);
                    s = true;
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                        t[i] = new Thread(ThreadFunc_S);
                    s = false;
                }
                
                t[0].Start("A"); t[1].Start("B");
                t[2].Start("C"); t[3].Start("D");

                for (int i = 0; i < 4; i++)
                    t[i].Join();

                Console.WriteLine();
                Console.WriteLine("--------------------------------------------------------------------------------");
            } while (s == true);            
            Console.Read();
        }
    }
}
