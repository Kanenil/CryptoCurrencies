using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoCurrencies.CoinGecko.Interfaces;
using CryptoCurrencies.CoinGecko.Models;
using CryptoCurrencies.CoinGecko.Services;
using CryptoCurrencies.WPF.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrencies.WPF.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        public MainViewModel(INavigationService navigationService) : base(navigationService)
        {
            Navigation.NavigateTo<HomeViewModel>();
        }

        [RelayCommand]
        private void NavigateToConvert()
        {
            if(Navigation.CurrentView.GetType() != typeof(ConvertViewModel))
                Navigation.NavigateTo<ConvertViewModel>();
        }

        [RelayCommand]
        private void NavigateToHome()
        {
            if (Navigation.CurrentView.GetType() != typeof(HomeViewModel))
                Navigation.NavigateTo<HomeViewModel>();
        }
    }
}
