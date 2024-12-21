using Application.Abstractions;
using Application.Models.DTOs.Reports.Clients;
using Application.Models.DTOs.Reports.Revenues;
using Domain.Extentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DemographicReport.Queries.GetDemographic
{
    public record GetDemographicQuery(DateTime? FromDate, DateTime? ToDate, int? BranchId, int? ClientId, int? ServiceId,string? City,string? Country) : IQuery<IEnumerable<DemographicDto>>;

    public class GetDemographicHandler : IQueryHandler<GetDemographicQuery, IEnumerable<DemographicDto>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetDemographicHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<DemographicDto>>> Handle(GetDemographicQuery request, CancellationToken cancellationToken)
        {
            var query = unitOfWork.BookingRepository.GetQuerable()
            .Where(b => !request.FromDate.HasValue || request.FromDate.ToDate() <= b.BookingDate)
            .Where(b => !request.ToDate.HasValue || request.ToDate.ToDate() >= b.BookingDate)
            .Where(b => !request.BranchId.HasValue || b.BranchId == request.BranchId)
            .Where(b => !request.ClientId.HasValue || b.ClientId == request.ClientId)
            .Where(b => !request.ServiceId.HasValue || b.BookingServices.Any(bs => bs.ServiceId == request.ServiceId));

            if (!string.IsNullOrWhiteSpace(request.City))
                query = query.Where(x => x.Client.City == request.City);

            if (!string.IsNullOrWhiteSpace(request.Country))
                query = query.Where(x => x.Client.Country == request.Country);

            var demoGraph = await query.Select(x => new { x.Client, x.Branch.Name }).Select(x => new DemographicDto
            {
                Age = DateTime.Now.Year - x.Client.BirthDate.Year,
                Branch = x.Name,
                City = x.Client.City,
                Country = x.Client.Country,
                Gender = x.Client.Gender
            }).ToListAsync();

            return new Result<IEnumerable<DemographicDto>> { IsSucceeded = true, Value = demoGraph };
        }
    }
}
