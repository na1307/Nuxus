namespace Nuxus.Backend;

internal sealed class AppDbContext(DbContextOptions options) : DbContext(options) {
    public DbSet<Package> Packages { get; set; } = null!;

    public DbSet<ApiKey> ApiKeys { get; set; } = null!;
}
