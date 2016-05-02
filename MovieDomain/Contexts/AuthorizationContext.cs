using Microsoft.AspNet.Identity.EntityFramework;
using MovieDomain.Configurations;
using MovieDomain.Entities;
using MovieDomain.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDomain.Contexts
{
    public class AuthorizationContext : IdentityDbContext<User, CustomRole, long, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public AuthorizationContext()
            : base("name=MovieBase")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new UserConfiguration());
            ConfigureIdentityTables(modelBuilder);
        }

        private void ConfigureIdentityTables(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomRole>().ToTable("Roles");
            modelBuilder.Entity<CustomUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<CustomUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<CustomUserClaim>().ToTable("UserClaims");
        }
    }
}
