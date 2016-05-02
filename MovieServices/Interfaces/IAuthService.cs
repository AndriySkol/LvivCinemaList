using Microsoft.AspNet.Identity;
using MovieDomain.Entities;
using MovieServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieServices.Interfaces
{
    public interface IAuthService
    {
        Task<User> FindAsync(string name, string password);

        Task<IdentityResult> Register(RegistrationBindingModel model);
    }
}
