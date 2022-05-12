using Andreys.Services;
using Andreys.ViewModels.Products;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public HttpResponse Add()
        {
            if (!IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(ProductInputModel model)
        {
            if (!IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(model.Name) || model.Name.Length < 4 || model.Name.Length > 20)
            {
                return this.Error("Name should be between 4 and 20 characters long.");
            }

            if (model.Description.Length > 10)
            {
                return this.Error("Max length of description is 10 characters.");
            }

            this.productsService.AddProduct(model);

            return this.Redirect("/");
        }

        public HttpResponse Details(int id)
        {
            if (!IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var model = productsService.GetProductById(id);

            return this.View(model);
        }

        public HttpResponse Delete(int id)
        {
            if (!IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.productsService.DeleteProduct(id);
            return this.Redirect("/");
        }
    }
}
