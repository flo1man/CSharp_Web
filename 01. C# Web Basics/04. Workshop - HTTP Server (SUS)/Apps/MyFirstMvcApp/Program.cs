﻿using SUS.HTTP;
using System;
using System.Threading.Tasks;

namespace MyFirstMvcApp
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            IHttpServer server = new HttpServer();  

            server.AddRoute("/", HomePage);
            server.AddRoute("/favicon.ico", Favicon);
            server.AddRoute("/about", About);
            server.AddRoute("/users/login", Login);

            await server.StartAsync(80);

        }

        static HttpResponse Favicon(HttpRequest request)
        {
            throw new NotImplementedException();
        }

        static HttpResponse HomePage (HttpRequest request)
        {
            throw new NotImplementedException();
        }

        static HttpResponse About(HttpRequest request)
        {
            throw new NotImplementedException();
        }

        static HttpResponse Login(HttpRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
