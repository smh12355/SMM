using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Smm.Application.Common.Interfaces;

namespace SMM.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SmmDbContext>((serviceProvider, options) =>
        {
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            options
                .UseNpgsql(configuration.GetConnectionString("Postgres"))
                .UseLoggerFactory(loggerFactory)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        });

        services.AddScoped<ISmmDbContext>(provider => provider.GetRequiredService<SmmDbContext>());
         
        return services;
    }
}
