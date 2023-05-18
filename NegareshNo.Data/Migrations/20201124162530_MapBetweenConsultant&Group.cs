using Microsoft.EntityFrameworkCore.Migrations;

namespace NegareshNo.Data.Migrations
{
    public partial class MapBetweenConsultantGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consultant_Groups",
                columns: table => new
                {
                    ConsultantId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultant_Groups", x => new { x.ConsultantId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_Consultant_Groups_Consultants_ConsultantId",
                        column: x => x.ConsultantId,
                        principalTable: "Consultants",
                        principalColumn: "ConsultantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consultant_Groups_ConsultingGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "ConsultingGroups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consultant_Groups_GroupId",
                table: "Consultant_Groups",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consultant_Groups");
        }
    }
}
