using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MovieDomain.Entities
{
    public class Movie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string Url { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public decimal? ImdbRate { get; set; }

        public decimal? KinopoiskRate { get; set; }

        public string Genres { get; set; }

        public string Director { get; set; }


        public string MainActors { get; set; }

        public string Country { get; set; }

        public int? Year { get; set; }

        public virtual ICollection<Cinema> Cinemas { get; set; }
        public virtual ICollection<Showtime> Showtimes { get; set; }

        public Movie()
        {
            Cinemas = new List<Cinema>();
            Showtimes = new List<Showtime>();
        }

        public override bool Equals(object obj)
        {
            Movie input = obj as Movie;
            if(input == null)
            {
                return false;
            }

            return input.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}