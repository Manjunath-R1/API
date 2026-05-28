using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.Common.WorkFlowDataAccess.Migrations
{
    public partial class DMLWorkFlowSiteAdminRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActorDefinitionIsInRole] ON");
            migrationBuilder.Sql("Insert [WorkFlow].[ActorDefinitionIsInRole] ([ID], [RoleId], [WorkflowDefinitionID], [Name]) values (7, 7, 1, 'SiteAdmin')");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActorDefinitionIsInRole] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[RestrictionDefinition] ON");
            migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (25, 1, 6, 7)");
            migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (26, 1, 7, 7)");
            migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (27, 1, 9, 7)");
            migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (28, 1, 10, 7)");
            migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (29, 1, 14, 7)");
            migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (30, 1, 1, 7)");
            migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (31, 1, 2, 7)");
            migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (32, 1, 3, 7)");
            migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (33, 1, 4, 7)");
            migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (34, 1, 8, 7)");
            migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (35, 1, 13, 7)");
            migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (36, 1, 17, 7)");


            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[RestrictionDefinition] OFF");  
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [WorkFlow].[RestrictionDefinition] where [ID] between 25 and 36");
            migrationBuilder.Sql("delete from [WorkFlow].[ActorDefinitionIsInRole] where [ID] = 7"); 
        }
    }
}
