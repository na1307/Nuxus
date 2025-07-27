namespace Nuxus.Backend;

internal static class DomainHelper {
    public static string GetCurrentDomain(IHttpContextAccessor httpContextAccessor) {
        var hc = httpContextAccessor.HttpContext;
        var scheme = hc?.Request.Scheme;
        var host = hc?.Request.Host.Value;

        return scheme is not null && host is not null ? $"{scheme}://{host}" : string.Empty;
    }
}
