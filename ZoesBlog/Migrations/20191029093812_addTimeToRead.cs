using Microsoft.EntityFrameworkCore.Migrations;

namespace ZoesBlog.Migrations
{
    public partial class addTimeToRead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TimeToRead",
                table: "BlogPosts",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeToRead",
                table: "BlogPosts");
        }
    }
}
