using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs.Reports
{
    public class TotalsRevenueDto
    {
        public decimal TotalRevenue { get; set; }
        public List<RevenueByServiceDto> RevenueByServices { get; set; }
        public List<RevenueByBranchDto> RevenueByBranches { get; set; }
        public List<RevenueByPaymentMethodDto> RevenueByPaymentMethods { get; set; }
    }

    public record RevenueByServiceDto
    {
        public string ServiceName { get; set; }
        public decimal Revenue { get; set; }
    }

    public record RevenueByBranchDto
    {
        public string BranchName { get; set; }
        public decimal Revenue { get; set; }
    }

    public record RevenueByPaymentMethodDto
    {
        public string PaymentMethod { get; set; }
        public decimal Revenue { get; set; }
    }
}
