//-------------------------------------------------------------------------------
// <copyright file="OnePerRequestModule.cs" company="bbv Software Services AG">
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
    using System.Web;
    using Ninject.Activation.Caching;

    /// <summary>
    /// Provides callbacks to more aggressively collect objects scoped to HTTP requests.
    /// </summary>
    public class OnePerRequestModule : GlobalKernelRegistration, IHttpModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OnePerRequestModule"/> class.
        /// </summary>
        public OnePerRequestModule()
        {
            this.ReleaseScopeAtRequestEnd = true;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the request scope shall be released immediately after the request has ended.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the request scope shall be released immediately after the request has ended.; otherwise, <c>false</c>.
        /// </value>
        public bool ReleaseScopeAtRequestEnd
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes the module.
        /// </summary>
        /// <param name="application">The <see cref="HttpApplication"/> whose instances will be managed.</param>
        public void Init(HttpApplication application)
        {
            application.EndRequest += (o, e) => this.DeactivateInstancesForCurrentHttpRequest();
        }

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"/>.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Deactivates instances owned by the current <see cref="HttpContext"/>.
        /// </summary>
        public void DeactivateInstancesForCurrentHttpRequest()
        {
            if (this.ReleaseScopeAtRequestEnd)
            {
                var context = HttpContext.Current;
                this.MapKernels(kernel => kernel.Components.Get<ICache>().Clear(context));
            }
        }
    }
}
