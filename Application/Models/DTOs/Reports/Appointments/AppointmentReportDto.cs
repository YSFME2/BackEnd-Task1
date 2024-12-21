using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs.Reports.Appointments
{
    public class AppointmentReportDto
    {
        public int TotalAppointments { get; set; }
        public List<AppointmentByServiceDto> AppointmentsByService { get; set; }
        public List<AppointmentByBranchDto> AppointmentsByBranch { get; set; }
        public List<AppointmentByClientDto> AppointmentsByClient { get; set; }
        public List<AppointmentByStatusDto> AppointmentsByStatus { get; set; }
    }

    public class AppointmentByServiceDto
    {
        public string ServiceName { get; set; }
        public int AppointmentCount { get; set; }
    }

    public class AppointmentByBranchDto
    {
        public string BranchName { get; set; }
        public int AppointmentCount { get; set; }
    }
    public class AppointmentByClientDto
    {
        public string ClientName { get; set; }
        public int AppointmentCount { get; set; }
    }

    public class AppointmentByStatusDto
    {
        public string Status { get; set; }
        public int AppointmentCount { get; set; }
    }
}
