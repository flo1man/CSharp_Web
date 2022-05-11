using Suls.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suls.Services
{
    public interface IUsersService
    {
        void CreateUser(RegisterInputModel model);

        bool IsEmailAvailable(string email);

        bool IsUsernameAvailable(string username);

        string GetUser(LoginInputModel model);

        string GetUsernameById(string userId);
    }
}
