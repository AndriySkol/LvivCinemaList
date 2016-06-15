using MovieDemo.Models;
using MovieDomain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MovieDomain.Entities;
using MovieServices.Models;
using MovieServices.Services;

namespace MovieDemo.Controllers
{
    public class ShowtimeController : Controller
    {

        private IMovieService _service;

        public ShowtimeController(IMovieService service)
        {
            _service = service;
         
        }
        // GET: Showtime

        public ActionResult Index(long? id, string orderString)
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
                    bool isAdmin = User.IsInRole("Admin");
                    long userId = User.Identity.GetUserId<long>();

                    User user = context.Users.Find(userId);
                    model.IsUserBanned = user.IsBanned;

                    var comments = context.Comments
                        .Where(c => c.MovieId == model.Showtime.MovieId && c.CinemaId == model.Showtime.CinemaId && (isAdmin || !c.isBanned))
                        .Select(c => new { Comment = c, Rate = c.User.Rates.FirstOrDefault(r => r.MovieId == c.MovieId && r.CinemaId == c.CinemaId), User = c.User, UserLiked = c.Liked.Any(u => u.Id == userId), UserNotLiked = c.NotLiked.Any(u => u.Id == userId) }).ToList();

                    model.Comments = comments.ConvertAll(c => new CommentDTO(c.Comment, c.Rate, c.User, c.UserLiked, c.UserNotLiked));
                    model.CommentOrder = orderString ?? String.Empty;
                    switch(orderString)
                    {
                        case "timeAsc" :
                            model.Comments = model.Comments.OrderBy(c => c.Time).ToList();
                            break;
                        case "timeDesc" :
                             model.Comments = model.Comments.OrderByDescending(c => c.Time).ToList();
                            break;
                        case "popularFirst":
                             model.Comments = model.Comments.OrderByDescending(c => c.CommentRate).ToList();
                             break;
                        case "highFirst":
                             model.Comments = model.Comments.OrderByDescending(c => c.UserRate.Value).ThenByDescending(c => c.CommentRate).ToList();
                             break;
                        case "worseFirst" :
                            model.Comments = model.Comments.OrderBy(c => c.UserRate.Value).ThenByDescending(c => c.CommentRate).ToList();
                            break;
                        default: break;

                    }



                    model.CommentsAmount = model.Comments.Count;
                    var likes = context.Likes
                        .Where(c => c.MovieId == model.Showtime.MovieId && c.CinemaId == model.Showtime.CinemaId);
                    model.LikesAmount = likes.Count();
                    model.IsLiked = likes.Any(l => l.UserId == userId);
                    model.IsRate = model.LikesAmount != 0;
                    Rate like = likes.FirstOrDefault(l => l.UserId == userId && l.MovieId == model.Movie.Id && l.CinemaId == model.Cinema.Id); 
                    if(like == null)
                    {
                        model.UserRate = null;
                    }
                    else
                    {
                        model.UserRate = like.Value ;

                    }
                    if(model.IsRate)
                    {
                        model.Rate = likes.Sum(l => l.Value) * 1.0 / model.LikesAmount;
                    }

                }
            }
            return View(model);
        }

        public ActionResult ShowMovie(long id, DateTime date, bool toSchedule = false)
        {
            MovieViewModel model = new MovieViewModel { GoesToSchedule = toSchedule };
            model.ScheduleModel = _service.Get(date, null, null, new long[] { id });
            model.Movie = _service.GetMovie(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(CommentBindingModel comment)
        {
           
            using (var context = new MovieContext())
            {
                User user = context.Users.Find(User.Identity.GetUserId<long>());
                if(!user.IsBanned)
                {
                    context.Comments.Add(
                   new Comment
                   {
                       CinemaId = comment.CinemaId,
                       MovieId = comment.MovieId,
                       Time = DateTime.Now,
                       Text = comment.Text,
                       UserId = User.Identity.GetUserId<long>()
                   });
                   context.SaveChanges();
                }
               
            }

            return RedirectToAction("Index", new { id = comment.ShowtimeId });
        }
    }
}