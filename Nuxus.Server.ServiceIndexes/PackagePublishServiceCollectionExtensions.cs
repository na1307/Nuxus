namespace Nuxus.Server.ServiceIndexes;

/// <summary>
/// Provides extension methods for adding package publish services to the dependency injection container.
/// </summary>
public static class PackagePublishServiceCollectionExtensions {
    /// <summary>
    /// Adds a default package publish resource to the provided service collection with a string-based ID and a default version.
    /// </summary>
    /// <param name="services">The dependency injection service collection to which the package publish resource will be added.</param>
    /// <param name="id">The string value representing the resource ID of the package publish service.</param>
    /// <returns>The modified service collection with the package publish resource added.</returns>
    public static IServiceCollection AddPackagePublish(this IServiceCollection services, string id)
        => AddPackagePublish(services, id, PackagePublishVersion.Two);

    /// <summary>
    /// Adds a default package publish resource to the provided service collection with a string-based ID and a default version.
    /// </summary>
    /// <param name="services">The dependency injection service collection to which the package publish resource will be added.</param>
    /// <param name="id">The string value representing the resource ID of the package publish service.</param>
    /// <returns>The modified service collection with the package publish resource added.</returns>
    public static IServiceCollection AddPackagePublish(this IServiceCollection services, Uri id)
        => AddPackagePublish(services, id, PackagePublishVersion.Two);

    /// <summary>
    /// Adds a package publish resource to the provided service collection with a string-based ID and the specified version.
    /// </summary>
    /// <param name="services">The dependency injection service collection to which the resource will be added.</param>
    /// <param name="id">The string value representing the resource ID of the package publish service.</param>
    /// <param name="version">The version of the package publish service to be used.</param>
    /// <returns>The modified service collection with the package publish resource added.</returns>
    public static IServiceCollection AddPackagePublish(this IServiceCollection services, string id, PackagePublishVersion version)
        => AddPackagePublish(services, new Uri(id), version);

    /// <summary>
    /// Adds a package publish resource to the provided service collection with the specified ID and version.
    /// </summary>
    /// <param name="services">The dependency injection service collection to which the resource will be added.</param>
    /// <param name="id">The URI representing the resource ID of the package publish service.</param>
    /// <param name="version">The version of the package publish service to be used.</param>
    /// <returns>The modified service collection with the package publish resource added.</returns>
    public static IServiceCollection AddPackagePublish(this IServiceCollection services, Uri id, PackagePublishVersion version)
        => services.AddScoped<IResource>(_ => new PackagePublish(id, version));
}
