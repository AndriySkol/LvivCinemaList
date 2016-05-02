using MovieDomain.Identity;
using System.Data.Entity.ModelConfiguration;

namespace MovieDomain.Configurations
{
	internal class CustomUserRolesConfigurations : EntityTypeConfiguration<CustomUserRole>
	{
		public CustomUserRolesConfigurations()
		{
			ToTable("UserRoles");

			HasKey(x => new { x.UserId, x.RoleId });
		}
	}
}
