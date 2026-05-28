using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class seeding_RolePermissionTable_NULExecutiveRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].RolePermission ON ");

            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (177, 8, 1, 'LoanApplication', 1, 1, 177)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (178, 8, 2, 'LoanApplication', 1, 1, 178)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (179, 8, 3, 'LoanApplication', 1, 1, 179)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (180, 8, 4, 'LoanApplication', 1, 1, 180)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (181, 8, 5, 'LoanApplication', 1, 1, 181)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (182, 8, 6, 'LoanApplication', 1, 1, 182)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (183, 8, 7, 'Admin', 1, 1, 183)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (184, 8, 9, 'Admin', 1, 1, 184)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (185, 8, 10, 'Admin', 1, 1, 185)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (186, 8, 11, 'FundingSource', 1, 1, 186)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (187, 8, 15, 'FundingSource', 1, 1, 187)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (188, 8, 17, 'FundingSource', 1, 1, 188)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (189, 8, 18, 'FundingSource', 1, 1, 189)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (190, 8, 19, 'FundingSource', 1, 1, 190)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (191, 8, 20, 'Master', 1, 1, 191)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (192, 8, 21, 'Admin', 1, 1, 192)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (193, 8, 22, 'Admin', 1, 1, 193)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (194, 8, 24, 'Admin', 1, 1, 194)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (195, 8, 26, 'Contact', 1, 1, 195)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (196, 8, 27, 'Contact', 1, 1, 196)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (197, 8, 28, 'Contact', 1, 1, 197)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (198, 8, 29, 'Master', 1, 1, 198)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (199, 8, 33, 'LoanApplication', 1, 1, 199)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (200, 8, 35, 'Upload', 1, 1, 200)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (201, 8, 36, 'Contact', 1, 1, 201)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (202, 8, 40, 'Admin', 1, 1, 202)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (203, 8, 41, 'Admin', 1, 1, 203)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (204, 8, 42, 'Admin', 1, 1, 204)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (205, 8, 43, 'Admin', 1, 1, 205)");

            migrationBuilder.Sql("SET IDENTITY_INSERT Master.RolePermission OFF ");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[RolePermission]  where RolePermissionID between 177 and 205");
        }
    }
}
