// -------------------------------------------------------------------------------------------------
// <copyright file="OwinBootstrapper.cs" company="Ninject Project Contributors">
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

namespace Ninject.Web.Common.OwinHost
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Ninject.Modules;

    /// <summary>
    /// The Owin bootstrapper.
    /// </summary>
    public class OwinBootstrapper
    {
        /// <summary>
        /// The Ninject Owin request scope.
        /// </summary>
        public const string NinjectOwinRequestScope = "NinjectOwinRequestScope";

        private readonly IList<INinjectModule> modules = new List<INinjectModule>();
        private readonly Func<IKernel> createKernelCallback;
        private readonly IBootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Initializes a new instance of the <see cref="OwinBootstrapper"/> class.
        /// </summary>
        /// <param name="createKernelCallback">The create kernel callback function.</param>
        public OwinBootstrapper(Func<IKernel> createKernelCallback)
        {
            this.createKernelCallback = createKernelCallback;
        }

        /// <summary>
        /// Adds a Ninject module.
        /// </summary>
        /// <param name="ninjectModule">The Ninject module.</param>
        public void AddModule(NinjectModule ninjectModule)
        {
            lock (this.modules)
            {
                this.modules.Add(ninjectModule);
            }
        }

        /// <summary>
        /// Handles the owin context.
        /// </summary>
        /// <param name="next">The next moddleware factory.</param>
        /// <returns>
        /// The Ninject moddleware factory.
        /// </returns>
        public Func<IDictionary<string, object>, Task> Execute(Func<IDictionary<string, object>, Task> next)
        {
            this.bootstrapper.Initialize(() =>
            {
                var kernel = this.createKernelCallback();
                lock (this.modules)
                {
                    kernel.Load(this.modules);
                }

                return kernel;
            });

            return async context =>
            {
                using (var scope = new OwinRequestScope())
                {
                    context[NinjectOwinRequestScope] = scope;
                    await next(context);
                }
            };
        }
    }
}