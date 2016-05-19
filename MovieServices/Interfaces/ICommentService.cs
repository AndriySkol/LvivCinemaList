using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieServices.Interfaces
{
    public interface ICommentService
    {
        void Like(long cId, long userId);
        void UnLike(long cId, long userId);
    }
}
