using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicPlayer.Data.Migrations
{
    public partial class AddedAlbumEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Album",
                table: "SongInfos");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "SongInfos");

            migrationBuilder.AddColumn<int>(
                name: "AlbumId",
                table: "SongInfos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Year = table.Column<DateTime>(nullable: false),
                    ImagePath = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "Id", "ImagePath", "Name", "Year" },
                values: new object[] { 1, null, "This Side Of The Big River", new DateTime(1975, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "SongInfos",
                keyColumn: "Id",
                keyValue: 1,
                column: "AlbumId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SongInfos",
                keyColumn: "Id",
                keyValue: 2,
                column: "AlbumId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SongInfos",
                keyColumn: "Id",
                keyValue: 3,
                column: "AlbumId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SongInfos",
                keyColumn: "Id",
                keyValue: 4,
                column: "AlbumId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SongInfos",
                keyColumn: "Id",
                keyValue: 5,
                column: "AlbumId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SongInfos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AlbumId", "Duration" },
                values: new object[] { 1, new TimeSpan(0, 0, 0, 0, 0) });

            migrationBuilder.CreateIndex(
                name: "IX_SongInfos_AlbumId",
                table: "SongInfos",
                column: "AlbumId");

            migrationBuilder.AddForeignKey(
                name: "FK_SongInfos_Albums_AlbumId",
                table: "SongInfos",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SongInfos_Albums_AlbumId",
                table: "SongInfos");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_SongInfos_AlbumId",
                table: "SongInfos");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "SongInfos");

            migrationBuilder.AddColumn<string>(
                name: "Album",
                table: "SongInfos",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Year",
                table: "SongInfos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SongInfos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Album", "Year" },
                values: new object[] { "This Side Of The Big River", new DateTime(1975, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "SongInfos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Album", "Year" },
                values: new object[] { "This Side Of The Big River", new DateTime(1975, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "SongInfos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Album", "Year" },
                values: new object[] { "This Side Of The Big River", new DateTime(1975, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "SongInfos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Album", "Year" },
                values: new object[] { "This Side Of The Big River", new DateTime(1975, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "SongInfos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Album", "Year" },
                values: new object[] { "This Side Of The Big River", new DateTime(1975, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "SongInfos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Album", "Duration", "Year" },
                values: new object[] { "This Side Of The Big River", new TimeSpan(0, 0, 4, 8, 0), new DateTime(1975, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
