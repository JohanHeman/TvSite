using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TvSite.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_mediaid_property_name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MediaId",
                table: "Comments",
                newName: "EpisodeMediaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EpisodeMediaId",
                table: "Comments",
                newName: "MediaId");
        }
    }
}
