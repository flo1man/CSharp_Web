using Suls.Data.Models;
using Suls.ViewModels;
using SulsApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suls.Services
{
    public class ProblemsService : IProblemsService
    {
        private readonly ApplicationDbContext db;

        public ProblemsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreateProblem(ProblemInputModel model)
        {
            var problem = new Problem
            {
                Name = model.Name,
                Points = model.Points,
            };

            db.Problems.Add(problem);
            db.SaveChanges();
        }

        public IEnumerable<ProblemViewModel> GetAll()
        {
            var problems = db.Problems.Select(x => new ProblemViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Count = x.Submissions.Count
            }).ToList();

            return problems;
        }

        public AllProblemDetailsViewModel GetAllDetails(string problemId)
        {
            var problemName = this.db.Problems
            .Where(x => x.Id == problemId)
            .Select(x => x.Name)
            .FirstOrDefault();

            var viewModel = new AllProblemDetailsViewModel
            {
                ProblemName = problemName,
                AllProblemDetails = this.db.Submissions
                .Where(x => x.ProblemId == problemId)
                    .Select(x => new ProblemDetailViewModel
                    {

                        Username = x.User.Username,
                        SubmittedOn = DateTime.UtcNow,
                        MaxPoints = x.Problem.Points,
                        AchievedResult = x.AchievedResult,
                        SubmissionId = x.Id,
                    }).ToList()
            };
            
            return viewModel;
        }

        public string GetNameById(string id)
        {
            var name = this.db.Problems
                .Where(x => x.Id == id)
                .Select(x => x.Name)
                .FirstOrDefault();

            return name;
        }
    }
}
