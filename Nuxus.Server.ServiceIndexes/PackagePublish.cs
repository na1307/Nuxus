namespace Nuxus.Server.ServiceIndexes;

internal sealed class PackagePublish(Uri id, PackagePublishVersion version) : IResource {
    public Uri Id { get; } = id;

    public string Version { get; } = version switch {
        PackagePublishVersion.Two => "2.0.0",
        _ => throw new ArgumentOutOfRangeException(nameof(version), version, null)
    };

    public string Type => $"PackagePublish/{Version}";
}
