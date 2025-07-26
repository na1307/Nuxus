using System.Text.Json.Serialization;

namespace Nuxus.Api;

internal sealed class PackageRegistrationLeaf {
    public PackageRegistrationLeaf() { }

    [SetsRequiredMembers]
    public PackageRegistrationLeaf(string id, PackageRegistrationCatalog catalogEntry, string packageContent) {
        Id = id;
        CatalogEntry = catalogEntry;
        PackageContent = packageContent;
    }

    [JsonPropertyName("@id")]
    public required string Id { get; init; }

    public required PackageRegistrationCatalog CatalogEntry { get; init; }

    public required string PackageContent { get; init; }
}
