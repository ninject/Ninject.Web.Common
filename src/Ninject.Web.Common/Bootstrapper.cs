// -------------------------------------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="Ninject Project Contributors">
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

    using Ninject.Infrastructure.Language;

    /// <summary>
    /// A basic bootstrapper that can be used to setup web applications.
    /// </summary>
    public class Bootstrapper : IBootstrapper
    {
        /// <summary>
        /// The ninject kernel of the application.
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