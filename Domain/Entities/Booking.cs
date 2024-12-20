using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Booking : AuditableEntity
    {
        public string Status { get; set; } = null!;
        public DateOnly BookingDate { get; set; }
        public TimeOnly BookingTime { get; set; }

        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; }
    }
}
