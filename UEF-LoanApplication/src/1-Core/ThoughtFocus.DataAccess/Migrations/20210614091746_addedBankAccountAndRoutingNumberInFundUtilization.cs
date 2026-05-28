using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class addedBankAccountAndRoutingNumberInFundUtilization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankRoutingNumber",
                schema: "FundingSource",
                table: "FundTransactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DestinationBankAccount",
                schema: "FundingSource",
                table: "FundTransactions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankRoutingNumber",
                schema: "FundingSource",
                table: "FundTransactions");

            migrationBuilder.DropColumn(
                name: "DestinationBankAccount",
                schema: "FundingSource",
                table: "FundTransactions");
        }
    }
}
