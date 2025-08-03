using System.Text.Json.Serialization;

namespace Nuxus.Server.ServiceIndexes;

internal interface IResource {
    [JsonPropertyName("@id")]
    Uri Id { get; }

    [JsonPropertyName("@type")]
    string Type { get; }
}
