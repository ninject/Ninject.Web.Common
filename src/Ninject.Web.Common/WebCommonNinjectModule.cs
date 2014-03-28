//-------------------------------------------------------------------------------
// <copyright file="NinjectHttpModule.cs" company="Ninject Project Contributors">
//   Copyright (c) 2009-2014 Ninject Project Contributors
//   Authors: Remo Gloor (remo.gloor@gmail.com)
//           
//   Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
//   you may not use this file except in compliance with one of the Licenses.
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
//-------------------------------------------------------------------------------

namespace Ninject.Web.Common
{
    using System.Web;

    /// <summary>
    /// Defines the bindings that are common for all web extensions.
    /// </summary>
    public class WebCommonNinjectModule : GlobalKernelRegistrationModule<OnePerRequestHttpModule>
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
#if !NET_35
            this.Bind<System.Web.Routing.RouteCollection>().ToConstant(System.Web.Routing.RouteTable.Routes);
            this.Bind<HttpContextBase>().ToMethod(ctx => new HttpContextWrapper(HttpContext.Current)).InTransientScope();
#endif
            this.Bind<HttpContext>().ToMethod(ctx => HttpContext.Current).InTransientScope();
        }
    }
}