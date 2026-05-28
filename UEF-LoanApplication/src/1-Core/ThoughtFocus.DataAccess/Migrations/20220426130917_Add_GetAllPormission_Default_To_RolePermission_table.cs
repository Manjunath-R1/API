using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class Add_GetAllPormission_Default_To_RolePermission_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql("SET IDENTITY_INSERT[Master].RolePermission ON");
            //migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(384, 1, 78, 'Admin', 1, 1, 384)");
            //migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(385, 7, 78, 'SiteAdmin', 1, 1, 385)");
            //migrationBuilder.Sql("SET IDENTITY_INSERT[Master].RolePermission OFF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql("delete from [Master].[RolePermission]  where RolePermissionID between 384 and 385");
        }
    }
}
