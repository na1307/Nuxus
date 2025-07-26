namespace Nuxus.Api;

internal sealed class PackageVersions {
    public PackageVersions() { }

    [SetsRequiredMembers]
    public PackageVersions(IEnumerable<string> versions) => Versions = versions;

    public required IEnumerable<string> Versions { get; init; }
}
