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
            Console.OutputEncoding = Encoding.UTF8;

            var dataGatherer = new ChitankaDataGatherer();

            var properties = dataGatherer.GatherData(10);

            var serialize = JsonConvert.SerializeObject(properties, Formatting.Indented);

            Console.WriteLine(serialize);
        }
    }
}
