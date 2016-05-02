using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDomain.Entities
{
    public class Cinema
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
        public virtual ICollection<Showtime> Showtimes { get; set; }

        public Cinema()
        {
            Movies = new List<Movie>();
            Showtimes = new List<Showtime>();
        }
    }
}
