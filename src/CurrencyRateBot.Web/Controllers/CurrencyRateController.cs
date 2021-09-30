using CurrencyRateBot.Web.Service;
using Microsoft.AspNetCore.Mvc;
using System;
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
            if (string.IsNullOrEmpty(from))
            {
                throw new ArgumentNullException(nameof(from));
            }

            if (string.IsNullOrEmpty(to))
            {
                throw new ArgumentNullException(nameof(to));
            }

            var result = await _currencyRateService.GetCurrencyRateAsync(from, to);

            return Ok(result);
        }
    }
}
