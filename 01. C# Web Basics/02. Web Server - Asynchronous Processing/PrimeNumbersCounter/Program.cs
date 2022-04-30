using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PrimeNumbersCounter
{
    internal class Program
    {
        static int Count = 0;

        static object lockObj = new object(); // good practice for lock object

        static void Main(string[] args)
        {
            //Task.Run(PrintPrimeCount);

            //ExceptionWithoutLock();


            Stopwatch sw = Stopwatch.StartNew();
            // Концепция за многонишково програмиране
            Thread thread1 = new Thread(()
                => PrintPrimeCount(1, 3_333_333));
            thread1.Start();
            Thread thread2 = new Thread(()
                => PrintPrimeCount(3_333_334, 6_666_666));
            thread2.Start();
            Thread thread3 = new Thread(()
                => PrintPrimeCount(6_666_667, 10_000_000));
            thread3.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();

            Console.WriteLine(Count);
            Console.WriteLine(sw.Elapsed);

            while (true)
            {
                var input = Console.ReadLine();
                Console.WriteLine(input);
            }
        }

        private static void ExceptionWithoutLock()
        {
            List<int> numbers = Enumerable.Range(0, 10000).ToList();
            for (int i = 0; i < 4; i++)
            {
                new Thread(() =>
                {
                    while (numbers.Count > 0)
                    {
                        numbers.RemoveAt(numbers.Count - 1);
                    }
                }).Start();
            }
        }

        public static void PrintPrimeCount(int min, int max)
        {

            for (int i = min; i <= max; i++)
            {
                bool isPrime = true;
                for (int j = 2; j <= Math.Sqrt(i); j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime)
                {
                    // monitor
                    lock(lockObj)
                    {
                        Count++;
                    }
                }
            }
        }
    }
}
