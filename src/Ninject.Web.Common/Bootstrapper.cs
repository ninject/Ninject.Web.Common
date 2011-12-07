//-------------------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="bbv Software Services AG">
//   Copyright (c) 2010-2011 bbv Software Services AG
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
    using System;
    using System.Web;
    using Ninject.Infrastructure.Language;

    /// <summary>
    /// A basic bootstrapper that can be used to setup web applications.
    /// </summary>
    public class Bootstrapper : IBootstrapper
    {
        /// <summary>
        /// The ninject kernel of the application
        /// </summary>
        private static IKernel kernelInstance;

        /// <summary>
        /// Gets the kernel.
        /// </summary>
        public IKernel Kernel
        {
            get { return kernelInstance; }
        }

        /// <summary>
        /// Starts the application.
        /// </summary>
        /// <param name="createKernelCallback">The create kernel callback function.</param>
        public void Initialize(Func<IKernel> createKernelCallback)
        {
            kernelInstance = createKernelCallback();

            kernelInstance.Components.GetAll<INinjectHttpApplicationPlugin>().Map(c => c.Start());
            kernelInstance.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            kernelInstance.Inject(this);
        }

        /// <summary>
        /// Initializes a <see cref="HttpApplication"/> instance.
        /// </summary>
        /// <param name="httpApplication">The <see cref="HttpApplication"/> instance.</param>
        public void InitializeHttpApplication(HttpApplication httpApplication)
        {
            kernelInstance.Inject(httpApplication);
        }

        /// <summary>
        /// Releases the kernel on application end.
        /// </summary>
        public void ShutDown()
        {
            if (kernelInstance != null)
            {
                kernelInstance.Components.GetAll<INinjectHttpApplicationPlugin>().Map(c => c.Stop());
                kernelInstance.Dispose();
                kernelInstance = null;
            }
        }
    }
}
