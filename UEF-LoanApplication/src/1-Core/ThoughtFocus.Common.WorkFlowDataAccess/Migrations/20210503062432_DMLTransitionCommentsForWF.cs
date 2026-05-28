using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.Common.WorkFlowDataAccess.Migrations
{
    public partial class DMLTransitionCommentsForWF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActionDefinition] ON");
            migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinition] ([ID], [TypeAsString], [FullTypeName], [MethodName], [WorkflowDefinitionID], [Name]) values(5, 'ThoughtFocus.Workflow.WorkflowActions,ThoughtFocus.Workflow', 'SaveTransitionComments', 'SaveTransitionComments', 1, 'SaveTransitionComments')");
	        migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActionDefinition] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActionDefinitionForActivity] ON");
            migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(23, 1, 1, 5, 5)");
            migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(24, 1, 1, 5, 6)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActionDefinitionForActivity] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ParameterDefinition] ON");
            migrationBuilder.Sql("Insert [WorkFlow].[ParameterDefinition] ([ID], [TypeAsString], [PurposeID], [WorkflowDefinitionID], [Name]) values(20, 'System.String', 3, 1, 'Comment')");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ParameterDefinition] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ParameterDefinitionForAction] ON");
            migrationBuilder.Sql("Insert [WorkFlow].[ParameterDefinitionForAction] ([ID], [IsInputParameter], [ParameterDefinitionID], [ActionDefinitionID], [Order]) values (5, 1, 18, 5, 1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ParameterDefinitionForAction] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ValidationDefination] ON");
            migrationBuilder.Sql("Insert [WorkFlow].[ValidationDefination] ([ValidationDefinationID], [ValidationDefinationName], [ValidationDefinationDescription], [IsEnabled], [ValidationTypeID]) values (2, 'CommentRequiredValidation', 'CommentRequiredValidation', 1, 2)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ValidationDefination] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TransitionValidationDefination] ON");
            migrationBuilder.Sql("Insert [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) values (2, 'CommentRequiredValidation', 1, 2, 7)");
            migrationBuilder.Sql("Insert [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) values (3, 'CommentRequiredValidation', 1, 2, 8)");
            migrationBuilder.Sql("Insert [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) values (4, 'CommentRequiredValidation', 1, 2, 9)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TransitionValidationDefination] OFF");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [WorkFlow].[TransitionValidationDefination] where TransitionValidationDefinationID IN (2,3,4)");
            migrationBuilder.Sql("delete from [WorkFlow].[ValidationDefination] where ValidationDefinationID = 2");
            migrationBuilder.Sql("delete from [WorkFlow].[ParameterDefinitionForAction] where [ID] = 5");
            migrationBuilder.Sql("delete from [WorkFlow].[ParameterDefinition] where [ID] = 20");
            migrationBuilder.Sql("delete from [WorkFlow].[ActionDefinitionForActivity] where [ID] IN(23,24)");
            migrationBuilder.Sql("delete from [WorkFlow].[ActionDefinition] where [ID] = 5");
        }
    }
}
