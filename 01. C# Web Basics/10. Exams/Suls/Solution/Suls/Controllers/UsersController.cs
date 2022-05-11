using Suls.Services;
using Suls.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Suls.Controllers
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
            if (IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel model)
        {
            if (IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = usersService.GetUser(model);

            if (userId == null)
            {
                return this.Error("Invalid username or password");
            }

            this.SignIn(userId);

            return this.Redirect("/");
        }

        public HttpResponse Register()
        {
            if (IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel model)
        {
            if (IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrEmpty(model.Username) 
                || model.Username.Length < 5 || model.Username.Length > 20
                || usersService.IsUsernameAvailable(model.Username))
            {
                return this.View();
            }

            if (string.IsNullOrEmpty(model.Email)
                || !new EmailAddressAttribute().IsValid(model.Email)
                || usersService.IsEmailAvailable(model.Email))
            {
                return this.View();
            }

            if (string.IsNullOrEmpty(model.Password)
                || model.Password.Length < 6 || model.Password.Length > 20
                || model.Password != model.ConfirmPassword)
            {
                return this.View();
            }

            usersService.CreateUser(model);

            return Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            this.SignOut();

            return Redirect("/");
        }
    }
}
