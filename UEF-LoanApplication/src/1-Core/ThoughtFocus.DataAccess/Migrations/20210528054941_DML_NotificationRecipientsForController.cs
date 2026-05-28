using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class DML_NotificationRecipientsForController : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[EmailTemplatePlaceholders] ON ");
            migrationBuilder.Sql("INSERT [Notification].[EmailTemplatePlaceholders] ([PlaceholderID],[DisplayName],[Placeholder],[Description],[PlaceHolderTypeID],[CreatedDateTime],[CreatedByUserID],[LastModifiedDateTime],[LastModifiedByUserID],[IsActive]) 	VALUES (11, N'@Controller',N'Controller',N'Controller', 4, N'2021-04-02 15:46:41.080',1,N'2021-04-02 15:46:41.080',1,1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[EmailTemplatePlaceholders] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[NotificationRecipients] ON ");
            migrationBuilder.Sql("INSERT [Notification].[NotificationRecipients] ([NotificationRecipientID],[ActivityNotificationID],[PlaceholderID],[IsTo],[IsCC],[IsActive],[CreatedDateTime],[CreatedByUserID],[LastModifiedDateTime],[LastModifiedByUserID]) VALUES (57, 25, 11, 1, 0, 1, N'2021-04-02 12:49:45.760',1,N'2021-04-02 12:49:45.760',1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[NotificationRecipients] OFF ");

            migrationBuilder.Sql("update Notification.NotificationRecipients set IsCC = 1, IsTo = 0 where NotificationRecipientID IN(51, 52)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update Notification.NotificationRecipients set IsCC = 0, IsTo = 1 where NotificationRecipientID IN(51, 52)");
            migrationBuilder.Sql("delete from Notification.EmailTemplatePlaceholders where PlaceholderID = 11");
            migrationBuilder.Sql("delete from Notification.NotificationRecipients where NotificationRecipientID = 57");
        }
    }
}
