﻿using CryptoCurrencies.CoinGecko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrencies.CoinGecko.Interfaces
{
    public interface ICoinsService
    {
        Task<List<MainCoin>> GetCoinMarkets(string vsCurrency);
        Task<List<DetailCoin>> GetCoinMarkets(string vsCurrency, string coin);
        Task<ChartCoin> GetMarketChartsByCoinId(string id, string vsCurrency, string days);
    }
}
