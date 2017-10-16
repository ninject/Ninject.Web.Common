// -------------------------------------------------------------------------------------------------
// <copyright file="INinjectSelfHost.cs" company="Ninject Project Contributors">
//   Copyright (c) 2010-2011 bbv Software Services AG.
//   Copyright (c) 2011-2017 Ninject Contributors.
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Web.Common.SelfHost
{
    /// <summary>
    /// A self host for web common.
    /// </summary>
    public interface INinjectSelfHost
    {
        /// <summary>
        /// Starts this self host.
        /// </summary>
        void Start();
    }
}