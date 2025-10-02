using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Nuxus;

/// <summary>
/// Provides extension methods for configuring services related to the Nuxus server.
/// </summary>
public static class NuxusServiceCollectionExtensions {
    /// <summary>
    /// Adds a Nuxus-specific DbContext of type <typeparamref name="TContext"/> to the service collection.
    /// </summary>
    /// <typeparam name="TContext">The type of the DbContext that implements <see cref="INuxusDbContext"/>.</typeparam>
    /// <param name="serviceCollection">The <see cref="IServiceCollection"/> to which the DbContext is added.</param>
    /// <param name="optionsAction">An optional action to configure the <see cref="DbContextOptionsBuilder"/>.</param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddNuxusDbContext<TContext>(
        this IServiceCollection serviceCollection,
        Action<DbContextOptionsBuilder>? optionsAction = null) where TContext : DbContext, INuxusDbContext {
        serviceCollection.AddDbContext<INuxusDbContext, TContext>(optionsAction);

        return serviceCollection;
    }
}
