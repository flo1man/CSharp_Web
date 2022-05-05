using MyFirstMvcApp.Controllers;
using SUS.HTTP;
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

        }

        public void ConfigureServices()
        {
        }
    }
}
