using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class DML_AdditionalMessagesSeedingForEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[EmailTemplatePlaceholders] ON ");
            migrationBuilder.Sql("INSERT [Notification].[EmailTemplatePlaceholders] ([PlaceholderID],[DisplayName],[Placeholder],[Description],[PlaceHolderTypeID],[CreatedDateTime],[CreatedByUserID],[LastModifiedDateTime],[LastModifiedByUserID],[IsActive]) 	VALUES (6, N'@AdditionalMessage',N'AdditionalMessage',N'Additional Message', 4, N'2021-04-02 15:46:41.080',1,N'2021-04-02 15:46:41.080',1,1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[EmailTemplatePlaceholders] OFF");
			
			migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[NotificationRecipients] ON ");
            migrationBuilder.Sql("INSERT [Notification].[NotificationRecipients] ([NotificationRecipientID],[ActivityNotificationID],[PlaceholderID],[IsTo],[IsCC],[IsActive],[CreatedDateTime],[CreatedByUserID],[LastModifiedDateTime],[LastModifiedByUserID]) VALUES (16, 6, 3, 1, 0, 1, N'2021-04-02 12:49:45.760',1,N'2021-04-02 12:49:45.760',1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[NotificationRecipients] OFF ");
			
            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [Body] =N'<table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''><tr> <td class=''bodycopy''>The status has been changed from <b>@CurrentStatus</b> to <b>@NextStatus</b>.<br /><b>@AdditionalMessage</b><br /></td></tr><tr><td class=''bodycopy''>Sincerely,<br />NUL.</td></tr>' WHERE [NotificationID] = 3");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Notification].[EmailTemplatePlaceholders]  where PlaceholderID = 6 ");
            migrationBuilder.Sql("delete from [Notification].[NotificationRecipients]  where NotificationRecipientID =16");
        
        }
    }
}
