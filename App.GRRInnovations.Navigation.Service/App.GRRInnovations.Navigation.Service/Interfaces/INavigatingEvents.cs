using App.GRRInnovations.Navigation.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.GRRInnovations.Navigation.Service.Interfaces
{
    public interface INavigatingEvents
    {
        /// <summary>
        /// Triggered before navigation away from the current page.
        /// </summary>
        /// <param name="parameters">Parameters to be passed onto the next page.</param>
        /// <returns>A completed task</returns>
        Task OnNavigatingFrom(NavigationParameters parameters);
    }
}
