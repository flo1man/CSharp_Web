using Git.Data;
using Git.Data.Models;
using Git.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Git.Services
{
    public class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext dbContext;

        public RepositoriesService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(RepositoryInputModel model, string ownerId)
        {
            var owner = this.dbContext.Users.Find(ownerId);

            var repository = new Repository
            {
                Name = model.Name,
                IsPublic = model.Type == "Public" ? true : false,
                CreatedOn = DateTime.UtcNow,
                Owner = owner,
            };

            this.dbContext.Repositories.Add(repository);
            this.dbContext.SaveChanges();
        }

        public IEnumerable<RepositoryViewModel> GetAllRepositories()
        {
            return dbContext.Repositories.Select(x => new RepositoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Commits = x.Commits.Count,
                CreatedOn = x.CreatedOn,
                Owner = x.Owner.Username,
            }).ToList();
        }

        public string GetRepositoryName(string id)
        {
            return this.dbContext.Repositories.FirstOrDefault(x => x.Id == id).Name;
        }
    }
}
