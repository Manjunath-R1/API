using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class edit_Email_Notification_Modification_table1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [Salutation] =N'<table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''> <tr> <td class=''bodycopy''> Dear @RecipientFullName, <br /><br /> </td> </tr> </table>' WHERE [NotificationID] = 22");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
