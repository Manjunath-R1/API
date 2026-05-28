using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class add_newBusinessRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[BusinessRole] ON ");
            migrationBuilder.Sql("INSERT [Master].[BusinessRole] ([BusinessRoleID], [BusinessRoleName], [Description], [IsActive], [DisplayOrder]) VALUES (5, N'President/Executive Director', N'President/Executive Director', 1, 1)");
            migrationBuilder.Sql("INSERT [Master].[BusinessRole] ([BusinessRoleID], [BusinessRoleName], [Description], [IsActive], [DisplayOrder]) VALUES (6, N'Grants Manager', N'Grants Manager', 1, 1)");
            migrationBuilder.Sql("INSERT [Master].[BusinessRole] ([BusinessRoleID], [BusinessRoleName], [Description], [IsActive], [DisplayOrder]) VALUES (7, N'Vice President', N'Vice President', 1, 1)");
            migrationBuilder.Sql("INSERT [Master].[BusinessRole] ([BusinessRoleID], [BusinessRoleName], [Description], [IsActive], [DisplayOrder]) VALUES (8, N'Development Director', N'Development Director', 1, 1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[BusinessRole] OFF ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[BusinessRole] where BusinessRoleID in(5,6,7,8)");
        }
    }
}
