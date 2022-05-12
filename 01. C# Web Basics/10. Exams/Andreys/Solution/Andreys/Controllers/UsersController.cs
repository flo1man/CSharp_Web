using Andreys.Services;
using Andreys.ViewModels.Users;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Andreys.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public HttpResponse Login()
        {
            if (IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel inputModel)
        {
            if (IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.usersService.GetUserId(inputModel);

            if (userId == null)
            {
                return this.Error("Invalid username or password");
            }

            this.SignIn(userId);

            return this.Redirect("/");
        }

        public HttpResponse Register()
        {
            if (IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel inputModel)
        {
            if (IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrEmpty(inputModel.Username) || inputModel.Username.Length < 5 || inputModel.Username.Length > 10)
            {
                return this.Error("Name should be between 5 and 10 characters");
            }

            if (inputModel.Password != inputModel.ConfirmPassword)
            {
                return this.Error("Passwords do not match");
            }

            if (string.IsNullOrEmpty(inputModel.Password) || inputModel.Password.Length < 6 || inputModel.Password.Length > 20)
            {
                return this.Error("Password should be between 6 and 20 characters");
            }

            if (!usersService.IsUsernameAvailable(inputModel))
            {
                return this.Error("Username not available");
            }

            if (!usersService.IsEmailAvailable(inputModel)
                || !new EmailAddressAttribute().IsValid(inputModel.Email)
                || string.IsNullOrEmpty(inputModel.Email))
            {
                return this.Error("Email not available");
            }

            this.usersService.Create(inputModel);

            return Redirect("/Users/Login");
        }

        public HttpResponse LogOut()
        {
            if (IsUserLoggedIn())
            {
                this.SignOut();
            }

            return this.Redirect("/");
        }
    }
}
