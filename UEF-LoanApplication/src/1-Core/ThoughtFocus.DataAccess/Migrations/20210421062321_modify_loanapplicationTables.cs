using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class modify_loanapplicationTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Question");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                schema: "Application",
                table: "LoanBusinessDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                schema: "Application",
                table: "LoanBusinessDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "Application",
                table: "LoanBusinessDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProgramInvitationID",
                schema: "Application",
                table: "LoanApplication",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                schema: "Application",
                table: "BusinessOwner",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FundingApplication",
                schema: "Application",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanApplicationID = table.Column<long>(type: "bigint", nullable: false),
                    ProgramID = table.Column<long>(type: "bigint", nullable: false),
                    RequestedFundAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FundSourceFundingSourceID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundingApplication", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FundingApplication_FundingSources_FundSourceFundingSourceID",
                        column: x => x.FundSourceFundingSourceID,
                        principalSchema: "FundingSource",
                        principalTable: "FundingSources",
                        principalColumn: "FundingSourceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FundingApplication_LoanApplication_LoanApplicationID",
                        column: x => x.LoanApplicationID,
                        principalSchema: "Application",
                        principalTable: "LoanApplication",
                        principalColumn: "LoanApplicationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResponseType",
                schema: "Master",
                columns: table => new
                {
                    TypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponseType", x => x.TypeID);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                schema: "Question",
                columns: table => new
                {
                    QuestionID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ResponseTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK_Questions_ResponseType_ResponseTypeID",
                        column: x => x.ResponseTypeID,
                        principalSchema: "Master",
                        principalTable: "ResponseType",
                        principalColumn: "TypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionResponse",
                schema: "Application",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanApplicationID = table.Column<long>(type: "bigint", nullable: false),
                    QuestionID = table.Column<long>(type: "bigint", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FundingApplicationID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionResponse", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuestionResponse_FundingApplication_FundingApplicationID",
                        column: x => x.FundingApplicationID,
                        principalSchema: "Application",
                        principalTable: "FundingApplication",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionResponse_LoanApplication_LoanApplicationID",
                        column: x => x.LoanApplicationID,
                        principalSchema: "Application",
                        principalTable: "LoanApplication",
                        principalColumn: "LoanApplicationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionResponse_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalSchema: "Question",
                        principalTable: "Questions",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplication_ProgramInvitationID",
                schema: "Application",
                table: "LoanApplication",
                column: "ProgramInvitationID");

            migrationBuilder.CreateIndex(
                name: "IX_FundingApplication_FundSourceFundingSourceID",
                schema: "Application",
                table: "FundingApplication",
                column: "FundSourceFundingSourceID");

            migrationBuilder.CreateIndex(
                name: "IX_FundingApplication_LoanApplicationID",
                schema: "Application",
                table: "FundingApplication",
                column: "LoanApplicationID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionResponse_FundingApplicationID",
                schema: "Application",
                table: "QuestionResponse",
                column: "FundingApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionResponse_LoanApplicationID",
                schema: "Application",
                table: "QuestionResponse",
                column: "LoanApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionResponse_QuestionID",
                schema: "Application",
                table: "QuestionResponse",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ResponseTypeID",
                schema: "Question",
                table: "Questions",
                column: "ResponseTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanApplication_ProgramInvitation_ProgramInvitationID",
                schema: "Application",
                table: "LoanApplication",
                column: "ProgramInvitationID",
                principalSchema: "Admin",
                principalTable: "ProgramInvitation",
                principalColumn: "ProgramInvitationID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanApplication_ProgramInvitation_ProgramInvitationID",
                schema: "Application",
                table: "LoanApplication");

            migrationBuilder.DropTable(
                name: "QuestionResponse",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "FundingApplication",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "Questions",
                schema: "Question");

            migrationBuilder.DropTable(
                name: "ResponseType",
                schema: "Master");

            migrationBuilder.DropIndex(
                name: "IX_LoanApplication_ProgramInvitationID",
                schema: "Application",
                table: "LoanApplication");

            migrationBuilder.DropColumn(
                name: "Comment",
                schema: "Application",
                table: "LoanBusinessDetail");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                schema: "Application",
                table: "LoanBusinessDetail");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "Application",
                table: "LoanBusinessDetail");

            migrationBuilder.DropColumn(
                name: "ProgramInvitationID",
                schema: "Application",
                table: "LoanApplication");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                schema: "Application",
                table: "BusinessOwner");
        }
    }
}
