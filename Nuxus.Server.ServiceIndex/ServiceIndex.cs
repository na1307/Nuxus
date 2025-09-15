namespace Nuxus.Server.ServiceIndex;

internal sealed class ServiceIndex(ServiceIndexVersion version, IServiceProvider provider, IHttpContextAccessor contextAccessor) {
    public string Version { get; } = version switch {
        ServiceIndexVersion.Three => "3.0.0",
        _ => throw new ArgumentOutOfRangeException(nameof(version), version, null)
    };

    public IEnumerable<IResource> Resources { get; } = provider.GetServices<IResource>().Select(r => {
        if (!r.Id.IsAbsoluteUri) {
            r.Id = new(DomainHelper.GetCurrentDomain(contextAccessor) + r.Id.OriginalString);
        }

        return r;
    });
}
