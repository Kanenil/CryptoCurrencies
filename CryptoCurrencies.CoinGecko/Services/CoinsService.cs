using CryptoCurrencies.CoinGecko.EndPoints;
using CryptoCurrencies.CoinGecko.Interfaces;
using CryptoCurrencies.CoinGecko.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrencies.CoinGecko.Services
{
    public class CoinsService : ApiSender, ICoinsService
    {
        public async Task<List<MainCoin>> GetCoinMarkets(string vsCurrency, int page,int countOnPage)
        {
            return await GetAsync<List<MainCoin>>(QueryStringService.AppendQueryString(CoinsEndPoints.CoinMarkets,
                new Dictionary<string, object>
                {
                    { "vs_currency", vsCurrency },
                    { "order", "market_cap_desc" },
                    { "per_page", countOnPage },
                    { "page", page },
                    { "sparkline", true },
                    { "locale", "en" },
                    { "price_change_percentage", "24h,7d" },
                }));
        }

        public async Task<List<DetailCoin>> GetCoinMarkets(string vsCurrency, int page, string[] coins)
        {
            return await GetAsync<List<DetailCoin>>(QueryStringService.AppendQueryString(CoinsEndPoints.CoinMarkets,
                new Dictionary<string, object>
                {
                    { "vs_currency", vsCurrency },
                    { "order", "market_cap_desc" },
                    { "ids", string.Join(",", coins) },
                    { "per_page", 10 },
                    { "page", page },
                    { "sparkline", true },
                    { "locale", "en" },
                    { "price_change_percentage", "24h,7d" },
                }));
        }


        public async Task<List<MainCoin>> GetCoinsBySearch(string vsCurrency, int page, string query)
        {
            var resp = await GetAsync<SearchCoin>(QueryStringService.AppendQueryString($"{BaseEndPoint.ApiEndPoint.AbsoluteUri}search",
                new Dictionary<string, object>()
                {
                    { "query", query }
                }));

            return await GetAsync<List<MainCoin>>(QueryStringService.AppendQueryString(CoinsEndPoints.CoinMarkets,
                new Dictionary<string, object>
                {
                    { "vs_currency", vsCurrency },
                    { "order", "market_cap_desc" },
                    { "ids", string.Join(",", resp.Coins.Select(x=>x.Id)) },
                    { "per_page", 10 },
                    { "page", page },
                    { "sparkline", true },
                    { "locale", "en" },
                    { "price_change_percentage", "24h,7d" },
                })); ;
        }

        public async Task<dynamic> GetOhlcByCoinId(string id, string vsCurrency, string days)
        {
            return await GetAsync<dynamic>(QueryStringService.AppendQueryString(
            CoinsEndPoints.OhlcByCoinId(id),
            new Dictionary<string, object>
            {
                {"vs_currency", string.Join(",",vsCurrency)},
                {"days", days},
            }));
        }

        public async Task<MarketCoin> GetTickerByCoinId(string id, int? page)
        {
            return await GetAsync<MarketCoin>(QueryStringService.AppendQueryString(
            CoinsEndPoints.TickersByCoinId(id), new Dictionary<string, object>
            {
                {"page", page},
                {"include_exchange_logo", true},
            }));
        }
    }
}
