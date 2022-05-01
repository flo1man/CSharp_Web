using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DemoExercise
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Source: https://chitanka.info/

            Console.OutputEncoding = Encoding.UTF8;

            var dataGatherer = new ChitankaDataGatherer();

            // 05/01/2022 - 10762 books
            Stopwatch sw = Stopwatch.StartNew();
            ConcurrentBag<RawProperty> properties
                = dataGatherer.GatherDataParallel(500 + 1);

            Console.WriteLine(sw.Elapsed);
            var serialize = JsonConvert.SerializeObject(properties, Formatting.Indented);

            File.WriteAllText(@"../../../chitanka.bg-raw-data.json", serialize);




            //var properties = dataGatherer.GatherData(100);

            //var listOfDataFromAngle = dataGatherer.GatherDataAngleSharp(100);
            //var rawProperties = listOfDataFromAngle.Result;
        }
    }
}
