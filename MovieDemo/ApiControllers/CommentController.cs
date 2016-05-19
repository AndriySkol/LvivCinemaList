using MovieDomain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MovieDomain.Entities;
using MovieServices.Interfaces;

namespace MovieDemo.ApiControllers
{
    public class CommentController : ApiController
    {
        private ICommentService _service;
        public CommentController(ICommentService service)
        {
            _service = service;
        }
        public IHttpActionResult Like([FromBody]long cid)
        {
            long userId = User.Identity.GetUserId<long>();
            _service.Like(cid, userId);
            return Ok();
        }

        public IHttpActionResult UnLike([FromBody]long cid)
        {
            long userId = User.Identity.GetUserId<long>();
            _service.UnLike(cid, userId);
            return Ok();
        }
    }
}
