using Microsoft.AspNetCore.Mvc;

namespace customerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
    

        private readonly ILogger<WeatherForecastController> _logger;

        public CustomerController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Get")]
        public IActionResult Get()
        {
            return Ok(new List<string> { "Hilmi Celayir", "Saniye Yýldýz", "Nevin Yýldýz", "Fatih Yýlmaz" });
        }
    }
}