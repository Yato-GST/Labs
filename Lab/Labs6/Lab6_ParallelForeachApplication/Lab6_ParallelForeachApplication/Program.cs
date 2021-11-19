using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Net;
using System.Threading.Tasks;

namespace Lab6_ParallelForeachApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> urls = new List<string>
            {
                "https://google.com",
                "https://youtube.com",
                "https://facebook.com",
                "https://instagram.com"
            };
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

            foreach (string url in urls)
            {
                WebClient client = new WebClient();
                Console.WriteLine("Скачиваем: {0}", url);
                client.DownloadString(url);
            }

            long elapsed = sw.ElapsedMilliseconds;
            Console.WriteLine("Затраченное время в миллисекундах: {0}", elapsed);
            sw.Stop();
            Console.WriteLine("============================ ");
            sw.Restart();
            Parallel.ForEach(urls, url =>
            {
                WebClient client = new WebClient();
                Console.WriteLine("Скачиваем: {0}", url);
                client.DownloadString(url);
            });
            elapsed = sw.ElapsedMilliseconds;
            Console.WriteLine("Затраченное время в миллисекундах: {0}", elapsed);
            sw.Stop();
            Console.ReadLine();
        }
    }
}
