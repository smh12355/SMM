using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SMM.Infrastructure;
using SMM.Persistence;
using System;

namespace SMM.CompositionRoot;

public static class DependencyInjection
{
    public static IServiceCollection AddSmmServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddInfrastructure(configuration);
        return services;
    }
}


