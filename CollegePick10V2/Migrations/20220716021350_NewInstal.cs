using Microsoft.EntityFrameworkCore.Migrations;

namespace CollegePick10V2.Migrations
{
    public partial class NewInstal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a57957f0-61c4-4124-84b7-8602a34aebde", "64ff3ee0-7f25-49b9-a4e4-26c670d6164a", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4686db7b-9fd0-4b79-99ef-215baa2c23be", "df64248b-155c-4aac-8ed6-dea8ccb9cbeb", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4686db7b-9fd0-4b79-99ef-215baa2c23be");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a57957f0-61c4-4124-84b7-8602a34aebde");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "488cd598-56ca-4cd4-a124-54274237cfca", "4c85f37c-213e-4fb6-8ff3-8b175c362fcd", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f3e98dad-5734-4e1a-b16c-4542ae190342", "6de24c35-019c-4df7-862c-5a0293229157", "Administrator", "ADMINISTRATOR" });
        }
    }
}
