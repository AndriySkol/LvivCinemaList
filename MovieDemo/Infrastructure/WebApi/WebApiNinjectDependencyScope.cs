﻿using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Web.Http.Dependencies;
using Ninject;
using Ninject.Syntax;
using System;


namespace MovieDemo.Infrastructure.WebApi
{
    public class WebAPINinjectDependencyScope : IDependencyScope
    {
        private IResolutionRoot resolver;

        internal WebAPINinjectDependencyScope(IResolutionRoot resolver)
        {
            Contract.Assert(resolver != null);

            this.resolver = resolver;
        }

        public void Dispose()
        {
            IDisposable disposable = resolver as IDisposable;
            if (disposable != null)
                disposable.Dispose();

            resolver = null;
        }

        public object GetService(Type serviceType)
        {
            if (resolver == null)
                throw new ObjectDisposedException("this", "This scope has already been disposed");

            return resolver.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (resolver == null)
                throw new ObjectDisposedException("this", "This scope has already been disposed");

            return resolver.GetAll(serviceType);
        }
    }
}