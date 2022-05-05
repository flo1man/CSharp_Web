using MyFirstMvcApp.Controllers;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMvcApp
{
    public class StartUp : IMvcApplication
    {
        public void Configure(List<Route> routeTable)
        {
            routeTable.Add(new Route("/", HttpMethod.Get, new HomeController().Index));
            routeTable.Add(new Route("/Users/Login", HttpMethod.Get, new UsersController().Login));
            routeTable.Add(new Route("/Users/Login", HttpMethod.Post, new UsersController().DoLogin));
            routeTable.Add(new Route("/Users/Register", HttpMethod.Get, new UsersController().Register));
            routeTable.Add(new Route("/Cars/Add", HttpMethod.Get, new CarsController().Add));
            routeTable.Add(new Route("/Cars/All", HttpMethod.Get, new CarsController().All));

            routeTable.Add(new Route("/favicon.ico", HttpMethod.Get, new StaticFilesController().Favicon));
            routeTable.Add(new Route("/css/bootstrap.min.css", HttpMethod.Get, new StaticFilesController().BootstrapCss));
            routeTable.Add(new Route("/css/custom.css", HttpMethod.Get, new StaticFilesController().CustomCss));
            routeTable.Add(new Route("/js/custom.js", HttpMethod.Get, new StaticFilesController().CustomJs));
            routeTable.Add(new Route("/js/bootstrap.bundle.min.js", HttpMethod.Get, new StaticFilesController().BootstrapJs));
        }

        public void ConfigureServices()
        {
        }
    }
}
