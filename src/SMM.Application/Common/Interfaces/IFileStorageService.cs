using System;

namespace SMM.Application.Common.Interfaces;

/// <summary>
/// Interface to work with s3
/// </summary>
public interface IFileStorageService
{
    /// <summary>
    /// Upload file
    /// </summary>
    Task<string> UploadAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken = default);

    /// <summary>
    /// Download file
    /// </summary>
    Task<Stream> DownloadAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Clear by key
    /// </summary>
    Task DeleteAsync(string key, CancellationToken cancellationToken = default);
}


