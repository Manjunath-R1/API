using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class add_Name_ActionTable_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Action] ON ");
            migrationBuilder.Sql("Insert [Master].[Action] ([ActionID], [Name], [Description], [IsActive], [DisplayOrder]) values (76, 'GetProgramInvitationEmail', 'Get ProgramInvitation Email', 1, 76)");
            migrationBuilder.Sql("Insert [Master].[Action] ([ActionID], [Name], [Description], [IsActive], [DisplayOrder]) values (77, 'SaveOrUpdateProgramInvitationEmail', 'SaveOrUpdateProgramInvitationEmail', 1, 77)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Action] OFF ");

            //migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[RolePermission] ON ");
            //migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (374, 1, 76, 'FundingSource', 1, 1, 374)");
            //migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (375, 3, 76, 'FundingSource', 1, 1, 375)");
            //migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (376, 7, 76, 'FundingSource', 1, 1, 376)");
            //migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (377, 8, 76, 'FundingSource', 1, 1, 377)");
            //migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (378, 4, 76, 'FundingSource', 1, 1, 378)");

            //migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (379, 1, 77, 'FundingSource', 1, 1, 379)");
            //migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (380, 3, 77, 'FundingSource', 1, 1, 380)");
            //migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (381, 7, 77, 'FundingSource', 1, 1, 381)");
            //migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (382, 8, 77, 'FundingSource', 1, 1, 382)");
            //migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (383, 4, 77, 'FundingSource', 1, 1, 383)");

            //migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[RolePermission] OFF ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql("delete from [Master].[Action]  where ActionID = 76");
            //migrationBuilder.Sql("delete from [Master].[Action]  where ActionID = 77");
            //migrationBuilder.Sql("delete from [Master].[RolePermission]  where RolePermissionID between 374 and 383");
        }
    }
}
