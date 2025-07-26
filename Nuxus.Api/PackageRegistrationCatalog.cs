using System.Text.Json.Serialization;

namespace Nuxus.Api;

internal sealed class PackageRegistrationCatalog {
    public PackageRegistrationCatalog() { }

    [SetsRequiredMembers]
    public PackageRegistrationCatalog(string id, string packageId, string version) {
        Id = id;
        PackageId = packageId;
        Version = version;
    }

    [JsonPropertyName("@id")]
    public required string Id { get; init; }

    [JsonPropertyName("id")]
    public required string PackageId { get; init; }

    public required string Version { get; init; }
}
