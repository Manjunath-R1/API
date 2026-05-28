using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class add_role_permission__businessEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].RolePermission ON ");


            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (439, 4, 10, 'Admin', 1, 1, 439)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (440, 6, 10, 'Admin', 1, 1, 440)");


            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (441, 5, 81, 'FundingSource', 1, 1, 441)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (442, 8, 81, 'FundingSource', 1, 1, 442)");

            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (443, 5, 82, 'FundingSource', 1, 1, 443)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (444, 8, 82, 'FundingSource', 1, 1, 444)");

            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (414, 4, 20, 'Master', 1, 1, 414)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (415, 6, 20, 'Master', 1, 1, 415)");


            migrationBuilder.Sql("SET IDENTITY_INSERT Master.RolePermission OFF ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[RolePermission]  where RolePermissionID in (439,440,441,442,443,444,414,415)");
        }
    }
}
