using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class update_NotificationRecipients_Distable3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update [Notification].[NotificationRecipients]  set IsCC = 1 where  ActivityNotificationID = 16 and NotificationRecipientID in (19,20,23,24)");
            migrationBuilder.Sql("update [Notification].[NotificationRecipients]  set IsCC = 0 where  ActivityNotificationID = 2 and NotificationRecipientID in (22)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
