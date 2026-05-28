using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class edit_Email_Notification_Modification_table2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [Body] =N'<table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''> <tr> <td class=''bodycopy''>The UW has accepted the application @LoanNumber. </br> Please proceed with creation of Schedule of Payment Agreement for this Business.</b></td> </tr> <tr> <td class=''bodycopy''> <b> Business Name : @BusinessName</b> </td> </tr> <tr>@CallBackURL</tr> <tr> <td class=''bodycopy''>Sincerely,<br />NUL</td> </tr>' WHERE [NotificationID] = 22");
            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [Footer] =N'<td class=''Emailfooter''> <table width=''100%'' border=''0'' cellspacing=''0'' cellpadding=''0''> <tr> <td align=''center'' style=''background:#44525f;padding:15.0pt 22.5pt 11.25pt 22.5pt;color:#ffffff'' class=''footercopy''>© @CurrentYear, NUL. All rights reserved.<br /></td> </tr> </table> </td> </table> </td> </tr> </table> </td> </tr> </table></body></html>' WHERE [NotificationID] = 22");

            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [Body] =N'<table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''><tr> <td class=''bodycopy''>Schedule of Payment Agreement for this Business has been updated successfully.</td></tr><tr><td class=''bodycopy''> <b> Business Name : @BusinessName</b> </td></tr><tr>@CallBackURL</tr><tr><td class=''bodycopy''>Sincerely,<br />NUL</td></tr>' WHERE [NotificationID] = 23");
            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [Body] =N'<table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''><tr> <td class=''bodycopy''>Please update the Schedule of Payment for this Business Entity under Funding Details section.</td></tr><tr><td class=''bodycopy''> <b> Business Name : @BusinessName</b> </td></tr><tr>@CallBackURL</tr><tr><td class=''bodycopy''>Sincerely,<br />NUL</td></tr>' WHERE [NotificationID] = 24");
            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [Body] =N'<table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''><tr> <td class=''bodycopy''>Congratulations! The @Disbursement of funds for the application @LoanNumber has been disbursed to your account.</td></tr><tr><td class=''bodycopy''> <b> Business Name : @BusinessName</b> </td></tr><tr>@CallBackURL</tr><tr><td class=''bodycopy''>Sincerely,<br />NUL</td></tr>' WHERE [NotificationID] = 25");
            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [Body] =N'table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''><tr> <td class=''bodycopy''>The National Urban League has requested you to upload Progress Report to proceed with additional funding.</td></tr><tr><td class=''bodycopy''> <b> Business Name : @BusinessName .</b> </td></tr><tr>@CallBackURL</tr><tr><td class=''bodycopy''>Sincerely,<br />NUL</td></tr>' WHERE [NotificationID] = 26");

            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [Body] =N'<table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''><tr> <td class=''bodycopy''>The National Urban League requests the following information to complete your application.<br /><br /> <b>NOTES : @AdditionalMessage</b>.</td></tr><tr><td class=''bodycopy''> <b> Business Name : @BusinessName .</b> </td></tr><tr>@CallBackURL</tr><tr><td class=''bodycopy''>Sincerely,<br />NUL</td></tr>' WHERE [NotificationID] = 7");
            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [Body] =N'<table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''><tr> <td class=''bodycopy''>The National Urban League requests the following information to complete your application.<br /><br /> <b>NOTES : @AdditionalMessage</b>.</td></tr><tr><td class=''bodycopy''> <b> Business Name : @BusinessName .</b> </td></tr><tr>@CallBackURL</tr><tr><td class=''bodycopy''>Sincerely,<br />NUL</td></tr>' WHERE [NotificationID] = 21");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
