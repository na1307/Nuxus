namespace Nuxus.Backend;

[PrimaryKey(nameof(Name), nameof(Version))]
internal sealed record class Package(string Name, string Version, IEnumerable<string>? TargetFrameworks, DateTime UploadTime, Guid UploadUserId);
