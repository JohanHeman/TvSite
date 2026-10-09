using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TvSite.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class messegehere : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EpisodeNumber",
                table: "WatchedEpisodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SeasonNumber",
                table: "WatchedEpisodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TvSeriesId",
                table: "WatchedEpisodes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EpisodeNumber",
                table: "WatchedEpisodes");

            migrationBuilder.DropColumn(
                name: "SeasonNumber",
                table: "WatchedEpisodes");

            migrationBuilder.DropColumn(
                name: "TvSeriesId",
                table: "WatchedEpisodes");
        }
    }
}
