using MovieDemo.Infrastructure.WebApi;
using MovieDomain.UnitOfWork;
using MovieServices.Auth;
using MovieServices.Interfaces;
using MovieServices.Service;
using MovieServices.Services;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MovieDemo.Infrastructure
{
    public static class MovieDemoIoC
    {
        public static void Configure(IKernel kernel)
        {
            

            // for WebAPI
            GlobalConfiguration.Configuration.DependencyResolver = new WebApiNinjectDependencyResolver(kernel);
            // for MVC

            AddBindings(kernel);
        }

        public static void AddBindings(IKernel kernel)
        {
            kernel.Bind<IMovieService>().To<MovieService>();
            kernel.Bind<MovieDomain.UnitOfWork.IUnitOfWorkFactory>().To<UnitOfWorkFactory>();
            kernel.Bind<IUserManager>().To<CustomUserManager>();
            kernel.Bind<IAuthService>().To<AuthService>();
           
        }
    }
}