using Microsoft.AspNet.Identity.EntityFramework;
using MovieDomain.Contexts;

namespace MovieDomain.Identity
{
	public class CustomRoleStore : RoleStore<CustomRole, long, CustomUserRole>
	{
		public CustomRoleStore(AuthorizationContext context)
			: base(context)
		{		
		}
	}
}
