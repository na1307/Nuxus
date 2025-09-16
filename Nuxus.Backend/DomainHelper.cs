namespace Nuxus.Backend;

internal static class DomainHelper {
    public static string GetCurrentDomain(IHttpContextAccessor contextAccessor) {
        var request = contextAccessor.HttpContext!.Request;

        return $"{request.Scheme}://{request.Host}";
    }
}
