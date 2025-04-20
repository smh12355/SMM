using System;

namespace Smm.Application.Common.Interfaces;

/// <summary>
/// Db context to work with postgres
/// </summary>
public interface ISmmDbContext
{
    /// <summary>
    /// Save chages in database
    /// </summary>
    /// <param name="cancellationToken">Casellation token</param>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}


