using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialMediaPlatform.Server.Migrations
{
    /// <inheritdoc />
    public partial class RemovedEditorRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9fcfa14c-e964-4e7d-b2fd-02a86bb2c77e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4b79d16-5151-4d0b-97cf-03cf9d3d4bc0");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b381a0b2-76a2-4753-bad5-ea60625b45d8", null, "User", "USER" },
                    { "da7e6c22-c68c-4711-a833-ff988c92986e", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b381a0b2-76a2-4753-bad5-ea60625b45d8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da7e6c22-c68c-4711-a833-ff988c92986e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9fcfa14c-e964-4e7d-b2fd-02a86bb2c77e", null, "User", "USER" },
                    { "b4b79d16-5151-4d0b-97cf-03cf9d3d4bc0", null, "Admin", "ADMIN" }
                });
        }
    }
}
