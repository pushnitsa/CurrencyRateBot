using CurrencyRateBot.Web.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CurrencyRateBot.Controllers
{
    [ApiController]
    [Route("api/currency")]
    public class CurrencyRateController : Controller
    {
        private readonly CurrencyRateService _currencyRateService;

        public CurrencyRateController(CurrencyRateService currencyRateService)
        {
            _currencyRateService = currencyRateService;
        }

        [HttpGet]
        public async Task<ActionResult<decimal>> GetCurrencyRate(string from, string to)
        {
            var result = await _currencyRateService.GetCurrencyRateAsync(from, to);

            return Ok(result);
        }
    }
}
