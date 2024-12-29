using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialMediaPlatform.Server.Migrations
{
    /// <inheritdoc />
    public partial class Messages2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "89a26265-6f25-41b3-9dfb-140f4b1e974f", null, "Admin", "ADMIN" },
                    { "bc7a5f9c-f68e-488a-a717-22ee820253c2", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89a26265-6f25-41b3-9dfb-140f4b1e974f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc7a5f9c-f68e-488a-a717-22ee820253c2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "044bec44-e213-4758-8e49-c4c40af39c6a", null, "Admin", "ADMIN" },
                    { "c43a2fe2-bb97-4816-91d5-ef272112ee88", null, "User", "USER" }
                });
        }
    }
}
