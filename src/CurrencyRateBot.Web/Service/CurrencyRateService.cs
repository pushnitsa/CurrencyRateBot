using System.Net.Http;
using System.Text;
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
            var client = new HttpClient();

            var data = new StringContent(
                $"{{\"method\":\"spotRateHistory\",\"data\":{{\"base\":\"{from}\",\"term\":\"{to}\",\"period\":\"day\"}}}}",
                Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.61 Safari/537.36");

            var responseData = await client.PostAsync(_currencyRateServerOptions.Url, data);

            var jsonParsedData = JsonDocument.Parse(await responseData.Content.ReadAsStringAsync());

            var result = jsonParsedData
                .RootElement
                .GetProperty("data")
                .GetProperty("CurrentInterbankRate")
                .GetDecimal();

            return result;
        }
    }
}
