using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class EmailNotifocationRequestedCompledToCC_Changed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("update [Notification].[NotificationRecipients] set IsTo = 0 ,IsCC =1  where NotificationRecipientID = 72");
            migrationBuilder.Sql("update [Notification].[NotificationRecipients] set IsTo = 0 ,IsCC =1 where NotificationRecipientID = 73");
            migrationBuilder.Sql("update [Notification].[NotificationRecipients] set IsTo = 1 ,IsCC =0  where NotificationRecipientID = 74");
            migrationBuilder.Sql("update [Notification].[NotificationRecipients] set IsTo = 1 ,IsCC =0  where NotificationRecipientID = 75");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update [Notification].[NotificationRecipients] set IsTo = 1 ,IsCC =0  where NotificationRecipientID = 72");
            migrationBuilder.Sql("update [Notification].[NotificationRecipients] set IsTo = 1 ,IsCC =0 where NotificationRecipientID = 73");
            migrationBuilder.Sql("update [Notification].[NotificationRecipients] set IsTo = 0 ,IsCC =1  where NotificationRecipientID = 74");
            migrationBuilder.Sql("update [Notification].[NotificationRecipients] set IsTo = 0 ,IsCC =1  where NotificationRecipientID = 75");
        }
    }
}
