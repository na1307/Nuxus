namespace Nuxus;

public sealed record class Package(string Name, string Version, IEnumerable<string>? TargetFrameworks, DateTime UploadTime, Guid UploadUserId);
