using MovieDomain.Entities;
using MovieServices.Models;
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
        public double Rate { get; set; }
        public bool IsRate { get; set; }
        public int? UserRate { get; set; }
        public IList<CommentDTO> Comments { get; set; }
        public bool IsLiked { get; set; }
        public int LikesAmount { get; set; }
        public int CommentsAmount { get; set; }
        public string CommentOrder { get; set; }
    }
}