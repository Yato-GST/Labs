using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;

namespace lab1_Свойства_потока
{
    class Program
    {
        static void SomeFunc (object a)
        {
            for (int i = 0; i < 20; i++)
            {
                Console.Write(a);
                Thread.Sleep(10);
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Выбирите пример(1...3)?");
            string namber_example = Console.ReadLine();

            switch (namber_example)
            {
                case "1":
                    // Объявляем массив потоков
                    Thread[] arThr = new Thread[5];
                    for (int i = 0; i < arThr.Length; i++)
                    {
                        arThr[i] = new Thread(SomeFunc);
                        arThr[i].Start(i);
                        arThr[i].Name = "N" + i;
                    }
                    for (int i = 0; i < arThr.Length; i++)
                    {
                        // Выводим информацию о потоках
                        Console.WriteLine("Thread Id: {0}," +
                            "name: {1}, IsAlive: {2}",
                            arThr[i].ManagedThreadId,
                            arThr[i].Name,
                            arThr[i].IsAlive);
                    }                    
                    break;

                case "2":
                    Thread t = Thread.CurrentThread;
                    t.Name = "MAIN THREAD";
                    foreach (PropertyInfo p in t.GetType().GetProperties())
                    {
                        Console.WriteLine("{0}:{1}",
                            p.Name, p.GetValue(t, null));
                    }
                    break;

                case "3":
                    Console.Title = "Информация о главном потоке программы";
                    Thread thread = Thread.CurrentThread;
                    thread.Name = "MyThread";
                    Console.WriteLine(@"Имя домена приложения: {0}
                        ID контекста: {1}
                        Имя потока: {2}
                        Запущен ли поток? {3}
                        Приоритет потока: {4}
                        Состояние потока: {5}",
                        Thread.GetDomain().FriendlyName, Thread.CurrentContext.ContextID, 
                        thread.Name, thread.IsAlive, thread.Priority, thread.ThreadState);
                    break;
            }
            Console.ReadKey();
        }
    }
}
