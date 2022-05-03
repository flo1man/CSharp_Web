using MyFirstMvcApp.Controllers;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMvcApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            List<Route> routeTable = new List<Route>();

            
            // TODO: <StartUp>
            await Host.CreateHostAsync(new StartUp(), 80);

        }
    }
}
