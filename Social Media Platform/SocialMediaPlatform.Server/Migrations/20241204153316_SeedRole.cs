using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialMediaPlatform.Server.Migrations
{
    /// <inheritdoc />
    public partial class SeedRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1335da30-425d-4911-b114-acb6075b6629", null, "User", "USER" },
                    { "56d7e5f5-9cab-4a2f-ac37-7f0809a3b348", null, "Admin", "ADMIN" },
                    { "cf00cc32-7cfd-4e2b-bd1b-c1d4c8e660bc", null, "Editor", "EDITOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1335da30-425d-4911-b114-acb6075b6629");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56d7e5f5-9cab-4a2f-ac37-7f0809a3b348");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cf00cc32-7cfd-4e2b-bd1b-c1d4c8e660bc");
        }
    }
}
