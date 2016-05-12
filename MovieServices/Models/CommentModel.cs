using MovieDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieServices.Models
{
    public class CommentModel
    {
        public string Text { get; set; }
        public int Rate { get; set; }
        public DateTime DateTime { get; set; }
        public User Author { get; set; }
    }
}
