using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class added_ApplicationRelatedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_QuestionResponse_FundingApplicationID",
                schema: "Application",
                table: "QuestionResponse");

             migrationBuilder.DropForeignKey(
                name: "FK_QuestionResponse_FundingApplication_FundingApplicationID",
                schema: "Application",
                table: "QuestionResponse");

            migrationBuilder.DropColumn(
                name: "FundingApplicationID",
                schema: "Application",
                table: "QuestionResponse");

            migrationBuilder.DropColumn(
                name: "NAICS",
                schema: "Application",
                table: "LoanBusinessDetail");

            migrationBuilder.DropColumn(
                name: "SIC",
                schema: "Application",
                table: "LoanBusinessDetail");

            migrationBuilder.AddColumn<long>(
                name: "NAICS_ID",
                schema: "Application",
                table: "LoanBusinessDetail",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SIC_ID",
                schema: "Application",
                table: "LoanBusinessDetail",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "Application",
                table: "BusinessOwner",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NAICS",
                schema: "Master",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndustryTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NAICS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProgramInvitees",
                schema: "Admin",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramInvitationID = table.Column<long>(type: "bigint", nullable: false),
                    ContactID = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramInvitees", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProgramInvitees_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalSchema: "Contact",
                        principalTable: "Contacts",
                        principalColumn: "ContactID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramInvitees_ProgramInvitation_ProgramInvitationID",
                        column: x => x.ProgramInvitationID,
                        principalSchema: "Admin",
                        principalTable: "ProgramInvitation",
                        principalColumn: "ProgramInvitationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SIC",
                schema: "Master",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Division = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndustryTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoanBusinessDetail_NAICS_ID",
                schema: "Application",
                table: "LoanBusinessDetail",
                column: "NAICS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanBusinessDetail_SIC_ID",
                schema: "Application",
                table: "LoanBusinessDetail",
                column: "SIC_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramInvitees_ContactID",
                schema: "Admin",
                table: "ProgramInvitees",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramInvitees_ProgramInvitationID",
                schema: "Admin",
                table: "ProgramInvitees",
                column: "ProgramInvitationID");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanBusinessDetail_NAICS_NAICS_ID",
                schema: "Application",
                table: "LoanBusinessDetail",
                column: "NAICS_ID",
                principalSchema: "Master",
                principalTable: "NAICS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanBusinessDetail_SIC_SIC_ID",
                schema: "Application",
                table: "LoanBusinessDetail",
                column: "SIC_ID",
                principalSchema: "Master",
                principalTable: "SIC",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanBusinessDetail_NAICS_NAICS_ID",
                schema: "Application",
                table: "LoanBusinessDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanBusinessDetail_SIC_SIC_ID",
                schema: "Application",
                table: "LoanBusinessDetail");

            migrationBuilder.DropTable(
                name: "NAICS",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "ProgramInvitees",
                schema: "Admin");

            migrationBuilder.DropTable(
                name: "SIC",
                schema: "Master");

            migrationBuilder.DropIndex(
                name: "IX_LoanBusinessDetail_NAICS_ID",
                schema: "Application",
                table: "LoanBusinessDetail");

            migrationBuilder.DropIndex(
                name: "IX_LoanBusinessDetail_SIC_ID",
                schema: "Application",
                table: "LoanBusinessDetail");

            migrationBuilder.DropColumn(
                name: "NAICS_ID",
                schema: "Application",
                table: "LoanBusinessDetail");

            migrationBuilder.DropColumn(
                name: "SIC_ID",
                schema: "Application",
                table: "LoanBusinessDetail");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "Application",
                table: "BusinessOwner");

            migrationBuilder.AddColumn<long>(
                name: "FundingApplicationID",
                schema: "Application",
                table: "QuestionResponse",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NAICS",
                schema: "Application",
                table: "LoanBusinessDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SIC",
                schema: "Application",
                table: "LoanBusinessDetail",
                type: "nvarchar(max)",
                nullable: true);    

             migrationBuilder.AddForeignKey(
                name: "FK_QuestionResponse_FundingApplication_FundingApplicationID",
                schema: "Application",
                table: "QuestionResponse",
                column: "FundingApplicationID",
                principalSchema: "Application",
                principalTable: "FundingApplication",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionResponse_FundingApplicationID",
                schema: "Application",
                table: "QuestionResponse",
                column: "FundingApplicationID");
        }
    }
}