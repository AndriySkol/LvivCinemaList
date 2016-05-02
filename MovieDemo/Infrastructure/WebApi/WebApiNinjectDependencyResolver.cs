using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace MovieDemo.Infrastructure.WebApi
{
    public class WebApiNinjectDependencyResolver : WebAPINinjectDependencyScope, IDependencyResolver
    {
        private IKernel kernel;

        public WebApiNinjectDependencyResolver(IKernel kernel)
            : base(kernel)
        {
            this.kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new WebAPINinjectDependencyScope(kernel.BeginBlock());
        }
    }
}