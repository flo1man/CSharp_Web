using Suls.Services;
using Suls.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suls.Controllers
{
    public class ProblemsController : Controller
    {
        private readonly IProblemsService problemsService;
        private readonly IUsersService usersService;

        public ProblemsController(IProblemsService problemsService, IUsersService usersService)
        {
            this.problemsService = problemsService;
            this.usersService = usersService;
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
        public HttpResponse Create(ProblemInputModel model)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(model.Name)
                || model.Name.Length < 5 || model.Name.Length > 20)
            {
                return this.View();
            }

            if (model.Points < 50 || model.Points > 300)
            {
                return this.View();
            }

            problemsService.CreateProblem(model);

            return this.Redirect("/");
        }

        public HttpResponse Details(string problemId)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var viewModels = problemsService.GetAllDetails(problemId);

            return this.View(viewModels);
        }
    }
}
