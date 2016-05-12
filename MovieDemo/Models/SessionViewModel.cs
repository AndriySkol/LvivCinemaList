using MovieDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieDemo.Models
{
    public class SessionViewModel
    {
        public Showtime Showtime { get; set; }
        public Movie Movie { get; set; }
        public Cinema Cinema { get; set; }

        public IList<Comment> Comments { get; set; }
        public bool IsLiked { get; set; }
        public int LikesAmount { get; set; }
        public int CommentsAmount { get; set; }
    }
}