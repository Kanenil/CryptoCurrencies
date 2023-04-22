using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CryptoCurrencies.CoinGecko.Models
{
    public class SparklineIn7D
    {
        [JsonPropertyName("price")]
        public decimal[] Price { get; set; }
    }
}
