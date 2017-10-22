// -------------------------------------------------------------------------------------------------
// <copyright file="HttpApplicationInitializationHttpModule.cs" company="Ninject Project Contributors">
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

namespace Ninject.Web.Common
{
    using System;
    using System.Web;

    using Ninject.Infrastructure.Disposal;

    /// <summary>
    /// Initializes a <see cref="HttpApplication"/> instance
    /// </summary>
    public class HttpApplicationInitializationHttpModule : DisposableObject, IHttpModule
    {
        private readonly Func<IKernel> lazyKernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpApplicationInitializationHttpModule"/> class.
        /// </summary>
        /// <param name="lazyKernel">The kernel retriever.</param>
        public HttpApplicationInitializationHttpModule(Func<IKernel> lazyKernel)
        {
            this.lazyKernel = lazyKernel;
        }

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication"/> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication context)
        {
            this.lazyKernel().Inject(context);
        }
    }
}