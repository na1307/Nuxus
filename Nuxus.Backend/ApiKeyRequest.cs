namespace Nuxus.Backend;

internal sealed record class ApiKeyRequest(Guid UserId, string KeyName);
