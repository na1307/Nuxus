namespace Nuxus.Api;

internal sealed class AppDbContext(DbContextOptions options) : DbContext(options) {
    public DbSet<ApiKey> ApiKeys { get; set; } = null!;

    public DbSet<Package> Packages { get; set; } = null!;
}
