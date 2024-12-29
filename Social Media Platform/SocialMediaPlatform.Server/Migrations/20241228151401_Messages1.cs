using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialMediaPlatform.Server.Migrations
{
    /// <inheritdoc />
    public partial class Messages1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d7801fdc-b41b-412c-93f0-c68f3da7c699");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fe7c32a0-cf3e-442d-937e-372651b9da80");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "044bec44-e213-4758-8e49-c4c40af39c6a", null, "Admin", "ADMIN" },
                    { "c43a2fe2-bb97-4816-91d5-ef272112ee88", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "044bec44-e213-4758-8e49-c4c40af39c6a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c43a2fe2-bb97-4816-91d5-ef272112ee88");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d7801fdc-b41b-412c-93f0-c68f3da7c699", null, "User", "USER" },
                    { "fe7c32a0-cf3e-442d-937e-372651b9da80", null, "Admin", "ADMIN" }
                });
        }
    }
}
