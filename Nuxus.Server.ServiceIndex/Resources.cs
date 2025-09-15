using System.Collections;
using System.Collections.ObjectModel;

namespace Nuxus.Server.ServiceIndex;

/// <summary>
/// Represents a mutable collection of NuGet service resources, providing methods to add package-related endpoints for use in client operations.
/// </summary>
/// <remarks>
/// Use the provided methods to add specific resource endpoints, such as package base addresses, publish endpoints, or registration base URLs,
/// to the collection. Each method allows specifying the resource identifier and, optionally, the protocol version. The collection can be enumerated
/// to access the added resources. This class is not thread-safe; concurrent modifications should be synchronized externally.
/// </remarks>
public sealed class Resources : IResources {
    private readonly Collection<IResource> collection = [];

    /// <summary>
    /// Adds a package base address resource for the specified service endpoint using version 3.0.0 of the protocol.
    /// </summary>
    /// <param name="id">The path of the service endpoint to associate with the package base address resource. Cannot be null or empty.</param>
    /// <returns>A <see cref="Resources"/> instance representing the added package base address resource.</returns>
    public Resources AddPackageBaseAddress(string id) => AddPackageBaseAddress(id, PackageBaseAddressVersion.Three);

    /// <summary>
    /// Adds a package base address resource for the specified service endpoint using version 3.0.0 of the protocol.
    /// </summary>
    /// <param name="id">The URI of the service endpoint to associate with the package base address resource. Cannot be null.</param>
    /// <returns>A <see cref="Resources"/> instance representing the updated set of resources, including the newly added package base address.</returns>
    public Resources AddPackageBaseAddress(Uri id) => AddPackageBaseAddress(id, PackageBaseAddressVersion.Three);

    /// <summary>
    /// Adds a package base address resource for the specified service endpoint and version.
    /// </summary>
    /// <param name="id">The path as a relative or absolute URI string. This value must represent a valid URI.</param>
    /// <param name="version">The version of the package base address resource to add.</param>
    /// <returns>A <see cref="Resources"/> instance containing the added package base address resource.</returns>
    public Resources AddPackageBaseAddress(string id, PackageBaseAddressVersion version)
        => AddPackageBaseAddress(new Uri(id, UriKind.RelativeOrAbsolute), version);

    /// <summary>
    /// Adds a new package base address resource to the collection with the specified identifier and version.
    /// </summary>
    /// <param name="id">The URI that uniquely identifies the package base address resource to add. Cannot be null.</param>
    /// <param name="version">The version of the package base address resource to add.</param>
    /// <returns>The current <see cref="Resources"/> instance, enabling method chaining.</returns>
    public Resources AddPackageBaseAddress(Uri id, PackageBaseAddressVersion version) {
        collection.Add(new PackageBaseAddress(id, version));

        return this;
    }

    /// <summary>
    /// Adds a package publish resource for the specified service endpoint using version 2.0.0 of the protocol.
    /// </summary>
    /// <param name="id">The URI of the service endpoint to associate with the package publish resource. Cannot be null or empty.</param>
    /// <returns>A <see cref="Resources"/> instance representing the added package publish resource.</returns>
    public Resources AddPackagePublish(string id) => AddPackagePublish(id, PackagePublishVersion.Two);

    /// <summary>
    /// Adds a package publish resource for the specified service endpoint using version 2.0.0 of the protocol.
    /// </summary>
    /// <param name="id">The URI of the service endpoint to associate with the package publish resource. Cannot be null.</param>
    /// <returns>A <see cref="Resources"/> instance representing the updated set of resources, including the newly added package publish.</returns>
    public Resources AddPackagePublish(Uri id) => AddPackagePublish(id, PackagePublishVersion.Two);

    /// <summary>
    /// Adds a package publish resource for the specified service endpoint and version.
    /// </summary>
    /// <param name="id">The path as a relative or absolute URI string. This value must represent a valid URI.</param>
    /// <param name="version">The version of the package publish resource to add.</param>
    /// <returns>A <see cref="Resources"/> instance containing the added package publish resource.</returns>
    public Resources AddPackagePublish(string id, PackagePublishVersion version)
        => AddPackagePublish(new Uri(id, UriKind.RelativeOrAbsolute), version);

    /// <summary>
    /// Adds a new package publish resource to the collection with the specified identifier and version.
    /// </summary>
    /// <param name="id">The URI that uniquely identifies the package publish resource to add. Cannot be null.</param>
    /// <param name="version">The version of the package publish resource to add.</param>
    /// <returns>The current <see cref="Resources"/> instance, enabling method chaining.</returns>
    public Resources AddPackagePublish(Uri id, PackagePublishVersion version) {
        collection.Add(new PackagePublish(id, version));

        return this;
    }

    /// <summary>
    /// Adds a registrations base url resource for the specified service endpoint using version 3.6.0 of the protocol.
    /// </summary>
    /// <param name="id">The URI of the service endpoint to associate with the registrations base url resource. Cannot be null or empty.</param>
    /// <returns>A <see cref="Resources"/> instance representing the added registrations base url resource.</returns>
    public Resources AddRegistrationsBaseUrl(string id) => AddRegistrationsBaseUrl(id, RegistrationsBaseUrlVersion.ThreeSix);

    /// <summary>
    /// Adds a registrations base url resource for the specified service endpoint using version 3.6.0 of the protocol.
    /// </summary>
    /// <param name="id">The URI of the service endpoint to associate with the registrations base url resource. Cannot be null.</param>
    /// <returns>A <see cref="Resources"/> instance representing the updated set of resources, including the newly added registrations base url.</returns>
    public Resources AddRegistrationsBaseUrl(Uri id) => AddRegistrationsBaseUrl(id, RegistrationsBaseUrlVersion.ThreeSix);

    /// <summary>
    /// Adds a registrations base url resource for the specified service endpoint and version.
    /// </summary>
    /// <param name="id">The path as a relative or absolute URI string. This value must represent a valid URI.</param>
    /// <param name="version">The version of the registrations base url resource to add.</param>
    /// <returns>A <see cref="Resources"/> instance containing the added registrations base url resource.</returns>
    public Resources AddRegistrationsBaseUrl(string id, RegistrationsBaseUrlVersion version)
        => AddRegistrationsBaseUrl(new Uri(id, UriKind.RelativeOrAbsolute), version);

    /// <summary>
    /// Adds a new registrations base url resource to the collection with the specified identifier and version.
    /// </summary>
    /// <param name="id">The URI that uniquely identifies the registrations base url resource to add. Cannot be null.</param>
    /// <param name="version">The version of the registrations base url resource to add.</param>
    /// <returns>The current <see cref="Resources"/> instance, enabling method chaining.</returns>
    public Resources AddRegistrationsBaseUrl(Uri id, RegistrationsBaseUrlVersion version) {
        collection.Add(new RegistrationsBaseUrl(id, version));

        return this;
    }

#pragma warning disable SA1600
    IEnumerator<IResource> IEnumerable<IResource>.GetEnumerator() => collection.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<IResource>)this).GetEnumerator();
#pragma warning restore SA1600
}
