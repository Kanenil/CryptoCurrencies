﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrencies.CoinGecko.EndPoints
{
    public static class CoinsEndPoints
    {
        public static readonly string Coins = "coins";
        public static readonly string CoinMarkets = "coins/markets";
        public static string OhlcByCoinId(string id) => BaseEndPoint.AddCoinsIdUrl(id) + "/ohlc";
        public static string TickersByCoinId(string id) => BaseEndPoint.AddCoinsIdUrl(id) + "/tickers";
    }
}
