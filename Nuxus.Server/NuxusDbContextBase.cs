using Microsoft.EntityFrameworkCore;

namespace Nuxus.Server;

/// <summary>
/// An abstract base class for the Nuxus database context that serves as the foundation
/// for managing the application's database operations.
/// It provides DbSets for the `Packages` and `ApiKeys` entities and handles the configuration
/// of their models by overriding the `OnModelCreating` method.
/// </summary>
/// <remarks>
/// This class inherits from <see cref="DbContext"/> and implements the <see cref="INuxusDbContext"/> interface.
/// It is designed to encapsulate entity framework core functionality and entity mappings.
/// </remarks>
public abstract class NuxusDbContextBase(DbContextOptions options) : DbContext(options), INuxusDbContext {
    /// <inheritdoc />
    public DbSet<Package> Packages { get; set; }

    /// <inheritdoc />
    public DbSet<ApiKey> ApiKeys { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Package>(entity => {
            entity.HasKey(e => new {
                e.Name,
                e.Version
            });
            entity.Property(e => e.Name).HasColumnName("Name").IsRequired();
            entity.Property(e => e.Version).HasColumnName("Version").IsRequired();
            entity.Property(e => e.TargetFrameworks).HasColumnName("TargetFrameworks").IsRequired();
            entity.Property(e => e.UploadTime).HasColumnName("UploadTime").IsRequired();
            entity.Property(e => e.UploadUserId).HasColumnName("UploadUserId").IsRequired();
        });

        modelBuilder.Entity<ApiKey>(entity => {
            entity.HasKey(e => new {
                e.UserId,
                e.KeyName
            });
            entity.Property(e => e.UserId).HasColumnName("UserId").IsRequired();
            entity.Property(e => e.KeyName).HasColumnName("KeyName").IsRequired();
            entity.Property(e => e.Hash).HasColumnName("Hash").IsRequired();
            entity.Property(e => e.Salt).HasColumnName("Salt").IsRequired();
        });
    }
}
