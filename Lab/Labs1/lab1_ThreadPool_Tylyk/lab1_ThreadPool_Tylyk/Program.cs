using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace lab1_ThreadPool_Tylyk
{
    public class MyThread
    {
        public void ThreadNumbers()
        {
            //  stream information
            Console.WriteLine("{0} поток использует метод ThredNumbers",
                Thread.CurrentThread.Name);
            //  display numbers
            Console.WriteLine("Числа: ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write(i + ", ");
                //  Using delay
                Thread.Sleep(3000);
            }
            Console.WriteLine();
        }
    }
    class ThreadClass
    {
        private string greeting;
        public ThreadClass (string sGreeting)
        {
            greeting = sGreeting;
        }
        public void Run()
        {
            Console.WriteLine(greeting);
        }
    }
    class Program
    {
        static void LocalWorkItem()
        {
            Console.WriteLine("Hello from static method");
        }
        static void ThreadFunction()
        {
            //  Similar to the main stream, we output the text three times
            int count = 3;
            while (count > 0)
            {
                Console.WriteLine("Это дочерний поток программы!");
                --count;
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Выбирите пример(1...2)?");
            string namber_example = Console.ReadLine();

            switch (namber_example)
            {
                case "1":
                    Console.WriteLine("Сколько использовано потоков (1 или 2)?");
                    string number = Console.ReadLine();

                    Thread mythread = Thread.CurrentThread;
                    mythread.Name = "Первичный";

                    //  display information about the stream
                    Console.WriteLine("--> {0} главный поток",
                        Thread.CurrentThread.Name);
                    MyThread mt = new MyThread();
                    switch (number)
                    {
                        case "1":
                            mt.ThreadNumbers();
                            break;
                        case "2":
                            // Create a stream
                            Thread backgroundThread = new Thread(new
                                ThreadStart(mt.ThreadNumbers));
                            backgroundThread.Name = "Вторичный";
                            backgroundThread.Start();
                            break;
                        default:
                            Console.WriteLine("ипользует 1 поток");
                            goto case "1";
                    }
                    Console.WriteLine("Работа в другом потоке");
                    Console.ReadLine();
                    break;

                case "2":
                    Thread thr1 = new Thread(LocalWorkItem);
                    thr1.Start();
                    Thread thr2 = new Thread(() =>
                    {
                        Console.WriteLine("hello from lamda-expression");
                    });
                    thr2.Start();
                    ThreadClass thrClass = new ThreadClass("Hello from thread-class");
                    Thread thr3 = new Thread(thrClass.Run);
                    thr3.Start();
                    Console.ReadLine();
                    break;

                case "3":
                    //  Create object stream
                    Thread thread = new Thread(ThreadFunction);
                    //  Start stream
                    thread.Start();
                    //  Display 3 times text
                    int count = 3;
                    while (count > 0)
                    {
                        Console.WriteLine("Это главный поток програмы!");
                        count--;
                    }

                    Thread thr_1 = new Thread(() =>
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            Console.Write("A");
                        }
                    });
                    Thread thr_2 = new Thread(() =>
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            Console.Write("B");
                        }
                    });
                    Thread thr_3 = new Thread(() =>
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            Console.Write("C");
                        }
                    });
                    thr_1.Start();
                    thr_2.Start();
                    thr_1.Join();
                    thr_2.Join();
                    thr_3.Start();
                    //  Ждем ввода от пользователя, что бы окно консоли не закрылось автоматически
                    //  We are waiting for user input so that the console window does not close automatically
                    Console.Read();                    
                    break;
            }            
        }        
    }
}
