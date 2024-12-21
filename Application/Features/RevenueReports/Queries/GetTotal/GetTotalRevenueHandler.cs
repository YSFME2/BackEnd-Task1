using Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RevenueReports.Queries.GetTotal
{
    public record GetTotalRevenueQuery(DateTime? FromDate, DateTime? ToDate) : IQuery<TotalRevenueDto>;

    public class GetTotalRevenueHandler : IQueryHandler<GetTotalRevenueQuery, TotalRevenueDto>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetTotalRevenueHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<TotalRevenueDto>> Handle(GetTotalRevenueQuery request, CancellationToken cancellationToken)
        {
            var query = unitOfWork.TransactionRepository.GetQuerable();

            if (request.FromDate != null)
                query = query.Where(x => x.PaymentDate >= request.FromDate);

            if(request.ToDate != null)
                query = query.Where(x => x.PaymentDate <= request.ToDate);

            return new Result<TotalRevenueDto> { IsSuccessed = true, Value = new TotalRevenueDto { TotalRevenue = await query.Select(x => x.Amount).SumAsync() } };
        }
    }
}
