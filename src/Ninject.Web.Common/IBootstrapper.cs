// -------------------------------------------------------------------------------------------------
// <copyright file="IBootstrapper.cs" company="Ninject Project Contributors">
//   Copyright (c) 2010-2011 bbv Software Services AG.
//   Copyright (c) 2011-2017 Ninject Contributors.
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Web.Common
{
    using System;
    using Ninject.Infrastructure;

    /// <summary>
    /// A basic bootstrapper that can be used to setup web applications.
    /// </summary>
    public interface IBootstrapper : IHaveKernel
    {
        /// <summary>
        /// Starts the application.
        /// </summary>
        /// <param name="createKernelCallback">The create kernel callback function.</param>
        void Initialize(Func<IKernel> createKernelCallback);

        /// <summary>
        /// Releases the kernel on application end.
        /// </summary>
        void ShutDown();
    }
}