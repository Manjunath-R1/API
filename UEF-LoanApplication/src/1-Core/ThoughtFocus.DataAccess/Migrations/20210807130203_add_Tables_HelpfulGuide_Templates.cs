using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class add_Tables_HelpfulGuide_Templates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TemplateTypes",
                schema: "Master",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "HelpfulGuideTemplates",
                schema: "Master",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelpfulGuideTemplates", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HelpfulGuideTemplates_TemplateTypes_TypeID",
                        column: x => x.TypeID,
                        principalSchema: "Master",
                        principalTable: "TemplateTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProgramHelpfulGuides",
                schema: "FundingSource",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramID = table.Column<long>(type: "bigint", nullable: false),
                    TamplateID = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramHelpfulGuides", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProgramHelpfulGuides_FundingSources_ProgramID",
                        column: x => x.ProgramID,
                        principalSchema: "FundingSource",
                        principalTable: "FundingSources",
                        principalColumn: "FundingSourceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgramHelpfulGuides_HelpfulGuideTemplates_TamplateID",
                        column: x => x.TamplateID,
                        principalSchema: "Master",
                        principalTable: "HelpfulGuideTemplates",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HelpfulGuideTemplates_TypeID",
                schema: "Master",
                table: "HelpfulGuideTemplates",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramHelpfulGuides_ProgramID",
                schema: "FundingSource",
                table: "ProgramHelpfulGuides",
                column: "ProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramHelpfulGuides_TamplateID",
                schema: "FundingSource",
                table: "ProgramHelpfulGuides",
                column: "TamplateID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgramHelpfulGuides",
                schema: "FundingSource");

            migrationBuilder.DropTable(
                name: "HelpfulGuideTemplates",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "TemplateTypes",
                schema: "Master");
        }
    }
}
