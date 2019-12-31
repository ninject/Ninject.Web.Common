// -------------------------------------------------------------------------------------------------
// <copyright file="NinjectHttpApplication.cs" company="Ninject Project Contributors">
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

namespace Ninject.Web.Common.WebHost
{
    using System;
    using System.Web;

    using Ninject.Infrastructure;

    /// <summary>
    /// Base implementation of <see cref="HttpApplication"/> that adds injection support.
    /// </summary>
    public abstract class NinjectHttpApplication : HttpApplication, IHaveKernel
    {
        /// <summary>
        /// The one per request module to release request scope at the end of the request.
        /// </summary>
        private readonly OnePerRequestHttpModule onePerRequestHttpModule;

        /// <summary>
        /// The bootstrapper that starts the application.
        /// </summary>
        private readonly IBootstrapper bootstrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectHttpApplication"/> class.
        /// </summary>
        protected NinjectHttpApplication()
        {
            this.onePerRequestHttpModule = new OnePerRequestHttpModule();
            this.onePerRequestHttpModule.Init(this);
            this.bootstrapper = new Bootstrapper();
        }

        /// <summary>
        /// Gets the kernel.
        /// </summary>
        /// <value>The kernel.</value>
        [Obsolete("Do not use Ninject as Service Locator")]
        public IKernel Kernel
        {
            get
            {
                return this.bootstrapper.Kernel;
            }
        }

        /// <summary>
        /// Executes custom initialization code after all event handler modules have been added.
        /// </summary>
        public override void Init()
        {
            base.Init();
            this.bootstrapper.Kernel.Inject(this);
        }

        /// <summary>
        /// Starts the application.
        /// </summary>
        public void Application_Start()
        {
            lock (this)
            {
                this.bootstrapper.Initialize(this.CreateKernel);
                this.onePerRequestHttpModule.ReleaseScopeAtRequestEnd = this.bootstrapper.Kernel.Settings.Get("ReleaseScopeAtRequestEnd", true);
                this.OnApplicationStarted();
            }
        }

        /// <summary>
        /// Releases the kernel on application end.
        /// </summary>
        public void Application_End()
        {
            this.OnApplicationStopped();
            this.bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        protected abstract IKernel CreateKernel();

        /// <summary>
        /// Called when the application is started.
        /// </summary>
        protected virtual void OnApplicationStarted()
        {
        }

        /// <summary>
        /// Called when the application is stopped.
        /// </summary>
        protected virtual void OnApplicationStopped()
        {
        }
    }
}