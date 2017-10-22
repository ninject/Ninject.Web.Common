// -------------------------------------------------------------------------------------------------
// <copyright file="RequestScopeHandler.cs" company="Ninject Project Contributors">
//   Copyright (c) 2010-2011 bbv Software Services AG.
//   Copyright (c) 2011-2017 Ninject Project Contributors. All rights reserved.
//
//   Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
//   You may not use this file except in compliance with one of the Licenses.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//   or
//       http://www.microsoft.com/opensource/licenses.mspx
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
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