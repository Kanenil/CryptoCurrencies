﻿using CryptoCurrencies.CoinGecko.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptoCurrencies.CoinGecko.Services
{
    public abstract class ApiSender : IApiSender
    {

        public async Task<T> GetAsync<T>(Uri resourceUri)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/111.0.0.0 Safari/537.36");
                //$"http://api.scraperapi.com?api_key=230fa5ed96ed6cb1b35ad802d0282018&url={resourceUri.AbsoluteUri}"
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"http://api.scraperapi.com?api_key=230fa5ed96ed6cb1b35ad802d0282018&url={resourceUri.AbsoluteUri}"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");

                    var response = await httpClient.SendAsync(request);

                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync();

                    try
                    {
                        return JsonSerializer.Deserialize<T>(content);
                    }
                    catch (Exception e)
                    {
                        throw new HttpRequestException(e.Message);
                    }
                }
            } 
        }
    }
}
