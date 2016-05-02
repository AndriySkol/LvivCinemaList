using MovieDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieDemo.Models
{
    public class FilterViewModel
    {
        public string DateString { get; set; }
        public Dictionary<Cinema, bool> Cinemas { get; set; }
        public Dictionary<Movie, bool> Movies { get; set; }
        public Dictionary<string, bool> Technologies { get; set; }
    }
}