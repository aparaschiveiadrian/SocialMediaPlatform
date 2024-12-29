using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialMediaPlatform.Server.Migrations
{
    /// <inheritdoc />
    public partial class Messages78 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6188f115-566a-4b4e-a527-93cb9b2ce16f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1d724e4-39dd-447c-b67e-2b0954d14708");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b96ced30-c26c-4572-94ca-cb9c776687be", null, "User", "USER" },
                    { "c3909ec3-0e39-46fc-82a7-d91bfe216fe4", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b96ced30-c26c-4572-94ca-cb9c776687be");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c3909ec3-0e39-46fc-82a7-d91bfe216fe4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6188f115-566a-4b4e-a527-93cb9b2ce16f", null, "User", "USER" },
                    { "c1d724e4-39dd-447c-b67e-2b0954d14708", null, "Admin", "ADMIN" }
                });
        }
    }
}
