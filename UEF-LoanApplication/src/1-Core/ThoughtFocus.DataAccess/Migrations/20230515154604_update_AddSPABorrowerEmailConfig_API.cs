using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class update_AddSPABorrowerEmailConfig_API : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update [Notification].[NotificationRecipients]  set PlaceholderID = 3 where  NotificationRecipientID = 95");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
