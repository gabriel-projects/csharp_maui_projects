using App.GRRInnovations.Navigation.Service.Builders;
using App.GRRInnovations.Navigation.Service.Interfaces;
using App.GRRInnovations.Navigation.Service.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.GRRInnovations.Navigation.Service.Extensions
{
    public static class MauiAppBuilderExtensions
    {
        public static MauiAppBuilder UseNavigationMvvm(this MauiAppBuilder mauiAppBuilder, Action<ServiceMvvmBuilder> burkusMvvmBuilderAction)
        {
            mauiAppBuilder.Services.TryAddEnumerable(
                ServiceDescriptor.Transient<IMauiInitializeService, NavigationMvvmMauiInitializer>());
            mauiAppBuilder.Services.TryAddEnumerable(
                ServiceDescriptor.Transient<INavigationService, NavigationService>());

            var serviceMvvmBuilder = new InternalServiceNavigationMvvmBuilder();
            burkusMvvmBuilderAction(serviceMvvmBuilder);

            mauiAppBuilder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<INavigationMvvmBuilder>(serviceMvvmBuilder));

            return mauiAppBuilder;
        }
    }
}
