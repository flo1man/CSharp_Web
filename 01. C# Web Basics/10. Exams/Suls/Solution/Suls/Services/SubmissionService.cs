using Suls.Data.Models;
using Suls.ViewModels;
using SulsApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suls.Services
{
    public class SubmissionService : ISubmissionService
    {
        private readonly ApplicationDbContext db;
        private readonly Random random;

        public SubmissionService(ApplicationDbContext db, Random random)
        {
            this.db = db;
            this.random = random;
        }

        public void CreateSubmission(string problemId, string userId, SubmissionInputModel model)
        {
            var problemMaxPoints = this.db.Problems
                .Where(x => x.Id == problemId)
                .Select(x => x.Points)
                .FirstOrDefault();

            var submission = new Submission
            {
                Code = model.Code,
                CreatedOn = DateTime.UtcNow,
                ProblemId = problemId,
                UserId = userId,
                AchievedResult = random.Next(0, problemMaxPoints + 1),
            };

            this.db.Submissions.Add(submission);
            this.db.SaveChanges();
        }

        public void DeleteSubmission(string submissionId)
        {
            var submission = this.db.Submissions
                .FirstOrDefault(x => x.Id == submissionId);

            this.db.Submissions.Remove(submission);
            this.db.SaveChanges();
        }
    }
}
