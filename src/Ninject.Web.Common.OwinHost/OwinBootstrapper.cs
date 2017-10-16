// -------------------------------------------------------------------------------------------------
// <copyright file="OwinBootstrapper.cs" company="Ninject Project Contributors">
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
    using Microsoft.Owin;

    /// <summary>
    /// The Owin bootstrapper.
    /// </summary>
    public class OwinBootstrapper
    {
        /// <summary>
        /// The Ninject Owin request scope.
        /// </summary>
        public const string NinjectOwinRequestScope = "NinjectOwinRequestScope";

        private readonly Func<IKernel> createKernelCallback;
        private readonly IBootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Initializes a new instance of the <see cref="OwinBootstrapper"/> class.
        /// </summary>
        /// <param name="createKernelCallback">The create kernel callback function.</param>
        public OwinBootstrapper(Func<IKernel> createKernelCallback)
        {
            this.createKernelCallback = createKernelCallback;
        }

        /// <summary>
        /// Handles the owin context.
        /// </summary>
        /// <param name="next">The next moddleware factory.</param>
        /// <returns>
        /// The Ninject moddleware factory.
        /// </returns>
        public Func<IDictionary<string, object>, Task> Execute(Func<IDictionary<string, object>, Task> next)
        {
            this.bootstrapper.Initialize(this.createKernelCallback);

            return async context =>
            {
                using (var scope = new OwinRequestScope())
                {
                    context[NinjectOwinRequestScope] = scope;
                    await next(context);
                }
            };
        }
    }
}