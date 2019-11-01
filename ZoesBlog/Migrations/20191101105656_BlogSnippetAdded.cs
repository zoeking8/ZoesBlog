using Microsoft.EntityFrameworkCore.Migrations;

namespace ZoesBlog.Migrations
{
    public partial class BlogSnippetAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Snippet",
                table: "BlogPosts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Snippet",
                table: "BlogPosts");
        }
    }
}
