using System;
using System.Collections.Generic;
using System.Text;

namespace Suls.ViewModels
{
    public class AllProblemDetailsViewModel
    {
        public IEnumerable<ProblemDetailViewModel> AllProblemDetails { get; set; }

        public string ProblemName { get; set; }
    }
}
