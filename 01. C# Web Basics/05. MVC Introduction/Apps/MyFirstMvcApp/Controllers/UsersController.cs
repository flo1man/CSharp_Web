using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMvcApp.Controllers
{
    public class UsersController : Controller
    {
        public HttpResponse Login(HttpRequest request)
        {
            return View("Views/Users/Login.html");
        }

        public HttpResponse Register(HttpRequest request)
        {
            return View("Views/Users/Register.html");
        }
    }
}
