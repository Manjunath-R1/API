using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class add_PaymentSummaryAddionfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<DateTime>(
            //    name: "DisbursedDate",
            //    schema: "FundingSource",
            //    table: "PaymentScheduleTransaction",
            //    type: "datetime2",
            //    nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdditionalNotesAgreement",
                schema: "FundingSource",
                table: "PaymentScheduleSummary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaymentSchedule",
                schema: "Application",
                table: "FundingApplication",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "DisbursedDate",
            //    schema: "FundingSource",
            //    table: "PaymentScheduleTransaction");

            migrationBuilder.DropColumn(
                name: "AdditionalNotesAgreement",
                schema: "FundingSource",
                table: "PaymentScheduleSummary");

            migrationBuilder.DropColumn(
                name: "IsPaymentSchedule",
                schema: "Application",
                table: "FundingApplication");
        }
    }
}
