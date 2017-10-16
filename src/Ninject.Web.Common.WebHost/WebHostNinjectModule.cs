// -------------------------------------------------------------------------------------------------
// <copyright file="WebHostNinjectModule.cs" company="Ninject Project Contributors">
//   Copyright (c) 2010-2011 bbv Software Services AG.
//   Copyright (c) 2011-2017 Ninject Contributors.
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Web.Common.WebHost
{
    using System.Web;
    using System.Web.Routing;

    /// <summary>
    /// Defines the bindings that are common for all ASP.NET web extensions.
    /// </summary>
    public class WebHostNinjectModule : GlobalKernelRegistrationModule<OnePerRequestHttpModule>
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            base.Load();
            this.Bind<RouteCollection>().ToConstant(RouteTable.Routes);
            this.Bind<HttpContextBase>().ToMethod(ctx => new HttpContextWrapper(HttpContext.Current)).InTransientScope();
            this.Bind<HttpContext>().ToMethod(ctx => HttpContext.Current).InTransientScope();
        }
    }
}