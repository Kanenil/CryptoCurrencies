using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CryptoCurrencies.CoinGecko.Models
{
    public class ChartCoin
    {
        [JsonPropertyName("prices")]
        public decimal?[][] Prices { get; set; }

        [JsonPropertyName("market_caps")]
        public decimal?[][] MarketCaps { get; set; }

        [JsonPropertyName("total_volumes")]
        public decimal?[][] TotalVolumes { get; set; }
    }
}
