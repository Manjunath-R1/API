using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class add_role_permission__ActivateContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         migrationBuilder.Sql("SET IDENTITY_INSERT Master.Action ON ");
        migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (46, 'ActivateContact', 'Activate Contact', 1, 46)");
        migrationBuilder.Sql("SET IDENTITY_INSERT Master.Action OFF ");
                    
        migrationBuilder.Sql("SET IDENTITY_INSERT [Master].RolePermission ON ");
        migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (168, 1, 46, 'Contact', 1, 1, 168)");
        migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (169, 3, 46, 'Contact', 1, 1, 169)");
        migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (170, 7, 46, 'Contact', 1, 1, 170)");

        migrationBuilder.Sql("SET IDENTITY_INSERT Master.RolePermission OFF ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[Action]  where ActionID = 46");
			migrationBuilder.Sql("delete from [Master].[RolePermission]  where RolePermissionID between 168 and 170");

        }
    }
}
