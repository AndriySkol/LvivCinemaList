using MovieDomain.Identity;
using System.Data.Entity.ModelConfiguration;


namespace MovieDomain.Configurations
{
	internal class CustomUserLoginsConfigurations : EntityTypeConfiguration<CustomUserLogin>
	{
		public CustomUserLoginsConfigurations()
		{
			ToTable("UserLogins");

			HasKey(x => new { x.ProviderKey, x.LoginProvider, x.UserId });
		}
	}
}
