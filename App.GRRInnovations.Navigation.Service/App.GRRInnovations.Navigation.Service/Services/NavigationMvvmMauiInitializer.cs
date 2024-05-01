using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.GRRInnovations.Navigation.Service.Services
{
    public class NavigationMvvmMauiInitializer : IMauiInitializeService
    {
        public void Initialize(IServiceProvider serviceProvider)
        {
            var serviceScope = serviceProvider.CreateScope();

            ServiceResolver.RegisterScope(serviceScope);
        }
    }
}
