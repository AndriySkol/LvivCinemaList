using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieDemo.Controllers
{
    public class CommentBindingModel
    {
        public string Text { get; set; }
        public long MovieId { get; set; }
        public long CinemaId { get; set; }
        public long ShowtimeId { get; set; }
    }
}
