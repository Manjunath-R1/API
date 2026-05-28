using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class add_role_permission_Unlockaccount_NotificationTable_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Action] ON ");
            migrationBuilder.Sql("Insert [Master].[Action] ([ActionID], [Name], [Description], [IsActive], [DisplayOrder]) values (48, 'Unlockaccount', 'Unlock account', 1, 48)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Action] OFF ");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[RolePermission] ON ");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (174, 1, 48, 'Contact', 1, 1, 174)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (175, 3, 48, 'Contact', 1, 1, 175)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (176, 7, 48, 'Contact', 1, 1, 176)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[RolePermission] OFF ");

            migrationBuilder.Sql(" update [Master].[Notification] set body='<table width=''100%'' border=''0'' cellspacing=''3'' cellpadding=''0''><tr> <td class=''bodycopy''> Admin has initiated request to reset the access for this account. <br /> Please click on the button below to proceed<br /><br /></td></tr><tr><td >@CallBackURL</td></tr><tr><td class=''bodycopy''><br/>Note : This link will be activate for 24 hours from the time it''s sent<br/><br/></td></tr><tr><td class=''bodycopy''>Sincerely,<br />NUL</td></tr>',MessageSubject= 'Password Reset Access' where NotificationID = 17");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[Action]  where ActionID = 48");
            migrationBuilder.Sql("delete from [Master].[RolePermission]  where RolePermissionID between 174 and 176");

        }
    }
}
