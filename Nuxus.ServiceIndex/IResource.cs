using System.Text.Json.Serialization;

namespace Nuxus.ServiceIndex;

internal interface IResource {
    [JsonPropertyName("@id")]
    Uri Id { get; set; }

    [JsonPropertyName("@type")]
    string Type { get; }
}
