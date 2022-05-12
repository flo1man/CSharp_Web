using Suls.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suls.Services
{
    public interface IProblemsService
    {
        void CreateProblem(ProblemInputModel model);

        IEnumerable<ProblemViewModel> GetAll();

        AllProblemDetailsViewModel GetAllDetails(string problemId);

        string GetNameById(string id);
    }
}
