using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using Nuxus.Server;
using Nuxus.Server.ServiceIndex;
using System.Security.Cryptography;
using System.Text;

namespace Nuxus.Backend;

[SuppressMessage("Performance", "CA1862:대/소문자를 구분하지 않는 문자열 비교를 수행하려면 \'StringComparison\' 메서드 오버로드를 사용합니다.", Justification = "SQL")]
internal static class Program {
    private static readonly DirectoryInfo PackagesDirectory = Directory.CreateDirectory(Path.Combine(AppContext.BaseDirectory, "packages"));

    private static Task Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddNuxusDbContext<NuxusDbContext>(options => options.UseSqlite(connectionString));
        builder.Services.AddHttpContextAccessor();

        // Add services to the container.
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi("v3");

        builder.Services.AddCors(options => options.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

        builder.Services.AddServiceIndex(resources => {
            resources.AddPackageBaseAddress("/v3/package");
            resources.AddPackagePublish("/v3/package");
            resources.AddRegistrationsBaseUrl("/v3/metadata");
        });

        builder.Services.AddRazorPages();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment()) {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseCors("AllowAll");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.MapOpenApi();
        }

        // Service index
        app.MapServiceIndex("/v3/index.json").WithName("ServiceIndex").WithDescription("Service index");

        // Package Content
        app.MapGet("/v3/package/{packageName}/index.json", Version).WithName("PackageVersion").WithDescription("Package Version");

        app.MapGet("/v3/package/{packageName}/{packageVersion}/{packageName2}.{packageVersion2}.nupkg", DownloadPackage)
            .WithName("DownloadPackage").WithDescription("Download Package");

        app.MapGet("/v3/package/{packageName}/{packageVersion}/{packageName2}.nuspec", DownloadManifest)
            .WithName("DownloadManifest").WithDescription("Download Manifest");

        // Push and Delete
        app.MapPut("/v3/package", Push).DisableAntiforgery().WithName("PackagePublish").WithDescription("Push a package");
        app.MapDelete("/v3/package/{packageName}/{packageVersion}", Delete).WithName("PackageDelete").WithDescription("Delete a package");

        // Package metadata
        app.MapGet("/v3/metadata/{packageName}/index.json", Metadata).WithName("PackageMetadata").WithName("Package Metadata");

        // API Key management
        app.MapPost("/api-key", AddApiKey).WithName("AddApiKey").WithDescription("Add API Key");
        app.MapDelete("/api-key/{userName}/{keyName}", DeleteApiKey).WithName("DeleteApiKey").WithDescription("Delete API Key");

        app.MapStaticAssets();
        app.MapRazorPages().WithStaticAssets();

