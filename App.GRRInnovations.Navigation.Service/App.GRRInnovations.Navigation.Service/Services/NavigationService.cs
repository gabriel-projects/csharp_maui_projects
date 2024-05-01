using App.GRRInnovations.Navigation.Service.Interfaces;
using App.GRRInnovations.Navigation.Service.Models;
using App.GRRInnovations.Navigation.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.GRRInnovations.Navigation.Service.Services
{
    internal class NavigationService : INavigationService
    {
        #region Core navigation methods

        public async Task Push<T>() where T : Page
        {
            await Push<T>(new NavigationParameters());
        }

        public async Task Push<T>(NavigationParameters navigationParameters) where T : Page
        {
            await HandleNavigation<T>(async () =>
            {
                var pageToNavigateTo = ServiceResolver.Resolve<T>();

                if (navigationParameters.UseModalNavigation)
                {
                    await Application.Current.MainPage.Navigation.PushModalAsync(pageToNavigateTo, navigationParameters.UseAnimatedNavigation);
                }
                else
                {
                    await Application.Current.MainPage.Navigation.PushAsync(pageToNavigateTo, navigationParameters.UseAnimatedNavigation);
                }
            },
                navigationParameters);
        }

        public async Task Pop()
        {
            await Pop(new NavigationParameters());
        }

        public async Task Pop(NavigationParameters navigationParameters)
        {
            await HandleNavigation<Page>(async () =>
            {
                if (navigationParameters.UseModalNavigation)
                {
                    _ = await Application.Current.MainPage.Navigation.PopModalAsync(navigationParameters.UseAnimatedNavigation);
                }
                else
                {
                    _ = await Application.Current.MainPage.Navigation.PopAsync(navigationParameters.UseAnimatedNavigation);
                }
            },
                navigationParameters);
        }

        public async Task PopToRoot()
        {
            await PopToRoot(new NavigationParameters());
        }

        public async Task PopToRoot(NavigationParameters navigationParameters)
        {
            await HandleNavigation<Page>(async () =>
            {
                await Application.Current.MainPage.Navigation.PopToRootAsync(navigationParameters.UseAnimatedNavigation);
            },
                navigationParameters);
        }

        #endregion Core navigation methods

        #region URI navigation methods

        public async Task Navigate(string uri)
        {
            await Navigate(uri, new NavigationParameters());
        }

        public async Task Navigate(string uri, NavigationParameters navigationParameters)
        {
            // todo: how to consider modal navigation/animations/parameters etc. at each segment of the navigation
            // todo: need to consider what query parameter should and shouldn't be.
            // should they be only for passing simple variables (strings, bools etc.) rather than complex json objects
            var navigation = Application.Current.MainPage.Navigation;

            var segments = UriUtility.GetUriSegments(uri);
            var instructions = segments.Select(UriUtility.ParseUriSegment)
                .ToList();

            var pagesToRemove = new List<Page>();

            if (UriUtility.IsUriAbsolute(uri))
            {
                if (instructions.Any(instruction => instruction.PageType == typeof(GoBackUriSegment)))
                {
                    throw new Exception("You can't perform 'go back' actions during absolute URI navigation");
                }

                // add every page as a page to be removed
                pagesToRemove.AddRange(navigation.NavigationStack);

                foreach (var page in navigation.NavigationStack)
                {
                    await LifecycleEventUtility.TriggerOnNavigatingFrom(page?.BindingContext, navigationParameters);
                }
            }

            if (instructions.Count > 1)
            {
                // handle all but the last instruction
                for (int i = 0; i < instructions.Count() - 1; i++)
                {
                    if (instructions[i].PageType == typeof(GoBackUriSegment))
                    {
                        // handle "Go Back" URI segments
                        var pageToRemove = navigation.NavigationStack[^(i + 1)];
                        pagesToRemove.Add(pageToRemove);

                        await LifecycleEventUtility.TriggerOnNavigatingFrom(
                            pageToRemove?.BindingContext,
                            instructions[i].QueryParameters.MergeNavigationParameters(navigationParameters));
                    }
                    else
                    {
                        // push pages relatively onto the stack
                        var pageToNavigateTo = ServiceResolver.Resolve(instructions[i].PageType) as Page;
                        var pushNavigationParameters = instructions[i].QueryParameters.MergeNavigationParameters(navigationParameters);

                        if (pushNavigationParameters.UseModalNavigation)
                        {
                            await Application.Current.MainPage.Navigation.PushModalAsync(pageToNavigateTo, navigationParameters.UseAnimatedNavigation);
                        }
                        else
                        {
                            await Application.Current.MainPage.Navigation.PushAsync(pageToNavigateTo, navigationParameters.UseAnimatedNavigation);
                        }

                        await LifecycleEventUtility.TriggerOnNavigatedTo(
                            pageToNavigateTo?.BindingContext,
                            pushNavigationParameters);
                    }
                }
            }

            // handle final instruction
            var lastInstruction = instructions.Last();

            if (lastInstruction.PageType == typeof(GoBackUriSegment))
            {
                // remove all the pages that needed removed
                foreach (var pageToRemove in pagesToRemove)
                {
                    navigation.RemovePage(pageToRemove);
                    await LifecycleEventUtility.TriggerOnNavigatedFrom(
                        pageToRemove?.BindingContext,
                        lastInstruction.QueryParameters.MergeNavigationParameters(navigationParameters));
                }

                var pageToPop = navigation.NavigationStack.Last();
                var popNavigationParameters = lastInstruction.QueryParameters.MergeNavigationParameters(navigationParameters);

                // pop final page
                if (popNavigationParameters.UseModalNavigation)
                {
                    _ = await Application.Current.MainPage.Navigation.PopModalAsync(navigationParameters.UseAnimatedNavigation);
                }
                else
                {
                    _ = await Application.Current.MainPage.Navigation.PopAsync(navigationParameters.UseAnimatedNavigation);
                }

                await LifecycleEventUtility.TriggerOnNavigatedFrom(
                    pageToPop?.BindingContext,
                    popNavigationParameters);
            }
            else
            {
                // push page relatively onto the stack
                var pageToNavigateTo = ServiceResolver.Resolve(lastInstruction.PageType) as Page;
                var pushNavigationParameters = lastInstruction.QueryParameters.MergeNavigationParameters(navigationParameters);

                if (pushNavigationParameters.UseModalNavigation)
                {
                    await Application.Current.MainPage.Navigation.PushModalAsync(pageToNavigateTo, navigationParameters.UseAnimatedNavigation);
                }
                else
                {
                    await Application.Current.MainPage.Navigation.PushAsync(pageToNavigateTo, navigationParameters.UseAnimatedNavigation);
                }

                // remove all the pages that needed removed
                foreach (var pageToRemove in pagesToRemove)
                {
                    navigation.RemovePage(pageToRemove);
                    await LifecycleEventUtility.TriggerOnNavigatedFrom(
                        pageToRemove?.BindingContext,
                        pushNavigationParameters);
                }
            }

            var lastNavigationParameters = lastInstruction.QueryParameters.MergeNavigationParameters(navigationParameters);

            var toBindingContext = MauiPageUtility.GetTopPageBindingContext();
            await LifecycleEventUtility.TriggerOnNavigatedTo(toBindingContext, lastNavigationParameters);
        }

        #endregion URI navigation methods


        #region Internal implementation
        private async Task HandleNavigation<T>(Func<Task> navigationAction, NavigationParameters navigationParameters)
            where T : Page
        {
            var fromBindingContext = MauiPageUtility.GetTopPageBindingContext();

            await LifecycleEventUtility.TriggerOnNavigatingFrom(fromBindingContext, navigationParameters);

            await navigationAction.Invoke();

            await LifecycleEventUtility.TriggerOnNavigatedFrom(fromBindingContext, navigationParameters);

            var toBindingContext = MauiPageUtility.GetTopPageBindingContext();
            await LifecycleEventUtility.TriggerOnNavigatedTo(toBindingContext, navigationParameters);
        }
        #endregion
    }
}
