using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BookingService : AuditableEntity
    {
        public decimal Price { get; set; }


        public int BookingId { get; set; }
        public virtual Booking Booking { get; set; }
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }

    }
}
