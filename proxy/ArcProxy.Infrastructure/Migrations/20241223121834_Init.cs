using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArcProxy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeoServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Uri = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeoServiceRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RequestLimit = table.Column<int>(type: "INTEGER", nullable: false),
                    ServiceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoServiceRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeoServiceRules_GeoServices_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "GeoServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeoServiceRules_ServiceId",
                table: "GeoServiceRules",
                column: "ServiceId",
                unique: true);

            migrationBuilder.InsertData(
                "GeoServices",
                new string[] { "Id", "Uri", "Name" },
                new string[,] {
                    { "1", "A06_ATE_TE_WGS84/ATE_Minsk_public/MapServer/1", "населенные пункты(ад. деление)" },
                    { "2", "A05_EGRNI_WGS84/Uchastki_Minsk_public/MapServer/0", "земельные участки" },
                    { "3", "A01_ZIS_WGS84/Land_Minsk_public/MapServer/0", "виды земель" }
                });

            migrationBuilder.InsertData(
                "GeoServiceRules",
                new string[] { "ServiceId", "RequestLimit" },
                new string[,] {
                    { "1", "10" },
                    { "2", "10" },
                    { "3", "10" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeoServiceRules");

            migrationBuilder.DropTable(
                name: "GeoServices");
        }
    }
}
