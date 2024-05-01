using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.GRRInnovations.Navigation.Service.Interfaces
{
    public interface INavigationMvvmBuilder
    {
        /// <summary>
        /// Internal Burkus.Mvvm.Maui use only. Not intended for consuming project usage.
        /// </summary>
        Func<INavigationService, IServiceProvider, Task> onStartFunc { get; set; }
    }
}
