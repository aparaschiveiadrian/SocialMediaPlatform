using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialMediaPlatform.Server.Migrations
{
    /// <inheritdoc />
    public partial class Messages6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5789f06a-f178-4d13-8118-be852ba4b6e9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7be25efe-e035-4f4e-9f05-eee96a080d20");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7d5238c8-3bdf-486b-806f-7abd0c7470fe", null, "Admin", "ADMIN" },
                    { "e45b1e10-0bf2-40ca-b47c-324ef8611bcc", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d5238c8-3bdf-486b-806f-7abd0c7470fe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e45b1e10-0bf2-40ca-b47c-324ef8611bcc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5789f06a-f178-4d13-8118-be852ba4b6e9", null, "Admin", "ADMIN" },
                    { "7be25efe-e035-4f4e-9f05-eee96a080d20", null, "User", "USER" }
                });
        }
    }
}
