using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class Add_SPA_WOrkFlow_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanApplicationSchedulePaymentAreementDetail",
                schema: "Application",
                columns: table => new
                {
                    SPAID = table.Column<long>(type: "bigint", nullable: false)
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
                    table.PrimaryKey("PK_LoanApplicationSchedulePaymentAreementDetail", x => x.SPAID);
                    table.ForeignKey(
                        name: "FK_LoanApplicationSchedulePaymentAreementDetail_LoanApplication_ApplicationID",
                        column: x => x.ApplicationID,
                        principalSchema: "Application",
                        principalTable: "LoanApplication",
                        principalColumn: "LoanApplicationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentScheduleStatus",
                schema: "Application",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanApplicationID = table.Column<long>(type: "bigint", nullable: false),
                    DisbursementCount = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentScheduleStatus", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplicationSchedulePaymentAreementDetail_ApplicationID",
                schema: "Application",
                table: "LoanApplicationSchedulePaymentAreementDetail",
                column: "ApplicationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanApplicationSchedulePaymentAreementDetail",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "PaymentScheduleStatus",
                schema: "Application");
        }
    }
}
