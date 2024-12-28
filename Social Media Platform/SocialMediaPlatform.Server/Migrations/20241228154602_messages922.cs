using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialMediaPlatform.Server.Migrations
{
    /// <inheritdoc />
    public partial class messages922 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "656013e0-d285-42a6-9065-675759384178", null, "Admin", "ADMIN" },
                    { "7e25df31-c429-466b-a2b1-3250a9d2dce7", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "656013e0-d285-42a6-9065-675759384178");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e25df31-c429-466b-a2b1-3250a9d2dce7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5de2b815-64f1-474c-960c-a2e1e06ea482", null, "Admin", "ADMIN" },
                    { "94f1e337-3803-4176-8509-8da77fa00360", null, "User", "USER" }
                });
        }
    }
}
