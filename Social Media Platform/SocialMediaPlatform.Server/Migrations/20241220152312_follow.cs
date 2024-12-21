using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialMediaPlatform.Server.Migrations
{
    /// <inheritdoc />
    public partial class follow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82099edc-ebfb-4a3c-883e-83994a3c56a5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e5694d55-1d1e-4757-9af1-f6090584be33");

            migrationBuilder.RenameColumn(
                name: "isPending",
                table: "Follows",
                newName: "IsPending");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "32fa55fc-5762-4172-bdbb-d2f2fbbd5f55", null, "Admin", "ADMIN" },
                    { "b922d034-ab74-4020-a058-1eebeff141d6", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32fa55fc-5762-4172-bdbb-d2f2fbbd5f55");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b922d034-ab74-4020-a058-1eebeff141d6");

            migrationBuilder.RenameColumn(
                name: "IsPending",
                table: "Follows",
                newName: "isPending");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "82099edc-ebfb-4a3c-883e-83994a3c56a5", null, "Admin", "ADMIN" },
                    { "e5694d55-1d1e-4757-9af1-f6090584be33", null, "User", "USER" }
                });
        }
    }
}
