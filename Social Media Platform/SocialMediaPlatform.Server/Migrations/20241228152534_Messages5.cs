using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialMediaPlatform.Server.Migrations
{
    /// <inheritdoc />
    public partial class Messages5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PendingUserConversation");

            migrationBuilder.DropTable(
                name: "SeenUserConversation");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e624cbd-9b4b-46ba-a354-671c64b4ff3c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4200925a-049d-4d66-9c38-5a48918c001f");

            migrationBuilder.CreateTable(
                name: "PendingUsers",
                columns: table => new
                {
                    ConversationId = table.Column<int>(type: "integer", nullable: false),
                    PendingUsersId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingUsers", x => new { x.ConversationId, x.PendingUsersId });
                    table.ForeignKey(
                        name: "FK_PendingUsers_AspNetUsers_PendingUsersId",
                        column: x => x.PendingUsersId,
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
                    Conversation1Id = table.Column<int>(type: "integer", nullable: false),
                    SeenUserListId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeenUsers", x => new { x.Conversation1Id, x.SeenUserListId });
                    table.ForeignKey(
                        name: "FK_SeenUsers_AspNetUsers_SeenUserListId",
                        column: x => x.SeenUserListId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeenUsers_Conversations_Conversation1Id",
                        column: x => x.Conversation1Id,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5789f06a-f178-4d13-8118-be852ba4b6e9", null, "Admin", "ADMIN" },
                    { "7be25efe-e035-4f4e-9f05-eee96a080d20", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PendingUsers_PendingUsersId",
                table: "PendingUsers",
                column: "PendingUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_SeenUsers_SeenUserListId",
                table: "SeenUsers",
                column: "SeenUserListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PendingUsers");

            migrationBuilder.DropTable(
                name: "SeenUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5789f06a-f178-4d13-8118-be852ba4b6e9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7be25efe-e035-4f4e-9f05-eee96a080d20");

            migrationBuilder.CreateTable(
                name: "PendingUserConversation",
                columns: table => new
                {
                    ConversationId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingUserConversation", x => new { x.ConversationId, x.UserId });
                    table.ForeignKey(
                        name: "FK_PendingUserConversation_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PendingUserConversation_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeenUserConversation",
                columns: table => new
                {
                    ConversationId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeenUserConversation", x => new { x.ConversationId, x.UserId });
                    table.ForeignKey(
                        name: "FK_SeenUserConversation_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeenUserConversation_Conversations_ConversationId",
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
                    { "0e624cbd-9b4b-46ba-a354-671c64b4ff3c", null, "User", "USER" },
                    { "4200925a-049d-4d66-9c38-5a48918c001f", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PendingUserConversation_UserId",
                table: "PendingUserConversation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SeenUserConversation_UserId",
                table: "SeenUserConversation",
                column: "UserId");
        }
    }
}
