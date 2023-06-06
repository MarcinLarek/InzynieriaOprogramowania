using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektZaliczeniowy.Data.Migrations
{
    public partial class comment3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostID",
                table: "Comment");

            migrationBuilder.AddColumn<int>(
                name: "CommentedPost",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentedPost",
                table: "Comment");

            migrationBuilder.AddColumn<string>(
                name: "PostID",
                table: "Comment",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
