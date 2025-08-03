﻿namespace Nuxus.Server.ServiceIndexes;

internal sealed class PackageBaseAddress(Uri id, PackageBaseAddressVersion version) : IResource {
    public Uri Id { get; } = id;

    public string Version { get; } = version switch {
        PackageBaseAddressVersion.Three => "3.0.0",
        _ => throw new ArgumentOutOfRangeException(nameof(version), version, null)
    };

    public string Type => $"PackageBaseAddress/{Version}";
}
