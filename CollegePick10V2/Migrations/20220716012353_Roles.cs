using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CollegePick10V2.Migrations
{
    public partial class Roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CurrentWeeks",
                keyColumn: "GameID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CurrentWeeks",
                keyColumn: "GameID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "TeamID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "TeamID",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "488cd598-56ca-4cd4-a124-54274237cfca", "4c85f37c-213e-4fb6-8ff3-8b175c362fcd", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f3e98dad-5734-4e1a-b16c-4542ae190342", "6de24c35-019c-4df7-862c-5a0293229157", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "488cd598-56ca-4cd4-a124-54274237cfca");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f3e98dad-5734-4e1a-b16c-4542ae190342");

            migrationBuilder.InsertData(
                table: "CurrentWeeks",
                columns: new[] { "GameID", "FavoriteID", "FavoriteScore", "GameProgress", "GameTime", "GameUrl", "HomeTeam", "Locked", "OverUnder", "PointSpread", "UnderDogID", "UnderdogScore" },
                values: new object[,]
                {
                    { 1, 1, null, null, new DateTime(2022, 7, 15, 20, 17, 0, 397, DateTimeKind.Local).AddTicks(2524), null, 1, false, 45.5m, 21.5m, 2, null },
                    { 2, 2, null, null, new DateTime(2022, 7, 15, 20, 17, 0, 403, DateTimeKind.Local).AddTicks(5764), null, 2, false, 52m, 23m, 1, null }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "TeamID", "Abbreviation", "EspnName", "FulleName", "Rank" },
                values: new object[,]
                {
                    { 1, "ABCHRST", "Abilene Christian", "Abilene Christian Wildcats", null },
                    { 2, "AF", "Air Force", "Air Force Falcons", null }
                });
        }
    }
}
