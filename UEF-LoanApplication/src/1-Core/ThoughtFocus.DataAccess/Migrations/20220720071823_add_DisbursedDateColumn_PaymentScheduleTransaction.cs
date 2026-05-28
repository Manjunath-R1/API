using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class add_DisbursedDateColumn_PaymentScheduleTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DisbursedDate",
                schema: "FundingSource",
                table: "PaymentScheduleTransaction",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisbursedDate",
                schema: "FundingSource",
                table: "PaymentScheduleTransaction");
        }
    }
}
