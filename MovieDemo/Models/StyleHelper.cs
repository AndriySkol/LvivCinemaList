using MovieDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieDemo.Models
{
    public static class StyleHelper
    {
        public static string CinemaStyle(Cinema cinema)
        {
            if(cinema.Name == "KingCross")
            {
                return "primary";
            }
            else
            {
                return "success";
            }
        }
    }
}