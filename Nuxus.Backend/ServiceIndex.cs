namespace Nuxus.Backend;

internal sealed class ServiceIndex(IEnumerable<Resource> resources) {
    [SuppressMessage("Performance", "CA1822:멤버를 static으로 표시하세요.", Justification = "API")]
    public string Version => "3.0.0";

    public IEnumerable<Resource> Resources { get; } = resources;
}
