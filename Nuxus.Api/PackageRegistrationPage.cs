using System.Text.Json.Serialization;

namespace Nuxus.Api;

internal sealed class PackageRegistrationPage {
    public PackageRegistrationPage() { }

    [SetsRequiredMembers]
    public PackageRegistrationPage(string id, IEnumerable<PackageRegistrationLeaf> items, string parent, string lower, string upper) {
        Id = id;
        Items = items;
        Parent = parent;
        Lower = lower;
        Upper = upper;
    }

    [JsonPropertyName("@id")]
    public required string Id { get; init; }

    public int Count => Items.Count();

    public required IEnumerable<PackageRegistrationLeaf> Items { get; init; }

    public required string Parent { get; init; }

    public required string Lower { get; init; }

    public required string Upper { get; init; }
}
