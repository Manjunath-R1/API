using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class Emails_seedingFor_NotificationRecipients_UpdateFor_Notification_UW_RMI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[NotificationRecipients] ON ");
            migrationBuilder.Sql("INSERT [Notification].[NotificationRecipients] ([NotificationRecipientID], [ActivityNotificationID], [PlaceholderID], [IsTo], [IsCC], [IsActive], [CreatedDateTime], [CreatedByUserID], [LastModifiedDateTime], [LastModifiedByUserID]) VALUES (101, 34, 7, 0, 1, 1, CAST(N'2022-01-01T12:49:45.7600000' AS DateTime2), 1, CAST(N'2022-01-01T12:49:45.7600000' AS DateTime2), 1)");
            migrationBuilder.Sql("INSERT [Notification].[NotificationRecipients] ([NotificationRecipientID], [ActivityNotificationID], [PlaceholderID], [IsTo], [IsCC], [IsActive], [CreatedDateTime], [CreatedByUserID], [LastModifiedDateTime], [LastModifiedByUserID]) VALUES (102, 34, 11, 0, 1, 1, CAST(N'2022-01-01T12:49:45.7600000' AS DateTime2), 1, CAST(N'2022-01-01T12:49:45.7600000' AS DateTime2), 1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[NotificationRecipients] OFF");

            migrationBuilder.Sql("update Master.Notification set Body = '<table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''><tr> <td class=''bodycopy''>Additional information is accepted and ready for further review.</td></tr><tr><td class=''bodycopy''><b>Business Name : @BusinessName.</b></td></tr><tr>@CallBackURL</tr><tr><td class=''bodycopy''>Sincerely,<br />NUL</td></tr>'where NotificationID = 19");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Notification].[NotificationRecipients]  where NotificationRecipientID  between 101 and 102");
            migrationBuilder.Sql("update Master.Notification set Body = '<table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''><tr> <td class=''bodycopy''>Additional information accepted and ready for further review.</td></tr><tr><td class=''bodycopy''><b>Business Name : @BusinessName.</b></td></tr><tr>@CallBackURL</tr><tr><td class=''bodycopy''>Sincerely,<br />NUL</td></tr>'where NotificationID = 19");


        }
    }
}
