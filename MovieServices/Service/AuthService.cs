using MovieDomain.Entities;
using MovieServices.Auth;
using MovieServices.Interfaces;
using MovieServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieServices.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserManager _userManager;

        public AuthService(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public Task<MovieDomain.Entities.User> FindAsync(string name, string password)
        {
            return _userManager.FindAsync(name, password);
        }

        public Task<Microsoft.AspNet.Identity.IdentityResult> Register(RegistrationBindingModel model)
        {
            User user = new User
            {
                UserName = model.Login,
                Email = model.Email,
            };
            return _userManager.CreateAsync(user, model.Password);
        }
    }
}
