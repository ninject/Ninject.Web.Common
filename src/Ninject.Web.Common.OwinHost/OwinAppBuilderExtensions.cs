// -------------------------------------------------------------------------------------------------
// <copyright file="OwinAppBuilderExtensions.cs" company="Ninject Project Contributors">
//   Copyright (c) 2010-2011 bbv Software Services AG.
//   Copyright (c) 2011-2017 Ninject Contributors.
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Web.Common.OwinHost
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Owin;

    /// <summary>
    /// The OWIN app builder extensions.
    /// </summary>
    public static class OwinAppBuilderExtensions
    {
        /// <summary>
        /// The ninject OWIN bootstrapper key.
        /// </summary>
        public const string NinjectOwinBootstrapperKey = "NinjectOwinBootstrapper";

        /// <summary>
        /// Uses ninject middleware.
        /// </summary>
        /// <param name="app">The <see cref="IAppBuilder"/> passed in.</param>
        /// <param name="createKernel">The kernel callback.</param>
        /// <returns>The <see cref="IAppBuilder"/> passed out.</returns>
        public static IAppBuilder UseNinjectMiddleware(this IAppBuilder app, Func<IKernel> createKernel)
        {
            var bootstrapper = new OwinBootstrapper(createKernel);

            app.Properties.Add(NinjectOwinBootstrapperKey, bootstrapper);

            var middleware = new Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>(bootstrapper.Execute);

            return app.Use(middleware);
        }
    }
}