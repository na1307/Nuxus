namespace Nuxus.Server.ServiceIndex;

/// <summary>
/// Provides extension methods for registering Package Base Address services with an <see cref="IServiceCollection"/>.
/// </summary>
public static class PackageBaseAddressServiceCollectionExtensions {
    /// <summary>
    /// Registers a Package Base Address service with the specified settings into the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <param name="id">The base address as a string for the package resource.</param>
    /// <returns>The <see cref="IServiceCollection"/> for chaining additional configuration.</returns>
    public static IServiceCollection AddPackageBaseAddress(this IServiceCollection services, string id)
        => AddPackageBaseAddress(services, id, PackageBaseAddressVersion.Three);

    /// <summary>
    /// Registers a Package Base Address service with the specified settings into the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <param name="id">The base address as a string for the package resource.</param>
    /// <returns>The <see cref="IServiceCollection"/> for chaining additional configuration.</returns>
    public static IServiceCollection AddPackageBaseAddress(this IServiceCollection services, Uri id)
        => AddPackageBaseAddress(services, id, PackageBaseAddressVersion.Three);

    /// <summary>
    /// Registers a Package Base Address service with the specified settings into the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <param name="id">The base address as a string for the package resource.</param>
    /// <param name="version">The version of the Package Base Address to use.</param>
    /// <returns>The <see cref="IServiceCollection"/> for chaining additional configuration.</returns>
    public static IServiceCollection AddPackageBaseAddress(this IServiceCollection services, string id, PackageBaseAddressVersion version)
        => AddPackageBaseAddress(services, new Uri(id, UriKind.RelativeOrAbsolute), version);

    /// <summary>
    /// Registers a Package Base Address service with the specified URI and version into the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <param name="id">The base address URI for the package resource.</param>
    /// <param name="version">The version of the Package Base Address to use.</param>
    /// <returns>The <see cref="IServiceCollection"/> for chaining additional configuration.</returns>
    public static IServiceCollection AddPackageBaseAddress(this IServiceCollection services, Uri id, PackageBaseAddressVersion version)
        => services.AddScoped<IResource>(_ => new PackageBaseAddress(id, version));
}
