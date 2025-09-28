using Microsoft.EntityFrameworkCore;

namespace Nuxus.Server;

/// <summary>
/// Interface for the Nuxus database context that handles packages and API keys in the database.
/// </summary>
/// <remarks>
/// <see cref="NuxusDbContextBase"/> is the default implementation of this interface.
/// Any implementation must inherit from <see cref="DbContext"/> and override the <c>OnModelCreating</c> method.
/// </remarks>
public interface INuxusDbContext {
    /// <summary>
    /// Represents the database set for managing <see cref="Package"/> entities within the context.
    /// Provides access to query, add, update, and delete package records in the database.
    /// </summary>
    DbSet<Package> Packages { get; }

    /// <summary>
    /// Represents the database set for managing <see cref="ApiKey"/> entities within the context.
    /// Provides access to query, add, update, and delete API key records in the database.
    /// </summary>
    DbSet<ApiKey> ApiKeys { get; }

    internal sealed DbContext ToDbContext() => (DbContext)this;
}
