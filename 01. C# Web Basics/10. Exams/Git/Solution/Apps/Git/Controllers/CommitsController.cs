using Git.Services;
using Git.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly ICommitsService commitsService;
        private readonly IRepositoriesService repositoriesService;

        public CommitsController(ICommitsService commitsService, IRepositoriesService repositoriesService)
        {
            this.commitsService = commitsService;
            this.repositoriesService = repositoriesService;
        }

        public HttpResponse All()
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = GetUserId();
            var models = commitsService.GetCommits(userId);

            return this.View(models);
        }

        public HttpResponse Create(string id)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var repositoryName = this.repositoriesService.GetRepositoryName(id);

            if (repositoryName == null)
            {
                return this.Error("Invalid Id");
            }

            var viewModel = new CommitsInputModel
            {
                Id = id,
                Name = repositoryName,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(CommitViewModel inputModel, string id)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = GetUserId();

            this.commitsService.Create(userId, id, inputModel);

            return this.View("/Commits/All");
        }

        public HttpResponse Delete(string id)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            this.commitsService.DeleteCommit(id);

            return this.Redirect("/Commits/All");
        }
    }
}
