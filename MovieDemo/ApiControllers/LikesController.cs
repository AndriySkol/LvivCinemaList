using MovieDomain.Contexts;
using MovieDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MovieServices.Service;
using MovieServices.Interfaces;

namespace MovieDemo.ApiControllers
{
    public class LikesController : ApiController
    {
        private ILikeService _service;
        public LikesController(ILikeService service)
        {
            _service = service;
        }
        public IHttpActionResult Like(LikeBindingModel like)
        {
            
            long userId = User.Identity.GetUserId<long>();
            if(like != null)
            {
                _service.SubmitLike(like, userId);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
 
        }

    }
}