        return app.RunAsync();
    }

    private static async Task<IResult> Version(NuxusDbContext db, string packageName) {
        var packages = db.Packages.Where(p => p.Name.ToLower() == packageName);

        if (!await packages.AnyAsync()) {
            return TypedResults.NotFound();
        }

        var versions = packages.Select(p => p.Version);

        return TypedResults.Json(new PackageVersions(versions));
    }

    private static async Task<IResult> DownloadPackage(
        NuxusDbContext db,
        string packageName,
        string packageVersion,
        string packageName2,
        string packageVersion2) {
        var package = await db.Packages.SingleOrDefaultAsync(p => p.Name.ToLower() == packageName && p.Version.ToLower() == packageVersion);

        if (package is null || $"{packageName}.{packageVersion}" != $"{packageName2}.{packageVersion2}") {
            return TypedResults.NotFound();
        }

        var filePath = Path.Combine(PackagesDirectory.FullName, $"{packageName}.{packageVersion}.nupkg");

        return File.Exists(filePath) ? TypedResults.PhysicalFile(filePath, "application/zip") : TypedResults.NotFound();
    }

    private static async Task<IResult> DownloadManifest(
        NuxusDbContext db,
        string packageName,
        string packageVersion,
        string packageName2) {
        var package = await db.Packages.SingleOrDefaultAsync(p => p.Name.ToLower() == packageName && p.Version.ToLower() == packageVersion);

        if (package is null || packageName != packageName2) {
            return TypedResults.NotFound();
        }

        var filePath = Path.Combine(PackagesDirectory.FullName, $"{packageName}.{packageVersion}.nupkg");

        if (!File.Exists(filePath)) {
            return TypedResults.NotFound();
        }

        using PackageArchiveReader par = new(filePath);
        await using var nuspec = par.GetNuspec();
        using StreamReader reader = new(nuspec);

        return TypedResults.Text(await reader.ReadToEndAsync(), "application/xml");
    }

    private static async Task<IResult> Push(NuxusDbContext db, [FromHeader(Name = "X-NuGet-ApiKey")] string apiKey, HttpRequest request) {
        var userKey = GetApiKey(db, apiKey);

        if (userKey is null) {
            return TypedResults.Unauthorized();
        }

        if (!request.HasFormContentType) {
            return TypedResults.BadRequest();
        }

        var form = await request.ReadFormAsync();
        var files = form.Files;

        if (files.Count != 1) {
            return TypedResults.BadRequest();
        }

        var fileName = Path.Combine(PackagesDirectory.FullName, Path.GetRandomFileName());

        await using (FileStream fs = new(fileName, FileMode.CreateNew, FileAccess.Write, FileShare.None, 4096, true)) {
            await files[0].CopyToAsync(fs);
        }

        string id;
        string version;
        IEnumerable<string> targetFrameworks;

        try {
            using PackageArchiveReader par = new(fileName);
            var nuspec = par.NuspecReader;
            id = nuspec.GetId();
            version = nuspec.GetVersion().ToFullString();
            targetFrameworks = nuspec.GetDependencyGroups().Select(static dg => dg.TargetFramework.Framework);
            var existingPackage = await db.Packages.FindAsync(id, version);

            if (existingPackage is not null) {
                File.Delete(fileName);

                return TypedResults.Conflict();
            }
        } catch (Exception ex) when (ex is InvalidDataException or PackagingException) {
            File.Delete(fileName);

            return TypedResults.BadRequest();
        }

        File.Move(fileName, Path.Combine(Path.GetDirectoryName(fileName)!, $"{id}.{version}.nupkg"));
        await db.Packages.AddAsync(new(id, version, targetFrameworks, DateTime.Now, userKey.UserId));
        await db.SaveChangesAsync();

        return TypedResults.Created();
    }

    private static async Task<IResult> Delete(
        NuxusDbContext db,
        string packageName,
        string packageVersion,
        [FromHeader(Name = "X-NuGet-ApiKey")] string apiKey) {
        var userKey = GetApiKey(db, apiKey);

        if (userKey is null) {
            return TypedResults.Unauthorized();
        }

        var packageEntry = await db.Packages.SingleOrDefaultAsync(p
            => p.Name.ToLower() == packageName.ToLower() && p.Version.ToLower() == packageVersion.ToLower());

        if (packageEntry is null) {
            return TypedResults.NotFound();
        }

        if (userKey.UserId != packageEntry.UploadUserId) {
            return TypedResults.Unauthorized();
        }

        var filePath = Path.Combine(PackagesDirectory.FullName, $"{packageName}.{packageVersion}.nupkg");

        if (!File.Exists(filePath)) {
            return TypedResults.NotFound();
        }

        File.Delete(filePath);
        db.Packages.Remove(packageEntry);
        await db.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    private static async Task<IResult> Metadata(NuxusDbContext db, IHttpContextAccessor httpContextAccessor, string packageName) {
        var packages = db.Packages.Where(p => p.Name.ToLower() == packageName);

        if (!await packages.AnyAsync()) {
            return TypedResults.NotFound();
        }

        var domain = DomainHelper.GetCurrentDomain(httpContextAccessor);

        var leaves = packages.Select(p
            => new PackageRegistrationLeaf($"{domain}/v3/metadata/{p.Name.ToLowerInvariant()}/{p.Version.ToLowerInvariant()}.json",
                new(string.Empty, p.Name, p.Version),
                $"{domain}/v3/package/{p.Name.ToLowerInvariant()}/{p.Version.ToLowerInvariant()}/{p.Name.ToLowerInvariant()}.{p.Version.ToLowerInvariant()}.nupkg"));

        var currentPath = $"{domain}{httpContextAccessor.HttpContext!.Request.Path}";
        var minver = await packages.MinAsync(p => p.Version);
        var maxver = await packages.MaxAsync(p => p.Version);

        return TypedResults.Json(new PackageRegistration([new($"{currentPath}#page/{minver}/{maxver}", leaves, currentPath, minver, maxver)]));
    }

    private static async Task<IResult> AddApiKey(NuxusDbContext db, ApiKeyRequest request) {
        var existing = await db.ApiKeys.FindAsync(request.UserId, request.KeyName);

        if (existing is not null) {
            return TypedResults.Conflict();
        }

        var (originalApiKey, hashString, salt) = GetUniqueKey(db);

        db.ApiKeys.Add(new(request.UserId, request.KeyName, hashString, salt));
        await db.SaveChangesAsync();

        return TypedResults.Ok(new {
            ApiKey = originalApiKey
        });

        static (string, string, byte[]) GetUniqueKey(NuxusDbContext db) {
            string originalApiKey;
            byte[] salt;
            string hashString;
            ApiKey? existingKey;

            do {
                originalApiKey = GenerateRandomString(48);
                salt = RandomNumberGenerator.GetBytes(16);
                hashString = HashString(originalApiKey, salt);
                existingKey = db.ApiKeys.AsEnumerable().SingleOrDefault(ak => ak.Hash == hashString && ak.Salt.SequenceEqual(salt));
            } while (existingKey is not null);

            return (originalApiKey, hashString, salt);

            static string GenerateRandomString(int length) {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var result = new char[length];

                for (var i = 0; i < length; i++) {
                    result[i] = chars[Random.Shared.Next(chars.Length)];
                }

                return new(result);
            }

            static string HashString(string original, byte[] salt) {
                var hashBytes = SHA512.HashData(Encoding.UTF8.GetBytes(original).Concat(salt).ToArray());

                return Convert.ToBase64String(hashBytes);
            }
        }
    }

    private static async Task<IResult> DeleteApiKey(NuxusDbContext db, string userName, string keyName) {
        var key = await db.ApiKeys.FindAsync(userName, keyName);

        if (key is null) {
            return TypedResults.NotFound();
        }

        db.ApiKeys.Remove(key);
        await db.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    private static ApiKey? GetApiKey(NuxusDbContext db, string apiKey) {
        return !string.IsNullOrWhiteSpace(apiKey) ? db.ApiKeys.AsEnumerable().FirstOrDefault(Predicate) : null;

        bool Predicate(ApiKey storedKey) {
            var keyBytes = Encoding.UTF8.GetBytes(apiKey).Concat(storedKey.Salt).ToArray();
            var hashBytes = SHA512.HashData(keyBytes);

            return Convert.ToBase64String(hashBytes) == storedKey.Hash;
        }
    }
}
