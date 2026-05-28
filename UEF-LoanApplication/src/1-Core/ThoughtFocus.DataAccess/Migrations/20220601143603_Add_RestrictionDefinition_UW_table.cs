using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class Add_RestrictionDefinition_UW_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[RestrictionDefinition] ON");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (137, 1, 62, NULL, NULL, 10)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[RestrictionDefinition] OFF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
