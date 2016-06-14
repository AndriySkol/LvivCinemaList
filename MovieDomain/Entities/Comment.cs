using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDomain.Entities
{
    public class Comment 
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public long HelpfulCount { get; set; }
        public long UnhelpfulCount { get; set; }
        public DateTime Time { get; set; }

        public bool isBanned { get; set; }

        public Movie Movie { get; set; }
        public Cinema Cinema { get; set; }
        public User User { get; set; }
        public long? UserId { get; set; }
        public long CinemaId { get; set; }
        public long MovieId { get; set; }

        public virtual ICollection<User> Liked { get; set; }
        public virtual ICollection<User> NotLiked { get; set; }

        public Comment()
        {
            Liked = new List<User>();
            NotLiked = new List<User>();
        }
        
    }
}
