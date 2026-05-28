using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class Update_GetAllPormission_Default_To_RolePermission_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql("UPDATE [Master].[RolePermission] SET [Subject] =N'FundingSource' WHERE [RolePermissionID] = 384");
            //migrationBuilder.Sql("UPDATE [Master].[RolePermission] SET [Subject] =N'FundingSource' WHERE [RolePermissionID] = 385");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
