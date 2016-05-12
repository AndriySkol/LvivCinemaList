using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieDemo.ApiControllers
{
    public class LikeBindingModel
    {
        public long MovieId { get; set; }
        public long CinemaId { get; set; }
    }
}
