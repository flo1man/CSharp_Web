using Suls.Data.Models;
using Suls.ViewModels;
using SulsApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Suls.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreateUser(RegisterInputModel model)
        {
            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Password = ComputeHash(model.Password),
            };

            db.Users.Add(user);
            db.SaveChanges();
        }

        public string GetUser(LoginInputModel model)
        {
            return this.db.Users
                .FirstOrDefault(x => x.Username == model.Username && x.Password == ComputeHash(model.Password))?
                .Id;
        }

        public string GetUsernameById(string userId)
        {
            return this.db.Users.FirstOrDefault(x => x.Id == userId).Username;
        }

        public bool IsEmailAvailable(string email)
        {
            return this.db.Users
                .Any(x => x.Email == email);
        }

        public bool IsUsernameAvailable(string username)
        {
            return this.db.Users
                .Any(x => x.Username == username);
        }

        private string ComputeHash(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);

            using (var hash = SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);
                var hashedInputStringBuilder = new StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }
    }
}
