using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class AlterFundTransactionTable_AddedAccountNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OriginatingBankAccount",
                schema: "FundingSource",
                table: "FundTransactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDate",
                schema: "FundingSource",
                table: "FundTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginatingBankAccount",
                schema: "FundingSource",
                table: "FundTransactions");

            migrationBuilder.DropColumn(
                name: "TransactionDate",
                schema: "FundingSource",
                table: "FundTransactions");
        }
    }
}
