using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class Add_SPA_WOrkFlow_tables3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PaymentScheduleStatus_LoanApplicationID",
                schema: "Application",
                table: "PaymentScheduleStatus",
                column: "LoanApplicationID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentScheduleStatus_LoanApplication_LoanApplicationID",
                schema: "Application",
                table: "PaymentScheduleStatus",
                column: "LoanApplicationID",
                principalSchema: "Application",
                principalTable: "LoanApplication",
                principalColumn: "LoanApplicationID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentScheduleStatus_LoanApplication_LoanApplicationID",
                schema: "Application",
                table: "PaymentScheduleStatus");

            migrationBuilder.DropIndex(
                name: "IX_PaymentScheduleStatus_LoanApplicationID",
                schema: "Application",
                table: "PaymentScheduleStatus");
        }
    }
}
