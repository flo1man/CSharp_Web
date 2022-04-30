using System;
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
            var properties = dataGatherer.GatherData(100);

            dataGatherer.GatherDataWithAngleSharp(100);
            var listOfDataFromAngle = dataGatherer.GetRawProperties();

            var serialize = JsonConvert.SerializeObject(properties, Formatting.Indented);

            File.WriteAllText(@"../../../chitanka.bg-raw-data.json", serialize);
        }
    }
}
