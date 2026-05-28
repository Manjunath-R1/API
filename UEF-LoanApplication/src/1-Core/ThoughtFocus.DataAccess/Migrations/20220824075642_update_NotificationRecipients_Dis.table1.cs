using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class update_NotificationRecipients_Distable1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update [Notification].[NotificationRecipients]  set IsCC = 1 where  ActivityNotificationID = 16 and NotificationRecipientID = 19");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
