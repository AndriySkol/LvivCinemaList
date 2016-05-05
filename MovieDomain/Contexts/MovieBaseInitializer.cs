using Microsoft.AspNet.Identity;
using MovieDomain.Auth;
using MovieDomain.Entities;
using MovieDomain.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MovieDomain.Contexts
{
    class MovieBaseInitializer : DropCreateDatabaseIfModelChanges<MovieContext>
    {
        protected override void Seed(MovieContext context)
        {
            var cross = new Cinema { Name = "KingCross" };
            var forum = new Cinema { Name = "Forum" };
            context.Cinemas.Add(cross);
            context.Cinemas.Add(forum);
            context.SaveChanges();
            XElement elementKross = XElement.Load("http://planetakino.ua/lvov/ua/showtimes/xml/");
            XElement elementForum = XElement.Load("http://planetakino.ua/lvov2/ua/showtimes/xml/");
            var moviesCross = elementKross.Element("movies").Elements().Select(e => GetMovie(e));
            var moviesForum = elementForum.Element("movies").Elements().Select(e => GetMovie(e));
            var movies = moviesCross.Union(moviesForum).ToList();
            movies.ForEach(m => cross.Movies.Add(m));
            context.SaveChanges();
            var showtimesKross =
                elementKross.Descendants("show").Select(s => GetShowtime(s, cross.Id));
            var showtimesForum =
                elementForum.Descendants("show").Select(s => GetShowtime(s, forum.Id));
            showtimesKross = showtimesKross.Where(s => movies.Any(m => m.Id == s.MovieId)).ToList();
            showtimesForum = showtimesForum.Where(s => movies.Any(m => m.Id == s.MovieId)).ToList();
            context.Showtimes.AddRange(showtimesKross);
            context.Showtimes.AddRange(showtimesForum);
            context.SaveChanges();
            var authContext = new AuthorizationContext();
            CustomUserManager userManager = new CustomUserManager(new CustomUserStore(authContext));
            User admin = new User { UserName = "admin", Email = "admin@mail.com" };
            userManager.Create(admin, "12345678");
            RoleManager<CustomRole, long> manager = new RoleManager<CustomRole, long>(new CustomRoleStore(authContext));
            CustomRole role = new CustomRole { Name = "Admin" };
            manager.Create(role);
            userManager.AddToRole<User, long>(admin.Id, "admin");
        }


        Showtime GetShowtime(XElement element, long Id)
        {
            Showtime showtime = new Showtime {
                OrderUrl = element.Attribute("order-url").Value,
                CinemaId = Id,
                Technology = element.Attribute("technology").Value,
                Date = DateTime.Parse(element.Attribute("full-date").Value),
                MovieId = int.Parse(element.Attribute("movie-id").Value),
                HallId = int.Parse(element.Attribute("hall-id").Value)
            };
            return showtime;
           
        }
        Movie GetMovie(XElement element)
        {
            string endData = element.Element("dt-end").Value;
            Movie result  = 
                new Movie{
                Id = int.Parse(element.Attribute("id").Value),
                Url = element.Attribute("url").Value,
                Name = element.Element("title").Value,
                Start = DateTime.Parse(element.Element("dt-start").Value),
                End = endData == String.Empty ? null : (DateTime?)DateTime.Parse(endData)
            };
            return result;
        }
            
    }
}
