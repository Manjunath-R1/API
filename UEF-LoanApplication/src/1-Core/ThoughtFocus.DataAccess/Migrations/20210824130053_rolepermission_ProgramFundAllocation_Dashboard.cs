using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class rolepermission_ProgramFundAllocation_Dashboard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Action] ON ");
            migrationBuilder.Sql("Insert [Master].[Action] ([ActionID], [Name], [Description], [IsActive], [DisplayOrder]) values (49, 'GetProgramFundAllocation', 'Get ProgramFundAllocation', 1, 49)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Action] OFF ");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[RolePermission] ON ");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (206, 1, 49, 'Dashboard', 1, 1, 206)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (207, 3, 49, 'Dashboard', 1, 1, 207)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (208, 7, 49, 'Dashboard', 1, 1, 208)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (209, 8, 49, 'Dashboard', 1, 1, 209)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (210, 4, 49, 'Dashboard', 1, 1, 210)");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[RolePermission] OFF ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.Sql("delete from [Master].[Action]  where ActionID = 49");
             migrationBuilder.Sql("delete from [Master].[RolePermission]  where RolePermissionID between 206 and 210");

        }
    }
}
