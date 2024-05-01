using App.GRRInnovations.Navigation.Service.Interfaces;
using App.GRRInnovations.Navigation.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.GRRInnovations.Navigation.Service.ViewModels
{
    public partial class SigninSignupViewModel : BaseViewModel
    {
        public SigninSignupViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public override async Task OnNavigatingFrom(NavigationParameters parameters)
        {
            await base.OnNavigatingFrom(parameters);
        }
    }
}
