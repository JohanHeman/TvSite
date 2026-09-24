using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TvSite.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateentities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediaEntry");

            migrationBuilder.AddColumn<string>(
                name: "MediaEntityId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MediaListEntry",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MediaId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ListState = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaListEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaListEntry_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MediaListEntry_MediaEntity_MediaId",
                        column: x => x.MediaId,
                        principalTable: "MediaEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_MediaEntityId",
                table: "Comments",
                column: "MediaEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaListEntry_ApplicationUserId",
                table: "MediaListEntry",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaListEntry_MediaId",
                table: "MediaListEntry",
                column: "MediaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_MediaEntity_MediaEntityId",
                table: "Comments",
                column: "MediaEntityId",
                principalTable: "MediaEntity",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_MediaEntity_MediaEntityId",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "MediaListEntry");

            migrationBuilder.DropIndex(
                name: "IX_Comments_MediaEntityId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "MediaEntityId",
                table: "Comments");

            migrationBuilder.CreateTable(
                name: "MediaEntry",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MediaId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ListState = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaEntry_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MediaEntry_MediaEntity_MediaId",
                        column: x => x.MediaId,
                        principalTable: "MediaEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MediaEntry_ApplicationUserId",
                table: "MediaEntry",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaEntry_MediaId",
                table: "MediaEntry",
                column: "MediaId");
        }
    }
}
