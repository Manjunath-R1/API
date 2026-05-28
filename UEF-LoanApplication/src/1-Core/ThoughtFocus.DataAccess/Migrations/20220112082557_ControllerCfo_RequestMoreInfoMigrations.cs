using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class ControllerCfo_RequestMoreInfoMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[ApplicationStatus] ON");
            migrationBuilder.Sql("INSERT [Master].[ApplicationStatus] ([ApplicationStatusID], [ApplicationStatusName], [Description], [IsActive], [DisplayOrder], [WorkFlowID]) VALUES (16, N'RequestedMoreDetails', N'Requested More Information', 1, 0, 1)");
            migrationBuilder.Sql("INSERT [Master].[ApplicationStatus] ([ApplicationStatusID], [ApplicationStatusName], [Description], [IsActive], [DisplayOrder], [WorkFlowID]) VALUES (17, N'RequestMoreDeatailsCompleted', N'Request Completed', 1, 0, 1)");
            migrationBuilder.Sql("INSERT [Master].[ApplicationStatus] ([ApplicationStatusID], [ApplicationStatusName], [Description], [IsActive], [DisplayOrder], [WorkFlowID]) VALUES (18, N'UWReview', N'UW Review', 1, 0, 1)");
            migrationBuilder.Sql("INSERT [Master].[ApplicationStatus] ([ApplicationStatusID], [ApplicationStatusName], [Description], [IsActive], [DisplayOrder], [WorkFlowID]) VALUES (19, N'UWApproved', N'UW Approved', 1, 0, 1)");
            migrationBuilder.Sql("INSERT [Master].[ApplicationStatus] ([ApplicationStatusID], [ApplicationStatusName], [Description], [IsActive], [DisplayOrder], [WorkFlowID]) VALUES (20, N'RequestMoreDeatailsCompletedByBorrower', N'Request Completed', 1, 0, 1)");
            migrationBuilder.Sql("INSERT [Master].[ApplicationStatus] ([ApplicationStatusID], [ApplicationStatusName], [Description], [IsActive], [DisplayOrder], [WorkFlowID]) VALUES (21, N'UWReviewToRequestedMoreDetails', N'Requested More Information', 1, 0, 1)");
            migrationBuilder.Sql("INSERT [Master].[ApplicationStatus] ([ApplicationStatusID], [ApplicationStatusName], [Description], [IsActive], [DisplayOrder], [WorkFlowID]) VALUES (22, N'UWReviewRequestedMoreDetailsCompletedByCfoController', N'UW Review', 1, 0, 1)");
            migrationBuilder.Sql("INSERT [Master].[ApplicationStatus] ([ApplicationStatusID], [ApplicationStatusName], [Description], [IsActive], [DisplayOrder], [WorkFlowID]) VALUES (23, N'UWReviewRequestedMoreDetailsCompletedByBorrower', N'UW Review', 1, 0, 1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[ApplicationStatus] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActivityDefinition] ON");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (16, N'RequestedMoreDetails', 0, 0, 1, 1, 1, N'RequestedMoreDetails')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (17, N'RequestMoreDeatailsCompleted', 0, 0, 1, 1, 1, N'RequestMoreDeatailsCompleted')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (18, N'UWReview', 0, 0, 1, 1, 1, N'UWReview')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (19, N'UWApproved', 0, 0, 1, 1, 1, N'UWApproved')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (20, N'RequestMoreDeatailsCompletedByBorrower', 0, 0, 1, 1, 1, N'RequestMoreDeatailsCompletedByBorrower')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (21, N'UWReviewToRequestedMoreDetails', 0, 0, 1, 1, 1, N'UWReviewToRequestedMoreDetails')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (22, N'UWReviewRequestedMoreDetailsCompletedByCfoController', 0, 0, 1, 1, 1, N'UWReviewRequestedMoreDetailsCompletedByCfoController')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (23, N'UWReviewRequestedMoreDetailsCompletedByBorrower', 0, 0, 1, 1, 1, N'UWReviewRequestedMoreDetailsCompletedByBorrower')");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActivityDefinition] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActionDefinitionForActivity] ON");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (33, 1, 16, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (34, 2, 16, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (35, 5, 16, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (36, 1, 17, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (37, 2, 17, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (38, 5, 17, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (39, 1, 18, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (40, 2, 18, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (41, 5, 18, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (42, 1, 19, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (43, 2, 19, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (44, 5, 19, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (45, 1, 20, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (46, 2, 20, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (47, 5, 20, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (48, 1, 21, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (49, 2, 21, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (50, 5, 21, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (51, 1, 22, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (52, 2, 22, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (53, 5, 22, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (54, 1, 23, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (55, 2, 23, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (56, 5, 23, 1, 1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActionDefinitionForActivity] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TransitionDefinition] ON");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (18, 1, 3, 5, 1, 14, 16, N'AgreementSubmittedToRequestedMoreInfo')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (19, 1, 3, 2, 1, 16, 16, N'RequestedMoreInfoToSave')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (20, 1, 2, 4, 1, 16, 18, N'RequestMoreInfoToAccepted')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (21, 1, 2, 3, 1, 16, 20, N'RequestMoreInfoToSubmitted')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (22, 1, 2, 6, 1, 18, 19, N'UWReviewToUWApproved')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (23, 1, 2, 6, 1, 19, 12, N'UWApprovedToCFOApproved')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (24, 1, 3, 5, 1, 18, 21, N'UWReviewToRequestedMoreDetails')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (25, 1, 2, 4, 1, 20, 18, N'RequestMoreInfoSbumittedByBorrowerToUWReview')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (26, 1, 3, 5, 1, 20, 16, N'RequestMoreDeatailsCompletedByBorrowerToRequestedMoreDetails')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (27, 1, 3, 2, 1, 21, 21, N'UWReviewRequestedMoreDetailsToSave')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (28, 1, 2, 4, 1, 21, 22, N'UWReviewToRequestedMoreDetailsToCompletedByCfoController')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (29, 1, 2, 6, 1, 22, 19, N'UWReviewRequestedMoreDetailsCompletedByCfoControllerToUWApproved')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (30, 1, 3, 5, 1, 22, 21, N'UWReviewRequestedMoreDetailsCompletedByCfoControllerToRequestedMoreDetails')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (31, 1, 2, 3, 1, 21, 23, N'UWReviewToRequestedMoreDetailsToCompletedByBorrower')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (32, 1, 2, 6, 1, 23, 19, N'UWReviewRequestedMoreDetailsCompletedByBorrowerToUWApproved')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (33, 1, 3, 5, 1, 23, 21, N'RequestCompletedByBorrowerToRequestedMoreDetails')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (34, 1, 3, 2, 1, 5, 5, N'RequestedMoreInformationToSave')");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TransitionDefinition] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TransitionValidationDefination] ON");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) VALUES (6, N'CommentRequiredValidation', 1, 2, 18)");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) VALUES (7, N'CommentRequiredValidation', 1, 2, 19)");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) VALUES (8, N'CommentRequiredValidation', 1, 2, 21)");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) VALUES (9, N'CommentRequiredValidation', 1, 2, 24)");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) VALUES (10, N'CommentRequiredValidation', 1, 2, 26)");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) VALUES (11, N'CommentRequiredValidation', 1, 2, 27)");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) VALUES (12, N'CommentRequiredValidation', 1, 2, 30)");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) VALUES (13, N'CommentRequiredValidation', 1, 2, 31)");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) VALUES (14, N'CommentRequiredValidation', 1, 2,33)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TransitionValidationDefination] OFF");


            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[RestrictionDefinition] ON");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (37, 1, 18, NULL, NULL, 4)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (38, 1, 18, NULL, NULL, 6)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (39, 1, 19, NULL, NULL, 4)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (40, 1, 19, NULL, NULL, 6)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (41, 1, 20, NULL, NULL, 6)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (42, 1, 20, NULL, NULL, 4)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (43, 1, 19, NULL, NULL, 2)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (44, 1, 21, NULL, NULL, 2)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (45, 1, 22, NULL, NULL, 3)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (46, 1, 23, NULL, NULL, 4)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (47, 1, 24, NULL, NULL, 3)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (48, 1, 25, NULL, NULL, 4)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (49, 1, 25, NULL, NULL, 6)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (50, 1, 26, NULL, NULL, 6)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (51, 1, 26, NULL, NULL, 4)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (52, 1, 27, NULL, NULL, 4)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (53, 1, 28, NULL, NULL, 4)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (54, 1, 28, NULL, NULL, 6)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (55, 1, 29, NULL, NULL, 3)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (56, 1, 30, NULL, NULL, 3)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (57, 1, 31, NULL, NULL, 2)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (58, 1, 32, NULL, NULL, 3)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (59, 1, 33, NULL, NULL, 3)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (60, 1, 27, NULL, NULL, 6)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (61, 1, 27, NULL, NULL, 2)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (62, 1, 34, NULL, NULL, 2)");
            migrationBuilder.Sql("INSERT [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionExecuteRule_ID], [ActorDefinitionIsIdentity_ID], [ActorDefinitionIsInRole_ID]) VALUES (63, 1, 34, NULL, NULL, 7)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[RestrictionDefinition] OFF");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[ApplicationStatus]  where [ApplicationStatusID] between 16 and 23");
            migrationBuilder.Sql("delete from [WorkFlow].[RestrictionDefinition]  where [ID] between 37 and 63");
            migrationBuilder.Sql("delete from [WorkFlow].[TransitionValidationDefination]  where [TransitionValidationDefinationID] between 6 and 14");
            migrationBuilder.Sql("delete from [WorkFlow].[TransitionDefinition]  where [ID] between 18 and 34");
            migrationBuilder.Sql("delete from [WorkFlow].[ActionDefinitionForActivity]  where [ID] between 33 and 56");
            migrationBuilder.Sql("delete from [WorkFlow].[ActivityDefinition]  where [ID] between 16 and 23");

        }
    }
}
