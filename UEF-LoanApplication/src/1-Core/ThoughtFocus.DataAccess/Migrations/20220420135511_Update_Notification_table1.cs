using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class Update_Notification_table1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [Body] =N'<p width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''> <p> <p class=''bodycopy''> Dear @RecipientFullName, <br /><br /> </p> </p></p><p width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''> <p> <p class=''bodycopy''> The National Urban League invites @BusinessName to submit a funding application for consideration. This request is confidential and is prepared for the sole use of your business and should not be dispibuted to other businesses. Several items will need to be attached during the application submission process. Required attachments will include an elecponic or scanned copy of your: <br /> </p> </p> <p> <p class=''bodycopy''> <ul> <li> Form W-9 - Request for Taxpayer Identification Number and Certification </li> <li> Certificate of Good Standing (*not required for sole proprietorships) </li> <li> Proof of Ownership </li> <li> ACH Vendor Form </li> <li> If this is for loans and grants, there will likely be other documents required such as financial statements </li> </ul> </p> </p> <p> <p class=''bodycopy''> <br /> @CallBackURL </p> </p> <p> <p class=''bodycopy''> <br /> Your request will be reviewed promptly, and you can expect a formal response to your application within @TimeFrame. If you need additional information or have any questions, please contact Stephanie DeVane via email @EmailAddress. We look forward to receiving your application. <br /> </p> </p> <p> <p><br /> </p> </p> </p></p></p></p></p>' WHERE [NotificationID] = 2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
