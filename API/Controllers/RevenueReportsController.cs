using Application.Features.RevenueReports.Queries.GetTotal;
using Application.Features.RevenueReports.Queries.GetTotals;
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
        public async  Task<IActionResult> GetTotalAsync([FromQuery]GetTotalRevenueQuery request)
        {
            var result = await sender.Send(request);
            if (result.IsSucceeded)
                return Ok(result.Value);

            return BadRequest(result.Error);
        }
        [HttpGet("totals")]
        public async  Task<IActionResult> GetTotalsAsync([FromQuery]GetTotalsRevenueQuery request)
        {
            var result = await sender.Send(request);
            if (result.IsSucceeded)
                return Ok(result.Value);

            return BadRequest(result.Error);
        }
    }
}
