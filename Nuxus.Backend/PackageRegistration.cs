namespace Nuxus.Backend;

internal sealed class PackageRegistration(IEnumerable<PackageRegistrationPage> items) {
    public int Count => Items.Count();

    public IEnumerable<PackageRegistrationPage> Items { get; } = items;
}
