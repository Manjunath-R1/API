using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class edit_Email_Notification_Modification_table14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [Body] =N'<table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''><tr> <td class=''bodycopy''>This is to notify pending disbursement to be executed for the application <b>@LoanNumber</b>.</td></tr><tr><td class=''bodycopy''> <b> Business Name : @BusinessName .</b> </td></tr><tr>@CallBackURL</tr><tr><td class=''bodycopy''>Sincerely,<br />NUL</td></tr>' WHERE [NotificationID] = 29");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
