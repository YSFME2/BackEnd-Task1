using Application.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IUnitOfWork
    {
        IGenericRepository<Booking> BookingRepository { get; }
        IGenericRepository<BookingService> BookingServiceRepository { get; }
        IGenericRepository<Branch> BranchRepository { get; }
        IGenericRepository<Client> ClientRepository { get; }
        IGenericRepository<Service> ServiceRepository { get; }
        IGenericRepository<Transaction> TransactionRepository { get; }
        
        Task<int> CompleteAsync();
    }
}
