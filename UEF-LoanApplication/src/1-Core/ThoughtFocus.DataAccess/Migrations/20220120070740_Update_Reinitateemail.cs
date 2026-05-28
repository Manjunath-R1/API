using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class Update_Reinitateemail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update [Notification].[NotificationRecipients]  set PlaceholderID = 7 where  NotificationRecipientID = 98");
            migrationBuilder.Sql("update [Notification].[NotificationRecipients]  set PlaceholderID = 8 where  NotificationRecipientID = 100");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           migrationBuilder.Sql("update [Notification].[NotificationRecipients]  set PlaceholderID = 8 where  NotificationRecipientID = 98");
            migrationBuilder.Sql("update [Notification].[NotificationRecipients]  set PlaceholderID = 11 where  NotificationRecipientID = 100");

        }
    }
}
