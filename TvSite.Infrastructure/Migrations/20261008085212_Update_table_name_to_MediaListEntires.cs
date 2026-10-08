using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TvSite.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_table_name_to_MediaListEntires : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaListEntry_AspNetUsers_ApplicationUserId",
                table: "MediaListEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MediaListEntry",
                table: "MediaListEntry");

            migrationBuilder.RenameTable(
                name: "MediaListEntry",
                newName: "MediaListEntries");

            migrationBuilder.RenameIndex(
                name: "IX_MediaListEntry_ApplicationUserId",
                table: "MediaListEntries",
                newName: "IX_MediaListEntries_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MediaListEntries",
                table: "MediaListEntries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaListEntries_AspNetUsers_ApplicationUserId",
                table: "MediaListEntries",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaListEntries_AspNetUsers_ApplicationUserId",
                table: "MediaListEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MediaListEntries",
                table: "MediaListEntries");

            migrationBuilder.RenameTable(
                name: "MediaListEntries",
                newName: "MediaListEntry");

            migrationBuilder.RenameIndex(
                name: "IX_MediaListEntries_ApplicationUserId",
                table: "MediaListEntry",
                newName: "IX_MediaListEntry_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MediaListEntry",
                table: "MediaListEntry",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaListEntry_AspNetUsers_ApplicationUserId",
                table: "MediaListEntry",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
