using Microsoft.EntityFrameworkCore;
using Smm.Application.Common.Interfaces;
using System;

namespace SMM.Persistence;

/// <summary>
/// Smm db context
/// </summary>
public class SmmDbContext : DbContext, ISmmDbContext
{
    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="options">Options</param>
    public SmmDbContext(DbContextOptions<SmmDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Realisation of interface IsmmDbContext
    /// </summary>
    /// <param name="cancellationToken">Cansellation token</param>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => base.SaveChangesAsync(cancellationToken);
}


