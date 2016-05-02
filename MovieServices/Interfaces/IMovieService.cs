using MovieDomain.Entities;
using MovieServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieServices.Services
{
    public interface IMovieService
    {
        ScheduleModel Get(DateTime start, long[] cinemaId = null, string[] technlogies = null, long[] movieId = null);
    }
}
