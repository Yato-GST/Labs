using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Lab1_Локальное_хранилище_потока
{
    public class Data
    {
        public static int sharedVar;
        [ThreadStatic] public static int localVar;
    }
    public class ThreadWork
    {
        private string sharedWord;
        public void Run(string secretWord)
        {
            sharedWord = secretWord;
            Save(secretWord);
            Thread.Sleep(500);
            Show();
        }
        private void Save(string s)
        {
            // Получаем идентификатор слота по имени 
            LocalDataStoreSlot slot = Thread.GetNamedDataSlot("Secret");
            // Сохраняем данные 
            Thread.SetData(slot, s);
        }
        private void Show()
        {
            LocalDataStoreSlot slot = Thread.GetNamedDataSlot("Secret");
            string secretWord = Thread.GetData(slot).ToString();
            Console.WriteLine("Thread {0}, secret word: {1}, shared word: {2}", 
                Thread.CurrentThread.ManagedThreadId, secretWord, sharedWord);
        }
    }
    class Program
    {
        static void threadFunc(object i)
        {
            Console.WriteLine("Thread {0}: Before changing.. Shared: {1}, local: {2}", i, Data.sharedVar, Data.localVar);
            Data.sharedVar = (int)i;
            Data.localVar = (int)i;
            Console.WriteLine("Thread {0}: After changing.. Shared: {1}, local: {2}", i, Data.sharedVar, Data.localVar);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Выбирите пример(1...3)?");
            string namber_example = Console.ReadLine();

            switch (namber_example)
            {
                case "1":
                    Thread t1 = new Thread(threadFunc);
                    Thread t2 = new Thread(threadFunc);

                    Data.sharedVar = 3; Data.localVar = 3;

                    t1.Start(1); t2.Start(2);
                    t1.Join(); t2.Join();
                    break;

                case "2":
                    ThreadLocal<int> localSum = new ThreadLocal<int>(() => 0);
                    t1 = new Thread(() => {
                        for (int i = 0; i < 10; i++)
                            localSum.Value++;
                        Console.WriteLine(localSum.Value);
                    });
                    t2 = new Thread(() => {
                        for (int i = 0; i < 10; i++)
                            localSum.Value--;
                        Console.WriteLine(localSum.Value);
                    });
                    t1.Start(); t2.Start();
                    t1.Join(); t2.Join();
                    Console.WriteLine(localSum.Value);
                    break;

                case "3":
                    ThreadWork thr = new ThreadWork();
                    new Thread(() => thr.Run("one")).Start();
                    new Thread(() => thr.Run("two")).Start();
                    new Thread(() => thr.Run("three")).Start();
                    Thread.Sleep(1000);
                    break;
            }

            Console.Read();
        }
    }
}
