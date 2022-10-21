using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CollegePick10V2.Migrations
{
    public partial class RemoveKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "WeekNumber",
                table: "GameArchives",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PickID",
                table: "PickArchives",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "WeekNumber",
                table: "GameArchives",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GameID",
                table: "GameArchives",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

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
                value: new DateTime(2022, 7, 14, 17, 28, 24, 836, DateTimeKind.Local).AddTicks(848));

            migrationBuilder.UpdateData(
                table: "CurrentWeeks",
                keyColumn: "GameID",
                keyValue: 2,
                column: "GameTime",
                value: new DateTime(2022, 7, 14, 17, 28, 24, 841, DateTimeKind.Local).AddTicks(7315));
        }
    }
}
