using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class add_role_permission__ResetContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT Master.Action ON ");
            migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (47, 'ResetContact', 'Reset Contact', 1, 47)");
            migrationBuilder.Sql("SET IDENTITY_INSERT Master.Action OFF ");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].RolePermission ON ");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (171, 1, 47, 'Contact', 1, 1, 171)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (172, 3, 47, 'Contact', 1, 1, 172)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (173, 7, 47, 'Contact', 1, 1, 173)");
            migrationBuilder.Sql("SET IDENTITY_INSERT Master.RolePermission OFF ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[Action]  where ActionID = 47");
            migrationBuilder.Sql("delete from [Master].[RolePermission]  where RolePermissionID between 171 and 173");
        }
    }
}
