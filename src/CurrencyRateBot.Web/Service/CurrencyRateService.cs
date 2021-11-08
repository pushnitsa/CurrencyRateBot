using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace CurrencyRateBot.Web.Service
{
    public class CurrencyRateService
    {
        private readonly CurrencyRateServerOptions _currencyRateServerOptions;

        public CurrencyRateService(IOptions<CurrencyRateServerOptions> currencyRateServerOptions)
        {
            _currencyRateServerOptions = currencyRateServerOptions.Value;
        }

        public virtual async Task<decimal> GetCurrencyRateAsync(string from, string to)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_currencyRateServerOptions.Url);

            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/json";

            var requestData = $"{{\"method\":\"spotRateHistory\",\"data\":{{\"base\":\"{from}\",\"term\":\"{to}\",\"period\":\"day\"}}}}";
            var byteArray = System.Text.Encoding.UTF8.GetBytes(requestData);

            httpWebRequest.ContentLength = byteArray.Length;
            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.61 Safari/537.36";
            httpWebRequest.Accept = "*/*";

            await using (var dataStream = httpWebRequest.GetRequestStream())
            {
                await dataStream.WriteAsync(byteArray.AsMemory(0, byteArray.Length));
            }

            var response = await httpWebRequest.GetResponseAsync();
            await using var stream = response.GetResponseStream();
            using var reader = new StreamReader(stream);
            var responseData = await reader.ReadToEndAsync();

            var jsonParsedData = JsonDocument.Parse(responseData);

            var result = jsonParsedData
                .RootElement
                .GetProperty("data")
                .GetProperty("CurrentInterbankRate")
                .GetDecimal();

            return result;
        }
    }
}
