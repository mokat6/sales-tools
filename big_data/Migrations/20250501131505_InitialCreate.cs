using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace big_data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companiezz",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryGoogle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RatingGoogle = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RatedCount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoogleMapsUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BigFishScore = table.Column<int>(type: "int", nullable: true),
                    Classification = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companiezz", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactsLOL",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsOnWhatsApp = table.Column<bool>(type: "bit", nullable: true),
                    ContactedFromEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Checked = table.Column<bool>(type: "bit", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SentFrom = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactsLOL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactsLOL_Companiezz_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companiezz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactsLOL_CompanyId",
                table: "ContactsLOL",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactsLOL");

            migrationBuilder.DropTable(
                name: "Companiezz");
        }
    }
}
