using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NegareshNo.Data.Migrations
{
    public partial class CreateNegareshNoDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consultants",
                columns: table => new
                {
                    ConsultantId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConsultantFullName = table.Column<string>(maxLength: 30, nullable: false),
                    CareerSummary = table.Column<string>(maxLength: 200, nullable: false),
                    CareerRecord = table.Column<string>(nullable: false),
                    ImageName = table.Column<string>(nullable: true),
                    Job = table.Column<string>(maxLength: 15, nullable: false),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    MobilePhone = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    EducationalRecord = table.Column<string>(nullable: true),
                    WorkRecord = table.Column<string>(nullable: true),
                    LiceneRecord = table.Column<string>(nullable: true),
                    Skills = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultants", x => x.ConsultantId);
                });

            migrationBuilder.CreateTable(
                name: "ConsultingGroups",
                columns: table => new
                {
                    GroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GroupTitle = table.Column<string>(maxLength: 50, nullable: false),
                    ImageName = table.Column<string>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultingGroups", x => x.GroupId);
                });

            migrationBuilder.CreateTable(
                name: "Permmisions",
                columns: table => new
                {
                    PermmisionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PermmisionTitle = table.Column<string>(nullable: false),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permmisions", x => x.PermmisionId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleTitle = table.Column<string>(maxLength: 30, nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    ArticleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConsultingId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 30, nullable: false),
                    Summery = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    PublishedDate = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ArticleId);
                    table.ForeignKey(
                        name: "FK_Articles_Consultants_ConsultingId",
                        column: x => x.ConsultingId,
                        principalTable: "Consultants",
                        principalColumn: "ConsultantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRequests",
                columns: table => new
                {
                    RequestId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GroupId = table.Column<int>(nullable: false),
                    ConsultantId = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    TellPhoneNumber = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    RegistrationConsultingTime = table.Column<DateTime>(nullable: false),
                    GivenTime = table.Column<DateTime>(nullable: false),
                    IsDone = table.Column<bool>(nullable: false),
                    HasTime = table.Column<bool>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRequests", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_UserRequests_Consultants_ConsultantId",
                        column: x => x.ConsultantId,
                        principalTable: "Consultants",
                        principalColumn: "ConsultantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRequests_ConsultingGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "ConsultingGroups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Role_Consultants",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false),
                    ConsultantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role_Consultants", x => new { x.RoleId, x.ConsultantId });
                    table.ForeignKey(
                        name: "FK_Role_Consultants_Consultants_ConsultantId",
                        column: x => x.ConsultantId,
                        principalTable: "Consultants",
                        principalColumn: "ConsultantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Role_Consultants_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Role_Permmisions",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false),
                    PermmisionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role_Permmisions", x => new { x.RoleId, x.PermmisionId });
                    table.ForeignKey(
                        name: "FK_Role_Permmisions_Permmisions_PermmisionId",
                        column: x => x.PermmisionId,
                        principalTable: "Permmisions",
                        principalColumn: "PermmisionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Role_Permmisions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ConsultingId",
                table: "Articles",
                column: "ConsultingId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Consultants_ConsultantId",
                table: "Role_Consultants",
                column: "ConsultantId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Permmisions_PermmisionId",
                table: "Role_Permmisions",
                column: "PermmisionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRequests_ConsultantId",
                table: "UserRequests",
                column: "ConsultantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRequests_GroupId",
                table: "UserRequests",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Role_Consultants");

            migrationBuilder.DropTable(
                name: "Role_Permmisions");

            migrationBuilder.DropTable(
                name: "UserRequests");

            migrationBuilder.DropTable(
                name: "Permmisions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Consultants");

            migrationBuilder.DropTable(
                name: "ConsultingGroups");
        }
    }
}
