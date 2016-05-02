using Microsoft.AspNet.Identity.EntityFramework;
using MovieDomain.Contexts;
using MovieDomain.Entities;

namespace MovieDomain.Identity
{
	public class CustomUserStore : UserStore<User, CustomRole, long, CustomUserLogin, CustomUserRole, CustomUserClaim>
	{
		public CustomUserStore(AuthorizationContext context) 
			: base(context)
		{			
		}
	}
}