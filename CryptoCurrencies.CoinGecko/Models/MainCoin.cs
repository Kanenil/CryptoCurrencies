using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CryptoCurrencies.CoinGecko.Models
{
    public class MainCoin
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("image")]
        public Uri Image { get; set; }

        [JsonPropertyName("current_price")]
        public decimal? CurrentPrice { get; set; }

        [JsonPropertyName("market_cap")]
        public decimal? MarketCap { get; set; }

        [JsonPropertyName("market_cap_rank")]
        public long? MarketCapRank { get; set; }

        [JsonPropertyName("price_change_percentage_24h_in_currency")]
        public decimal? PriceChangePercentage24HInCurrency { get; set; }

        [JsonPropertyName("price_change_percentage_7d_in_currency")]
        public decimal? PriceChangePercentage7DInCurrency { get; set; }
    }
}
