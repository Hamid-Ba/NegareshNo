using Microsoft.EntityFrameworkCore.Migrations;

namespace NegareshNo.Data.Migrations
{
    public partial class AddSummeryToConsultantGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GroupTitle",
                table: "ConsultingGroups",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "Summery",
                table: "ConsultingGroups",
                maxLength: 150,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Summery",
                table: "ConsultingGroups");

            migrationBuilder.AlterColumn<string>(
                name: "GroupTitle",
                table: "ConsultingGroups",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25);
        }
    }
}
