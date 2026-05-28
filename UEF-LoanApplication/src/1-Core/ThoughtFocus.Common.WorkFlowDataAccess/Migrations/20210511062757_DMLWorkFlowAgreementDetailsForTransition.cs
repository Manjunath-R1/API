using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.Common.WorkFlowDataAccess.Migrations
{
    public partial class DMLWorkFlowAgreementDetailsForTransition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActionDefinition] ON");
            migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinition] ([ID], [TypeAsString], [FullTypeName], [MethodName], [WorkflowDefinitionID], [Name]) values(6, 'ThoughtFocus.Workflow.WorkflowActions,ThoughtFocus.Workflow', 'SaveAgreementDetails', 'SaveAgreementDetails', 1, 'SaveAgreementDetails')");
	        migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActionDefinition] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActionDefinitionForActivity] ON");
            migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(31, 1, 1, 6, 14)");
            migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(32, 1, 1, 6, 15)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActionDefinitionForActivity] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ParameterDefinitionForAction] ON");
            migrationBuilder.Sql("Insert [WorkFlow].[ParameterDefinitionForAction] ([ID], [IsInputParameter], [ParameterDefinitionID], [ActionDefinitionID], [Order]) values (6, 1, 18, 6, 1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ParameterDefinitionForAction] OFF");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [WorkFlow].[ParameterDefinitionForAction] where [ID] = 6");
            migrationBuilder.Sql("delete from [WorkFlow].[ActionDefinitionForActivity] where [ID] IN(31,32)");
            migrationBuilder.Sql("delete from [WorkFlow].[ActionDefinition] where [ID] = 6");
        
        }
    }
}
