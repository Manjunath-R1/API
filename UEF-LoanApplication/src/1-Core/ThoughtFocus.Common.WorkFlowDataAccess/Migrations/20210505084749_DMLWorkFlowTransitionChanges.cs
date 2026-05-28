using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.Common.WorkFlowDataAccess.Migrations
{
    public partial class DMLWorkFlowTransitionChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActivityDefinition] ON");
            migrationBuilder.Sql("Insert [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) values (14, 'AgreementSubmitted', 0, 0, 1, 1, 1, 'AgreementSubmitted')");
            migrationBuilder.Sql("Insert [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) values (15, 'AgreementRejected', 0, 0, 1, 1, 1, 'AgreementRejected')");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActivityDefinition] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActionDefinitionForActivity] ON");
            migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(25, 1, 1, 1, 14)");
            migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(26, 1, 1, 1, 15)");
            migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(27, 1, 1, 2, 14)");
            migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(28, 1, 1, 2, 15)");
            migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(29, 1, 1, 1, 3)");
            migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(30, 1, 1, 3, 3)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActionDefinitionForActivity] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[CommandDefinition] ON");
            migrationBuilder.Sql("Insert [WorkFlow].[CommandDefinition] ([ID], [CommandIconClass], [WorkflowDefinitionID], [Name]) values (12, 'fa fa-check ', 1, 'Read Agreement')");
            migrationBuilder.Sql("Insert [WorkFlow].[CommandDefinition] ([ID], [CommandIconClass], [WorkflowDefinitionID], [Name]) values (13, 'fa fa-check ', 1, 'Disagree')");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[CommandDefinition] OFF");           

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TriggerDefinition] ON");
            migrationBuilder.Sql("Insert [WorkFlow].[TriggerDefinition] ([ID], [Name], [CommandID], [TypeId]) values (12, 'ReadAgrrementTrigger', 12, 1)");
            migrationBuilder.Sql("Insert [WorkFlow].[TriggerDefinition] ([ID], [Name], [CommandID], [TypeId]) values (13, 'DisagreeTrigger', 13, 1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TriggerDefinition] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TransitionDefinition] ON");
            migrationBuilder.Sql("Insert [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [Name], [FromID], ToID) values (2, 1, 2, 2, 1, 'InitializedToDrafted', 1, 3)");
            migrationBuilder.Sql("Insert [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [Name], [FromID], ToID) values (3, 1, 2, 2, 1, 'DraftedToDrafted', 3, 3)");
            migrationBuilder.Sql("Insert [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [Name], [FromID], ToID) values (4, 1, 3, 3, 1, 'DraftedToSubmitted', 3, 4)");
            migrationBuilder.Sql("Insert [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [Name], [FromID], ToID) values (17, 1, 2, 13, 1, 'ApprovedToAgreementRejected', 8, 15)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TransitionDefinition] OFF");

            migrationBuilder.Sql("update WorkFlow.TransitionDefinition set [Name] = 'ApprovedToAgreementSubmitted', [ToID] = 14, [TriggerID] = 12 where Name = 'ApprovedToAgreementUploaded'");
            migrationBuilder.Sql("update WorkFlow.TransitionDefinition set [Name] = 'AgreementSubmittedToFundingCompleted', [FromID] = 14 where Name = 'FundingInitiatedToFundingCompleted'");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[RestrictionDefinition] ON");
            migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (13, 1, 17, 2)");
            migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (14, 1, 2, 2)");
            migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (15, 1, 3, 2)");
            migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (16, 1, 4, 2)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[RestrictionDefinition] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ValidationDefination] ON");
            migrationBuilder.Sql("Insert [WorkFlow].[ValidationDefination] ([ValidationDefinationID], [ValidationDefinationName], [ValidationDefinationDescription], [IsEnabled], [ValidationTypeID]) values (3, 'AgreementValidation', 'AgreementValidation', 1, 2)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ValidationDefination] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TransitionValidationDefination] ON");
            migrationBuilder.Sql("Insert [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) values (5, 'CommentRequiredValidation', 1, 3, 13)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TransitionValidationDefination] OFF");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [WorkFlow].[TransitionValidationDefination] where TransitionValidationDefinationID = 5");
            migrationBuilder.Sql("delete from [WorkFlow].[ValidationDefination] where ValidationDefinationID = 3");
            migrationBuilder.Sql("update WorkFlow.TransitionDefinition set [Name] = 'ApprovedToAgreementUploaded', [ToID] = 10, [TriggerID] = 11 where Name = 'ApprovedToAgreementSubmitted'");
            migrationBuilder.Sql("update WorkFlow.TransitionDefinition set [Name] = 'FundingInitiatedToFundingCompleted', [FromID] = 12 where Name = 'AgreementSubmittedToFundingCompleted'");
            migrationBuilder.Sql("delete from [WorkFlow].[RestrictionDefinition] where [ID] IN (13, 14, 15, 16)");
            migrationBuilder.Sql("delete from [WorkFlow].[TransitionDefinition] where [ID] IN(2,3,4,17)"); 
            migrationBuilder.Sql("delete from [WorkFlow].[TriggerDefinition] where [ID] IN (12,13)"); 
            migrationBuilder.Sql("delete from [WorkFlow].[CommandDefinition] where [ID] IN (12,13)"); 
            migrationBuilder.Sql("delete from [WorkFlow].[ActionDefinitionForActivity] where [ID] IN (25,26,27,28,29,30)");
            migrationBuilder.Sql("delete from [WorkFlow].[ActivityDefinition] where [ID] IN (14,15)"); 
        }
    }
}
