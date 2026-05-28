using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class seeding_NotificationRecipients_NotificationTable_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[NotificationRecipients] ON ");
            migrationBuilder.Sql("INSERT [Notification].[NotificationRecipients] ([NotificationRecipientID],[ActivityNotificationID],[PlaceholderID],[IsTo],[IsCC],[IsActive],[CreatedDateTime],[CreatedByUserID],[LastModifiedDateTime],[LastModifiedByUserID])VALUES (60,28,2,0,1,1, N'2021-04-02 12:49:45.760',1,N'2021-04-02 12:49:45.760',1)");
            migrationBuilder.Sql("INSERT [Notification].[NotificationRecipients] ([NotificationRecipientID],[ActivityNotificationID],[PlaceholderID],[IsTo],[IsCC],[IsActive],[CreatedDateTime],[CreatedByUserID],[LastModifiedDateTime],[LastModifiedByUserID])VALUES (61,28,8,0,1,1, N'2021-04-02 12:49:45.760',1,N'2021-04-02 12:49:45.760',1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[NotificationRecipients] OFF");

            migrationBuilder.Sql(" update [Master].[Notification] set body='<table width=''100%'' border=''0'' cellspacing=''3'' cellpadding=''0''><tr> <td class=''bodycopy''> Admin has initiated request to reset the access for this account. <br/> Please click on the button below to proceed</td><br/><td style=''padding: 20px 0 20px 100px''>@CallBackURL</td></tr><tr><td class=''bodycopy''><br/>Note : This link will be active for 24 hours from the time it''s sent<br/><br/></td></tr><tr><td class=''bodycopy''>Sincerely,<br />NUL<br/><br/></td></tr>',MessageSubject= 'Password Reset Access',Salutation ='<table width=''100%''border=''0'' cellspacing=''3'' cellpadding=''0''> <tr> <td class=''bodycopy''> Dear @RecipientFullName,</td> </tr> </table>' where NotificationID = 17");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Notification].[NotificationRecipients]  where NotificationRecipientID between 60 and 61");

        }
    }
}
