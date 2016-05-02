using Microsoft.AspNet.Identity;
using MovieDomain.Entities;
using MovieDomain.Identity;
using MovieServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieServices.Auth
{
    public class CustomUserManager : UserManager<User, long>, IUserManager
    {
        public CustomUserManager(CustomUserStore store)
            : base(store)
        {
            UserValidator = new UserValidator<User, long>(this)
            {
                AllowOnlyAlphanumericUserNames = false
            };
        }




        public System.Security.Claims.ClaimsIdentity CreateExternalIdentity(User user, string authenticationType)
        {
            throw new NotImplementedException();
        }
    }
}
