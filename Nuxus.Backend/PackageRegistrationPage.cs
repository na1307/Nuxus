using System.Text.Json.Serialization;

namespace Nuxus.Backend;

internal sealed class PackageRegistrationPage(string id, IEnumerable<PackageRegistrationLeaf> items, string parent, string lower, string upper) {
    [JsonPropertyName("@id")]
    public string Id { get; } = id;

    public int Count => Items.Count();

    public IEnumerable<PackageRegistrationLeaf> Items { get; } = items;

    public string Parent { get; } = parent;

    public string Lower { get; } = lower;

    public string Upper { get; } = upper;
}
