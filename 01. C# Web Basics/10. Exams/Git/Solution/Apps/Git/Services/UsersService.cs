using Git.Data;
using Git.Data.Models;
using Git.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Git.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreateUser(RegisterInputModel inputModel)
        {
            var user = new User
            {
                Username = inputModel.Username,
                Email = inputModel.Email,
                Password = ComputeHash(inputModel.Password),
            };
            db.Users.Add(user);
            db.SaveChanges();
        }

        public string GetUserId(LoginInputModel model)
        {
            return this.db.Users
                .FirstOrDefault(x => x.Username == model.Username && x.Password == ComputeHash(model.Password))?
                .Id; 
        }

        public bool IsEmailAvailable(string email)
        {
            return this.db.Users
                .FirstOrDefault(x => x.Email == email) == null ? true : false;
        }

        public bool IsUsernameAvailable(string username)
        {
            return this.db.Users
                .FirstOrDefault(x => x.Username == username) == null ? true : false;
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
