using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrencies.CoinGecko.Interfaces
{
    public interface IApiSender
    {
        Task<T> GetAsync<T>(Uri resourceUri);
    }
}
