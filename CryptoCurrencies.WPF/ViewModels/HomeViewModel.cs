using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoCurrencies.CoinGecko.Interfaces;
using CryptoCurrencies.CoinGecko.Models;
using CryptoCurrencies.WPF.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrencies.WPF.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        private readonly ICoinsService _coinsService;
        public HomeViewModel(INavigationService navigationService, ICoinsService coinsService) : base(navigationService)
        {
            _coinsService = coinsService;

            LoadCoinsCommand = new AsyncRelayCommand(LoadCoinsAsync);
            DismissErrorCommand = new AsyncRelayCommand(LoadCoinsAsync);
        }

        [ObservableProperty]
        private string _culture = "en-US";

        [ObservableProperty]
        private ObservableCollection<MainCoin> _coins;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private bool _hasError;

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private string _description;

        public IAsyncRelayCommand LoadCoinsCommand { get; }
        public IAsyncRelayCommand DismissErrorCommand { get; }
        private async Task LoadCoinsAsync()
        {
            IsLoading = true;
            HasError = false;

            try
            {
                Title = "Loading Data";
                Description = "Please wait, we are loading coin data";

                Coins = new ObservableCollection<MainCoin>(await _coinsService.GetCoinMarkets("usd"));

                IsLoading = false;
            }
            catch (Exception)
            {
                Title = "Too many requests";
                Description = "Please wait for the API to be available again";

                HasError = true;
            }
        }
    }
}
