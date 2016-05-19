using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MovieDomain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDomain.Entities
{
    public class User : IdentityUser<long, CustomUserLogin, CustomUserRole, CustomUserClaim>, IUser<long>
    {
        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<Comment> LikedComments { get; set; }
        public virtual ICollection<Comment> UnlikedComments { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public User() : base()
        {
            
            Rates = new List<Rate>();
            Comments = new List<Comment>();
            LikedComments = new List<Comment>();
            UnlikedComments = new List<Comment>();
        }

        public User(int id) : this()
        {
            Id = id;
        }
    }
}
