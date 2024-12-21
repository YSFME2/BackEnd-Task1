using Application.Features.RevenueReports.Queries.GetTotal;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenueReportsController : ControllerBase
    {
        private readonly ISender sender;

        public RevenueReportsController(ISender sender)
        {
            this.sender = sender;
        }

        [HttpGet("total")]
        public async  Task<IActionResult> GetTotal([FromQuery]GetTotalRevenueQuery request)
        {
            var result = await sender.Send(request);
            if (result.IsSuccessed)
                return Ok(result.Value);

            return BadRequest(result.Error);
        }
    }
}
