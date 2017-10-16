// -------------------------------------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="Ninject Project Contributors">
//   Copyright (c) 2010-2011 bbv Software Services AG.
//   Copyright (c) 2011-2017 Ninject Contributors.
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Web.Common
{
    using System;
    using Ninject.Infrastructure.Language;

    /// <summary>
    /// A basic bootstrapper that can be used to setup web applications.
    /// </summary>
    public class Bootstrapper : IBootstrapper
    {
        /// <summary>
        /// The ninject kernel of the application
        /// </summary>
        private static IKernel kernelInstance;

        /// <summary>
        /// Gets the kernel.
        /// </summary>
        public IKernel Kernel
        {
            get { return kernelInstance; }
        }

        /// <summary>
        /// Starts the application.
        /// </summary>
        /// <param name="createKernelCallback">The create kernel callback function.</param>
        public void Initialize(Func<IKernel> createKernelCallback)
        {
            kernelInstance = createKernelCallback();
            kernelInstance.Components.GetAll<INinjectHttpApplicationPlugin>().Map(c => c.Start());
        }

        /// <summary>
        /// Releases the kernel on application end.
        /// </summary>
        public void ShutDown()
        {
            if (kernelInstance != null)
            {
                kernelInstance.Components.GetAll<INinjectHttpApplicationPlugin>().Map(c => c.Stop());
                kernelInstance.Dispose();
                kernelInstance = null;
            }
        }
    }
}