using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrencies.CoinGecko.EndPoints
{
    public static class BaseEndPoint
    {
        public static readonly Uri ApiEndPoint = new("https://api.coingecko.com/api/v3/");
        public static string AddCoinsIdUrl(string id) => "coins/" + id;
    }
}
