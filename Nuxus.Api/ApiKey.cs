namespace Nuxus.Api;

[PrimaryKey(nameof(UserId), nameof(KeyName))]
[SuppressMessage("Performance", "CA1819:속성은 배열을 반환하지 않아야 합니다.", Justification = "API")]
internal sealed record class ApiKey(string UserId, string KeyName, string Hash, byte[] Salt);
