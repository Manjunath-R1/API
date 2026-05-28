using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class ModifyApplicantsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LoanApplicantDetails_LoanApplicationID",
                schema: "Application",
                table: "LoanApplicantDetails");

            migrationBuilder.AddColumn<string>(
                name: "FacebookUrl",
                schema: "Application",
                table: "LoanBusinessDetail",
                type: "nvarchar(max)",
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

            migrationBuilder.AddColumn<string>(
                name: "Url",
                schema: "Application",
                table: "LoanBusinessDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                schema: "Application",
                table: "LoanApplicantDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplicantDetails_LoanApplicationID",
                schema: "Application",
                table: "LoanApplicantDetails",
                column: "LoanApplicationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LoanApplicantDetails_LoanApplicationID",
                schema: "Application",
                table: "LoanApplicantDetails");

            migrationBuilder.DropColumn(
                name: "FacebookUrl",
                schema: "Application",
                table: "LoanBusinessDetail");

            migrationBuilder.DropColumn(
                name: "NAICS",
                schema: "Application",
                table: "LoanBusinessDetail");

            migrationBuilder.DropColumn(
                name: "SIC",
                schema: "Application",
                table: "LoanBusinessDetail");

            migrationBuilder.DropColumn(
                name: "Url",
                schema: "Application",
                table: "LoanBusinessDetail");

            migrationBuilder.AlterColumn<long>(
                name: "PhoneNumber",
                schema: "Application",
                table: "LoanApplicantDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplicantDetails_LoanApplicationID",
                schema: "Application",
                table: "LoanApplicantDetails",
                column: "LoanApplicationID",
                unique: true);
        }
    }
}
