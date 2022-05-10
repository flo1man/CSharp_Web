using Git.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Services
{
    public interface ICommitsService
    {
        void Create(string userId, string id, CommitViewModel inputModel);
        IEnumerable<CommitViewModel> GetCommits(string userId);
        void DeleteCommit(string id);
    }
}
