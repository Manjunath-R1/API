using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class AddLoanApplicationAgreementDetailsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanApplicationAgreementDetails",
                schema: "Application",
                columns: table => new
                {
                    LoanApplicationAgreementDetailID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationID = table.Column<long>(type: "bigint", nullable: false),
                    TransitionID = table.Column<long>(type: "bigint", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanApplicationAgreementDetails", x => x.LoanApplicationAgreementDetailID);
                    table.ForeignKey(
                        name: "FK_LoanApplicationAgreementDetails_LoanApplication_ApplicationID",
                        column: x => x.ApplicationID,
                        principalSchema: "Application",
                        principalTable: "LoanApplication",
                        principalColumn: "LoanApplicationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplicationAgreementDetails_ApplicationID",
                schema: "Application",
                table: "LoanApplicationAgreementDetails",
                column: "ApplicationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanApplicationAgreementDetails",
                schema: "Application");
        }
    }
}
