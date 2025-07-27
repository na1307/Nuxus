namespace Nuxus.Api;

internal sealed class PackageVersions(IEnumerable<string> versions) {
    public IEnumerable<string> Versions { get; } = versions;
}
