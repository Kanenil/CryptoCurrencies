using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CryptoCurrencies.CoinGecko.Models
{
    public class DetailCoin : MainCoin
    {
        [JsonPropertyName("price_change_24h")]
        public decimal? PriceChange24H { get; set; }

        [JsonPropertyName("low_24h")]
        public decimal? Low24H { get; set; }

        [JsonPropertyName("high_24h")]
        public decimal? High24H { get; set; }

        [JsonPropertyName("total_volume")]
        public decimal? TotalVolume { get; set; }

        [JsonPropertyName("market_cap_change_percentage_24h")]
        public decimal? MarketCapChangePercentage24H { get; set; }
    }
}
