using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class update_FundingTypes_table1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE [Master].[FundingTypes] SET [IsActive] = 0 WHERE [FundingTypeID] = 1");
            migrationBuilder.Sql("UPDATE [Master].[ApplicationType] SET [IsActive] = 0 WHERE [ApplicationTypeID] = 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
