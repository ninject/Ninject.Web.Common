// -------------------------------------------------------------------------------------------------
// <copyright file="OnePerRequestHttpModule.cs" company="Ninject Project Contributors">
//   Copyright (c) 2010-2011 bbv Software Services AG.
//   Copyright (c) 2011-2017 Ninject Contributors.
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Web.Common.WebHost
{
    using System.Web;
    using Ninject.Activation.Caching;

    /// <summary>
    /// Provides callbacks to more aggressively collect objects scoped to HTTP requests.
    /// </summary>
    public sealed class OnePerRequestHttpModule : GlobalKernelRegistration, IHttpModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OnePerRequestHttpModule"/> class.
        /// </summary>
        public OnePerRequestHttpModule()
        {
            this.ReleaseScopeAtRequestEnd = true;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the request scope shall be released immediately after the request has ended.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the request scope shall be released immediately after the request has ended.; otherwise, <c>false</c>.
        /// </value>
        public bool ReleaseScopeAtRequestEnd
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes the module.
        /// </summary>
        /// <param name="application">The <see cref="HttpApplication"/> whose instances will be managed.</param>
        public void Init(HttpApplication application)
        {
            application.EndRequest += (o, e) => this.DeactivateInstancesForCurrentHttpRequest();
        }

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"/>.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Deactivates instances owned by the current <see cref="HttpContext"/>.
        /// </summary>
        public void DeactivateInstancesForCurrentHttpRequest()
        {
            if (this.ReleaseScopeAtRequestEnd)
            {
                var context = HttpContext.Current;
                this.MapKernels(kernel => kernel.Components.Get<ICache>().Clear(context));
            }
        }
    }
}