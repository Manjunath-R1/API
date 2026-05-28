using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class AgreementRejected_ReInitiate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[CommandDefinition] ON");
            migrationBuilder.Sql("INSERT [WorkFlow].[CommandDefinition] ([ID], [CommandIconClass], [WorkflowDefinitionID], [Name]) VALUES (14, N'fa fa-share-square ', 1, N'ReInitiate')");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[CommandDefinition] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TriggerDefinition] ON");
            migrationBuilder.Sql("INSERT [WorkFlow].[TriggerDefinition] ([ID], [Name], [CommandID], [TypeId]) VALUES (14, N'ReInitiate', 14, 1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TriggerDefinition] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[ApplicationStatus] ON");
            migrationBuilder.Sql("INSERT [Master].[ApplicationStatus] ([ApplicationStatusID], [ApplicationStatusName], [Description], [IsActive], [DisplayOrder], [WorkFlowID]) VALUES (24, N'AgreementSubmittedByReInitiate', N'Agreement Submitted', 1, 0, 1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[ApplicationStatus] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActivityDefinition] ON");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (24, N'AgreementSubmittedByReInitiate', 0, 0, 1, 1, 1, N'AgreementSubmittedByReInitiate')");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActivityDefinition] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActionDefinitionForActivity] ON");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (57, 1, 24, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (58, 2, 24, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (59, 5, 24, 1, 1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActionDefinitionForActivity] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TransitionDefinition] ON");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (35, 1, 2, 14, 1, 15, 24, N'AgreementRejectedToReInitiate')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (36, 1, 2, 6, 1, 24, 12, N'AgreementReInitiateSubmittedToCFOApproved')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (37, 1, 3, 5, 1, 24, 16, N'AgreementSubmittedByReInitiateToRequestedMoreInfo')");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TransitionDefinition] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TransitionValidationDefination] ON");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) VALUES (15, N'CommentRequiredValidation', 1, 2, 35)");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) VALUES (16, N'CommentRequiredValidation', 1, 2, 37)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TransitionValidationDefination] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[RestrictionDefinition] ON");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (64, 1, 35, NULL, NULL, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (65, 1, 35, NULL, NULL, 3)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (66, 1, 35, NULL, NULL, 7)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (67, 1, 36, NULL, NULL, 4)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (68, 1, 37, NULL, NULL, 4)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (69, 1, 37, NULL, NULL, 6)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[RestrictionDefinition] OFF");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [WorkFlow].[TriggerDefinition]  where [ID] = 14");
            migrationBuilder.Sql("delete from [WorkFlow].[CommandDefinition]  where [ID] = 14");
            migrationBuilder.Sql("delete from [Master].[ApplicationStatus]  where [ApplicationStatusID] = 24");
            migrationBuilder.Sql("delete from [WorkFlow].[RestrictionDefinition]  where [ID] between 64 and 69");
            migrationBuilder.Sql("delete from [WorkFlow].[TransitionValidationDefination]  where [TransitionValidationDefinationID] between 15 and 16");
            migrationBuilder.Sql("delete from [WorkFlow].[TransitionDefinition]  where [ID] between 35 and 37");
            migrationBuilder.Sql("delete from [WorkFlow].[ActionDefinitionForActivity]  where [ID] between 57 and 59");
            migrationBuilder.Sql("delete from [WorkFlow].[ActivityDefinition]  where [ID] = 24");

        }
    }
}
