using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialMediaPlatform.Server.Migrations
{
    /// <inheritdoc />
    public partial class ConversationNamelll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PendingUsers");

            migrationBuilder.DropTable(
                name: "SeenUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "603b7c2c-d812-412a-8b60-de9948090d54");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64dbd4a2-2d42-4c84-a405-51459e02a02b");

            migrationBuilder.AddColumn<List<string>>(
                name: "PendingUserIds",
                table: "Conversations",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<List<string>>(
                name: "SeenUserIds",
                table: "Conversations",
                type: "text[]",
                nullable: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3b67d3c4-4ff0-4a47-8707-286e513790ae", null, "Admin", "ADMIN" },
                    { "464df6df-e747-4d2f-b04f-7261558f148a", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b67d3c4-4ff0-4a47-8707-286e513790ae");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "464df6df-e747-4d2f-b04f-7261558f148a");

            migrationBuilder.DropColumn(
                name: "PendingUserIds",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "SeenUserIds",
                table: "Conversations");

            migrationBuilder.CreateTable(
                name: "PendingUsers",
                columns: table => new
                {
                    ConversationId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingUsers", x => new { x.ConversationId, x.UserId });
                    table.ForeignKey(
                        name: "FK_PendingUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PendingUsers_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeenUsers",
                columns: table => new
                {
                    ConversationId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeenUsers", x => new { x.ConversationId, x.UserId });
                    table.ForeignKey(
                        name: "FK_SeenUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeenUsers_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "603b7c2c-d812-412a-8b60-de9948090d54", null, "Admin", "ADMIN" },
                    { "64dbd4a2-2d42-4c84-a405-51459e02a02b", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PendingUsers_UserId",
                table: "PendingUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SeenUsers_UserId",
                table: "SeenUsers",
                column: "UserId");
        }
    }
}
