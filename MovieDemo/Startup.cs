using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieDemo.App_Start;
using MovieDemo.Infrastructure;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Routing;

[assembly: OwinStartup(typeof(MovieDemo.Startup))]
namespace MovieDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            MovieDemoIoC.Configure(NinjectWebCommon.Kernel);
            this.ConfigureAuth(app);
            //app.UseWebApi(RegisterHttpConfiguration());

        }
    }
}