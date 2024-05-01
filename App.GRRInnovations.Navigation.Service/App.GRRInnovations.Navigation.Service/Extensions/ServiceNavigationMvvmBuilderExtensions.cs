using App.GRRInnovations.Navigation.Service.Builders;
using App.GRRInnovations.Navigation.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.GRRInnovations.Navigation.Service.Extensions
{
    public static class ServiceNavigationMvvmBuilderExtensions
    {
        /// <summary>
        /// Define where the app should go first when starting. You must navigate to a page when starting.
        /// </summary>
        /// <param name="builder">BurkusMvvmBuilder</param>
        /// <param name="onStartFunc">Function to perform when starting with access to <see cref="INavigationService"/> and <see cref="IServiceProvider"/></param>
        /// <returns></returns>
        public static ServiceMvvmBuilder OnStart(this ServiceMvvmBuilder builder, Func<INavigationService, IServiceProvider, Task> onStartFunc)
        {
            var internalBuilder = builder as InternalServiceNavigationMvvmBuilder;

            if (internalBuilder != null)
            {
                internalBuilder.onStartFunc = onStartFunc;
            }

            return builder;
        }

        /// <summary>
        /// Define where the app should go first when starting. You must navigate to a page when starting.
        /// </summary>
        /// <param name="builder">BurkusMvvmBuilder</param>
        /// <param name="onStartFunc">Function to perform when starting with access to <see cref="INavigationService"/>.</param>
        /// <returns></returns>
        public static ServiceMvvmBuilder OnStart(this ServiceMvvmBuilder builder, Func<INavigationService, Task> onStartFunc)
        {
            return OnStart(builder, (nav, sp) => onStartFunc(nav));
        }

        /// <summary>
        /// Define where the app should go first when starting. You must navigate to a page when starting.
        /// </summary>
        /// <param name="builder">BurkusMvvmBuilder</param>
        /// <param name="onStartFunc">Function to perform when starting with access to <see cref="IServiceProvider"/>.</param>
        /// <returns></returns>
        public static ServiceMvvmBuilder OnStart(this ServiceMvvmBuilder builder, Func<IServiceProvider, Task> onStartFunc)
        {
            return OnStart(builder, (nav, sp) => onStartFunc(sp));
        }
    }

}
