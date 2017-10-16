// -------------------------------------------------------------------------------------------------
// <copyright file="RequestScopeHandler.cs" company="Ninject Project Contributors">
//   Copyright (c) 2010-2011 bbv Software Services AG.
//   Copyright (c) 2011-2017 Ninject Contributors.
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Web.Common.Xml
{
    using Ninject.Components;
    using Ninject.Extensions.Xml.Scopes;
    using Ninject.Syntax;

    /// <summary>
    /// The processor for the request scope
    /// </summary>
    public class RequestScopeHandler : NinjectComponent, IScopeHandler
    {
        /// <summary>
        /// Gets the name of the scope processed by this instance.
        /// </summary>
        /// <value>The name of the scope processed by this instance.</value>
        public string ScopeName
        {
            get
            {
                return "request";
            }
        }

        /// <summary>
        /// Sets the scope using the given syntax.
        /// </summary>
        /// <param name="syntax">The syntax that is used to set the scope.</param>
        public void SetScope(IBindingInSyntax<object> syntax)
        {
            syntax.InRequestScope();
        }
    }
}