using App.GRRInnovations.Navigation.Service.Interfaces;

namespace App.GRRInnovations.Navigation.Service.Builders
{
    internal class InternalServiceNavigationMvvmBuilder : ServiceMvvmBuilder, INavigationMvvmBuilder
    {
        public Func<INavigationService, IServiceProvider, Task> onStartFunc { get; set; }
    }
}
