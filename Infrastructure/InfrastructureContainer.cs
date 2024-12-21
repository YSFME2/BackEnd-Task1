using Infrastructure.Persistence.Interceptors;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Infrastructure
{
    public static class InfrastructureContainer
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfigurationManager configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ISaveChangesInterceptor, SaveChangesEntityInterceptor>();
            services.AddDbContext<IAppDbContext, AppDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            });

            using (var serviceProvider = services.BuildServiceProvider())
            using (var dbContext = serviceProvider.GetService<AppDbContext>())
            {
                if (dbContext != null)
                {
                    //dbContext.Database.EnsureCreated();
                    if (dbContext.Database.GetPendingMigrations().Count() > 0)
                        dbContext.Database.Migrate();
                }
            }

            return services;
        }
    }
}
