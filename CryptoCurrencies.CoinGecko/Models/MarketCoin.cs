using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CryptoCurrencies.CoinGecko.Models
{
    public class MarketCoin
    {
        [JsonPropertyName("tickers")]
        public List<TickerInfo> Tickers { get; set; }
    }

    public class TickerInfo
    {

        [JsonPropertyName("base")]
        public string Base { get; set; }

        [JsonPropertyName("target")]
        public string Target { get; set; }

        [JsonPropertyName("market")]
        public MarketInfo Market { get; set; }

        [JsonPropertyName("trade_url")]
        public string TradeUrl { get; set; }

        [JsonPropertyName("trust_score")]
        public string TrustScore { get; set; }

        [JsonPropertyName("last")]
        public decimal Last { get; set; }

        [JsonPropertyName("volume")]
        public decimal Volume { get; set; }
    }

    public class MarketInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("logo")]
        public string Logo { get; set; }
    }
}
