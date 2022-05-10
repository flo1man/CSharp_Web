using Git.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Services
{
    public interface IRepositoriesService
    {
        void Create(RepositoryInputModel model, string ownerId);

        IEnumerable<RepositoryViewModel> GetAllRepositories();

        string GetRepositoryName(string id);
    }
}
