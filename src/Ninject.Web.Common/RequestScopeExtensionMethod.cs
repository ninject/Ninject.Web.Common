// -------------------------------------------------------------------------------------------------
// <copyright file="RequestScopeExtensionMethod.cs" company="Ninject Project Contributors">
//   Copyright (c) 2010-2011 bbv Software Services AG.
//   Copyright (c) 2011-2017 Ninject Contributors.
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Web.Common
{
    using System.Linq;

    using Ninject.Activation;
    using Ninject.Syntax;

    /// <summary>
    /// Defines extension methods the specify InRequestScope.
    /// </summary>
    public static class RequestScopeExtensionMethod
    {
        /// <summary>
        /// Sets the scope to request scope.
        /// </summary>
        /// <typeparam name="T">The type of the service.</typeparam>
        /// <param name="syntax">The syntax.</param>
        /// <returns>The syntax to define more information.</returns>
        public static IBindingNamedWithOrOnSyntax<T> InRequestScope<T>(this IBindingInSyntax<T> syntax)
        {
            return syntax.InScope(GetScope);
        }

        /// <summary>
        /// Gets the scope.
        /// </summary>
        /// <param name="ctx">The context.</param>
        /// <returns>The scope.</returns>
        private static object GetScope(IContext ctx)
        {
            var scope = ctx.Kernel.Components.GetAll<INinjectHttpApplicationPlugin>().Select(c => c.GetRequestScope(ctx)).FirstOrDefault(s => s != null);
            return scope;
        }
    }
}