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
}
