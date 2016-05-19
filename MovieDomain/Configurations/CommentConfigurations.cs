using MovieDomain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDomain.Configurations
{
    internal class CommentConfigurations : EntityTypeConfiguration<Comment>
    {
        public CommentConfigurations()
        {
           this.HasMany(c => c.Liked).WithMany(u => u.LikedComments);
            this.HasMany(c => c.NotLiked).WithMany(u => u.UnlikedComments);
            this.HasOptional(c => c.User).WithMany(u => u.Comments).HasForeignKey(c => c.UserId);
        }
    }
}
