// -------------------------------------------------------------------------------------------------
// <copyright file="NinjectSelfHostBootstrapper.cs" company="Ninject Project Contributors">
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

namespace Ninject.Web.Common.SelfHost
{
    using System;

    using Ninject.Web.Common;

    /// <summary>
    /// Self hosting bootstrapper.
    /// </summary>
    public class NinjectSelfHostBootstrapper : IDisposable
    {
        /// <summary>
        /// The web common default bootstrapper.
        /// </summary>
        private readonly IBootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectSelfHostBootstrapper"/> class.
        /// </summary>
        /// <param name="createKernelCallback">The create kernel callback.</param>
        /// <param name="selfHostConfigurations">The self host configurations.</param>
        public NinjectSelfHostBootstrapper(Func<IKernel> createKernelCallback, params object[] selfHostConfigurations)
        {
            this.bootstrapper.Initialize(() =>
                {
                    var kernel = createKernelCallback();
                    foreach (var selfHostConfiguration in selfHostConfigurations)
                    {
                        kernel.Bind(selfHostConfiguration.GetType()).ToConstant(selfHostConfiguration);
                    }

                    return kernel;
                });
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            var selfhosts = this.bootstrapper.Kernel.GetAll<INinjectSelfHost>();
            foreach (var selfhost in selfhosts)
            {
                selfhost.Start();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.bootstrapper.ShutDown();
        }
    }
}