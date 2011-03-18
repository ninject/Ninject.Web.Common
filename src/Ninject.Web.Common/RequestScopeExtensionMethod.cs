//-------------------------------------------------------------------------------
// <copyright file="NinjectHttpApplication.cs" company="bbv Software Services AG">
//   Copyright (c) 2010 bbv Software Services AG
//   Author: Remo Gloor (remo.gloor@gmail.com)
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

namespace Ninject.Web.Common
{
    using System.Linq;

    using Ninject.Activation;
    using Ninject.Syntax;

    /// <summary>
    /// Defines extension methods the specifiy InRequestScope.
    /// </summary>
    public static class RequestScopeExtensionMethod
    {
        /// <summary>
        /// Sets the scope to request scope.
        /// </summary>
        /// <typeparam name="T">The type of the service.</typeparam>
        /// <param name="syntax">The syntax.</param>
        /// <returns>Teh syntax to define more inforametion.</returns>
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
            var scope = ctx.Kernel.Components.GetAll<INinjectHttpApplicationPlugin>().Select(c => c.RequestScope).FirstOrDefault(s => s != null);
            return scope;
        }
    }
}