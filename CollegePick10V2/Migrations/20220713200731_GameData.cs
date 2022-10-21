using Microsoft.EntityFrameworkCore.Migrations;

namespace CollegePick10V2.Migrations
{
    public partial class GameData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { "d59bb365-8ea5-4a53-bce9-c76f52c65d7c", "1b7b527c-3360-4146-b2da-444e5fb69500", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9cebc473-65f1-434f-9c5e-14a500ca223f", "2f9bf283-5d58-44ef-8ef4-4d0bb9f4432a", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9cebc473-65f1-434f-9c5e-14a500ca223f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d59bb365-8ea5-4a53-bce9-c76f52c65d7c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d2f35cc1-308c-4aa1-b25f-b8a212eb619f", "a4caacc3-d2b7-4fba-bb5b-6d2b5a375347", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f75ee7c6-faa0-4de6-b05b-d5235c4cb9a9", "1b87ad10-b501-4330-af93-4de542ea228c", "Administrator", "ADMINISTRATOR" });
        }
    }
}
