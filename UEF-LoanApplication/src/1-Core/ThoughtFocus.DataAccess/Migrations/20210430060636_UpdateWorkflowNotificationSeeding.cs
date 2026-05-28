using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class UpdateWorkflowNotificationSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [Body] =N'<table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''><tr> <td class=''bodycopy''>The status has been changed from <b>@CurrentStatus</b> to <b>@NextStatus</b>.<br /><br /></td></tr><tr><td class=''bodycopy''>Sincerely,<br />NUL.</td></tr>' WHERE [NotificationID] = 3");

            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [Body] = N'<table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''><tr> <td class=''bodycopy''>Kindly find the attached documents.<br /><br /></td></tr><tr><td class=''bodycopy''>Sincerely,<br />NUL.</td></tr>' WHERE [NotificationID] = 4");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
