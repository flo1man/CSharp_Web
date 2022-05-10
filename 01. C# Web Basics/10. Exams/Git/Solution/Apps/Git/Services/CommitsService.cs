using Git.Data;
using Git.Data.Models;
using Git.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Services
{
    public class CommitsService : ICommitsService
    {
        private readonly ApplicationDbContext db;

        public CommitsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(string userId, string repositoryId, CommitViewModel inputModel)
        {
            var commit = new Commit
            {
                Description = inputModel.Description,
                CreatedOn = DateTime.UtcNow,
                CreatorId = userId,
                RepositoryId = repositoryId,
            };

            this.db.Commits.Add(commit);
            this.db.SaveChanges();
        }

        public void DeleteCommit(string id)
        {
            var model = this.db.Commits.FirstOrDefault(c => c.Id == id);
            db.Commits.Remove(model);
            db.SaveChanges();
        }

        public IEnumerable<CommitViewModel> GetCommits(string userId)
        {
            var viewModels = db.Commits.Where(x => x.CreatorId == userId)
                .Select(x => new CommitViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    CreatedOn = x.CreatedOn,
                    Name = x.Repository.Name,
                })
                .ToList();

            return viewModels;
        }
    }
}
