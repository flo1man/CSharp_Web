using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMvcApp.Controllers
{
    public class CarsController : Controller
    {
        public HttpResponse All(HttpRequest request)
        {
            return View();
        }

        public HttpResponse Add(HttpRequest request)
        {
            return View();
        }
    }
}
