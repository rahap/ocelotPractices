using Microsoft.AspNetCore.Mvc;
using productApi.Models;

namespace productApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private List<string> list = new List<string> { "Kalem", "Kitap", "Silgi", "Defter" };

    private readonly ILogger<WeatherForecastController> _logger;

        public ProductController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        // [HttpGet("list/LeaveType")]
        [HttpGet( "/Products")]
        public IActionResult Products()
        {
            return Ok(list);
        }
        //index  gönderme
        [HttpGet( "/Product/{index}")]
        public IActionResult Product(int index)
        {
            return Ok(list[index]);
        }

        ////        [HttpGet("get/CheckPersonIsPartTimeQuery")]
        //[HttpGet("list/FilterProduct")]
        //public async Task<IActionResult> FilterProduct([FromQuery] RequestProduct request)
        //{
        //    return Ok(list.FirstOrDefault(r => request.Name.Contains(r)));
        //}
        [HttpPost("/FilterProduct")]
        public IActionResult FilterProduct(RequestProduct request)
        {
         
            return Ok(list.FirstOrDefault(r => request.Name.Contains(r)));
        }


        //[HttpGet("list/ListHarmonyLocationForLeaveType/{leaveTypeRef}")]
        //public async Task<IActionResult> ListHarmonyLocationForLeaveTypeQuery(short leaveTypeRef)
        //{
        //    var result = await _querySender.QueryAsync(new ListHarmonyLocationForLeaveTypeQuery(leaveTypeRef));

        //    return Ok(result);
        //}

        //[HttpGet("get/GetPersonLeaveBenefit")]
        //public async Task<IActionResult> GetPersonLeaveBenefitQuery([FromQuery] GetPersonLeaveBenefitQuery query)
        //{
        //    // var result = await _querySender.QueryAsync(new GetPersonLeaveBenefitQuery(personRef));
        //    var result = await _querySender.QueryAsync(query);

        //    return Ok(result);
        //}

    }
}