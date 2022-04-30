using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace TaskDemo
{
    internal class Program
    {
        static async Task Main(string[] args)  //Main meyhod-а го правим ASYNC
        {

            HttpClient httpClient = new HttpClient(); //създаваме HttpClient, за можем да работим с мрежата
            string url = "https://softuni.bg";
            HttpResponseMessage httpResponse = await httpClient.GetAsync(url); //поискваме търсената иформация
            string result = await httpResponse.Content.ReadAsStringAsync(); // получаваме поисканата инфоормация
            Console.WriteLine(result);


            Console.WriteLine("serfsedgsgsgrt");  // този код е ВЪЗМОЖНО да се изпълни преди резултата от предишния


            Stopwatch sw2 = Stopwatch.StartNew();
            List<Task> tasks = new List<Task>();

            for (int i = 0; i <= 100; i++)
            {
                var task = Task.Run(async () =>
                {
                    HttpClient httpClient = new HttpClient();
                    var url = $"https://vicove.com/vic-{i}";
                    var httpResponse = await httpClient.GetAsync(url);
                    var vic = await httpResponse.Content.ReadAsStringAsync();
                    Console.WriteLine(vic.Length);
                });
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());
            sw2.Stop();
            Console.WriteLine(sw2.Elapsed);
        }
    }
}
