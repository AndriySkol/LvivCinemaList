using MovieDomain.Entities;
using MovieServices.Models;
using MovieServices.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public ActionResult Index()
        {
            ScheduleModel model = _service.Get(DateTime.Today);
            return View(model);
        }

        public ActionResult TimeBadge(Showtime session)
        {
            ViewBag.Position = PositionEvaluator.GetPercent(session.Date);
            ViewBag.Color = session.Cinema.Name == "KingCross" ? "pink" : "teal";
            return PartialView("TimeBadge", session);
        }
    }
}