using MovieDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MovieServices.Models
{
    public class ScheduleKey
    {
        public Movie Movie { get; set; }
        public Cinema Cinema { get; set; }

        public override int GetHashCode()
        {
            return 17 + Convert.ToInt32(Movie.Id) * 23 + Convert.ToInt32(Cinema.Id) * 23;
        }

        public override bool Equals(object obj)
        {
            ScheduleKey key = obj as ScheduleKey;

            if(key == null)
            {
                return false;
            }

            return key.Movie.Id == Movie.Id && key.Cinema.Id == Cinema.Id;
        }
    }

    public class ScheduleModel
    {
        public DateTime Day { get; set; }
        public Dictionary<ScheduleKey, List<Showtime>> Sessions { get; set; }
    }
}