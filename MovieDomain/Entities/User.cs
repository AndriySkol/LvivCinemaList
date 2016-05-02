using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MovieDomain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDomain.Entities
{
    public class User : IdentityUser<long, CustomUserLogin, CustomUserRole, CustomUserClaim>, IUser<long>
    {
    }
}
