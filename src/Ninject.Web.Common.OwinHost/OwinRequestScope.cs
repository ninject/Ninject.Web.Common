//-------------------------------------------------------------------------------
// <copyright file="OwinRequestScope.cs" company="bbv Software Services AG">
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

    using Ninject.Infrastructure.Disposal;

    /// <summary>
    /// The OWIN request scope.
    /// </summary>
    public class OwinRequestScope : INotifyWhenDisposed
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OwinRequestScope"/> class.
        /// </summary>
        public OwinRequestScope()
        {
            this.Disposed += (s, e) => { };
        }

        /// <summary>
        /// The disposed event.
        /// </summary>
        public event EventHandler Disposed;

        /// <summary>
        /// Gets a value indicating whether is disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.IsDisposed = true;
            this.Disposed(this, EventArgs.Empty);
        }
    }
}