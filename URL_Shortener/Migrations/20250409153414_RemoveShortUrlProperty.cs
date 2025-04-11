using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URL_Shortener.Migrations
{
    /// <inheritdoc />
    public partial class RemoveShortUrlProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortUrl",
                table: "ShortenedUrls");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "ShortenedUrls",
                newName: "ShortCode");

            migrationBuilder.RenameIndex(
                name: "IX_ShortenedUrls_Code",
                table: "ShortenedUrls",
                newName: "IX_ShortenedUrls_ShortCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShortCode",
                table: "ShortenedUrls",
                newName: "Code");

            migrationBuilder.RenameIndex(
                name: "IX_ShortenedUrls_ShortCode",
                table: "ShortenedUrls",
                newName: "IX_ShortenedUrls_Code");

            migrationBuilder.AddColumn<string>(
                name: "ShortUrl",
                table: "ShortenedUrls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
