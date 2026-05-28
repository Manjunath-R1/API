using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class seeding_RoleTable_AddRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Role] ON ");
            migrationBuilder.Sql("INSERT [Master].[Role] ([RoleID], [CreatedDateTime], [CreatedByUserID], [LastModifiedDateTime], [LastModifiedByUserID], [IsActive], [RoleName], [RoleDescription], [DisplayOrder], [IsLoginRole])VALUES (8, CAST(N'2021-08-16 00:00:00.000' AS DateTime), 7, CAST(N'2021-08-16 00:00:00.000' AS DateTime), 7, 1, N'NULExecutive', N'NUL Executive', 1, 1)");
            migrationBuilder.Sql("INSERT [Master].[Role] ([RoleID], [CreatedDateTime], [CreatedByUserID], [LastModifiedDateTime], [LastModifiedByUserID], [IsActive], [RoleName], [RoleDescription], [DisplayOrder], [IsLoginRole])VALUES (9, CAST(N'2021-08-16 00:00:00.000' AS DateTime), 7, CAST(N'2021-08-16 00:00:00.000' AS DateTime), 7, 0, N'FundingAdministrator', N'Funding Administrator', 1, 1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Role] OFF ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           migrationBuilder.Sql("delete from [Master].[Role]  where ID between 8 and 9");
        }
    }
}
