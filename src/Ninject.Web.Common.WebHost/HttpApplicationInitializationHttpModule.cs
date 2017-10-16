// -------------------------------------------------------------------------------------------------
// <copyright file="HttpApplicationInitializationHttpModule.cs" company="Ninject Project Contributors">
//   Copyright (c) 2010-2011 bbv Software Services AG.
//   Copyright (c) 2011-2017 Ninject Contributors.
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Web.Common
{
    using System;
    using System.Web;
    using Ninject.Infrastructure.Disposal;

    /// <summary>
    /// Initializes a <see cref="HttpApplication"/> instance
    /// </summary>
    public class HttpApplicationInitializationHttpModule : DisposableObject, IHttpModule
    {
        private readonly Func<IKernel> lazyKernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpApplicationInitializationHttpModule"/> class.
        /// </summary>
        /// <param name="lazyKernel">The kernel retriever.</param>
        public HttpApplicationInitializationHttpModule(Func<IKernel> lazyKernel)
        {
            this.lazyKernel = lazyKernel;
        }

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication"/> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication context)
        {
            this.lazyKernel().Inject(context);
        }
    }
}