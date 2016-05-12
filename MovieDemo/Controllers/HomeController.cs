using MovieDemo.Models;
using MovieDomain.Entities;
using MovieServices.Models;
using MovieServices.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MovieDemo.Controllers
{
    public class HomeController : Controller
    {
        private IMovieService _service;

        public HomeController(IMovieService service)
        {
            _service = service;
         
        }

        // GET: Home
        public ActionResult Index([FromUri]long[] cinemas, long[] movies, string[] technologies, DateTime? date)
        {

            Func<Movie, bool> movieSelect =  m => false;
            Func<Cinema, bool> cinemaSelect = (m => false);
            Func<string, bool> techSelect = m => false;
            if(cinemas != null)
            {
                cinemaSelect = m => cinemas.Contains(m.Id);
            }
            
            if(movies != null)
            {
                movieSelect = m => movies.Contains(m.Id);
            }

            if(technologies != null)
            {
                techSelect = m => technologies.Contains(m);
            }

            if(!date.HasValue)
            {
                date = DateTime.Today;
            }


            ViewBag.IsAuth = User.Identity.IsAuthenticated;
            Dictionary<Cinema, bool> cinemaDict = _service.GetCinemas().ToDictionary(cinema => cinema, cinemaSelect);
            Dictionary<Movie, bool>  movieDict = _service.GetCurrentMovies().ToDictionary(movie => movie, movieSelect);
            FilterViewModel filterModel = new FilterViewModel { DateString = date.Value.ToString("yyyy-MM-dd"),Cinemas = cinemaDict, Movies = movieDict,
                Technologies = new List<string> { "DigiPlan-2d", "DigiPlan-3d", "imax-3d", "4dx-3d" }.ToDictionary(el => el, techSelect) };
            ScheduleModel schedModel = _service.Get(date.Value, cinemas, technologies, movies);
            HomeViewModel viewModel = new HomeViewModel { FilterModel = filterModel, ScheduleModel = schedModel };
            return View(viewModel);


        }

        public ActionResult TimeBadge(Showtime session)
        {
            ViewBag.Position = PositionEvaluator.GetPercent(session.Date);
            ViewBag.Color = session.Cinema.Name == "KingCross" ? "pink" : "teal";
            return PartialView("TimeBadge", session);
        }

    }
}