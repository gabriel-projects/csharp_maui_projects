﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.GRRInnovations.Navigation.Service.Utilities
{
    internal static class MauiPageUtility
    {
        internal static Page GetTopPage()
        {
            var navigationStack = Application.Current.MainPage?.Navigation.NavigationStack;

            var modalStack = Application.Current.MainPage?.Navigation.ModalStack;

            if (modalStack != null && modalStack.Any())
            {
                // return a modal as the modals are on top
                return modalStack.Last();
            }

            if (navigationStack != null && navigationStack.Any())
            {

                return navigationStack.Last();
            }

            return null;
        }

        internal static object GetTopPageBindingContext()
        {
            return GetTopPage()?.BindingContext;
        }
    }
}
