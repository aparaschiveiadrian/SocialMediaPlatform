using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialMediaPlatform.Server.Migrations
{
    /// <inheritdoc />
    public partial class Messages7 : Migration
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
                keyValue: "7d5238c8-3bdf-486b-806f-7abd0c7470fe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e45b1e10-0bf2-40ca-b47c-324ef8611bcc");

            migrationBuilder.AddColumn<int>(
                name: "ConversationId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConversationId1",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6188f115-566a-4b4e-a527-93cb9b2ce16f", null, "User", "USER" },
                    { "c1d724e4-39dd-447c-b67e-2b0954d14708", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ConversationId",
                table: "AspNetUsers",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ConversationId1",
                table: "AspNetUsers",
                column: "ConversationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Conversations_ConversationId",
                table: "AspNetUsers",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id");

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
                name: "FK_AspNetUsers_Conversations_ConversationId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Conversations_ConversationId1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ConversationId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ConversationId1",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6188f115-566a-4b4e-a527-93cb9b2ce16f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1d724e4-39dd-447c-b67e-2b0954d14708");

            migrationBuilder.DropColumn(
                name: "ConversationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ConversationId1",
                table: "AspNetUsers");

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
                    { "7d5238c8-3bdf-486b-806f-7abd0c7470fe", null, "Admin", "ADMIN" },
                    { "e45b1e10-0bf2-40ca-b47c-324ef8611bcc", null, "User", "USER" }
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
    }
}
