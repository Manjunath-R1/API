using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class add_role_permission__Affiliate_and_deletebusiness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        migrationBuilder.Sql("SET IDENTITY_INSERT Master.Action ON ");
        migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (42, 'GetAllAffiliates', 'Get AllAffiliates', 1, 42)");
        migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (43, 'GetAffiliate', 'Get Affiliate', 1, 43)");
        migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (44, 'SaveOrUpdateAffiliate', 'Save/UpdateAffiliate', 1, 44)");
        migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (45, 'DeleteBusinessEntity', 'Delete BusinessEntity', 1, 45)");
        migrationBuilder.Sql("SET IDENTITY_INSERT Master.Action OFF ");
            
        migrationBuilder.Sql("SET IDENTITY_INSERT [Master].RolePermission ON ");
        migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (156, 1, 42, 'Admin', 1, 1, 156)");
        migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (157, 3, 42, 'Admin', 1, 1, 157)");
        migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (158, 7, 42, 'Admin', 1, 1, 158)");

        migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (159, 1, 43, 'Admin', 1, 1, 159)");
        migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (160, 3, 43, 'Admin', 1, 1, 160)");
        migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (161, 7, 43, 'Admin', 1, 1, 161)");

        migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (162, 1, 44, 'Admin', 1, 1, 162)");
        migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (163, 3, 44, 'Admin', 1, 1, 163)");
        migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (164, 7, 44, 'Admin', 1, 1, 164)");


        migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (165, 1, 45, 'Admin', 1, 1, 165)");
        migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (166, 3, 45, 'Admin', 1, 1, 166)");
        migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (167, 7, 45, 'Admin', 1, 1, 167)");
        migrationBuilder.Sql("SET IDENTITY_INSERT Master.RolePermission OFF ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[Action]  where ActionID in (42,43,44,45)");

			migrationBuilder.Sql("delete from [Master].[RolePermission]  where RolePermissionID between 156 and 167");

        }
    }
}
