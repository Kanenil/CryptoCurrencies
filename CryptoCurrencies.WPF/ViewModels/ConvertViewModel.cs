using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoCurrencies.CoinGecko.Interfaces;
using CryptoCurrencies.CoinGecko.Models;
using CryptoCurrencies.WPF.Core;
using CryptoCurrencies.WPF.Interfaces;
using CryptoCurrencies.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrencies.WPF.ViewModels
{
    public partial class ConvertViewModel : BaseViewModel
    {
        private readonly ICoinsService _coinsService;

        public ConvertViewModel(INavigationService navigationService, ICoinsService coinsService) : base(navigationService)
        {
            _coinsService = coinsService;

            LoadCoinsCommand = new AsyncRelayCommand(LoadCoinsAsync);
        }

        [ObservableProperty]
        private Loader _loader = new();

        [ObservableProperty]
        private ObservableCollection<MainCoin> _coins;

        [ObservableProperty]
        private string _result;

        [ObservableProperty]
        private string _count;

        [ObservableProperty]
        private MainCoin _from;

        [ObservableProperty]
        private MainCoin _to;

        public IAsyncRelayCommand LoadCoinsCommand { get; }
        private async Task LoadCoinsAsync()
        {
            Loader.IsLoading = true;
            Loader.HasError = false;

            try
            {
                Loader.Title = "Loading Currencies";
                Loader.Description = "Please wait, we are loading currencies";

                Coins = new(await _coinsService.GetCoinMarkets("usd", 1, 250));

                Loader.IsLoading = false;
            }
            catch (Exception)
            {
                Loader.Title = "Too many requests";
                Loader.Description = "Please wait for the API to be available again";

                Loader.HasError = true;
            }
        }

        [RelayCommand]
        private void Convert()
        {
            if (From != null && To != null && !string.IsNullOrEmpty(Count))
            {
                decimal count;
                if (decimal.TryParse(Count.Replace(',', '.'), out count))
                {
                    decimal result = (decimal)((From.CurrentPrice * count) / To.CurrentPrice);
                    Result = "Result: " + Math.Round(result, 10) + " " + To.Symbol.ToUpper();
                }
                else
                {
                    Result = "Count is not a number!";
                }
            }
        }

    }
}
