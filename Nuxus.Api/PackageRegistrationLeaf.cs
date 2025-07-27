using System.Text.Json.Serialization;

namespace Nuxus.Api;

internal sealed class PackageRegistrationLeaf(string id, PackageRegistrationCatalog catalogEntry, string packageContent) {
    [JsonPropertyName("@id")]
    public string Id { get; } = id;

    public PackageRegistrationCatalog CatalogEntry { get; } = catalogEntry;

    public string PackageContent { get; } = packageContent;
}
