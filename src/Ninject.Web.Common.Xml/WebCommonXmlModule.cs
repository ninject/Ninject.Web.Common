// -------------------------------------------------------------------------------------------------
// <copyright file="WebCommonXmlModule.cs" company="Ninject Project Contributors">
//   Copyright (c) 2010-2011 bbv Software Services AG.
//   Copyright (c) 2011-2017 Ninject Contributors.
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Web.Common.Xml
{
    using Ninject.Extensions.Xml.Scopes;
    using Ninject.Modules;

    /// <summary>
    /// Ninject module for Ninject.Web.Common.Xml
    /// </summary>
    public class WebCommonXmlModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            this.Kernel.Components.Add<IScopeHandler, RequestScopeHandler>();
        }
    }
}