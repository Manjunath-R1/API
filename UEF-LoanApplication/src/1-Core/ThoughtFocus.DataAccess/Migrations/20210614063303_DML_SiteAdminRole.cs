using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class DML_SiteAdminRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Role] ON ");
            migrationBuilder.Sql("INSERT [Master].[Role] ([RoleID], [CreatedDateTime], [CreatedByUserID], [LastModifiedDateTime], [LastModifiedByUserID], [IsActive], [RoleName], [RoleDescription], [DisplayOrder], [IsLoginRole]) VALUES (7, CAST(N'2017-10-27 00:00:00.000' AS DateTime), 7, CAST(N'2017-10-27 00:00:00.000' AS DateTime), 7, 1, N'SiteAdmin', N'Site Admin', 1, 1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Role] OFF ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[Role] where RoleID = 7");
        }
    }
}
