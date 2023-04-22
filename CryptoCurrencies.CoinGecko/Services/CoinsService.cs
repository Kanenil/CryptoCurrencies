using CryptoCurrencies.CoinGecko.EndPoints;
using CryptoCurrencies.CoinGecko.Interfaces;
using CryptoCurrencies.CoinGecko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrencies.CoinGecko.Services
{
    public class CoinsService : ApiSender, ICoinsService
    {
        public CoinsService(HttpClient httpClient) : base(httpClient) {}

        public async Task<List<Coin>> GetCoinMarkets(string vsCurrency)
        {
            return await GetCoinMarkets(vsCurrency, new string[] { }, null, null, null, false, null, null);
        }

        public async Task<List<Coin>> GetCoinMarkets(string vsCurrency, string[] ids, string order, int? perPage,
        int? page, bool sparkline, string priceChangePercentage, string category)
        {
            return await GetAsync<List<Coin>>(QueryStringService.AppendQueryString(CoinsEndPoints.CoinMarkets,
                new Dictionary<string, object>
                {
                {"vs_currency", vsCurrency},
                {"ids", string.Join(",", ids)},
                {"order",order},
                {"per_page", perPage},
                {"page", page},
                {"sparkline", sparkline},
                {"price_change_percentage", priceChangePercentage},
                {"category",category}
                }));
        }
    }
}
