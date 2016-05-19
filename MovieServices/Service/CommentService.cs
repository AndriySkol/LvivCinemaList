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
    public class CommentService : ICommentService
    {
        private IUnitOfWorkFactory _factory;
        public CommentService(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }

        public void Like(long cId, long userId)
        {
            using (var unit = _factory.Create())
            {

                var user = unit.Users.FirstOrDefault(u => u.Id == userId);
                Comment comment = unit.Comments.FirstOrDefault(c => c.Id == cId);
                bool hasUnliked = user.UnlikedComments.Contains(comment),
                    hasLiked = user.LikedComments.Contains(comment);

                if (hasUnliked)
                {
                    user.UnlikedComments.Remove(comment);
                    unit.Users.Update(user);
                }

                if (!hasLiked)
                {
                    user.LikedComments.Add(comment);
                    unit.Users.Update(user);
                }

                unit.Save();
            }
        }


        public void UnLike(long cId, long userId)
        {
            using(var unit = _factory.Create())
            {
                var user = unit.Users.FirstOrDefault(u => u.Id == userId);
                Comment comment = unit.Comments.FirstOrDefault(c => c.Id == cId);
                bool hasUnliked = user.UnlikedComments.Contains(comment),
                    hasLiked = user.LikedComments.Contains(comment);


                if (hasLiked)
                {
                    user.LikedComments.Remove(comment);
                    unit.Users.Update(user);
                }


                if (!hasUnliked)
                {
                    user.UnlikedComments.Add(comment);
                    unit.Users.Update(user);
                }

                unit.Save();
            }
        }
    }
}
