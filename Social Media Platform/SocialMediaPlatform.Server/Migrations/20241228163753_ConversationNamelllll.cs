using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialMediaPlatform.Server.Migrations
{
    /// <inheritdoc />
    public partial class ConversationNamelllll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6245ec92-f30d-44d7-bcd8-d425b281ba0c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "84433a47-07ca-4cda-a913-730c075b94b1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "463b6788-7641-4f77-b8fb-1c70ca8905b2", null, "Admin", "ADMIN" },
                    { "ae6c45e2-f66f-4389-b90d-37e18b38994b", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "463b6788-7641-4f77-b8fb-1c70ca8905b2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae6c45e2-f66f-4389-b90d-37e18b38994b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6245ec92-f30d-44d7-bcd8-d425b281ba0c", null, "User", "USER" },
                    { "84433a47-07ca-4cda-a913-730c075b94b1", null, "Admin", "ADMIN" }
                });
        }
    }
}
