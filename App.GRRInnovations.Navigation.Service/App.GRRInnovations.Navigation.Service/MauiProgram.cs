using App.GRRInnovations.Navigation.Service.Extensions;
using App.GRRInnovations.Navigation.Service.Interfaces;
using App.GRRInnovations.Navigation.Service.Services;
using App.GRRInnovations.Navigation.Service.Views;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Hosting;
using CommunityToolkit.Maui;
using App.GRRInnovations.Navigation.Service.ViewModels;

namespace App.GRRInnovations.Navigation.Service
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseNavigationMvvm(serviceNavigation =>
                {
                    serviceNavigation.OnStart(async (navigationService, serviceProvider) =>
                    {
                        //var preferences = serviceProvider.GetRequiredService<IPreferences>();
                        await navigationService.Navigate("/LoginView");
                    });
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .UseMauiCommunityToolkit();

            builder.Services.AddTransient<LoginView>();
            builder.Services.AddTransient<SigninSignupView>();

            builder.Services.AddScoped<SigninSignupViewModel>();
            builder.Services.AddScoped<LoginViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}