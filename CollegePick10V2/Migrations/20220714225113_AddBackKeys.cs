using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CollegePick10V2.Migrations
{
    public partial class AddBackKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PickID",
                table: "PickArchives",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GameID",
                table: "GameArchives",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PickArchives",
                table: "PickArchives",
                column: "PickID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameArchives",
                table: "GameArchives",
                column: "GameID");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PickArchives",
                table: "PickArchives");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameArchives",
                table: "GameArchives");

            migrationBuilder.DropColumn(
                name: "PickID",
                table: "PickArchives");

            migrationBuilder.DropColumn(
                name: "GameID",
                table: "GameArchives");

            migrationBuilder.UpdateData(
                table: "CurrentWeeks",
                keyColumn: "GameID",
                keyValue: 1,
                column: "GameTime",
                value: new DateTime(2022, 7, 14, 17, 49, 31, 314, DateTimeKind.Local).AddTicks(3691));

            migrationBuilder.UpdateData(
                table: "CurrentWeeks",
                keyColumn: "GameID",
                keyValue: 2,
                column: "GameTime",
                value: new DateTime(2022, 7, 14, 17, 49, 31, 320, DateTimeKind.Local).AddTicks(6088));
        }
    }
}
