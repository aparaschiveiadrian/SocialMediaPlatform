using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialMediaPlatform.Server.Migrations
{
    /// <inheritdoc />
    public partial class UserPrivacy2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e6b4d11-a30c-4657-b198-f2beac7918e4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35b15479-081e-4e4a-816e-859e9f33e987");

            migrationBuilder.RenameColumn(
                name: "isPrivate",
                table: "AspNetUsers",
                newName: "IsPrivate");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3b8f1895-396e-43d7-8006-4255d09c96f9", null, "User", "USER" },
                    { "5544ee02-f4c0-4c49-a4c1-13a7d4526265", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b8f1895-396e-43d7-8006-4255d09c96f9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5544ee02-f4c0-4c49-a4c1-13a7d4526265");

            migrationBuilder.RenameColumn(
                name: "IsPrivate",
                table: "AspNetUsers",
                newName: "isPrivate");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e6b4d11-a30c-4657-b198-f2beac7918e4", null, "Admin", "ADMIN" },
                    { "35b15479-081e-4e4a-816e-859e9f33e987", null, "User", "USER" }
                });
        }
    }
}
