using MovieDomain.Contexts;
using MovieDomain.Entities;
using MovieDomain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDomain.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private MovieContext _context = new MovieContext();

        public IRepository<Movie> Movies { get; set; }

        public IRepository<Showtime> Showtimes { get; set; }

        public IRepository<Cinema> Cinemas { get; set; }

        public IRepository<User> Users { get; set; }

        public IRepository<Comment> Comments { get; set; }

        public IRepository<Rate> Likes { get; set; }

        public UnitOfWork()
        {
            Movies = new Repository<Movie>(_context);
            Showtimes = new Repository<Showtime>(_context);
            Cinemas = new Repository<Cinema>(_context);
            Users = new Repository<User>(_context);
            Comments = new Repository<Comment>(_context);
            Likes = new Repository<Rate>(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
