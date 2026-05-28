using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class add_role_permission_civicrmscenarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Action] ON ");
            migrationBuilder.Sql("Insert [Master].[Action] ([ActionID], [Name], [Description], [IsActive], [DisplayOrder]) values (69, 'ExportCiviCRMData', 'Export CiviCRMData', 1, 69)");
            migrationBuilder.Sql("Insert [Master].[Action] ([ActionID], [Name], [Description], [IsActive], [DisplayOrder]) values (70, 'GetCiviCRMDataExportLog', 'Get CiviCRMDataExportLog', 1, 70)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Action] OFF ");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[RolePermission] ON ");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (279, 1, 69, 'Admin', 1, 1, 279)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (280, 3, 69, 'Admin', 1, 1, 280)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (281, 7, 69, 'Admin', 1, 1, 281)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (282, 1, 70, 'Admin', 1, 1, 282)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (283, 3, 70, 'Admin', 1, 1, 283)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (284, 7, 70, 'Admin', 1, 1, 284)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[RolePermission] OFF ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[Action]  where ActionID between 69 and 70");
            migrationBuilder.Sql("delete from [Master].[RolePermission]  where RolePermissionID between 279 and 284");
        }
    }
}
