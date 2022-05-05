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
        public HttpResponse All()
        {
            return View();
        }

        public HttpResponse Add()
        {
            return View();
        }
    }
}
