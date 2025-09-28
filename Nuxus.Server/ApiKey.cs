namespace Nuxus.Server;

public sealed record class ApiKey(Guid UserId, string KeyName, string Hash, byte[] Salt);
