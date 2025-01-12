using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialMediaPlatform.Server.Migrations
{
    /// <inheritdoc />
    public partial class init123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8a6447b-e6a0-4c7f-8ea1-18d54c06e6c5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6f61bf7-dfa5-469e-9239-50e54288dcf2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "18560f1a-6dae-4bb5-bbb3-1ddeb5fbfa9f", null, "Admin", "ADMIN" },
                    { "e29a2a6d-ec55-44e2-94a2-68529be2994d", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "b8a6447b-e6a0-4c7f-8ea1-18d54c06e6c5", null, "Admin", "ADMIN" },
                    { "f6f61bf7-dfa5-469e-9239-50e54288dcf2", null, "User", "USER" }
                });
        }
    }
}
