using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TvSite.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updated_name_from_MediaId_To_EpisodeMediaId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSoftDeleted",
                table: "WatchedEpisodes");

            migrationBuilder.RenameColumn(
                name: "EpisodeId",
                table: "WatchedEpisodes",
                newName: "EpisodeMediaId");

            migrationBuilder.RenameColumn(
                name: "MediaId",
                table: "Ratings",
                newName: "EpisodeMediaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EpisodeMediaId",
                table: "WatchedEpisodes",
                newName: "EpisodeId");

            migrationBuilder.RenameColumn(
                name: "EpisodeMediaId",
                table: "Ratings",
                newName: "MediaId");

            migrationBuilder.AddColumn<bool>(
                name: "IsSoftDeleted",
                table: "WatchedEpisodes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
