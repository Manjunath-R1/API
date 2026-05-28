using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class rolepermission_Get_AffiliateApplicationStatus_FundAllocationStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Action] ON ");
            migrationBuilder.Sql("Insert [Master].[Action] ([ActionID], [Name], [Description], [IsActive], [DisplayOrder]) values (53, 'GetAffiliateFundAllocation', 'Get AffiliateFundAllocation', 1, 53)");
            migrationBuilder.Sql("Insert [Master].[Action] ([ActionID], [Name], [Description], [IsActive], [DisplayOrder]) values (54, 'GetAffiliateApplicationStatus', 'Get AffiliateApplicationStatus', 1, 54)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Action] OFF ");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[RolePermission] ON ");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (226, 1, 53, 'Dashboard', 1, 1, 226)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (227, 3, 53, 'Dashboard', 1, 1, 227)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (228, 7, 53, 'Dashboard', 1, 1, 228)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (229, 8, 53, 'Dashboard', 1, 1, 229)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (230, 4, 53, 'Dashboard', 1, 1, 230)");

            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (231, 1, 54, 'Dashboard', 1, 1, 231)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (232, 3, 54, 'Dashboard', 1, 1, 232)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (233, 7, 54, 'Dashboard', 1, 1, 233)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (234, 8, 54, 'Dashboard', 1, 1, 234)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (235, 4, 54, 'Dashboard', 1, 1, 235)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[RolePermission] OFF ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[Action]  where ActionID between 53 and 54");
            migrationBuilder.Sql("delete from [Master].[RolePermission]  where RolePermissionID between 226 and 235");

        }
    }
}
