using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class DML_AddingNotifiationRecipientsForProgramInvitation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[NotificationRecipients] ON ");
            migrationBuilder.Sql("INSERT [Notification].[NotificationRecipients] ([NotificationRecipientID],[ActivityNotificationID],[PlaceholderID],[IsTo],[IsCC],[IsActive],[CreatedDateTime],[CreatedByUserID],[LastModifiedDateTime],[LastModifiedByUserID]) VALUES (21,2,2,0,1,1, N'2021-05-15 12:49:45.760',1,N'2021-05-15 12:49:45.760',1)");
            migrationBuilder.Sql("INSERT [Notification].[NotificationRecipients] ([NotificationRecipientID],[ActivityNotificationID],[PlaceholderID],[IsTo],[IsCC],[IsActive],[CreatedDateTime],[CreatedByUserID],[LastModifiedDateTime],[LastModifiedByUserID]) VALUES (22,2,8,0,1,1, N'2021-05-15 12:49:45.760',1,N'2021-05-15 12:49:45.760',1)");
            migrationBuilder.Sql("INSERT [Notification].[NotificationRecipients] ([NotificationRecipientID],[ActivityNotificationID],[PlaceholderID],[IsTo],[IsCC],[IsActive],[CreatedDateTime],[CreatedByUserID],[LastModifiedDateTime],[LastModifiedByUserID]) VALUES (23,16,2,0,1,1, N'2021-05-15 12:49:45.760',1,N'2021-05-15 12:49:45.760',1)");
            migrationBuilder.Sql("INSERT [Notification].[NotificationRecipients] ([NotificationRecipientID],[ActivityNotificationID],[PlaceholderID],[IsTo],[IsCC],[IsActive],[CreatedDateTime],[CreatedByUserID],[LastModifiedDateTime],[LastModifiedByUserID]) VALUES (24,16,8,0,1,1, N'2021-05-15 12:49:45.760',1,N'2021-05-15 12:49:45.760',1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[NotificationRecipients] OFF ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Notification].[NotificationRecipients]  where NotificationRecipientID between 21 and 24");
        }
    }
}
