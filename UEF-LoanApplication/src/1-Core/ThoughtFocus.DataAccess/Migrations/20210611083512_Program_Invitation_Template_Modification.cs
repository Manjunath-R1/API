using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class Program_Invitation_Template_Modification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [Body] =  N'<table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''> <tr> <td class=''bodycopy''> The National Urban League invites @BusinessName to submit a funding application for consideration. This request is confidential and is prepared for the sole use of your business and should not be distributed to other businesses. Several items will need to be attached during the application submission process. Required attachments will include an electronic or scanned copy of your: <br /> </td> </tr> <tr> <td class=''bodycopy''> <ul> <li> Form W-9 - Request for Taxpayer Identification Number and Certification </li> <li> Certificate of Good Standing (*not required for sole proprietorships) </li> <li> Proof of Ownership </li> <li> ACH Vendor Form </li> <li> If this is for loans and grants, there will likely be other documents required such as financial statements </li> </ul> </td> </tr> <tr> <td class=''bodycopy''> <br /> @CallBackURL </td> </tr> <tr> <td class=''bodycopy''> <br /> Your request will be reviewed promptly, and you can expect a formal response to your application within @TimeFrame. If you need additional information or have any questions, please contact Stephanie DeVane via email @EmailAddress. We look forward to receiving your application. <br /> </td> </tr> <tr> <td><br /> </td> </tr>' WHERE [NotificationID] = 2 ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [Body] =  N'<table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''> <tr> <td class=''bodycopy''> The National Urban League invites @BusinessName to submit a grant application for consideration. This request is confidential and is prepared for the sole use of your business and should not be distributed to other businesses. Several items will need to be attached during the application submission process. Required attachments will include an electronic or scanned copy of your: <br /> </td> </tr> <tr> <td class=''bodycopy''> <ul> <li> Form W-9 - Request for Taxpayer Identification Number and Certification </li> <li> Certificate of Good Standing (*not required for sole proprietorships) </li> <li> Proof of Ownership </li> <li> ACH Vendor Form </li> <li> If this is for loans and grants, there will likely be other documents required such as financial statements </li> </ul> </td> </tr> <tr> <td class=''bodycopy''> <br /> @CallBackURL </td> </tr> <tr> <td class=''bodycopy''> <br /> Your request will be reviewed promptly, and you can expect a formal response to your application within @TimeFrame. If you need additional information or have any questions, please contact Stephanie DeVane via email @EmailAddress. We look forward to receiving your application. <br /> </td> </tr> <tr> <td><br /> </td> </tr>' WHERE [NotificationID] = 2 ");
        }
    }
}
