using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialMediaPlatform.Server.Migrations
{
    /// <inheritdoc />
    public partial class messages92 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0112a53c-be02-4218-ae02-d6b061d66a93");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "84327d26-91cc-4fcf-85cc-dd0e454ee4c4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5de2b815-64f1-474c-960c-a2e1e06ea482", null, "Admin", "ADMIN" },
                    { "94f1e337-3803-4176-8509-8da77fa00360", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5de2b815-64f1-474c-960c-a2e1e06ea482");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94f1e337-3803-4176-8509-8da77fa00360");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0112a53c-be02-4218-ae02-d6b061d66a93", null, "Admin", "ADMIN" },
                    { "84327d26-91cc-4fcf-85cc-dd0e454ee4c4", null, "User", "USER" }
                });
        }
    }
}
