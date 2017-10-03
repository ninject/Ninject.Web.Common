//-------------------------------------------------------------------------------
// <copyright file="OwinAppBuilderExtensions.cs" company="bbv Software Services AG">
//   Copyright (c) 2012 bbv Software Services AG
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

namespace Ninject.Web.Common.OwinHost
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Owin;

    /// <summary>
    /// The OWIN app builder extensions.
    /// </summary>
    public static class OwinAppBuilderExtensions
    {
        /// <summary>
        /// The ninject OWIN bootstrapper.
        /// </summary>
        public const string NinjectOwinBootstrapperKey = "NinjectOwinBootstrapper";

        /// <summary>
        /// Use ninject middleware.
        /// </summary>
        /// <param name="app">
        /// The app.
        /// </param>
        /// <param name="createKernel">
        /// The function pointer.
        /// </param>
        /// <returns>
        /// The <see cref="IAppBuilder"/>.
        /// </returns>
        public static IAppBuilder UseNinjectMiddleware(this IAppBuilder app, Func<IKernel> createKernel)
        {
            var bootstrapper = new OwinBootstrapper(createKernel);

            app.Properties.Add(NinjectOwinBootstrapperKey, bootstrapper);


            var middleware = new Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>(bootstrapper.Execute);

            return app.Use(middleware);
        }  
    }
}