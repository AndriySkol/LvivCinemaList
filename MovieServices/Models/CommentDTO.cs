using MovieDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieServices.Models
{
    public class CommentDTO : Comment
    {
        public CommentDTO(Comment c, Rate rate, User user, bool userLiked, bool userNotLiked)
        {
            Id = c.Id;
            this.Cinema = c.Cinema;
            this.Movie = c.Movie;
            this.User = user;
            this.UserId = c.UserId;
            this.UserRate = rate;
            this.UnhelpfulCount = c.UnhelpfulCount;
            this.Time = c.Time;
            this.HelpfulCount = c.HelpfulCount;
            MovieId = c.MovieId;
            CinemaId = c.CinemaId;
            Text = c.Text;
            CommentRate = c.Liked.Count() - c.NotLiked.Count();
            CurrUserLiked = userLiked;
            CurrUserDisliked = userNotLiked;
            
        }
        public Rate UserRate { get; set; }
        public long CommentRate { get; set; }

        public bool CurrUserLiked { get; set; }
        public bool CurrUserDisliked { get; set; }
    }
}
