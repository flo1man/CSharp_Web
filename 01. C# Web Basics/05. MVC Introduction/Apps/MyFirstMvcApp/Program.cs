using MyFirstMvcApp.Controllers;
using SUS.HTTP;
using System;
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
            IHttpServer server = new HttpServer();  

            server.AddRoute("/", new HomeController().Index);
            server.AddRoute("/favicon.ico", new StaticFilesController().Favicon);
            server.AddRoute("/about", new HomeController().About);
            server.AddRoute("/users/login", new UsersController().Login);
            server.AddRoute("/users/register", new UsersController().Register);

            // Ако искаме когато стартираме приложението, автоматично да се зарежда страницата
            // Process.Start(@"C:\Program Files\Google\Chrome\Application\chrome.exe", "http://localhost/");
            await server.StartAsync(80);

        }
    }
}
