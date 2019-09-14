using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicPlayer.Data.Migrations
{
    public partial class AddedSeedValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SongInfos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Duration",
                value: new TimeSpan(0, 0, 4, 8, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SongInfos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Duration",
                value: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
