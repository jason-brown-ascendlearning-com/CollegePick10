using Microsoft.EntityFrameworkCore.Migrations;

namespace CollegePick10V2.Migrations
{
    public partial class AddScores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9cebc473-65f1-434f-9c5e-14a500ca223f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d59bb365-8ea5-4a53-bce9-c76f52c65d7c");

            migrationBuilder.AddColumn<int>(
                name: "FavoriteScore",
                table: "CurrentWeeks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnderdogScore",
                table: "CurrentWeeks",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "80676087-0e61-4561-b4d2-33504da0d1bd", "88eb2fa1-af9d-43fc-ab12-1a748233a090", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "40c4f7ba-424c-45ba-bfaf-6e189885b258", "4fb092d0-4960-4b47-a975-78c8d4a88d7c", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40c4f7ba-424c-45ba-bfaf-6e189885b258");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "80676087-0e61-4561-b4d2-33504da0d1bd");

            migrationBuilder.DropColumn(
                name: "FavoriteScore",
                table: "CurrentWeeks");

            migrationBuilder.DropColumn(
                name: "UnderdogScore",
                table: "CurrentWeeks");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d59bb365-8ea5-4a53-bce9-c76f52c65d7c", "1b7b527c-3360-4146-b2da-444e5fb69500", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9cebc473-65f1-434f-9c5e-14a500ca223f", "2f9bf283-5d58-44ef-8ef4-4d0bb9f4432a", "Administrator", "ADMINISTRATOR" });
        }
    }
}
