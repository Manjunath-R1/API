using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.Common.WorkFlowDataAccess.Migrations
{
    public partial class DMLFundUtilizationSeedingForWF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActionDefinition] ON");
           migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinition] ([ID], [TypeAsString], [FullTypeName], [MethodName], [WorkflowDefinitionID], [Name]) values(4, 'ThoughtFocus.Workflow.WorkflowActions,ThoughtFocus.Workflow', 'UpdateFundUtilization', 'UpdateFundUtilization', 1, 'UpdateFundUtilization')");
	       migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActionDefinition] OFF");

           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActionDefinitionForActivity] ON");
           migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(22, 1, 1, 4, 13)");
           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActionDefinitionForActivity] OFF");

           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ParameterDefinition] ON");
           migrationBuilder.Sql("Insert [WorkFlow].[ParameterDefinition] ([ID], [TypeAsString], [PurposeID], [WorkflowDefinitionID], [Name]) values(19, 'ThoughtFocus.Domain.Params.FundUtilizationParam,ThoughtFocus.Domain', 3, 1, 'FundUtilization')");
           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ParameterDefinition] OFF");

           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ParameterDefinitionForAction] ON");
           migrationBuilder.Sql("Insert [WorkFlow].[ParameterDefinitionForAction] ([ID], [IsInputParameter], [ParameterDefinitionID], [ActionDefinitionID], [Order]) values (4, 1, 18, 4, 1)");
           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ParameterDefinitionForAction] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ValidationDefination] ON");
            migrationBuilder.Sql("Insert [WorkFlow].[ValidationDefination] ([ValidationDefinationID], [ValidationDefinationName], [ValidationDefinationDescription], [IsEnabled], [ValidationTypeID]) values (1, 'FundUtilizationValidation', 'FundUtilizationValidation', 1, 2)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ValidationDefination] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TransitionValidationDefination] ON");
            migrationBuilder.Sql("Insert [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) values (1, 'FundUtilizationValidation', 1, 1, 16)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TransitionValidationDefination] OFF");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [WorkFlow].[TransitionValidationDefination] where TransitionValidationDefinationID = 1");
            migrationBuilder.Sql("delete from [WorkFlow].[ValidationDefination] where ValidationDefinationID = 1");
            migrationBuilder.Sql("delete from [WorkFlow].[ParameterDefinitionForAction] where [ID] = 4");
            migrationBuilder.Sql("delete from [WorkFlow].[ParameterDefinition] where [ID] = 19");
            migrationBuilder.Sql("delete from [WorkFlow].[ActionDefinitionForActivity] where [ID] = 22");
            migrationBuilder.Sql("delete from [WorkFlow].[ActionDefinition] where [ID] = 4");
            
        }
    }
}
