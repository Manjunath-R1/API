using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class Update_RestrictionDefinition_transition_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update [WorkFlow].[RestrictionDefinition] set transition_id = 17  where id = 13");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update [WorkFlow].[RestrictionDefinition] set transition_id = null  where id = 13");

        }
    }
}
