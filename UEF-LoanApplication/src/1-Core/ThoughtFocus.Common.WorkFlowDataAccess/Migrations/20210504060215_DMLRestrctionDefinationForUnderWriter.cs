using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.Common.WorkFlowDataAccess.Migrations
{
    public partial class DMLRestrctionDefinationForUnderWriter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update [WorkFlow].[RestrictionDefinition] set [ActorDefinitionIsInRole_ID] = 3 where Transition_ID IN(11,12)");
            migrationBuilder.Sql("update [WorkFlow].[CommandDefinition] set [Name] = 'Submit Agreement' where [Name] = 'Submit Aggrement' and [ID] = 11");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
