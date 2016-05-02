using MovieDomain.Entities;
using MovieDomain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDomain.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Movie> Movies { get; set; }

        IRepository<Showtime> Showtimes { get; set; }

        IRepository<Cinema> Cinemas { get; set; }

        IRepository<User> Users { get; set; }

        IRepository<Comment> Comments { get; set; }

        IRepository<Like> Likes { get; set; }

        void Save();

    }
}
