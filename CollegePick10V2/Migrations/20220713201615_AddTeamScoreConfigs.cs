using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CollegePick10V2.Migrations
{
    public partial class AddTeamScoreConfigs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40c4f7ba-424c-45ba-bfaf-6e189885b258");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "80676087-0e61-4561-b4d2-33504da0d1bd");

            migrationBuilder.InsertData(
                table: "CurrentWeeks",
                columns: new[] { "GameID", "FavoriteID", "FavoriteScore", "GameProgress", "GameTime", "GameUrl", "HomeTeam", "Locked", "OverUnder", "PointSpread", "UnderDogID", "UnderdogScore" },
                values: new object[,]
                {
                    { 1, 1, null, null, new DateTime(2022, 7, 13, 15, 16, 15, 123, DateTimeKind.Local).AddTicks(8456), null, 1, false, 45.5m, 21.5m, 2, null },
                    { 2, 2, null, null, new DateTime(2022, 7, 13, 15, 16, 15, 129, DateTimeKind.Local).AddTicks(8557), null, 2, false, 52m, 23m, 1, null }
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "80676087-0e61-4561-b4d2-33504da0d1bd", "88eb2fa1-af9d-43fc-ab12-1a748233a090", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "40c4f7ba-424c-45ba-bfaf-6e189885b258", "4fb092d0-4960-4b47-a975-78c8d4a88d7c", "Administrator", "ADMINISTRATOR" });
        }
    }
}
