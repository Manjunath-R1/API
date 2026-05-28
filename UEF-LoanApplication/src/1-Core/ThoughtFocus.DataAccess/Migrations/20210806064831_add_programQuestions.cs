using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class add_programQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProgramQuestions",
                schema: "FundingSource",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramID = table.Column<long>(type: "bigint", nullable: true),
                    QuestionID = table.Column<long>(type: "bigint", nullable: true),
                    IsMadatory = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramQuestions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProgramQuestions_FundingSources_ProgramID",
                        column: x => x.ProgramID,
                        principalSchema: "FundingSource",
                        principalTable: "FundingSources",
                        principalColumn: "FundingSourceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgramQuestions_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalSchema: "Question",
                        principalTable: "Questions",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramQuestions_ProgramID",
                schema: "FundingSource",
                table: "ProgramQuestions",
                column: "ProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramQuestions_QuestionID",
                schema: "FundingSource",
                table: "ProgramQuestions",
                column: "QuestionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgramQuestions",
                schema: "FundingSource");
        }
    }
}
