using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SMM.Application.Common.Interfaces;
using SMM.Infrastructure.Storage;
using SMM.Shared.Settings;

namespace SMM.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var s3Section = configuration.GetSection("S3");
        services.Configure<S3Settings>(options => s3Section.Bind(options));

        services.AddSingleton<IAmazonS3>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<S3Settings>>().Value;
            var config = new AmazonS3Config
            {
                ServiceURL = options.ServiceURL,
                ForcePathStyle = true // важно для MinIO!
            };

            return new AmazonS3Client(options.AccessKey, options.SecretKey, config);
        });

        services.AddScoped<IFileStorageService, S3FileStorageService>();

        return services;
    }
}


