using MovieServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieDemo.Models
{
    public class HomeViewModel
    {
        public ScheduleModel ScheduleModel { get; set; }
        public FilterViewModel FilterModel { get; set; }
    }
}