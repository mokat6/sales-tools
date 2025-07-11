using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace big_data.Migrations
{
    /// <inheritdoc />
    public partial class AddMarkdownNoteToCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsOnWhatsApp",
                table: "ContactsLOL",
                newName: "IsOnWhatsapp");

            migrationBuilder.AddColumn<string>(
                name: "MarkdownNote",
                table: "Companiezz",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarkdownNote",
                table: "Companiezz");

            migrationBuilder.RenameColumn(
                name: "IsOnWhatsapp",
                table: "ContactsLOL",
                newName: "IsOnWhatsApp");
        }
    }
}
