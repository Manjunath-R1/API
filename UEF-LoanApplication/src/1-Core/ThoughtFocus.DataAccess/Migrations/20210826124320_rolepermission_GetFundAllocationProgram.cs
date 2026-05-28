using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class rolepermission_GetFundAllocationProgram : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Action] ON ");
            migrationBuilder.Sql("Insert [Master].[Action] ([ActionID], [Name], [Description], [IsActive], [DisplayOrder]) values (50, 'GetFundAllocationProgram', 'Get FundAllocationProgram', 1, 50)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Action] OFF ");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[RolePermission] ON ");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (211, 1, 50, 'Dashboard', 1, 1, 211)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (212, 3, 50, 'Dashboard', 1, 1, 212)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (213, 7, 50, 'Dashboard', 1, 1, 213)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (214, 8, 50, 'Dashboard', 1, 1, 214)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (215, 4, 50, 'Dashboard', 1, 1, 215)");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[RolePermission] OFF ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[Action]  where ActionID = 50");
            migrationBuilder.Sql("delete from [Master].[RolePermission]  where RolePermissionID between 211 and 215");
        }
    }
}
