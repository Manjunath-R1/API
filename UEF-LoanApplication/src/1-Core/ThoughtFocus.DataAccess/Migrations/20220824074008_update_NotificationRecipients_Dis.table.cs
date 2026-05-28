using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class update_NotificationRecipients_Distable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update [Notification].[NotificationRecipients]  set IsCC = 0 where  ActivityNotificationID = 16");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
