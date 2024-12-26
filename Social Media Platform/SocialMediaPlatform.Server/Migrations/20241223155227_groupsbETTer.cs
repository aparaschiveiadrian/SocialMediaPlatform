using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialMediaPlatform.Server.Migrations
{
    /// <inheritdoc />
    public partial class groupsbETTer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f0e479a-4864-48b0-af38-fdffae21c2ee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a69bf555-b64e-4f75-9526-de0358573e64");

            migrationBuilder.AddColumn<string>(
                name: "ModeratorId",
                table: "Groups",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "44272882-4498-4cbd-b580-fe94d49b007b", null, "User", "USER" },
                    { "b4dc92ce-1609-4882-be05-221e23f20ee9", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44272882-4498-4cbd-b580-fe94d49b007b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4dc92ce-1609-4882-be05-221e23f20ee9");

            migrationBuilder.DropColumn(
                name: "ModeratorId",
                table: "Groups");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4f0e479a-4864-48b0-af38-fdffae21c2ee", null, "User", "USER" },
                    { "a69bf555-b64e-4f75-9526-de0358573e64", null, "Admin", "ADMIN" }
                });
        }
    }
}
