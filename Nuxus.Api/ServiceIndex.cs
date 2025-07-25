using System.Diagnostics.CodeAnalysis;

namespace Nuxus.Api;

internal sealed class ServiceIndex {
    public ServiceIndex() { }

    [SetsRequiredMembers]
    public ServiceIndex(IEnumerable<Resource> resources) => Resources = resources;

    [SuppressMessage("Performance", "CA1822:멤버를 static으로 표시하세요.", Justification = "API")]
    public string Version => "3.0.0";

    public required IEnumerable<Resource> Resources { get; init; }
}
