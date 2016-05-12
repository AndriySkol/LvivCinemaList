using System.Data.Entity.ModelConfiguration;
using MovieDomain.Identity;

namespace MovieDomain.Configurations
{
	internal class CustomUserClaimsConfigurations : EntityTypeConfiguration<CustomUserClaim>
	{
		public CustomUserClaimsConfigurations()
		{
			ToTable("UserClaims");

			HasKey(x => x.Id);
		}
	}
}
