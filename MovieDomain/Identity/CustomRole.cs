using Microsoft.AspNet.Identity.EntityFramework;

namespace MovieDomain.Identity
{
	public class CustomRole : IdentityRole<long, CustomUserRole>
	{
		public CustomRole()
		{
		}

		public CustomRole(string name)
		{
			Name = name;
		}
	}
}
