using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class Update_NotificationModes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update Master.NotificationModes set ModeType = 'email' where id =1");
            migrationBuilder.Sql("update Master.NotificationModes set ModeType = 'sms' where id = 2");
            migrationBuilder.Sql("update Master.NotificationModes set ModeType = 'both' where id = 3");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update Master.NotificationModes set ModeType = 'Email' where id =1");
            migrationBuilder.Sql("update Master.NotificationModes set ModeType = 'Text' where id = 2");
            migrationBuilder.Sql("update Master.NotificationModes set ModeType = 'Both' where id = 3");
        }
    }
}
