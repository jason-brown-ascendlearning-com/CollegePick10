using Microsoft.EntityFrameworkCore.Migrations;

namespace CollegePick10V2.Migrations
{
    public partial class InsertRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a10fe263-3be7-44a0-b454-081bbe9a2bc7", "7c7d51d9-a465-47d6-abef-cb760a71a6c5", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f8f0a571-99ad-498a-80ef-bda093f3c8df", "36d86099-5ed4-465a-893a-2943801b6615", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a10fe263-3be7-44a0-b454-081bbe9a2bc7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8f0a571-99ad-498a-80ef-bda093f3c8df");
        }
    }
}
