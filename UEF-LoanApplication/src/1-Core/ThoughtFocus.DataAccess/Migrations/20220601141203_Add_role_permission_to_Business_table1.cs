using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class Add_role_permission_to_Business_table1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT Master.RolePermission ON ");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (437, 4, 21, 'Admin', 1, 1, 437)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (438, 6, 21, 'Admin', 1, 1, 438)");


            migrationBuilder.Sql("SET IDENTITY_INSERT Master.RolePermission OFF ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
