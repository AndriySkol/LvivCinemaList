using MovieDomain.Entities;
using MovieServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieDemo.Models
{
    public class MovieViewModel
    {
        public Movie Movie { get; set; }
        public ScheduleModel ScheduleModel { get; set; }

        public bool GoesToSchedule { get; set; }
    }
}