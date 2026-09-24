using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TvSite.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class new_models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMedialists");

            migrationBuilder.CreateTable(
                name: "MediaEntity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Directors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Actors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirDate = table.Column<DateOnly>(type: "date", nullable: false),
                    MediaImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MediaEntry",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediaEntry");

            migrationBuilder.DropTable(
                name: "MediaEntity");

            migrationBuilder.CreateTable(
                name: "UserMedialists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ListState = table.Column<int>(type: "int", nullable: false),
                    Medias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMedialists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMedialists_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserMedialists_ApplicationUserId",
                table: "UserMedialists",
                column: "ApplicationUserId");
        }
    }
}
