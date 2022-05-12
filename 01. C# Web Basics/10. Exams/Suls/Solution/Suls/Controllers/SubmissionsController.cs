using Suls.Services;
using Suls.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suls.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly ISubmissionService submissionService;
        private readonly IProblemsService problemsService;

        public SubmissionsController(
            ISubmissionService submissionService,
            IProblemsService problemsService)
        {
            this.submissionService = submissionService;
            this.problemsService = problemsService;
        }

        public HttpResponse Create(string id)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var name = this.problemsService.GetNameById(id);
            var viewModel = new SubmissionCreateViewModel
            {
                Name = name,
                ProblemId = id
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(string problemId, SubmissionInputModel model)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.GetUserId();

            if (string.IsNullOrEmpty(model.Code)
                    || model.Code.Length < 30
                    || model.Code.Length > 800)
            {
                return this.Redirect("/Submissions/Create");
            }

            this.submissionService.CreateSubmission(problemId, userId, model);

            return this.Redirect("/");

        }
    }
}
