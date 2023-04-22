using CryptoCurrencies.CoinGecko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrencies.CoinGecko.Interfaces
{
    public interface ICoinsService
    {
        Task<List<Coin>> GetCoinMarkets(string vsCurrency);
        Task<List<Coin>> GetCoinMarkets(string vsCurrency, string[] ids, string order, int? perPage, int? page,
        bool sparkline, string priceChangePercentage, string category);
    }
}
