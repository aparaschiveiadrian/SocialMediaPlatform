using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialMediaPlatform.Server.Migrations
{
    /// <inheritdoc />
    public partial class Messages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "UserConversationUserId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserConversationConversationId",
                table: "AspNetUsers",
                newName: "ConversationId1");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMessageSentAt",
                table: "Conversations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d7801fdc-b41b-412c-93f0-c68f3da7c699", null, "User", "USER" },
                    { "fe7c32a0-cf3e-442d-937e-372651b9da80", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ConversationId1",
                table: "AspNetUsers",
                column: "ConversationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Conversations_ConversationId1",
                table: "AspNetUsers",
                column: "ConversationId1",
                principalTable: "Conversations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Conversations_ConversationId1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ConversationId1",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d7801fdc-b41b-412c-93f0-c68f3da7c699");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fe7c32a0-cf3e-442d-937e-372651b9da80");

            migrationBuilder.DropColumn(
                name: "LastMessageSentAt",
                table: "Conversations");

            migrationBuilder.RenameColumn(
                name: "ConversationId1",
                table: "AspNetUsers",
                newName: "UserConversationConversationId");

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
    }
}
