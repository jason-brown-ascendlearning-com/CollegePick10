using Microsoft.EntityFrameworkCore.Migrations;

namespace CollegePick10V2.Migrations
{
    public partial class testing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "cdf3d2dc-a975-4c8c-a1b0-51867061aa36", "ac725319-449e-491e-b149-d80d8923cea5", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "41b7d113-38ec-4ae7-bf27-715f9304f6bb", "0bdfdc43-703d-4c75-864d-aaf71669bbfa", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41b7d113-38ec-4ae7-bf27-715f9304f6bb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cdf3d2dc-a975-4c8c-a1b0-51867061aa36");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a57957f0-61c4-4124-84b7-8602a34aebde", "64ff3ee0-7f25-49b9-a4e4-26c670d6164a", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4686db7b-9fd0-4b79-99ef-215baa2c23be", "df64248b-155c-4aac-8ed6-dea8ccb9cbeb", "Administrator", "ADMINISTRATOR" });
        }
    }
}
