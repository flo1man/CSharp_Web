using SUS.HTTP;
using SUS.MvcFramework.ViewEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SUS.MvcFramework
{
    public abstract class Controller
    {
        private SusViewEngine viewEngine;

        public Controller()
        {
            this.viewEngine = new SusViewEngine();
        }


        public HttpResponse View(object viewModel = null, [CallerMemberName]string filePath = null)
        {
            var layout = System.IO.File.ReadAllText("Views/Shared/_Layout.html");
            layout = layout.Replace("@RenderBody()", "___VIEW_GOES_HERE___");
            layout = this.viewEngine.GetHtml(layout, viewModel);

            var viewContent = System.IO.File.ReadAllText("Views/"
                + this.GetType().Name.Replace("Controller", "")
                + "/"
                + filePath
                + ".html");

            viewContent = this.viewEngine.GetHtml(viewContent, viewModel);

            var responseHtml = layout.Replace("___VIEW_GOES_HERE___", viewContent);

            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            var response = new HttpResponse("text/html", responseBodyBytes);
            return response;
        }

        public HttpResponse File(string filePath, string contentType)
        {
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var response = new HttpResponse(contentType, fileBytes);
            return response;
        }

        public HttpResponse Redirect(string url)
        {
            var response = new HttpResponse(HttpStatusCode.FOUND);
            response.Headers.Add(new Header("location", url));
            return response;
        }
    }
}
