using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nuxus.Backend.Migrations;

/// <inheritdoc />
public sealed partial class InitialCreate : Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.CreateTable(
            name: "ApiKeys",
            columns: table => new {
                UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                KeyName = table.Column<string>(type: "TEXT", nullable: false),
                Hash = table.Column<string>(type: "TEXT", nullable: false),
                Salt = table.Column<byte[]>(type: "BLOB", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_ApiKeys", x => new { x.UserId, x.KeyName }));

        migrationBuilder.CreateTable(
            name: "Packages",
            columns: table => new {
                Name = table.Column<string>(type: "TEXT", nullable: false),
                Version = table.Column<string>(type: "TEXT", nullable: false),
                TargetFrameworks = table.Column<string>(type: "TEXT", nullable: false),
                UploadTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                UploadUserId = table.Column<Guid>(type: "TEXT", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Packages", x => new { x.Name, x.Version }));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropTable(name: "ApiKeys");
        migrationBuilder.DropTable(name: "Packages");
    }
}
