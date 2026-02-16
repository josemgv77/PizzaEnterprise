using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PizzaEnterprise.Domain.Interfaces;
using PizzaEnterprise.Infrastructure.AS400;
using PizzaEnterprise.Infrastructure.Persistence;
using PizzaEnterprise.Infrastructure.Persistence.Repositories;

namespace PizzaEnterprise.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("DefaultConnection string is not configured.");
            }
            
            if (connectionString.Contains("Data Source"))
            {
                // Use SQLite for cross-platform compatibility
                options.UseSqlite(connectionString,
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            }
            else
            {
                // Use SQL Server for production
                options.UseSqlServer(connectionString,
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            }
        });

        // Register repositories
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // Register Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Register AS400 Service
        services.AddScoped<IAS400Service, AS400Service>();

        return services;
    }
}
