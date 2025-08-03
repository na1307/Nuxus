﻿namespace Nuxus.Server.ServiceIndexes;

internal sealed class RegistrationsBaseUrl(Uri id, RegistrationsBaseUrlVersion version) : IResource {
    public Uri Id { get; } = id;

    public string Version { get; } = version switch {
        RegistrationsBaseUrlVersion.ThreeSix => "3.6.0",
        _ => throw new ArgumentOutOfRangeException(nameof(version), version, null)
    };

    public string Type => $"RegistrationsBaseUrl/{Version}";
}
