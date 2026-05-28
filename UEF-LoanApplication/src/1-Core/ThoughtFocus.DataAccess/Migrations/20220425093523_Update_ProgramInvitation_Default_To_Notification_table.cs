using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class Update_ProgramInvitation_Default_To_Notification_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [Body] =N'<p> Dear @RecipientFullName,  </p><p></p><p class=''bodycopy''> The National Urban League invites @BusinessName to submit a funding application for consideration. This request is confidential and is prepared for the sole use of your business and should not be disputed to other businesses. Several items will need to be attached during the application submission process. Required attachments will include an electronic or scanned copy of your:  </p><ul><li>Form W-9 - Request for Taxpayer Identification Number and Certification</li><li>Certificate of Good Standing (*not required for sole proprietorships)</li><li>Proof of Ownership</li><li>ACH Vendor Form</li></ul><p class=''bodycopy'' style=''display: none''> <br /> @CallBackURL</p><p class=''bodycopy'' style=''display: none''> <br /> Your request will be reviewed promptly, and you can expect a formal response to your application within @TimeFrame. If you need additional information or have any questions, please contact Stephanie DeVane via email @EmailAddress. We look forward to receiving your application. <br /></p>' WHERE [NotificationID] = 2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
