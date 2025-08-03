namespace Nuxus.Server.ServiceIndexes;

/// <summary>
/// Provides extension methods for <see cref="IEndpointRouteBuilder"/> to map service index endpoints.
/// </summary>
public static class ServiceIndexEndpointRouteBuilderExtensions {
    /// <summary>
    /// Maps a service index endpoint to the specified <see cref="IEndpointRouteBuilder"/> instance.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which the service index endpoint should be mapped.</param>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further configure the endpoint.</returns>
    public static RouteHandlerBuilder MapServiceIndex(this IEndpointRouteBuilder endpoints) => MapServiceIndex(endpoints, "/v3/index.json");

    /// <summary>
    /// Maps a service index endpoint to the specified <see cref="IEndpointRouteBuilder"/> instance.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which the service index endpoint should be mapped.</param>
    /// <param name="path">The URI path for the service index endpoint. Defaults to "/v3/index.json" if not provided. This must end with ".json".</param>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further configure the endpoint.</returns>
    /// <exception cref="ArgumentException">Thrown when the provided path does not end with ".json".</exception>
    public static RouteHandlerBuilder MapServiceIndex(this IEndpointRouteBuilder endpoints, string path)
        => MapServiceIndex(endpoints, path, ServiceIndexVersion.Three);

    /// <summary>
    /// Maps a service index endpoint to the specified <see cref="IEndpointRouteBuilder"/> instance.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which the service index endpoint should be mapped.</param>
    /// <param name="path">The URI path for the service index endpoint. This must end with ".json".</param>
    /// <param name="version">The version of the service index to be used.</param>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further configure the endpoint.</returns>
    /// <exception cref="ArgumentException">Thrown when the provided path does not end with ".json".</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the provided version is not a valid <see cref="ServiceIndexVersion"/>.</exception>
    public static RouteHandlerBuilder MapServiceIndex(this IEndpointRouteBuilder endpoints, string path, ServiceIndexVersion version) {
        if (!path.EndsWith(".json")) {
            throw new ArgumentException("Path must end with .json", nameof(path));
        }

        return endpoints.MapGet(path, (IServiceProvider provider) => TypedResults.Json(new ServiceIndex(version, provider)));
    }
}
