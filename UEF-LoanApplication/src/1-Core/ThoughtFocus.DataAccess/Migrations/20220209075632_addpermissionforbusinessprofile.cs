using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class addpermissionforbusinessprofile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[RolePermission] on");
            migrationBuilder.Sql("Insert [Master].[RolePermission]([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values(310, 8, 71, 'LoanApplication', 1, 1, 310)");
            migrationBuilder.Sql("Insert [Master].[RolePermission]([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values(311, 8, 72, 'Admin', 1, 1, 311)");
            migrationBuilder.Sql("Insert [Master].[RolePermission]([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values(312, 8, 73, 'Admin', 1, 1, 312)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[RolePermission] off");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[RolePermission]  where RolePermissionID between 310 and 312 ");
        }
    }
}
