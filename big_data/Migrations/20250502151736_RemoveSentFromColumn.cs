using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace big_data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSentFromColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SentFrom",
                table: "ContactsLOL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SentFrom",
                table: "ContactsLOL",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
