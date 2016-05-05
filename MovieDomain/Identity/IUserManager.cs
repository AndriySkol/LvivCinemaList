using Microsoft.AspNet.Identity;
using MovieDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieServices.Interfaces
{
    public interface IUserManager
    {
        Task<IdentityResult> CreateAsync(User user);

        Task<IdentityResult> CreateAsync(User user, string password);

        Task<IdentityResult> AddToRolesAsync(long userId, params string[] roles);

        Task<User> FindAsync(string userName, string password);

        Task<User> FindAsync(UserLoginInfo login);

        Task<IdentityResult> AddLoginAsync(long userId, UserLoginInfo login);

        Task<ClaimsIdentity> CreateIdentityAsync(User user, string authenticationType);

        ClaimsIdentity CreateExternalIdentity(User user, string authenticationType);

        Task<User> FindByEmailAsync(string email);

        Task<IList<string>> GetRolesAsync(long userId);

    }
}
