using MovieDomain.Contexts;
using MovieDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace MovieDemo.ApiControllers
{
    public class LikesController : ApiController
    {
      
        public IHttpActionResult Like(LikeBindingModel like)
        {
            using(var context = new MovieContext())
            {
                context.Likes.Add(new Like { MovieId = like.MovieId, CinemaId = like.CinemaId, UserId = User.Identity.GetUserId<long>() });
                context.SaveChanges();
            }

            return Ok();
        }

        public IHttpActionResult Unlike(LikeBindingModel like)
        {
            using (var context = new MovieContext())
            {
                var id = User.Identity.GetUserId<long>();
                var likeEntity = context.Likes.First(l => l.UserId == id  && l.CinemaId == like.CinemaId && l.MovieId == like.MovieId);

                context.Likes.Remove(likeEntity);
                context.SaveChanges();
            }

            return Ok();
        }
    }
}