// -------------------------------------------------------------------------------------------------
// <copyright file="NinjectHttpModule.cs" company="Ninject Project Contributors">
//   Copyright (c) 2010-2011 bbv Software Services AG.
//   Copyright (c) 2011-2017 Ninject Contributors.
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Web.Common.WebHost
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// HttpModule to add support for constructor injection to HttpModules
    /// </summary>
    public sealed class NinjectHttpModule : IHttpModule
    {
        private IList<IHttpModule> httpModules;

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication"/> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication context)
        {
            this.httpModules = new Bootstrapper().Kernel.GetAll<IHttpModule>().ToList();
            foreach (var httpModule in this.httpModules)
            {
                httpModule.Init(context);
            }
        }

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"/>.
        /// </summary>
        public void Dispose()
        {
            foreach (var httpModule in this.httpModules)
            {
                httpModule.Dispose();
            }

            this.httpModules.Clear();
        }
    }
}