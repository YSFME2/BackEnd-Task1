using Application.Abstractions;
using Application.Models.DTOs.Reports.Appointments;
using Application.Models.DTOs.Reports.Revenues;
using Domain.Extentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Features.AppointmentReports.Queries.GetAppointments
{
    public record GetAppointmentsQuery(DateTime? FromDate, DateTime? ToDate, int? BranchId, string? PaymentMethod, int? ClientId, int? ServiceId, string? Status) : IQuery<AppointmentReportDto>;

    public class GetAppointmentsHandler : IQueryHandler<GetAppointmentsQuery, AppointmentReportDto>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAppointmentsHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<AppointmentReportDto>> Handle(GetAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var appointment = new AppointmentReportDto();

            var query = unitOfWork.BookingRepository.GetQuerable()
            .Where(b => !request.FromDate.HasValue || request.FromDate.ToDate() <= b.BookingDate)
            .Where(b => !request.ToDate.HasValue || request.ToDate.ToDate() >= b.BookingDate)
            .Where(b => !request.BranchId.HasValue || b.BranchId == request.BranchId)
            .Where(b => !request.ClientId.HasValue || b.ClientId == request.ClientId)
            .Where(b => !request.ServiceId.HasValue || b.BookingServices.Any(bs => bs.ServiceId == request.ServiceId));

            if (!string.IsNullOrWhiteSpace(request.PaymentMethod))
                query = query.Where(x => x.Transactions.Any(y => y.PaymentMethod.Contains(request.PaymentMethod)));

            if (!string.IsNullOrWhiteSpace(request.Status))
                query = query.Where(x => x.Status.Contains(request.Status));


            appointment.TotalAppointments = await query.CountAsync();

            appointment.AppointmentsByService = await query
                .SelectMany(b => b.BookingServices)
                .GroupBy(bs => new { bs.ServiceId, bs.Service.Name })
                .Select(g => new AppointmentByServiceDto
                {
                    ServiceName = g.Key.Name,
                    AppointmentCount = g.Count()
                })
                .ToListAsync();

            appointment.AppointmentsByBranch = await query
                .GroupBy(b => new { b.BranchId, b.Branch.Name })
                .Select(g => new AppointmentByBranchDto
                {
                    BranchName = g.Key.Name,
                    AppointmentCount = g.Count()
                })
                .ToListAsync();


            appointment.AppointmentsByClient = await query
                .GroupBy(b => new { b.ClientId, b.Client.FirstName, b.Client.LastName })
                .Select(g => new AppointmentByClientDto
                {
                    ClientName = g.Key.FirstName + " " + g.Key.LastName,
                    AppointmentCount = g.Count()
                })
                .ToListAsync();

            appointment.AppointmentsByStatus = await query
                .GroupBy(b => b.Status)
                .Select(g => new AppointmentByStatusDto
                {
                    Status = g.Key,
                    AppointmentCount = g.Count()
                })
                .ToListAsync();

            return new Result<AppointmentReportDto> { IsSucceeded = true, Value = appointment };
        }
    }
}
