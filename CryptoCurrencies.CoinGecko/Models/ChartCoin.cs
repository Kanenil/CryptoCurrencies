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

    }
}
