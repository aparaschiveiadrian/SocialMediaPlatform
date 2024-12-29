using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialMediaPlatform.Server.Migrations
{
    /// <inheritdoc />
    public partial class ConversationsSeen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12a5f156-51d3-4d9c-849f-01045eda6775");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e0c5b98a-08b3-4aff-804a-ceeb332e4269");

            migrationBuilder.AddColumn<int>(
                name: "UserConversationConversationId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserConversationUserId",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "55a1992e-6ddd-4d9a-af71-fc909e3b6531", null, "Admin", "ADMIN" },
                    { "de7890f8-0b11-4a53-89d9-8e9487a76320", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserConversationUserId_UserConversationConversa~",
                table: "AspNetUsers",
                columns: new[] { "UserConversationUserId", "UserConversationConversationId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserConversations_UserConversationUserId_UserCo~",
                table: "AspNetUsers",
                columns: new[] { "UserConversationUserId", "UserConversationConversationId" },
                principalTable: "UserConversations",
                principalColumns: new[] { "UserId", "ConversationId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserConversations_UserConversationUserId_UserCo~",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserConversationUserId_UserConversationConversa~",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "55a1992e-6ddd-4d9a-af71-fc909e3b6531");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de7890f8-0b11-4a53-89d9-8e9487a76320");

            migrationBuilder.DropColumn(
                name: "UserConversationConversationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserConversationUserId",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "12a5f156-51d3-4d9c-849f-01045eda6775", null, "Admin", "ADMIN" },
                    { "e0c5b98a-08b3-4aff-804a-ceeb332e4269", null, "User", "USER" }
                });
        }
    }
}
