using System.Diagnostics.CodeAnalysis;

namespace Nuxus.Api;

internal sealed class PackageRegistration {
    public PackageRegistration() { }

    [SetsRequiredMembers]
    public PackageRegistration(IEnumerable<PackageRegistrationPage> items) => Items = items;

    public int Count => Items.Count();

    public required IEnumerable<PackageRegistrationPage> Items { get; init; }
}
