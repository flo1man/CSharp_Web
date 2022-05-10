using Git.Services;
using Git.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly IRepositoriesService repositoriesService;

        public RepositoriesController(IRepositoriesService repositoriesService)
        {
            this.repositoriesService = repositoriesService;
        }

        public HttpResponse All()
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModels = this.repositoriesService.GetAllRepositories();

            return this.View(viewModels);
        }

        public HttpResponse Create()
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(RepositoryInputModel inputModel)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(inputModel.Name) || inputModel.Name.Length < 3 || inputModel.Name.Length > 10)
            {
                return this.Error("Name should be between 3 and 10 characters");
            }

            var ownderId = GetUserId();

            if (ownderId == null)
            {
                return this.Error("Invalid operation!");
            }

            repositoriesService.Create(inputModel, ownderId);

            return this.Redirect("/Repositories/All");
        }
    }
}
