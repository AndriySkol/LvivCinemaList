using MovieDomain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDomain.Configurations
{
    internal class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("Users");

            HasKey(x => x.Id);
            HasMany(x => x.UnlikedComments).WithMany(x => x.NotLiked);
            HasMany(x => x.LikedComments).WithMany(x => x.Liked);
            HasMany(x => x.Comments).WithOptional(c => c.User).HasForeignKey(c => c.UserId);
        }
    }
}
