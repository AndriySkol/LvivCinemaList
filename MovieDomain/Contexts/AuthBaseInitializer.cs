using Microsoft.AspNet.Identity;
using MovieDomain.Auth;
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
    class AuthBaseInitializer : DropCreateDatabaseIfModelChanges<AuthorizationContext>
    {
        protected override void Seed(AuthorizationContext context)
        {
            
            

            base.Seed(context);
        }
    }
}
