using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialMediaPlatform.Server.Migrations
{
    /// <inheritdoc />
    public partial class ConversationNamel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "488c7e0e-6f96-4fc1-a2a6-e3e4f71f18aa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d215c720-0ce1-476b-82bc-d4d81653c037");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastMessageSentAt",
                table: "Conversations",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "603b7c2c-d812-412a-8b60-de9948090d54", null, "Admin", "ADMIN" },
                    { "64dbd4a2-2d42-4c84-a405-51459e02a02b", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "603b7c2c-d812-412a-8b60-de9948090d54");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64dbd4a2-2d42-4c84-a405-51459e02a02b");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastMessageSentAt",
                table: "Conversations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "488c7e0e-6f96-4fc1-a2a6-e3e4f71f18aa", null, "Admin", "ADMIN" },
                    { "d215c720-0ce1-476b-82bc-d4d81653c037", null, "User", "USER" }
                });
        }
    }
}
