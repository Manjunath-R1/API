using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class add_PaymentScheduleTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FundAgreementDocuments",
                schema: "FundingSource",
                columns: table => new
                {
                    DocumentID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanApplicationID = table.Column<long>(type: "bigint", nullable: false),
                    ProgramID = table.Column<long>(type: "bigint", nullable: false),
                    BusinessID = table.Column<long>(type: "bigint", nullable: false),
                    DocumentGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentTypeID = table.Column<int>(type: "int", nullable: false),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysicalFileStorageKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    FileUploadedSourceUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactID = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundAgreementDocuments", x => x.DocumentID);
                });

            migrationBuilder.CreateTable(
                name: "GenaralOption",
                schema: "Master",
                columns: table => new
                {
                    OptionID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenaralOption", x => x.OptionID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentScheduleSummary",
                schema: "FundingSource",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanApplicationID = table.Column<long>(type: "bigint", nullable: false),
                    ProgramID = table.Column<long>(type: "bigint", nullable: false),
                    BusinessID = table.Column<long>(type: "bigint", nullable: false),
                    FundRequestedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FundAllocatedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FundDisbursedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FundPendingAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContactID = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentScheduleSummary", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentScheduleTransaction",
                schema: "FundingSource",
                columns: table => new
                {
                    PaymentScheduleID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanApplicationID = table.Column<long>(type: "bigint", nullable: false),
                    ProgramID = table.Column<long>(type: "bigint", nullable: false),
                    BusinessID = table.Column<long>(type: "bigint", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FundingTypeID = table.Column<long>(type: "bigint", nullable: false),
                    FundedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionStatusID = table.Column<long>(type: "bigint", nullable: false),
                    ContactID = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentScheduleTransaction", x => x.PaymentScheduleID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FundAgreementDocuments",
                schema: "FundingSource");

            migrationBuilder.DropTable(
                name: "GenaralOption",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "PaymentScheduleSummary",
                schema: "FundingSource");

            migrationBuilder.DropTable(
                name: "PaymentScheduleTransaction",
                schema: "FundingSource");
        }
    }
}
