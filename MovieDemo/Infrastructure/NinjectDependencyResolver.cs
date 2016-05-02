using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using MovieServices.Services;
using MovieDomain.UnitOfWork;
using MovieDomain.Repository;
using Moq;
using MovieDomain.Entities;

namespace MovieDemo.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
          
        }

        /*private Mock<IUnitOfWork> GetMock()
        {
            Movie firstMovie = 
                new Movie
                    {
                        Id = 0, 
                        Start = DateTime.Today, 
                        End = null, Name = "Divergent", 
                        Url= "http:\\google.com.ua"
                    };
            Movie secondMovie = 
                new Movie 
                { 
                    Id = 1,  
                    Start = DateTime.Today, 
                    End = null, 
                    Name = "Zootopia",
                    Url= "http:\\google.com.ua"
                };
            List<Movie> moviesList = new List<Movie> { firstMovie, secondMovie };
            Showtime showtime1 = 
                
            List<Showtime> showtimeList =
                new List<Showtime> 
                {
                    new Showtime
                    {
                        MovieId = 1,
                        Date = DateTime.Today + TimeSpan.FromHours(10),
                        TechnologyId = 0,
                        Movie = firstMovie
                    },
                    new Showtime
                    {
                        MovieId = 1,
                        Date = DateTime.Today + TimeSpan.FromHours(14),
                        TechnologyId = 0
                    },
                    new Showtime
                    {
                        MovieId = 0,
                        Date = DateTime.Today + TimeSpan.FromHours(17),
                        TechnologyId = 0
                    },
                    new Showtime
                    {
                        MovieId = 0,
                        Date = DateTime.Today + TimeSpan.FromHours(19),
                        TechnologyId = 0
                    },
                };
            Mock<IRepository<Movie>> moviesMock = new Mock<IRepository<Movie>>();
            moviesMock.Setup(m => m.GetAll()).Returns(moviesList);
            
            Mock<IRepository<Showtime>> showtimesMock = new Mock<IRepository<Showtime>>();
            showtimesMock.Setup(m => m.GetAll()).Returns(showtimeList);

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.Movies).Returns(moviesMock.Object);
            unitOfWorkMock.Setup(m => m.Showtimes).Returns(showtimesMock.Object);
            return unitOfWorkMock;
        }*/
    }
}