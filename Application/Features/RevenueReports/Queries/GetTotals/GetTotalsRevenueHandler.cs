using Application.Abstractions;
using Domain.Extentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RevenueReports.Queries.GetTotals
{
    public record GetTotalsRevenueQuery(DateTime? FromDate, DateTime? ToDate, int? BranchId, string? PaymentMethod, int? ClientId, int? serviceId) : IQuery<TotalsRevenueDto>;

    public class GetTotalsRevenueHandler : IQueryHandler<GetTotalsRevenueQuery, TotalsRevenueDto>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetTotalsRevenueHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<TotalsRevenueDto>> Handle(GetTotalsRevenueQuery request, CancellationToken cancellationToken)
        {
            var totalRevenues = new TotalsRevenueDto();


            var query = unitOfWork.BookingServiceRepository.GetQuerable()
                .Where(x => !request.serviceId.HasValue || x.ServiceId == request.serviceId)
                .Where(x => !request.FromDate.HasValue || request.FromDate.ToDate() <= x.Booking.BookingDate)
                .Where(x => !request.ToDate.HasValue || request.ToDate.ToDate() >= x.Booking.BookingDate)
                .Where(x => !request.BranchId.HasValue || request.BranchId == x.Booking.BranchId)
                .Where(x => !request.ClientId.HasValue || request.ClientId == x.Booking.ClientId);

            if (!string.IsNullOrWhiteSpace(request.PaymentMethod))
                query = query.Where(x => x.Booking.Transactions.Any(y => y.PaymentMethod.Contains(request.PaymentMethod)));

            totalRevenues.TotalRevenue = await query.SelectMany(y => y.Booking.Transactions).Select(y => y.Amount).SumAsync();
            totalRevenues.RevenueByServices = await query
                .GroupBy(x => x.Service.Name)
                .Select(x => new RevenueByServiceDto
                {
                    ServiceName = x.Key,
                    Revenue = x.SelectMany(y => y.Booking.Transactions).Select(y => y.Amount).Sum()
                }).ToListAsync();

            totalRevenues.RevenueByBranches = await query
                .GroupBy(x => x.Booking.Branch.Name)
                .Select(x => new RevenueByBranchDto
                {
                    BranchName = x.Key,
                    Revenue = x.SelectMany(y => y.Booking.Transactions).Select(y => y.Amount).Sum()
                }).ToListAsync();

            totalRevenues.RevenueByClients = await query
                .GroupBy(x => x.Booking.Client)
                .Select(x => new RevenueByClientDto
                {
                    ClientName = x.Key.FirstName + " " + x.Key.LastName,
                    Revenue = x.SelectMany(y => y.Booking.Transactions).Select(y => y.Amount).Sum()
                }).ToListAsync();

            totalRevenues.RevenueByPaymentMethods = await query
                .SelectMany(x => x.Booking.Transactions)
                .GroupBy(x => x.PaymentMethod)
                .Select(x => new RevenueByPaymentMethodDto
                {
                    PaymentMethod = x.Key,
                    Revenue = x.Select(y => y.Amount).Sum()
                }).ToListAsync();


            return new Result<TotalsRevenueDto> { IsSucceeded = true, Value = totalRevenues };
        }
    }
}
