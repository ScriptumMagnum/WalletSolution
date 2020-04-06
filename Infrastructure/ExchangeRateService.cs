using DomainModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ExchangeRateService : IExchangeRateService
    {        
        public decimal GetExchangeRate(string sourceCurrencyCode, string destinationCurrencyCode)
        {
            string urlPattern = "https://api.exchangeratesapi.io/latest?base={0}";
            string url = string.Format(urlPattern, sourceCurrencyCode);

            WebClient client = new WebClient();
            string response = client.DownloadString(url);

            var jObj = JObject.Parse(response);

            var result = jObj.SelectToken($"$.rates.{destinationCurrencyCode}").Value<decimal>();

            return result;
        }

        public async Task<decimal> GetExchangeRateAsync(string sourceCurrencyCode, string destinationCurrencyCode)
        {
            string urlPattern = "https://api.exchangeratesapi.io/latest?base={0}";
            string url = string.Format(urlPattern, sourceCurrencyCode);

            WebClient client = new WebClient();
            string response = await client.DownloadStringTaskAsync(url);

            var jObj = JObject.Parse(response);

            var result = jObj.SelectToken($"$.rates.{destinationCurrencyCode}").Value<decimal>();

            return result;
        }
    }
}
