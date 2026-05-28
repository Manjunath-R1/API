using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class seeding_NotifocationMode_RolePermissionForNotifocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[NotificationModes] ON");
            migrationBuilder.Sql("Insert [Master].[NotificationModes]([ID],[ModeType],[IsActive]) Values(1,'Email',1)");
            migrationBuilder.Sql("Insert [Master].[NotificationModes]([ID],[ModeType],[IsActive]) Values(2,'Text',1)");
            migrationBuilder.Sql("Insert [Master].[NotificationModes]([ID],[ModeType],[IsActive]) Values(3,'Both',1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[NotificationModes] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Action] ON");
            migrationBuilder.Sql("Insert [Master].[Action] ([ActionID], [Name], [Description], [IsActive], [DisplayOrder]) values (74, 'SaveNotificationMode', 'Save NotificationMode', 1, 74)");
            migrationBuilder.Sql("Insert [Master].[Action] ([ActionID], [Name], [Description], [IsActive], [DisplayOrder]) values (75, 'GetNotificationModeByUser', 'Get NotificationMode By User', 1, 75)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Action] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[RolePermission] ON");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (313, 2, 74, 'Contact', 1, 1, 313)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (314, 2, 75, 'Contact', 1, 1, 314)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[RolePermission] OFF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[RolePermission]  where RolePermissionID between 313 and 314 ");
            migrationBuilder.Sql("delete from [Master].[ActionID]  where ActionID between 74 and 75 ");
            migrationBuilder.Sql("delete from [Master].[NotificationModes]  where ID between 1 and 3 ");

        }
    }
}
