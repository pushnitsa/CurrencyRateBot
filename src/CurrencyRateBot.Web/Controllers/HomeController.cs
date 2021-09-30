using Microsoft.AspNetCore.Mvc;

namespace CurrencyRateBot.Web.Controllers
{
    [Route("/")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }
    }
}
