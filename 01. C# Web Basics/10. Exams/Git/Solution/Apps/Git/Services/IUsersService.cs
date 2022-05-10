using Git.ViewModels;

namespace Git.Services
{
    public interface IUsersService
    {
        void CreateUser(RegisterInputModel inputModel);

        bool IsEmailAvailable(string email);

        string GetUserId(LoginInputModel model);

        bool IsUsernameAvailable(string username);
    }
}
