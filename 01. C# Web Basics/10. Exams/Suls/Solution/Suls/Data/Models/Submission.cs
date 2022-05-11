using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Suls.Data.Models
{
    public class Submission
    {
        public Submission()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        public string Code { get; set; }

        public int AchievedResult{ get; set; }

        public DateTime CreatedOn { get; set; }

        public string ProblemId { get; set; }
        public Problem Problem { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
