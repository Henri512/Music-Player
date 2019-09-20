using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicPlayer.Data.Migrations
{
    public partial class AddedSongInfoProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SongInfos_Albums_AlbumId",
                table: "SongInfos");

            migrationBuilder.DeleteData(
                table: "SongInfos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SongInfos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SongInfos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SongInfos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SongInfos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SongInfos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SongInfos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SongInfos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SongInfos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "BitRate",
                table: "SongInfos",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "TrackNo",
                table: "SongInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_SongInfos_Albums_AlbumId",
                table: "SongInfos",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SongInfos_Albums_AlbumId",
                table: "SongInfos");

            migrationBuilder.DropColumn(
                name: "TrackNo",
                table: "SongInfos");

            migrationBuilder.AlterColumn<int>(
                name: "BitRate",
                table: "SongInfos",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "Id", "Created", "ImagePath", "LastModified", "Name", "Year" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "../assets/album-logo-1.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This Side Of The Big River", new DateTime(1975, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "Id", "Created", "ImagePath", "LastModified", "Name", "Year" },
                values: new object[] { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "../assets/album-logo-2.jpeg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Essentials II", new DateTime(2014, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "SongInfos",
                columns: new[] { "Id", "AlbumId", "Author", "BitRate", "Created", "Duration", "Extension", "Genre", "LastModified", "Name", "RelativePath", "TimesPlayed" },
                values: new object[,]
                {
                    { 1, 1, "Chip Taylor", 798, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 3, 17, 0), "FLAC", "Country", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "01 - Same Ol' Story ", null, 0 },
                    { 2, 1, "Chip Taylor", 694, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 2, 47, 0), "FLAC", "Country", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "02 - Holding Me Together ", null, 0 },
                    { 3, 1, "Chip Taylor", 845, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 3, 24, 0), "FLAC", "Country", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "03 - Gettin' Older, Lookin' Back ", null, 0 },
                    { 4, 1, "Chip Taylor", 751, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 5, 30, 0), "FLAC", "Country", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "04 - John Tucker's On The Wagon Again ", null, 0 },
                    { 5, 1, "Chip Taylor", 901, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 3, 17, 0), "FLAC", "Country", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "05 - Big River ", null, 0 },
                    { 6, 1, "Chip Taylor", 725, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 4, 8, 0), "FLAC", "Country", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "06 - May God Be With Me ", null, 0 },
                    { 7, 2, "Karen Souza", 320, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 3, 27, 0), "mp3", "Vocal Jazz", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "01 - The Way It Is", null, 0 },
                    { 8, 2, "Karen Souza", 320, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 4, 6, 0), "mp3", "Vocal Jazz", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "02 - Wicked Game", null, 0 },
                    { 9, 2, "Karen Souza", 320, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 3, 42, 0), "mp3", "Vocal Jazz", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "03 - Everyday Is Like Sunday", null, 0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_SongInfos_Albums_AlbumId",
                table: "SongInfos",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
