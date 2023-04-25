using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoCurrencies.CoinGecko.Interfaces;
using CryptoCurrencies.CoinGecko.Models;
using CryptoCurrencies.WPF.Core;
using CryptoCurrencies.WPF.Interfaces;
using CryptoCurrencies.WPF.Stores;
using CryptoCurrencies.WPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace CryptoCurrencies.WPF.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        private readonly ICoinsService _coinsService;
        private readonly CoinStore _coinStore;
        public HomeViewModel(INavigationService navigationService, ICoinsService coinsService, CoinStore coinStore) : base(navigationService)
        {
            _coinsService = coinsService;
            _coinStore = coinStore;

            LoadCoinsCommand = new AsyncRelayCommand(LoadCoinsAsync);
        }

        [ObservableProperty]
        private ObservableCollection<MainCoin> _coins;

        [ObservableProperty]
        private Loader _coinsLoader = new();

        [ObservableProperty]
        private string _search;

        [ObservableProperty]
        private int _page = 1;

        public IAsyncRelayCommand LoadCoinsCommand { get; }
        private async Task LoadCoinsAsync()
        {
            CoinsLoader.IsLoading = true;
            CoinsLoader.HasError = false;

            try
            {
                CoinsLoader.Title = "Loading Data";
                CoinsLoader.Description = "Please wait, we are loading coin data";

                if(string.IsNullOrWhiteSpace(Search))
                    Coins = new(await _coinsService.GetCoinMarkets("usd", Page, 10));
                else
                    Coins = new(await _coinsService.GetCoinsBySearch("usd", Page, Search));

                CoinsLoader.IsLoading = false;
            }
            catch (Exception)
            {
                CoinsLoader.Title = "Too many requests";
                CoinsLoader.Description = "Please wait for the API to be available again";

                CoinsLoader.HasError = true;
            }
        }
        [RelayCommand]
        private void ChangePage(string change)
        {
            Page = Page + int.Parse(change);
            Task.Run(LoadCoinsAsync);
        }

        [RelayCommand]
        private void NavigateToDetails(string? coin)
        {
            _coinStore.SelectedCoin = coin;
            Navigation.NavigateTo<DetailViewModel>();
        }
    }
}
