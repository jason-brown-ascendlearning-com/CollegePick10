using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CollegePick10V2.Migrations
{
    public partial class Timezone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TimeZone",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CurrentWeeks",
                keyColumn: "GameID",
                keyValue: 1,
                column: "GameTime",
                value: new DateTime(2022, 7, 15, 20, 17, 0, 397, DateTimeKind.Local).AddTicks(2524));

            migrationBuilder.UpdateData(
                table: "CurrentWeeks",
                keyColumn: "GameID",
                keyValue: 2,
                column: "GameTime",
                value: new DateTime(2022, 7, 15, 20, 17, 0, 403, DateTimeKind.Local).AddTicks(5764));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeZone",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "CurrentWeeks",
                keyColumn: "GameID",
                keyValue: 1,
                column: "GameTime",
                value: new DateTime(2022, 7, 14, 17, 51, 12, 803, DateTimeKind.Local).AddTicks(7138));

            migrationBuilder.UpdateData(
                table: "CurrentWeeks",
                keyColumn: "GameID",
                keyValue: 2,
                column: "GameTime",
                value: new DateTime(2022, 7, 14, 17, 51, 12, 809, DateTimeKind.Local).AddTicks(4107));
        }
    }
}
