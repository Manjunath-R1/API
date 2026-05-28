using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class edit_Email_Notification_Modification_table3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [Body] =N'<table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''><tr> <td class=''bodycopy''>The National Urban League requests the following information to complete your application.<br /><br /> <b>Notes : @AdditionalMessage</b>.</td></tr><tr><td class=''bodycopy''> <b> Business Name : @BusinessName .</b> </td></tr><tr>@CallBackURL</tr><tr><td class=''bodycopy''>Sincerely,<br />NUL</td></tr>' WHERE [NotificationID] = 7");
            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [Body] =N'<table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''><tr> <td class=''bodycopy''>The National Urban League requests the following information to complete your application.<br /><br /> <b>Notes : @AdditionalMessage</b>.</td></tr><tr><td class=''bodycopy''> <b> Business Name : @BusinessName .</b> </td></tr><tr>@CallBackURL</tr><tr><td class=''bodycopy''>Sincerely,<br />NUL</td></tr>' WHERE [NotificationID] = 21");

            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [Body] =N'<table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''><tr> <td class=''bodycopy''>The borrower has submitted the additional information requested.<br /><br /> <b>Notes : @AdditionalMessage</b>.</td></tr><tr><td class=''bodycopy''> <b> Business Name : @BusinessName .</b> </td></tr><tr>@CallBackURL</tr><tr><td class=''bodycopy''>Sincerely,<br />NUL</td></tr>' WHERE [NotificationID] = 8");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
