using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class email_setting_seeding_for_LoanProcessor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[EmailTemplatePlaceholders] ON ");
            migrationBuilder.Sql("INSERT [Notification].[EmailTemplatePlaceholders] ([PlaceholderID],[DisplayName],[Placeholder],[Description],[PlaceHolderTypeID],[CreatedDateTime],[CreatedByUserID],[LastModifiedDateTime],[LastModifiedByUserID],[IsActive]) 	VALUES (12, N'@LoanProcessor',N'LoanProcessor',N'Loan Processor', 2, N'2021-04-02 15:46:41.080',1,N'2021-04-02 15:46:41.080',1,1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[EmailTemplatePlaceholders] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[NotificationRecipients] ON ");
            migrationBuilder.Sql("INSERT [Notification].[NotificationRecipients] ([NotificationRecipientID],[ActivityNotificationID],[PlaceholderID],[IsTo],[IsCC],[IsActive],[CreatedDateTime],[CreatedByUserID],[LastModifiedDateTime],[LastModifiedByUserID]) VALUES ( 62,17,12,0,1,1, N'2021-04-02 12:49:45.760',1,N'2021-04-02 12:49:45.760',1)");
            migrationBuilder.Sql("INSERT [Notification].[NotificationRecipients] ([NotificationRecipientID],[ActivityNotificationID],[PlaceholderID],[IsTo],[IsCC],[IsActive],[CreatedDateTime],[CreatedByUserID],[LastModifiedDateTime],[LastModifiedByUserID]) VALUES ( 63,18,12,0,1,1, N'2021-04-02 12:49:45.760',1,N'2021-04-02 12:49:45.760',1)");
            migrationBuilder.Sql("INSERT [Notification].[NotificationRecipients] ([NotificationRecipientID],[ActivityNotificationID],[PlaceholderID],[IsTo],[IsCC],[IsActive],[CreatedDateTime],[CreatedByUserID],[LastModifiedDateTime],[LastModifiedByUserID]) VALUES ( 64,19,12,1,0,1, N'2021-04-02 12:49:45.760',1,N'2021-04-02 12:49:45.760',1)");
            migrationBuilder.Sql("INSERT [Notification].[NotificationRecipients] ([NotificationRecipientID],[ActivityNotificationID],[PlaceholderID],[IsTo],[IsCC],[IsActive],[CreatedDateTime],[CreatedByUserID],[LastModifiedDateTime],[LastModifiedByUserID]) VALUES ( 65,20,12,0,1,1, N'2021-04-02 12:49:45.760',1,N'2021-04-02 12:49:45.760',1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[NotificationRecipients] OFF");

            migrationBuilder.Sql(" delete from WorkFlow.RestrictionDefinition where Transition_ID in (11,12,14) and ActorDefinitionIsInRole_ID=5");
           

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Notification].[EmailTemplatePlaceholders]  where PlaceholderID =12");
            migrationBuilder.Sql("delete from [Notification].[NotificationRecipients]  where NotificationRecipientID between 62 and 65");
        }
    }
}
