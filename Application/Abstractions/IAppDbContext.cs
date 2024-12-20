using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IAppDbContext
    {
        DbSet<Booking> Bookings { get; }
        DbSet<BookingService> BookingServices { get; }
        DbSet<Branch> Branches { get; }
        DbSet<Client> Clients { get; }
        DbSet<Service> Services { get; }
        DbSet<Transaction> Transactions { get; }
    }
}
