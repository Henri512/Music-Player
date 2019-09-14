using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicPlayer.Data.Migrations
{
    public partial class AddedSongInfoFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BitRate",
                table: "SongInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "SongInfos",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "SongInfos",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "SongInfos",
                maxLength: 30,
                nullable: true);

            migrationBuilder.InsertData(
                table: "SongInfos",
                columns: new[] { "Id", "Album", "Author", "BitRate", "Duration", "Extension", "Genre", "Name", "PhysicalPath", "TimesPlayed", "Year" },
                values: new object[,]
                {
                    { 1, "This Side Of The Big River", "Chip Taylor", 798, new TimeSpan(0, 0, 3, 17, 0), "FLAC", "Country", "01 - Same Ol' Story ", "D:\\Muzika\\Chip Taylor...This Side Of The Big River(1975)[FLAC]", 0, new DateTime(1975, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "This Side Of The Big River", "Chip Taylor", 694, new TimeSpan(0, 0, 2, 47, 0), "FLAC", "Country", "02 - Holding Me Together ", "D:\\Muzika\\Chip Taylor...This Side Of The Big River(1975)[FLAC]", 0, new DateTime(1975, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "This Side Of The Big River", "Chip Taylor", 845, new TimeSpan(0, 0, 3, 24, 0), "FLAC", "Country", "03 - Gettin' Older, Lookin' Back ", "D:\\Muzika\\Chip Taylor...This Side Of The Big River(1975)[FLAC]", 0, new DateTime(1975, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "This Side Of The Big River", "Chip Taylor", 751, new TimeSpan(0, 0, 5, 30, 0), "FLAC", "Country", "04 - John Tucker's On The Wagon Again ", "D:\\Muzika\\Chip Taylor...This Side Of The Big River(1975)[FLAC]", 0, new DateTime(1975, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "This Side Of The Big River", "Chip Taylor", 901, new TimeSpan(0, 0, 3, 17, 0), "FLAC", "Country", "05 - Big River ", "D:\\Muzika\\Chip Taylor...This Side Of The Big River(1975)[FLAC]", 0, new DateTime(1975, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "This Side Of The Big River", "Chip Taylor", 725, new TimeSpan(0, 0, 4, 8, 0), "FLAC", "Country", "06 - May God Be With Me ", "D:\\Muzika\\Chip Taylor...This Side Of The Big River(1975)[FLAC]", 0, new DateTime(1975, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "BitRate",
                table: "SongInfos");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "SongInfos");

            migrationBuilder.DropColumn(
                name: "Extension",
                table: "SongInfos");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "SongInfos");
        }
    }
}
