// -------------------------------------------------------------------------------------------------
// <copyright file="OwinHttpApplicationPlugin.cs" company="Ninject Project Contributors">
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

namespace Ninject.Web.Common.Owin
{
    using System.Runtime.Remoting.Messaging;

    using Ninject.Activation;

    /// <summary>
    /// The Owin Http Application plugin.
    /// </summary>
    public class OwinHttpApplicationPlugin : INinjectHttpApplicationPlugin
    {
        /// <summary>
        /// Gets or sets the <see cref="INinjectSettings"/>.
        /// </summary>
        public INinjectSettings Settings { get; set; }

        /// <summary>
        /// Disposes the instances.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Gets the owin request scope.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The request scope.</returns>
        public object GetRequestScope(IContext context)
        {
            return CallContext.GetData(OwinBootstrapper.NinjectOwinRequestScope);
        }

        /// <summary>
        /// Starts the instance.
        /// </summary>
        public void Start()
        {
        }

        /// <summary>
        /// Stops the instance.
        /// </summary>
        public void Stop()
        {
        }
    }
}