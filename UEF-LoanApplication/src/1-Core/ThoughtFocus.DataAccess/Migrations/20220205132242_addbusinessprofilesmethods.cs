using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class addbusinessprofilesmethods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT[Master].[Action] on ");
            migrationBuilder.Sql("Insert[Master].[Action] ([ActionID], [Name], [Description], [IsActive], [DisplayOrder]) values(71, 'SaveBusinessProfileData', 'Save Business Profile Data', 1, 71)");
            migrationBuilder.Sql("Insert[Master].[Action]([ActionID], [Name], [Description], [IsActive], [DisplayOrder]) values(72, 'GetBusinessProfileMasterData', 'Get Business Profile Master Data', 1, 72)");
            migrationBuilder.Sql("Insert[Master].[Action]([ActionID], [Name], [Description], [IsActive], [DisplayOrder]) values(73, 'GetAllBusinessEntityByUser', 'Get All BusinessEntity By User', 1, 73)");
            migrationBuilder.Sql("SET IDENTITY_INSERT[Master].[Action] OFF");


            migrationBuilder.Sql("SET IDENTITY_INSERT[Master].[RolePermission] on");
           
            migrationBuilder.Sql("Insert[Master].[RolePermission]([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values(289, 1, 71, 'LoanApplication', 1, 1, 289)");
            migrationBuilder.Sql("Insert[Master].[RolePermission]([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values(290, 2, 71, 'LoanApplication', 1, 1, 290)");
            migrationBuilder.Sql("Insert[Master].[RolePermission]([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values(291, 3, 71, 'LoanApplication', 1, 1, 291)");
            migrationBuilder.Sql("Insert[Master].[RolePermission]([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values(292, 4, 71, 'LoanApplication', 1, 1, 292)");
            migrationBuilder.Sql("Insert[Master].[RolePermission]([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values(293, 5, 71, 'LoanApplication', 1, 1, 293)");
            migrationBuilder.Sql("Insert[Master].[RolePermission]([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values(294, 6, 71, 'LoanApplication', 1, 1, 294)");
            migrationBuilder.Sql("Insert[Master].[RolePermission]([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values(295, 7, 71, 'LoanApplication', 1, 1, 295)");

            migrationBuilder.Sql("Insert[Master].[RolePermission]([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values(296, 1, 72, 'Admin', 1, 1, 296)");
            migrationBuilder.Sql("Insert[Master].[RolePermission]([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values(297, 2, 72, 'Admin', 1, 1, 297)");
            migrationBuilder.Sql("Insert[Master].[RolePermission]([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values(298, 3, 72, 'Admin', 1, 1, 298)");
            migrationBuilder.Sql("Insert[Master].[RolePermission]([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values(299, 4, 72, 'Admin', 1, 1, 299)");
            migrationBuilder.Sql("Insert[Master].[RolePermission]([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values(300, 5, 72, 'Admin', 1, 1, 300)");
            migrationBuilder.Sql("Insert[Master].[RolePermission]([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values(301, 6, 72, 'Admin', 1, 1, 301)");
            migrationBuilder.Sql("Insert[Master].[RolePermission]([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values(302, 7, 72, 'Admin', 1, 1, 302)");

            migrationBuilder.Sql("Insert[Master].[RolePermission]([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values(303, 1, 73, 'Admin', 1, 1, 303)");
            migrationBuilder.Sql("Insert[Master].[RolePermission]([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values(304, 2, 73, 'Admin', 1, 1, 304)");
            migrationBuilder.Sql("Insert[Master].[RolePermission]([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values(305, 3, 73, 'Admin', 1, 1, 305)");
            migrationBuilder.Sql("Insert[Master].[RolePermission]([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values(306, 4, 73, 'Admin', 1, 1, 306)");
            migrationBuilder.Sql("Insert[Master].[RolePermission]([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values(307, 5, 73, 'Admin', 1, 1, 307)");
            migrationBuilder.Sql("Insert[Master].[RolePermission]([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values(308, 6, 73, 'Admin', 1, 1, 308)");
            migrationBuilder.Sql("Insert[Master].[RolePermission]([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values(309, 7, 73, 'Admin', 1, 1, 309)");
            migrationBuilder.Sql("SET IDENTITY_INSERT[Master].[RolePermission] OFF");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from[Master].[Action]  where ActionID between 71 and 73 ");
            migrationBuilder.Sql("delete from[Master].[RolePermission]  where RolePermissionID between 289 and 309 ");

        }
    }
}
