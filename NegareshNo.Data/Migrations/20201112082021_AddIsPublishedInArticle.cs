using Microsoft.EntityFrameworkCore.Migrations;

namespace NegareshNo.Data.Migrations
{
    public partial class AddIsPublishedInArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Articles",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Articles");
        }
    }
}
