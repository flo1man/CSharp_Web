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
        static async Task Main(string[] args)
        {
            List<Route> routeTable = new List<Route>();

            routeTable.Add(new Route("/", new HomeController().Index));
            routeTable.Add(new Route("/favicon.ico", new StaticFilesController().Favicon));
            routeTable.Add(new Route("/users/login", new UsersController().Login));
            routeTable.Add(new Route("/users/register", new UsersController().Register));
            routeTable.Add(new Route("/cars/add", new CarsController().Add));
            routeTable.Add(new Route("/cars/all", new CarsController().All));

            await Host.CreateHostAsync(routeTable, 80);

        }
    }
}
