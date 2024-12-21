using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenueReportsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetTotal()
        {
            return Ok();
        }
    }
}
