using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.Common.WorkFlowDataAccess.Migrations
{
    public partial class DML_WorkFlowCommandNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Update [WorkFlow].[CommandDefinition] set [Name] = 'Initiate Funding' where [Name] = 'Fund Initiate' and [ID] = 9");
            migrationBuilder.Sql("Update [WorkFlow].[CommandDefinition] set [Name] = 'Complete Funding' where [Name] = 'Disburse' and [ID] = 10");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
