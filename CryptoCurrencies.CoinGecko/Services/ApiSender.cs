using CryptoCurrencies.CoinGecko.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptoCurrencies.CoinGecko.Services
{
    public abstract class ApiSender : IApiSender
    {
        private readonly HttpClient _httpClient;
        public ApiSender(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetAsync<T>(Uri resourceUri)
        {
            var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, resourceUri));

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
