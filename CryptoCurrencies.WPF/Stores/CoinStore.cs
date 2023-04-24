using CommunityToolkit.Mvvm.ComponentModel;
using CryptoCurrencies.CoinGecko.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrencies.WPF.Stores
{
    public partial class CoinStore : ObservableObject
    {
        [ObservableProperty]
        private string? _selectedCoin = null;

        [ObservableProperty]
        private string _culture = "en-US";
    }
}
