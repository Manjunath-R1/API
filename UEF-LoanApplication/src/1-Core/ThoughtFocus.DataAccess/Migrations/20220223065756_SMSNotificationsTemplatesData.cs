using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class SMSNotificationsTemplatesData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[SMSNotificationTemplate] ON");
            migrationBuilder.Sql("insert into [master].[SMSNotificationTemplate](ID, Template, IsActive, ApplicationStatusID) values(1, N'Your application <#REF No> for <Program Name> has been successfully submitted', 1, 4)");
            migrationBuilder.Sql("insert into [master].[SMSNotificationTemplate] (ID, Template, IsActive, ApplicationStatusID) values(2, N'Your application <#REF No> for <Program Name> requires additional details for further processing. Please provide requested details sent to your registered email id', 1, 5)");
            migrationBuilder.Sql("insert into [master].[SMSNotificationTemplate] (ID, Template, IsActive, ApplicationStatusID) values(3, N'Your application <#REF No> for <Program Name> has been accepted', 1, 7)");
            migrationBuilder.Sql("insert into [master].[SMSNotificationTemplate] (ID, Template, IsActive, ApplicationStatusID) values(4, N'Congratulations! Your application <#REF No> for <Program Name> has been approved. Please proceed with agreement acceptance', 1, 8)");
            migrationBuilder.Sql("insert into [master].[SMSNotificationTemplate] (ID, Template, IsActive, ApplicationStatusID) values(5, N'Your application <#REF No> for <Program Name> has been rejected', 1, 9)");
            migrationBuilder.Sql("insert into [master].[SMSNotificationTemplate] (ID, Template, IsActive, ApplicationStatusID) values(6, N'Congratulations! The funds for the application <#REF No> for <Program Name> has been disbursed to your account', 1, 13)");
            migrationBuilder.Sql("insert into [master].[SMSNotificationTemplate] (ID, Template, IsActive, ApplicationStatusID) values(7, N'Your application <#REF No> for <Program Name> requires additional details for further processing. Please provide requested details sent to your registered email id', 1, 16)");
            migrationBuilder.Sql("insert into [master].[SMSNotificationTemplate] (ID, Template, IsActive, ApplicationStatusID) values(8, N'Your application <#REF No> for <Program Name> requires additional details for further processing. Please provide requested details sent to your registered email id', 1, 21)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[SMSNotificationTemplate] OFF");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[SMSNotificationTemplate] where ID between 1 and 8");

        }
    }
}
