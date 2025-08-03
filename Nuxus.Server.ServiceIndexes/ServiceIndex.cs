namespace Nuxus.Server.ServiceIndexes;

internal sealed class ServiceIndex(ServiceIndexVersion version, IServiceProvider provider) {
    public string Version { get; } = version switch {
        ServiceIndexVersion.Three => "3.0.0",
        _ => throw new ArgumentOutOfRangeException(nameof(version), version, null)
    };

    public IEnumerable<IResource> Resources { get; } = provider.GetServices<IResource>();
}
