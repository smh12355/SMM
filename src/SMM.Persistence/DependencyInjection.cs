using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Smm.Application.Common.Interfaces;
using System;

namespace SMM.Persistence;

public static class DependencyInjection
{
    /// <summary>
    /// Extension method for persistence method
    /// </summary>
    /// <param name="services">Services</param>
    /// <param name="configuration">Configuration</param>
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SmmDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Postgres")));

        services.AddScoped<ISmmDbContext>(provider => provider.GetRequiredService<SmmDbContext>());

        return services;
    }
}
