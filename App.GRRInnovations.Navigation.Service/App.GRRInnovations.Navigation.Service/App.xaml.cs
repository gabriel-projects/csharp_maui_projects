using App.GRRInnovations.Navigation.Service.Interfaces;
using App.GRRInnovations.Navigation.Service.Services;

namespace App.GRRInnovations.Navigation.Service
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


                
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            Current.MainPage = new NavigationPage();

            var serviceNavigationBuilder = ServiceResolver.Resolve<INavigationMvvmBuilder>();
            var navigationService = ServiceResolver.Resolve<INavigationService>();
            var serviceProvider = ServiceResolver.GetServiceProvider();

            // perform the user's desired initialization logic
            if (serviceNavigationBuilder.onStartFunc != null)
            {
                {
                    serviceNavigationBuilder.onStartFunc.Invoke(navigationService, serviceProvider);
                }
            }

            return base.CreateWindow(activationState);
        }
    }
}
