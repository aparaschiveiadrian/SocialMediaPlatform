using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialMediaPlatform.Server.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b8f1895-396e-43d7-8006-4255d09c96f9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5544ee02-f4c0-4c49-a4c1-13a7d4526265");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "82099edc-ebfb-4a3c-883e-83994a3c56a5", null, "Admin", "ADMIN" },
                    { "e5694d55-1d1e-4757-9af1-f6090584be33", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82099edc-ebfb-4a3c-883e-83994a3c56a5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e5694d55-1d1e-4757-9af1-f6090584be33");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3b8f1895-396e-43d7-8006-4255d09c96f9", null, "User", "USER" },
                    { "5544ee02-f4c0-4c49-a4c1-13a7d4526265", null, "Admin", "ADMIN" }
                });
        }
    }
}
