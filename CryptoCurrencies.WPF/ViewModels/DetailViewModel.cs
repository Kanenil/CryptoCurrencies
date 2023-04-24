using CryptoCurrencies.WPF.Interfaces;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoCurrencies.WPF.Stores;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoCurrencies.CoinGecko.Models;
using CryptoCurrencies.CoinGecko.Interfaces;

namespace CryptoCurrencies.WPF.ViewModels
{
    public partial class DetailViewModel : BaseViewModel
    {
        private readonly CoinStore _coinStore;
        private readonly ICoinsService _coinsService;

        public DetailViewModel(INavigationService navigationService, CoinStore coinStore, ICoinsService coinsService) : base(navigationService)
        {
            _coinStore = coinStore;
            _coinsService = coinsService;

            LoadCoinCommand = new AsyncRelayCommand(LoadCoinAsync);
        }

        [ObservableProperty]
        private string _culture = "en-US";

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private bool _hasError;

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private string _description;

        [ObservableProperty]
        private DetailCoin _coin = new();

        public IAsyncRelayCommand LoadCoinCommand { get; }
        private async Task LoadCoinAsync()
        {
            IsLoading = true;
            HasError = false;

            try
            {
                Title = "Loading Data";
                Description = "Please wait, we are loading coin data";

                Culture = _coinStore.Culture;

                var resp = await _coinsService.GetCoinMarkets("usd", _coinStore.SelectedCoin);

                Coin = resp.FirstOrDefault();

                IsLoading = false;
            }
            catch (Exception)
            {
                Title = "Too many requests";
                Description = "Please wait for the API to be available again";

                HasError = true;
            }
        }

        [RelayCommand]
        private void MoveToHome()
        {
            if(LoadCoinCommand.IsRunning && LoadCoinCommand.CanBeCanceled)
                LoadCoinCommand.Cancel();
            Navigation.NavigateTo<HomeViewModel>();
        }
    }
}
