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
        Task<List<MainCoin>> GetCoinMarkets(string vsCurrency, int page);
        Task<List<DetailCoin>> GetCoinMarkets(string vsCurrency, int page, string[] coins);
        Task<List<MainCoin>> GetCoinsBySearch(string vsCurrency, int page, string query);
        Task<dynamic> GetOhlcByCoinId(string id, string vsCurrency, string days);
        Task<MarketCoin> GetTickerByCoinId(string id, int? page);
    }
}
