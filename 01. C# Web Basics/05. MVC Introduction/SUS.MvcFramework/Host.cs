using SUS.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS.MvcFramework
{
    public static class Host
    {
        public static async Task CreateHostAsync(List<Route> routes, int port = 80)
        {
            IHttpServer server = new HttpServer();

            foreach (var route in routes)
            {
                server.AddRoute(route.Path, route.Action);
            }

            // Ако искаме когато стартираме приложението, автоматично да се зарежда страницата
            // Process.Start(@"C:\Program Files\Google\Chrome\Application\chrome.exe", "http://localhost/");
            await server.StartAsync(port);
        }
    }
}
