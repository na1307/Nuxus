namespace Nuxus;

public sealed record class ApiKey(Guid UserId, string KeyName, string Hash, byte[] Salt);
