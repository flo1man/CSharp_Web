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
    public class StaticFilesController : Controller
    {
        public HttpResponse Favicon(HttpRequest request)
        {
            return File("wwwroot/favicon.ico", "image/vnd.microsoft.icon");
        }

        internal HttpResponse BootstrapCss(HttpRequest request)
        {
            return File("wwwroot/css/bootstrap.min.css", "text/css");
        }

        internal HttpResponse CustomCss(HttpRequest request)
        {
            return File("wwwroot/css/custom.css", "text/css");
        }

        internal HttpResponse CustomJs(HttpRequest request)
        {
            return File("wwwroot/js/bootstrap.bundle.min.js", "text/javascript"); ;
        }

        internal HttpResponse BootstrapJs(HttpRequest request)
        {
            return File("wwwroot/js/custom.js", "text/javascript");
        }
    }
}
