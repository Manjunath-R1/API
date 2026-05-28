using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class addfundisbursed_applicationStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update Master.ApplicationStatus set Description = 'Fund Disbursed' where ApplicationStatusName = 'AccountDisbursed' and ApplicationStatusID = 13 ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update Master.ApplicationStatus set Description = 'Fund Disbursed' where ApplicationStatusName = 'AccountDisbursed' and ApplicationStatusID = 13 ");
        }
    }
}
