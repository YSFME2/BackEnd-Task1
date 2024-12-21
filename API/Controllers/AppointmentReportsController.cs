using Application.Features.AppointmentReports.Queries.GetAppointments;
using Application.Features.RevenueReports.Queries.GetTotal;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentReportsController : ControllerBase
    {
        private readonly ISender sender;

        public AppointmentReportsController(ISender sender)
        {
            this.sender = sender;
        }
        [HttpGet]
        public async Task<IActionResult> GetAppointmentsAsync([FromQuery] GetAppointmentsQuery request)
        {
            var result = await sender.Send(request);
            if (result.IsSucceeded)
                return Ok(result.Value);

            return BadRequest(result.Error);
        }
    }
}
