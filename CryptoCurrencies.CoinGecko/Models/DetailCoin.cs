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
    }
}
