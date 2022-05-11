using System;
using System.Collections.Generic;
using System.Text;

namespace Suls.ViewModels
{
    public class ProblemDetailViewModel
    {
        public string SubmissionId { get; set; }

        public string Username { get; set; }

        public int AchievedResult { get; set; }

        public int MaxPoints { get; set; }

        public DateTime SubmittedOn { get; set; }
    }
}
