using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class update_Notifcation_table1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [MessageSubject] =N'Grant application is Approved' WHERE [NotificationID] = 4");
            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [MessageSubject] =N'Grant application submitted successfully' WHERE [NotificationID] = 6");
            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [MessageSubject] =N'Grant application accepted' WHERE [NotificationID] = 9");
            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [MessageSubject] =N'Grant application approved' WHERE [NotificationID] = 10");
            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [MessageSubject] =N'Grant application rejected' WHERE [NotificationID] = 11");
            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [MessageSubject] =N'CFO approved grant application' WHERE [NotificationID] = 14");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
