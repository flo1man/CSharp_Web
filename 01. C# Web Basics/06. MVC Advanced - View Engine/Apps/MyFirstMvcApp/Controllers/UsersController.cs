﻿using SUS.HTTP;
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
            return View();
        }

        public HttpResponse Register(HttpRequest request)
        {
            return View();
        }

        public HttpResponse DoLogin(HttpRequest request)
        {
            // TODO: read data
            // TODO: check user
            // TODO: log user
            // TODO: home page
            return this.Redirect("/");
        }

    }
}
