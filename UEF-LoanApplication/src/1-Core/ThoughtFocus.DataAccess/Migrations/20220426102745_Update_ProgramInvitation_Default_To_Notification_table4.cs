using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class Update_ProgramInvitation_Default_To_Notification_table4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [Body] =N'<p>Dear @RecipientFullName, </p><p class=''bodycopy''><br></p><p class=''bodycopy''>The National Urban League invites @BusinessName to submit a funding application for consideration. This request is confidential and is prepared for the sole use of your business and should not be distributed to other businesses. Several items will need to be attached during the application submission process. Required attachments will include an electronic or scanned copy of your: </p><p class=''bodycopy''><br></p><ul><li>Form W-9 - Request for Taxpayer Identification Number and Certification</li><li>Certificate of Good Standing (*not required for sole proprietorships)</li><li>Proof of Ownership</li><li>ACH Vendor Form</li><li>If this is for loans and grants, there will likely be other documents required such as financial statements</li></ul><p><br></p><p id=''callBackURL'' class=''bodycopy'' style=''display: none''>@CallBackURL </p>' WHERE [NotificationID] = 2");
            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [Footer] =N'<section class=''Emailfooter'' width=''100%'' border=''0'' ><p align=''center'' style=''background:#44525f;padding:15.0pt 22.5pt 11.25pt 22.5pt;color:#ffffff'' class=''footercopy''>© @CurrentYear, NUL. All rights reserved.<br /></p></section>' WHERE [NotificationID] = 2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
