using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs.Reports
{
    public record RevenueByClientDto
    {
        public string ClientName { get; set; }
        public decimal Revenue { get; set; }
    }
}
