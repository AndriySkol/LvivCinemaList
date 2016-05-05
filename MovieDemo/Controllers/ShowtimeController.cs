using MovieDemo.Models;
using MovieDomain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace MovieDemo.Controllers
{
    public class ShowtimeController : Controller
    {
        // GET: Showtime

        public ActionResult Index(long? id)
        {
            if(!id.HasValue)
            {
                return View("Default");
            }

            var model = new SessionViewModel();
            using(var context = new MovieContext())
            {
                model.Showtime = context.Showtimes.Include("Movie").Include("Cinema").First(s => s.Id == id);
                model.Movie = model.Showtime.Movie;
                model.Cinema = model.Showtime.Cinema;
                ViewBag.IsAuth = User.Identity.IsAuthenticated;
                if(User.Identity.IsAuthenticated)
                {
                    long userId = User.Identity.GetUserId<long>();
                    model.Comments = context.Comments.Include("User")
                        .Where(c => c.MovieId == model.Showtime.MovieId && c.CinemaId == model.Showtime.CinemaId).ToList();
                    model.CommentsAmount = model.Comments.Count;
                    var likes = context.Likes
                        .Where(c => c.MovieId == model.Showtime.MovieId && c.CinemaId == model.Showtime.CinemaId);
                    model.LikesAmount = likes.Count();
                    model.IsLiked = likes.Any(l => l.UserId == userId);
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(CommentBindingModel comment)
    }
}