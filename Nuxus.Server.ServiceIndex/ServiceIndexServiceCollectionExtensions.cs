namespace Nuxus.Server.ServiceIndex;

/// <summary>
/// Provides extension methods for registering service index resources with an <see cref="IServiceCollection"/>.
/// </summary>
/// <remarks>
/// These extension methods enable configuration and dependency injection of service index resources within an application's service collection.
/// Use these methods to add and customize <see cref="IResources"/> implementations for scoped lifetimes.
/// </remarks>
public static class ServiceIndexServiceCollectionExtensions {
    /// <summary>
    /// Adds the Resources service and its associated IResources interface to the service collection, allowing configuration via the specified setup action.
    /// </summary>
    /// <remarks>
    /// The Resources service is registered with scoped lifetime. The setup action is invoked once per scope to configure each Resources instance.
    /// This method enables dependency injection of IResources throughout the application.
    /// </remarks>
    /// <param name="collection">The service collection to which the Resources and IResources services will be added.</param>
    /// <param name="setupAction">An action delegate used to configure the Resources instance before it is registered.</param>
    /// <returns>The IServiceCollection instance with the Resources and IResources services registered.</returns>
    public static IServiceCollection AddServiceIndex(this IServiceCollection collection, Action<Resources> setupAction) {
        collection.AddScoped<IResources, Resources>(_ => {
            Resources rs = new();

            setupAction(rs);

            return rs;
        });

        return collection;
    }
}
