using Application.Features.DemographicReport.Queries.GetDemographic;
using Application.Features.RevenueReports.Queries.GetTotal;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemographicReportController : ControllerBase
    {
        private readonly ISender sender;

        public DemographicReportController(ISender sender)
        {
            this.sender = sender;
        }


        [HttpGet]
        public async Task<IActionResult> GetDemographicAsync([FromQuery] GetDemographicQuery request)
        {
            var result = await sender.Send(request);
            if (result.IsSucceeded)
                return Ok(result.Value);

            return BadRequest(result.Error);
        }
    }
}
