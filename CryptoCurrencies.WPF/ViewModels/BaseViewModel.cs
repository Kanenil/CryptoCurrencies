using CommunityToolkit.Mvvm.ComponentModel;
using CryptoCurrencies.WPF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrencies.WPF.ViewModels
{
    public abstract partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private INavigationService _navigation;

        public BaseViewModel(INavigationService navigationService)
        {
            Navigation = navigationService;
        }
    }
}
