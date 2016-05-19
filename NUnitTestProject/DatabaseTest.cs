using MovieDomain.Auth;
using MovieDomain.Contexts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestProject
{
    [TestFixture]
    public class DatabaseTest : AssertionHelper
    {
        [Test]
        public void ContainsAdminRole()
        {
            using(MovieContext context = new MovieContext())
            {
                Expect(context.Roles.Any(r => r.Name == "Admin"));
            }
        }

        [Test]
        public void ContainsAdmin()
        {
            var _userManager = new CustomUserManager(new MovieDomain.Identity.CustomUserStore(new AuthorizationContext()));
            var admin = _userManager.FindAsync("admin", "12345678").Result;
            Expect(admin, Is.Not.Null);
            Expect(_userManager.IsInRoleAsync(admin.Id, "Admin").Result);
        }
    }
}
