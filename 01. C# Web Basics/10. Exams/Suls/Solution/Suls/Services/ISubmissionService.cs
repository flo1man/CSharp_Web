using Suls.ViewModels;

namespace Suls.Services
{
    public interface ISubmissionService
    {
        public void CreateSubmission(string problemId, string userId, SubmissionInputModel model);

        void DeleteSubmission(string submissionId);
    }
}
