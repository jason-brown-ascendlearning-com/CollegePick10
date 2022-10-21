using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CollegePick10V2.Migrations
{
    public partial class AddGameArchive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameArchives",
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
                    FavoriteScore = table.Column<int>(type: "int", nullable: false),
                    UnderdogScore = table.Column<int>(type: "int", nullable: false),
                    WeekNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameArchives", x => x.GameID);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameArchives");

            migrationBuilder.UpdateData(
                table: "CurrentWeeks",
                keyColumn: "GameID",
                keyValue: 1,
                column: "GameTime",
                value: new DateTime(2022, 7, 14, 11, 15, 38, 282, DateTimeKind.Local).AddTicks(103));

            migrationBuilder.UpdateData(
                table: "CurrentWeeks",
                keyColumn: "GameID",
                keyValue: 2,
                column: "GameTime",
                value: new DateTime(2022, 7, 14, 11, 15, 38, 290, DateTimeKind.Local).AddTicks(370));
        }
    }
}
