using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CollegePick10V2.Migrations
{
    public partial class standingAndPayout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PickArchives",
                columns: table => new
                {
                    PickID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameID = table.Column<int>(type: "int", nullable: false),
                    PickTypeID = table.Column<int>(type: "int", nullable: false),
                    CorrectPoints = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickArchives", x => x.PickID);
                });

            migrationBuilder.CreateTable(
                name: "Picks",
                columns: table => new
                {
                    PickID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameID = table.Column<int>(type: "int", nullable: false),
                    PickTypeID = table.Column<int>(type: "int", nullable: false),
                    CorrectPoints = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picks", x => x.PickID);
                });

            migrationBuilder.CreateTable(
                name: "PickTypes",
                columns: table => new
                {
                    PickTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PickTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickTypes", x => x.PickTypeID);
                });

            migrationBuilder.UpdateData(
                table: "CurrentWeeks",
                keyColumn: "GameID",
                keyValue: 1,
                column: "GameTime",
                value: new DateTime(2022, 7, 14, 10, 3, 16, 924, DateTimeKind.Local).AddTicks(914));

            migrationBuilder.UpdateData(
                table: "CurrentWeeks",
                keyColumn: "GameID",
                keyValue: 2,
                column: "GameTime",
                value: new DateTime(2022, 7, 14, 10, 3, 16, 929, DateTimeKind.Local).AddTicks(4931));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PickArchives");

            migrationBuilder.DropTable(
                name: "Picks");

            migrationBuilder.DropTable(
                name: "PickTypes");

            migrationBuilder.UpdateData(
                table: "CurrentWeeks",
                keyColumn: "GameID",
                keyValue: 1,
                column: "GameTime",
                value: new DateTime(2022, 7, 13, 17, 43, 23, 188, DateTimeKind.Local).AddTicks(6647));

            migrationBuilder.UpdateData(
                table: "CurrentWeeks",
                keyColumn: "GameID",
                keyValue: 2,
                column: "GameTime",
                value: new DateTime(2022, 7, 13, 17, 43, 23, 193, DateTimeKind.Local).AddTicks(9913));
        }
    }
}
