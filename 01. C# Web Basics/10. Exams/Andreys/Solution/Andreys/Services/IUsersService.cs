using Andreys.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.Services
{
    public interface IUsersService
    {
        void Create(RegisterInputModel register);

        bool IsUsernameAvailable(RegisterInputModel register);

        bool IsEmailAvailable(RegisterInputModel registerl);

        string GetUserId(LoginInputModel login);
    }
}
