using MovieDomain.Identity;
using System.Data.Entity.ModelConfiguration;


namespace MovieDomain.Configurations
{
	internal class CustomRoleConfigurations : EntityTypeConfiguration<CustomRole>
	{
		public CustomRoleConfigurations()
		{
			ToTable("Roles");

			HasKey(x => x.Id);
		}
	}
}
