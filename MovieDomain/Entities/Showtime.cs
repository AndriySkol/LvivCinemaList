using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MovieDomain.Entities
{
    public enum Technology
    {
        D2,
        D3,
        D4x,
        IMAX

    };

    public class Showtime
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string OrderUrl { get; set; }

        public int HallId { get; set; }
        public string Technology { get; set; }
        public long CinemaId { get; set; }
        public long MovieId { get; set; }
        public Cinema Cinema { get; set; }
        public Movie Movie { get; set; }
    }
}