using System.Text.Json.Serialization;

namespace Nuxus.Backend;

internal sealed class PackageRegistrationCatalog(string id, string packageId, string version) {
    [JsonPropertyName("@id")]
    public string Id { get; } = id;

    [JsonPropertyName("id")]
    public string PackageId { get; } = packageId;

    public string Version { get; } = version;
}
