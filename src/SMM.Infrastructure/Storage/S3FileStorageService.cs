using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using SMM.Application.Common.Interfaces;
using SMM.Shared.Settings;
using System;
using System.Runtime;

namespace SMM.Infrastructure.Storage;

/// <summary>
/// S3 functionality class
/// </summary>
public class S3FileStorageService : IFileStorageService
{
    private readonly IAmazonS3 _amazonS3;
    private readonly IOptions<S3Settings> _options;

    /// <summary>
    /// Ctor
    /// </summary>
    public S3FileStorageService(
        IAmazonS3 amazonS3,
        IOptions<S3Settings> options)
    {
        _amazonS3 = amazonS3 ?? throw new ArgumentNullException(nameof(amazonS3));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string key, CancellationToken cancellationToken = default)
    {
        await _amazonS3.DeleteObjectAsync(_options.Value.BucketName, key, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Stream> DownloadAsync(string key, CancellationToken cancellationToken = default)
    {
        var response = await _amazonS3.GetObjectAsync(_options.Value.BucketName, key, cancellationToken);
        return response.ResponseStream;
    }

    /// <inheritdoc />
    public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken = default)
    {
        var request = new PutObjectRequest
        {
            BucketName = _options.Value.BucketName,
            Key = fileName,
            InputStream = fileStream,
            ContentType = contentType
        };

        await _amazonS3.PutObjectAsync(request, cancellationToken);

        return fileName;
    }
}


