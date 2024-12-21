using Application.Abstractions;
using Application.Models.DTOs.Reports.Revenues;
using Domain.Extentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RevenueReports.Queries.GetTotal
{
    public record GetTotalRevenueQuery(DateTime? FromDate, DateTime? ToDate, int? BranchId, string? PaymentMethod, int? ClientId, int? serviceId) : IQuery<TotalRevenueDto>;

    public class GetTotalRevenueHandler : IQueryHandler<GetTotalRevenueQuery, TotalRevenueDto>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetTotalRevenueHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<TotalRevenueDto>> Handle(GetTotalRevenueQuery request, CancellationToken cancellationToken)
        {
            var totalRevenues = new TotalRevenueDto();

            if (request.serviceId.HasValue && request.serviceId > 0)
            {
                var query = unitOfWork.BookingServiceRepository.GetQuerable()
                    .Where(x => x.ServiceId == request.serviceId)
                    .Where(x => !request.FromDate.HasValue || request.FromDate.ToDate() <= x.Booking.BookingDate)
                    .Where(x => !request.ToDate.HasValue || request.ToDate.ToDate() >= x.Booking.BookingDate)
                    .Where(x => !request.BranchId.HasValue || request.BranchId == x.Booking.BranchId)
                    .Where(x => !request.ClientId.HasValue || request.ClientId == x.Booking.ClientId);

                if (!string.IsNullOrWhiteSpace(request.PaymentMethod))
                    query = query.Where(x => x.Booking.Transactions.Any(y => y.PaymentMethod.Contains(request.PaymentMethod)));

                totalRevenues.TotalRevenue = await query.SelectMany(x=>x.Booking.Transactions).Select(x => x.Amount).SumAsync();
            }
            else
            {
                var query = unitOfWork.TransactionRepository.GetQuerable()
                    .Where(x => !request.FromDate.HasValue || request.FromDate <= x.PaymentDate)
                    .Where(x => !request.ToDate.HasValue || request.ToDate >= x.PaymentDate)
                    .Where(x => !request.BranchId.HasValue || request.BranchId == x.Booking.BranchId)
                    .Where(x => !request.ClientId.HasValue || request.ClientId == x.Booking.ClientId);

                if (!string.IsNullOrWhiteSpace(request.PaymentMethod))
                    query = query.Where(x => x.PaymentMethod.Contains(request.PaymentMethod));

                totalRevenues.TotalRevenue = await query.Select(x => x.Amount).SumAsync();
            }
            return new Result<TotalRevenueDto> { IsSucceeded = true, Value = totalRevenues };
        }
    }
}
