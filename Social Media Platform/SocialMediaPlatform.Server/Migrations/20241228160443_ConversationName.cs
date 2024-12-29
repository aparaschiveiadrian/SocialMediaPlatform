using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialMediaPlatform.Server.Migrations
{
    /// <inheritdoc />
    public partial class ConversationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "494d2613-8e58-4c66-a6c9-45967508cebb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9dfbb631-3fdb-4e27-a9e3-d13c582c3000");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Conversations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "488c7e0e-6f96-4fc1-a2a6-e3e4f71f18aa", null, "Admin", "ADMIN" },
                    { "d215c720-0ce1-476b-82bc-d4d81653c037", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "488c7e0e-6f96-4fc1-a2a6-e3e4f71f18aa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d215c720-0ce1-476b-82bc-d4d81653c037");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Conversations");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "494d2613-8e58-4c66-a6c9-45967508cebb", null, "Admin", "ADMIN" },
                    { "9dfbb631-3fdb-4e27-a9e3-d13c582c3000", null, "User", "USER" }
                });
        }
    }
}
