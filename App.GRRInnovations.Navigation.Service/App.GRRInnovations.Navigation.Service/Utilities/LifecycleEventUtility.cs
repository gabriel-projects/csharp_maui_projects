using App.GRRInnovations.Navigation.Service.Interfaces;
using App.GRRInnovations.Navigation.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.GRRInnovations.Navigation.Service.Utilities
{
    internal static class LifecycleEventUtility
    {
        internal static async Task TriggerOnNavigatingFrom(object bindingContext, NavigationParameters navigationParameters)
        {
            var navigatingFromViewModel = bindingContext as INavigatingEvents;

            if (navigatingFromViewModel != null)
            {
                await navigatingFromViewModel.OnNavigatingFrom(navigationParameters);
            }
        }

        internal static async Task TriggerOnNavigatedFrom(object bindingContext, NavigationParameters navigationParameters)
        {
            var navigatedFromViewModel = bindingContext as INavigatedEvents;

            if (navigatedFromViewModel != null)
            {
                await navigatedFromViewModel.OnNavigatedFrom(navigationParameters);
            }
        }

        internal static async Task TriggerOnNavigatedTo(object bindingContext, NavigationParameters navigationParameters)
        {
            var navigatedToViewModel = bindingContext as INavigatedEvents;

            if (navigatedToViewModel != null)
            {
                await navigatedToViewModel.OnNavigatedTo(navigationParameters);
            }
        }
    }
}
