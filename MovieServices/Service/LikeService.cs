using MovieDemo.ApiControllers;
using MovieDomain.Entities;
using MovieDomain.UnitOfWork;
using MovieServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieServices.Service
{
    public class LikeService : ILikeService
    {
        private IUnitOfWorkFactory _factory;
        public LikeService(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }

        public void SubmitLike(LikeBindingModel like, long userId)
        {
            using (var unit = _factory.Create())
            {
               
                Rate entity = unit.Likes.FirstOrDefault(l => l.MovieId == like.MovieId && l.CinemaId == like.CinemaId && l.UserId == userId);
                if (entity != null)
                {
                    entity.Value = like.Rate;
                    unit.Likes.Update(entity);
                }
                else
                {
                    unit.Likes.Insert(new Rate { UserId = userId, CinemaId = like.CinemaId, MovieId = like.MovieId, Value = like.Rate });
                }

                unit.Save();
            }
        }
    }
}
