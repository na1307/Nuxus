namespace Nuxus.Server.ServiceIndexes;

/// <summary>
/// Provides extension methods for adding RegistrationsBaseUrl services to an <see cref="IServiceCollection"/>.
/// </summary>
public static class RegistrationsBaseUrlServiceCollectionExtensions {
    /// <summary>
    /// Adds the RegistrationsBaseUrl to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to which the RegistrationsBaseUrl is added.</param>
    /// <param name="id">The base URL or identifier to use for registrations.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddRegistrationsBaseUrl(this IServiceCollection services, string id)
        => AddRegistrationsBaseUrl(services, id, RegistrationsBaseUrlVersion.ThreeSix);

    /// <summary>
    /// Adds the RegistrationsBaseUrl to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to which the RegistrationsBaseUrl is added.</param>
    /// <param name="id">The base URL or identifier to use for registrations.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddRegistrationsBaseUrl(this IServiceCollection services, Uri id)
        => AddRegistrationsBaseUrl(services, id, RegistrationsBaseUrlVersion.ThreeSix);

    /// <summary>
    /// Adds the RegistrationsBaseUrl to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to which the RegistrationsBaseUrl is added.</param>
    /// <param name="id">The base URL or identifier to use for registrations.</param>
    /// <param name="version">The version of the RegistrationsBaseUrl to be used.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddRegistrationsBaseUrl(this IServiceCollection services, string id, RegistrationsBaseUrlVersion version)
        => AddRegistrationsBaseUrl(services, new Uri(id), version);

    /// <summary>
    /// Adds the RegistrationsBaseUrl to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to which the RegistrationsBaseUrl is added.</param>
    /// <param name="id">The base URL or identifier to use for registrations.</param>
    /// <param name="version">The version of the RegistrationsBaseUrl to be used.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddRegistrationsBaseUrl(this IServiceCollection services, Uri id, RegistrationsBaseUrlVersion version)
        => services.AddScoped<IResource>(_ => new RegistrationsBaseUrl(id, version));
}
