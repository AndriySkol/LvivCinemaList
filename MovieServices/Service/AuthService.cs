using MovieDomain.Entities;
using MovieDomain.UnitOfWork;
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

        private IUnitOfWorkFactory _factory;

        public AuthService(IUserManager userManager, IUnitOfWorkFactory factory)
        {
            _userManager = userManager;
            _factory = factory;
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            return _userManager.GetRolesAsync(user.Id);
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


        public void BanUser(long userId)
        {
            using(var unit = _factory.Create())
            {
                User user = unit.Users.GetById(userId);
                user.IsBanned = true;
                unit.Users.Update(user);
                unit.Save();
            }
        }

        public void UnBanUser(long userId)
        {
            using (var unit = _factory.Create())
            {
                User user = unit.Users.GetById(userId);
                user.IsBanned = false;
                unit.Users.Update(user);
                unit.Save();
            }
        }
    }
}
