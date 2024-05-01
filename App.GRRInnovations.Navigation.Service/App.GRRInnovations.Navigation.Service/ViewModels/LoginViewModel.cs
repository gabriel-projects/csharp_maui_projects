using App.GRRInnovations.Navigation.Service.Interfaces;
using App.GRRInnovations.Navigation.Service.Models;
using App.GRRInnovations.Navigation.Service.Views;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.GRRInnovations.Navigation.Service.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        [RelayCommand]
        private async Task Login(string param)
        {
            var navigationParameters = new NavigationParameters
            {
                { "username", "gabriel" },
            };

            // after we login, we replace the stack so the user can't go back to the Login page
            await navigationService.Push<SigninSignupView>(navigationParameters);
        }
    }
}
