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
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), resourceUri))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");
                    request.Headers.Add("User-Agent", "Cryptocurrencies v0.0.1");

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

            //using (var handler = new HttpClientHandler())
            //{
            //    handler.UseDefaultCredentials = true;

            //    using (var client = new HttpClient(handler))
            //    {
            //        //client.DefaultRequestHeaders.Add("User-Agent", "Other");
            //        //var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"http://api.scraperapi.com?api_key=230fa5ed96ed6cb1b35ad802d0282018&url={resourceUri.AbsoluteUri}"));
            //        var response = await client.GetAsync(resourceUri);
            //        response.EnsureSuccessStatusCode();

            //        var content = await response.Content.ReadAsStringAsync();

            //        try
            //        {
            //            return JsonSerializer.Deserialize<T>(content);
            //        }
            //        catch (Exception e)
            //        {
            //            throw new HttpRequestException(e.Message);
            //        }
            //    }
            //}
                
        }
    }
}
