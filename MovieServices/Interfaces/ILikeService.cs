using MovieDemo.ApiControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieServices.Interfaces
{
    public interface ILikeService
    {
        void SubmitLike(LikeBindingModel like, long userId);
    }
}
