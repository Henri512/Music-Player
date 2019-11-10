using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicPlayer.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 300, nullable: false),
                    Year = table.Column<DateTime>(nullable: false),
                    Author = table.Column<string>(maxLength: 200, nullable: true),
                    ImagePath = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SongInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    AlbumId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 300, nullable: true),
                    TrackNo = table.Column<int>(nullable: false),
                    Author = table.Column<string>(maxLength: 300, nullable: true),
                    Duration = table.Column<TimeSpan>(nullable: false),
                    Genre = table.Column<string>(maxLength: 100, nullable: true),
                    BitRate = table.Column<string>(maxLength: 20, nullable: true),
                    TimesPlayed = table.Column<int>(nullable: false),
                    RelativePath = table.Column<string>(maxLength: 300, nullable: true),
                    Extension = table.Column<string>(maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SongInfos_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SongInfos_AlbumId",
                table: "SongInfos",
                column: "AlbumId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SongInfos");

            migrationBuilder.DropTable(
                name: "Albums");
        }
    }
}
