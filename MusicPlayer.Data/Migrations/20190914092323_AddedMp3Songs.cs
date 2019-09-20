using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicPlayer.Data.Migrations
{
    public partial class AddedMp3Songs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImagePath",
                value: ".../assets/album-logo-1.jpg");

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "Id", "Created", "ImagePath", "LastModified", "Name", "Year" },
                values: new object[] { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), ".../assets/album-logo-2.jpeg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Essentials II", new DateTime(2014, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "SongInfos",
                columns: new[] { "Id", "AlbumId", "Author", "BitRate", "Created", "Duration", "Extension", "Genre", "LastModified", "Name", "RelativePath", "TimesPlayed" },
                values: new object[] { 7, 2, "Karen Souza", 320, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 3, 27, 0), "mp3", "Vocal Jazz", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "01 - The Way It Is", "D:\\Muzika\\[Covers-Vocal Jazz] Karen Souza - Essentials II 2014 (Jamal The Moroccan)", 0 });

            migrationBuilder.InsertData(
                table: "SongInfos",
                columns: new[] { "Id", "AlbumId", "Author", "BitRate", "Created", "Duration", "Extension", "Genre", "LastModified", "Name", "RelativePath", "TimesPlayed" },
                values: new object[] { 8, 2, "Karen Souza", 320, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 4, 6, 0), "mp3", "Vocal Jazz", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "02 - Wicked Game", "D:\\Muzika\\[Covers-Vocal Jazz] Karen Souza - Essentials II 2014 (Jamal The Moroccan)", 0 });

            migrationBuilder.InsertData(
                table: "SongInfos",
                columns: new[] { "Id", "AlbumId", "Author", "BitRate", "Created", "Duration", "Extension", "Genre", "LastModified", "Name", "RelativePath", "TimesPlayed" },
                values: new object[] { 9, 2, "Karen Souza", 320, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 3, 42, 0), "mp3", "Vocal Jazz", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "03 - Everyday Is Like Sunday", "D:\\Muzika\\[Covers-Vocal Jazz] Karen Souza - Essentials II 2014 (Jamal The Moroccan)", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImagePath",
                value: null);
        }
    }
}
