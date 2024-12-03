using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaPlatform.Server.Migrations
{
    /// <inheritdoc />
    public partial class addedMediaUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MediaUrl",
                table: "Posts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaUrl",
                table: "Posts");
        }
    }
}
