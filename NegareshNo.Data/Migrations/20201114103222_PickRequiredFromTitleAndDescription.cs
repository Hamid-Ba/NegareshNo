using Microsoft.EntityFrameworkCore.Migrations;

namespace NegareshNo.Data.Migrations
{
    public partial class PickRequiredFromTitleAndDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "UserRequests",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "UserRequests",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "UserRequests",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "UserRequests",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
