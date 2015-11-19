//-------------------------------------------------------------------------------
// <copyright file="OwinBootstrapper.cs" company="bbv Software Services AG">
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

    using Microsoft.Owin;

    using Ninject.Modules;

    /// <summary>
    /// The OWIN bootstrapper.
    /// </summary>
     public class OwinBootstrapper
    {
        /// <summary>
        /// The ninject OWIN request scope.
        /// </summary>
        public const string NinjectOwinRequestScope = "Ninject_Owin_Request_Scope";

        /// <summary>
        /// The modules.
        /// </summary>
        private readonly IList<INinjectModule> modules = new List<INinjectModule>();

        /// <summary>
        /// The create kernel.
        /// </summary>
        private readonly Func<IKernel> createKernel;

        /// <summary>
        /// The bootstrapper.
        /// </summary>
        private Bootstrapper bootstrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="OwinBootstrapper"/> class.
        /// </summary>
        /// <param name="createKernel">
        /// The create kernel.
        /// </param>
        public OwinBootstrapper(Func<IKernel> createKernel)
        {
            this.createKernel = createKernel;
        }

        /// <summary>
        /// The execute.
        /// </summary>
        /// <param name="next">The next.</param>
        /// <returns>
        /// The <see cref="Func{IDictionary{string, object}, Task}"/>.
        /// </returns>
        public Func<IDictionary<string, object>, Task> Execute(Func<IDictionary<string, object>, Task> next)
        {
            if (this.bootstrapper == null)
            {
                lock (this.modules)
                {
                    if (this.bootstrapper == null)
                    {
                        var initializingBootstrapper = new Bootstrapper();
                        initializingBootstrapper.Initialize(this.CreateKernel);
                        this.bootstrapper = initializingBootstrapper;
                    }
                }
            }

            return async (context) => {
                using (var scope = new OwinRequestScope())
                {
                    context[NinjectOwinRequestScope] = scope;
                    await next(context);
                }
            };
        }

        /// <summary>
        /// Add a new module.
        /// </summary>
        /// <param name="ninjectModule">
        /// The module.
        /// </param>
        public void AddModule(NinjectModule ninjectModule)
        {
            this.modules.Add(ninjectModule);
        }

        /// <summary>
        /// Create the kernel.
        /// </summary>
        /// <returns>
        /// The <see cref="IKernel"/>.
        /// </returns>
        private IKernel CreateKernel()
        {
            var kernel = this.createKernel();
            kernel.Load(this.modules);
            return kernel;
        }
    }
}