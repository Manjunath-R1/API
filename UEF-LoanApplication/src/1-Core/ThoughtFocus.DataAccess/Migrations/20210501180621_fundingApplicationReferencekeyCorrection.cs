using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class fundingApplicationReferencekeyCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FundingApplication_FundingSources_FundSourceFundingSourceID",
                schema: "Application",
                table: "FundingApplication");

            migrationBuilder.DropIndex(
                name: "IX_FundingApplication_FundSourceFundingSourceID",
                schema: "Application",
                table: "FundingApplication");

            migrationBuilder.DropColumn(
                name: "FundSourceFundingSourceID",
                schema: "Application",
                table: "FundingApplication");

            migrationBuilder.CreateIndex(
                name: "IX_FundingApplication_ProgramID",
                schema: "Application",
                table: "FundingApplication",
                column: "ProgramID");

            migrationBuilder.AddForeignKey(
                name: "FK_FundingApplication_FundingSources_ProgramID",
                schema: "Application",
                table: "FundingApplication",
                column: "ProgramID",
                principalSchema: "FundingSource",
                principalTable: "FundingSources",
                principalColumn: "FundingSourceID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FundingApplication_FundingSources_ProgramID",
                schema: "Application",
                table: "FundingApplication");

            migrationBuilder.DropIndex(
                name: "IX_FundingApplication_ProgramID",
                schema: "Application",
                table: "FundingApplication");

            migrationBuilder.AddColumn<long>(
                name: "FundSourceFundingSourceID",
                schema: "Application",
                table: "FundingApplication",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FundingApplication_FundSourceFundingSourceID",
                schema: "Application",
                table: "FundingApplication",
                column: "FundSourceFundingSourceID");

            migrationBuilder.AddForeignKey(
                name: "FK_FundingApplication_FundingSources_FundSourceFundingSourceID",
                schema: "Application",
                table: "FundingApplication",
                column: "FundSourceFundingSourceID",
                principalSchema: "FundingSource",
                principalTable: "FundingSources",
                principalColumn: "FundingSourceID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
