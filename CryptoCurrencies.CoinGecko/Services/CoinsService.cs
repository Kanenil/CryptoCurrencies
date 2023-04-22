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
            return await GetAsync<List<Coin>>(QueryStringService.AppendQueryString(CoinsEndPoints.CoinMarkets,
                new Dictionary<string, object>
                {
                    { "vs_currency", vsCurrency },
                    { "order", "market_cap_desc" },
                    { "per_page", 10 },
                    { "page", 1 },
                    { "sparkline", true },
                    { "locale", "en" }
                }));
        }

        
    }
}
