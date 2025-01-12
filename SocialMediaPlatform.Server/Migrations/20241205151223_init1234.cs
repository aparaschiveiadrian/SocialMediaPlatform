using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialMediaPlatform.Server.Migrations
{
    /// <inheritdoc />
    public partial class init1234 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18560f1a-6dae-4bb5-bbb3-1ddeb5fbfa9f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e29a2a6d-ec55-44e2-94a2-68529be2994d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "318d2a07-c755-4d0c-9053-3652baa851b5", null, "User", "USER" },
                    { "aefff31e-2b5a-44a8-8e64-5f6f7043f5cb", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "318d2a07-c755-4d0c-9053-3652baa851b5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aefff31e-2b5a-44a8-8e64-5f6f7043f5cb");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "18560f1a-6dae-4bb5-bbb3-1ddeb5fbfa9f", null, "Admin", "ADMIN" },
                    { "e29a2a6d-ec55-44e2-94a2-68529be2994d", null, "User", "USER" }
                });
        }
    }
}
