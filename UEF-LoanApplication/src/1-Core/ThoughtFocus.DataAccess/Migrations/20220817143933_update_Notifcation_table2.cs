using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class update_Notifcation_table2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [Body] =N'<table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''><tr> <td class=''bodycopy''>Borrower has requested for further payment for grant application @LoanNumber.</td></tr><tr><td class=''bodycopy''> <b> Business Name : @BusinessName .</b> </td></tr><tr>@CallBackURL</tr><tr><td class=''bodycopy''>Sincerely,<br />@RecipientFullName</td></tr>' WHERE [NotificationID] = 30");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
