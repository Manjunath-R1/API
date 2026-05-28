using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class update_NotificationRecipients_UW_Table1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update [Notification].[NotificationRecipients]  set IsCC = 1 where  ActivityNotificationID = 2 and NotificationRecipientID in (22)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
