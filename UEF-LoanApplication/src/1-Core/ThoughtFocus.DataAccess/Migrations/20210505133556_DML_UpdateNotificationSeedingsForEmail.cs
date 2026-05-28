using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class DML_UpdateNotificationSeedingsForEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[EmailTemplatePlaceholders] ON ");
            migrationBuilder.Sql("INSERT [Notification].[EmailTemplatePlaceholders] ([PlaceholderID],[DisplayName],[Placeholder],[Description],[PlaceHolderTypeID],[CreatedDateTime],[CreatedByUserID],[LastModifiedDateTime],[LastModifiedByUserID],[IsActive]) 	VALUES (7, N'@NULTreasury',N'NULTreasury',N'NUL Treasury', 4, N'2021-04-02 15:46:41.080',1,N'2021-04-02 15:46:41.080',1,1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[EmailTemplatePlaceholders] OFF");
			
			migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[NotificationRecipients] ON ");
            migrationBuilder.Sql("INSERT [Notification].[NotificationRecipients] ([NotificationRecipientID],[ActivityNotificationID],[PlaceholderID],[IsTo],[IsCC],[IsActive],[CreatedDateTime],[CreatedByUserID],[LastModifiedDateTime],[LastModifiedByUserID]) VALUES (17, 11, 7, 1, 0, 1, N'2021-04-02 12:49:45.760',1,N'2021-04-02 12:49:45.760',1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[NotificationRecipients] OFF ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Notification].[EmailTemplatePlaceholders]  where PlaceholderID = 7 ");
            migrationBuilder.Sql("delete from [Notification].[NotificationRecipients]  where NotificationRecipientID =17");
        }
    }
}
