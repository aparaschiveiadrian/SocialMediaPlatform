using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialMediaPlatform.Server.Migrations
{
    /// <inheritdoc />
    public partial class UserPrivacy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "48a2ad8b-8103-443b-aa7e-bc2b426575df");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f01f1b0-978a-4852-8bda-8373edd9e68b");

            migrationBuilder.AddColumn<bool>(
                name: "isPending",
                table: "Follows",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isPrivate",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e6b4d11-a30c-4657-b198-f2beac7918e4", null, "Admin", "ADMIN" },
                    { "35b15479-081e-4e4a-816e-859e9f33e987", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e6b4d11-a30c-4657-b198-f2beac7918e4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35b15479-081e-4e4a-816e-859e9f33e987");

            migrationBuilder.DropColumn(
                name: "isPending",
                table: "Follows");

            migrationBuilder.DropColumn(
                name: "isPrivate",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "48a2ad8b-8103-443b-aa7e-bc2b426575df", null, "Admin", "ADMIN" },
                    { "5f01f1b0-978a-4852-8bda-8373edd9e68b", null, "User", "USER" }
                });
        }
    }
}
