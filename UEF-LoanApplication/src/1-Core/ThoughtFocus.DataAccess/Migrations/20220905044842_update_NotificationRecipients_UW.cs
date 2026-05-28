using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class update_NotificationRecipients_UW : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update [Notification].[NotificationRecipients]  set IsCC = 1 where  ActivityNotificationID = 16 and NotificationRecipientID in (19)");
            migrationBuilder.Sql("update [Notification].[NotificationRecipients]  set IsCC = 0 where  ActivityNotificationID = 16 and NotificationRecipientID in (20,23,24)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
