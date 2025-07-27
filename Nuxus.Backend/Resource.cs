using System.Text.Json.Serialization;

namespace Nuxus.Backend;

internal sealed record class Resource(
    [property: JsonPropertyName("@id")] string Id,
    [property: JsonPropertyName("@type")] string Type,
    string? Comment = null);
