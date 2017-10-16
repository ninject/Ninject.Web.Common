// -------------------------------------------------------------------------------------------------
// <copyright file="NinjectSelfHostBootstrapper.cs" company="Ninject Project Contributors">
//   Copyright (c) 2010-2011 bbv Software Services AG.
//   Copyright (c) 2011-2017 Ninject Contributors.
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Web.Common.SelfHost
{
    using System;
    using Ninject.Web.Common;

    /// <summary>
    /// Self hosting bootstrapper
    /// </summary>
    public class NinjectSelfHostBootstrapper : IDisposable
    {
        /// <summary>
        /// The web common default bootstrapper
        /// </summary>
        private readonly IBootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectSelfHostBootstrapper"/> class.
        /// </summary>
        /// <param name="createKernelCallback">The create kernel callback.</param>
        /// <param name="selfHostConfigurations">The self host configurations.</param>
        public NinjectSelfHostBootstrapper(Func<IKernel> createKernelCallback, params object[] selfHostConfigurations)
        {
            this.bootstrapper.Initialize(() =>
                {
                    var kernel = createKernelCallback();
                    foreach (var selfHostConfiguration in selfHostConfigurations)
                    {
                        kernel.Bind(selfHostConfiguration.GetType()).ToConstant(selfHostConfiguration);
                    }

                    return kernel;
                });
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            var selfhosts = this.bootstrapper.Kernel.GetAll<INinjectSelfHost>();
            foreach (var selfhost in selfhosts)
            {
                selfhost.Start();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.bootstrapper.ShutDown();
        }
    }
}