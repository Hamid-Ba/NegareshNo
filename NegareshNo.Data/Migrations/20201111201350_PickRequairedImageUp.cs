using Microsoft.EntityFrameworkCore.Migrations;

namespace NegareshNo.Data.Migrations
{
    public partial class PickRequairedImageUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageName",
                table: "ConsultingGroups",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageName",
                table: "ConsultingGroups",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
