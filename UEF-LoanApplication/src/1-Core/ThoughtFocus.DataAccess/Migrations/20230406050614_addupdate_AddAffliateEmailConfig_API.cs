using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class addupdate_AddAffliateEmailConfig_API : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[NotificationRecipients] ON");
            migrationBuilder.Sql("INSERT [Notification].[NotificationRecipients] ([NotificationRecipientID], [ActivityNotificationID], [PlaceholderID], [IsTo], [IsCC], [IsActive], [CreatedDateTime], [CreatedByUserID], [LastModifiedDateTime], [LastModifiedByUserID]) VALUES (189, 61, 9, 0, 1, 1, CAST(N'2023-03-30 11:29:30.537' AS DateTime2), 1, CAST(N'2023-03-30 11:29:30.537' AS DateTime2), 1)");


            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[NotificationRecipients] OFF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
