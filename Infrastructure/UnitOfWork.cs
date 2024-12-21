using Infrastructure.Persistence;
using Infrastructure.Repositories;

namespace Infrastructure;
public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    //IGenericRepository<Entity> entityRepository;
    //public IGenericRepository<Entity> EntityRepository => entityRepository ??= new GenericRepository<Entity>(context);
    IGenericRepository<Booking>? bookingRepository;
    public IGenericRepository<Booking> BookingRepository => bookingRepository ??= new GenericRepository<Booking>(context);
    IGenericRepository<BookingService>? bookingServiceRepository;
    public IGenericRepository<BookingService> BookingServiceRepository => bookingServiceRepository ??= new GenericRepository<BookingService>(context);
    IGenericRepository<Branch>? branchRepository;
    public IGenericRepository<Branch> BranchRepository => branchRepository ??= new GenericRepository<Branch>(context);
    IGenericRepository<Client>? clientRepository;
    public IGenericRepository<Client> ClientRepository => clientRepository ??= new GenericRepository<Client>(context);
    IGenericRepository<Service>? serviceRepository;
    public IGenericRepository<Service> ServiceRepository => serviceRepository ??= new GenericRepository<Service>(context);
    IGenericRepository<Transaction>? transactionRepository;
    public IGenericRepository<Transaction> TransactionRepository => transactionRepository ??= new GenericRepository<Transaction>(context);


    async Task<int> IUnitOfWork.CompleteAsync()
	{
		try
		{
			return await context.SaveChangesAsync();
		}
		catch
		{
			throw;
		}
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            context.Dispose();
        }
    }

}
