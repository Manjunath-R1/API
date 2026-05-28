using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class update_UpdateUVReviewStatus_API : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update [Master].[ApplicationStatus]  set Description = 'Payment Requested - UW Review' where  ApplicationStatusID = 18");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
