// -------------------------------------------------------------------------------------------------
// <copyright file="INinjectHttpApplicationPlugin.cs" company="Ninject Project Contributors">
//   Copyright (c) 2010-2011 bbv Software Services AG.
//   Copyright (c) 2011-2017 Ninject Contributors.
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Web.Common
{
    using Ninject.Activation;
    using Ninject.Components;

    /// <summary>
    /// Interface for the plugins of Ninject.Web.Common
    /// </summary>
    public interface INinjectHttpApplicationPlugin : INinjectComponent
    {
        /// <summary>
        /// Gets the request scope.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The request scope.</returns>
        object GetRequestScope(IContext context);

        /// <summary>
        /// Starts this instance.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops this instance.
        /// </summary>
        void Stop();
    }
}