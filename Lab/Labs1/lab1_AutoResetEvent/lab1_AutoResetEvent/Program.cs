using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace lab1_AutoResetEvent
{
    public class Params
    {
        public int a, b;
        public Params (int a, int b)
        {
            this.a = a;
            this.b = b;
        }
    }
    class Program
    {
        private static AutoResetEvent waitHandle = new AutoResetEvent(false);
        static void Main(string[] args)
        {
            Console.WriteLine("Главный поток. ID: "+Thread.CurrentThread.ManagedThreadId);

            Params pm = new Params(10, 10);
            Thread t = new Thread(new ParameterizedThreadStart(Add));
            t.Start(pm);

            // wait
            Thread.Sleep(5);

            // wait 
            waitHandle.WaitOne();
            Console.WriteLine("Все потоки завершились");
            Console.ReadLine();
        }
        static void Add(object obj)
        {
            if (obj is Params)
            {
                Console.WriteLine("ID потока метода Add(): "+Thread.CurrentThread.ManagedThreadId);
                Params pr = (Params)obj;
                Console.WriteLine("{0} + {1} = {2}", pr.a, pr.b, pr.a + pr.b);
                // Сообщить другому потоку о завершении работы
                waitHandle.Set();
            }
        }
    }
}
