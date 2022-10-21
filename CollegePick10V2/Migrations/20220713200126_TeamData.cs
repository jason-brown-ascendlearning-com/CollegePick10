using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CollegePick10V2.Migrations
{
    public partial class TeamData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a10fe263-3be7-44a0-b454-081bbe9a2bc7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8f0a571-99ad-498a-80ef-bda093f3c8df");

            migrationBuilder.CreateTable(
                name: "CurrentWeeks",
                columns: table => new
                {
                    GameID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FavoriteID = table.Column<int>(type: "int", nullable: false),
                    UnderDogID = table.Column<int>(type: "int", nullable: false),
                    HomeTeam = table.Column<int>(type: "int", nullable: false),
                    PointSpread = table.Column<decimal>(type: "decimal(3,1)", nullable: false),
                    OverUnder = table.Column<decimal>(type: "decimal(3,1)", nullable: false),
                    GameTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    GameProgress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentWeeks", x => x.GameID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FulleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EspnName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rank = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamID);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d2f35cc1-308c-4aa1-b25f-b8a212eb619f", "a4caacc3-d2b7-4fba-bb5b-6d2b5a375347", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f75ee7c6-faa0-4de6-b05b-d5235c4cb9a9", "1b87ad10-b501-4330-af93-4de542ea228c", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrentWeeks");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d2f35cc1-308c-4aa1-b25f-b8a212eb619f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f75ee7c6-faa0-4de6-b05b-d5235c4cb9a9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a10fe263-3be7-44a0-b454-081bbe9a2bc7", "7c7d51d9-a465-47d6-abef-cb760a71a6c5", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f8f0a571-99ad-498a-80ef-bda093f3c8df", "36d86099-5ed4-465a-893a-2943801b6615", "Administrator", "ADMINISTRATOR" });
        }
    }
}
