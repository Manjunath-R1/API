using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class edit_Email_Notification_Modification_table8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [Body] =N'<table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''> <tr> <td class=''bodycopy''>The UW has accepted the application <b>@LoanNumber</b>. </br> Please proceed with creation of Schedule of Payment Agreement for this Business.</b></td> </tr> <tr> <td class=''bodycopy''> <b> Business Name : @BusinessName</b> </td> </tr> <tr>@CallBackURL</tr> <tr> <td class=''bodycopy''>Sincerely,<br />NUL</td> </tr>' WHERE [NotificationID] = 22");

            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [Body] =N'<table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''><tr> <td class=''bodycopy''>Congratulations! The @Disbursement of funds for the application <b>@LoanNumber</b> has been disbursed to your account.</td></tr><tr><td class=''bodycopy''> <b> Business Name : @BusinessName</b> </td></tr><tr>@CallBackURL</tr><tr><td class=''bodycopy''>Sincerely,<br />NUL</td></tr>' WHERE [NotificationID] = 25");
            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [MessageSubject] = N'Schedule of Payment Agreement Updated' WHERE [NotificationID] = 23");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
