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
        public async Task<List<MainCoin>> GetCoinMarkets(string vsCurrency)
        {
            return await GetAsync<List<MainCoin>>(QueryStringService.AppendQueryString(CoinsEndPoints.CoinMarkets,
                new Dictionary<string, object>
                {
                    { "vs_currency", vsCurrency },
                    { "order", "market_cap_desc" },
                    { "per_page", 10 },
                    { "page", 1 },
                    { "sparkline", true },
                    { "locale", "en" },
                    { "price_change_percentage", "24h,7d" },
                }));
        }

        public async Task<List<DetailCoin>> GetCoinMarkets(string vsCurrency, string coin)
        {
            return await GetAsync<List<DetailCoin>>(QueryStringService.AppendQueryString(CoinsEndPoints.CoinMarkets,
                new Dictionary<string, object>
                {
                    { "vs_currency", vsCurrency },
                    { "order", "market_cap_desc" },
                    {"ids", string.Join(",", coin)},
                    { "per_page", 10 },
                    { "page", 1 },
                    { "sparkline", true },
                    { "locale", "en" },
                    { "price_change_percentage", "24h,7d" },
                }));
        }

        public async Task<ChartCoin> GetMarketChartsByCoinId(string id, string vsCurrency, string days)
        {
            return await GetAsync<ChartCoin>(QueryStringService.AppendQueryString(
            CoinsEndPoints.MarketChartByCoinId(id),
            new Dictionary<string, object>
            {
                {"vs_currency", string.Join(",",vsCurrency)},
                {"days", days},
            }));
        }
    }
}
