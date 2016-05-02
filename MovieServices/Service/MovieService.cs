using MovieDomain.Entities;
using MovieDomain.UnitOfWork;
using MovieServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MovieServices.Services
{
    public class MovieService : IMovieService
    {
        IUnitOfWorkFactory _unitOfWorkFactory;

        public int TechnologyId { get; set; }

        public MovieService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;

        }


        public ScheduleModel Get(DateTime start, long[] cinemaIds = null, string[] technlogies = null, long[] movieIds = null)
        {
            DateTime end = start + TimeSpan.FromDays(1);
            using(IUnitOfWork unit = _unitOfWorkFactory.Create())
            {
                IEnumerable<Showtime> query = unit.Showtimes.Get(s => s.Date >= start && s.Date <= end, s => s.OrderByDescending(el=> el.MovieId).ThenBy(el => el.CinemaId), "Cinema,Movie");
                if(cinemaIds != null)
                {
                    query = query.Where(s => cinemaIds.Contains(s.CinemaId));
                }

                if(technlogies != null)
                {
                    query = query.Where(s => technlogies.Contains(s.Technology));
                }

                if(movieIds != null)
                {
                    query = query.Where(s => movieIds.Contains(s.MovieId));
                }

                ScheduleModel model = new ScheduleModel { Day = start };
                model.Sessions = query.GroupBy(s => new ScheduleKey { Cinema = s.Cinema, Movie = s.Movie }).ToDictionary(el => el.Key, el => el.ToList());

                return model;
            }
        }

        public IEnumerable<Cinema> GetCinemas()
        {
            using(IUnitOfWork unit = _unitOfWorkFactory.Create())
            {
                return unit.Cinemas.Get();
            }
        }
        public IEnumerable<Movie> GetCurrentMovies()
        {
            using (IUnitOfWork unit = _unitOfWorkFactory.Create())
            {
                return unit.Movies.Get(m => m.End != null && m.End >= DateTime.Now && m.Start <= DateTime.Now);
            }
        }
    }
}
