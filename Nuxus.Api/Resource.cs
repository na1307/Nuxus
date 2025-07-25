using System.Text.Json.Serialization;

namespace Nuxus.Api;

internal sealed record class Resource(
    [property: JsonPropertyName("@id")] string Id,
    [property: JsonPropertyName("@type")] string Type,
    string? Comment = null);
