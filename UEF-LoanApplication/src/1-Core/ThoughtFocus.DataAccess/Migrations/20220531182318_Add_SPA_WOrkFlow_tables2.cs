using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class Add_SPA_WOrkFlow_tables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActionDefinitionForActivity] ON ");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (66, 7, 31, 1, 1) ");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (67, 7, 32, 1, 1)	");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (68, 7, 33, 1, 1)	");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (69, 7, 34, 1, 1)	");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (70, 7, 35, 1, 1)	");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (71, 7, 36, 1, 1)	");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (72, 7, 37, 1, 1)	");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (73, 7, 38, 1, 1)	");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (74, 7, 39, 1, 1)	");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (75, 7, 40, 1, 1)	");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (76, 8, 31, 1, 1)	");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (78, 8, 32, 1, 1)	");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (79, 8, 33, 1, 1)	");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (80, 8, 34, 1, 1)	");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (81, 8, 35, 1, 1)	");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (82, 8, 36, 1, 1)	");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (83, 8, 37, 1, 1)	");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (84, 8, 38, 1, 1)	");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (85, 8, 39, 1, 1)	");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (86, 8, 40, 1, 1)	");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (87, 9, 31, 1, 1)	");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (88, 10, 40, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (89, 11, 32, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (90, 11, 33, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (91, 7, 41, 1, 1)	");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (92, 7, 42, 1, 1)	");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (93, 8, 41, 1, 1)	");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (94, 8, 42, 1, 1)	");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (95, 7, 30, 1, 1)	");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (96, 10, 30, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (97, 12, 41, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (98, 12, 42, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (99, 7, 43, 1, 1)	");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (100, 8, 43, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (101, 11, 43, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (102, 7, 44, 1, 1) ");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (103, 8, 44, 1, 1) ");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (104, 11, 44, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (105, 7, 45, 1, 1) ");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (106, 8, 45, 1, 1) ");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (107, 11, 45, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (108, 7, 46, 1, 1) ");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (109, 8, 46, 1, 1) ");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (110, 11, 46, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (111, 7, 47, 1, 1) ");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (112, 8, 47, 1, 1) ");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (113, 11, 47, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (114, 7, 48, 1, 1) ");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (115, 8, 48, 1, 1) ");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (116, 11, 48, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (117, 7, 49, 1, 1) ");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (118, 8, 49, 1, 1) ");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (119, 11, 49, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (120, 7, 50, 1, 1) ");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (121, 8, 50, 1, 1) ");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (122, 11, 50, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (123, 7, 51, 1, 1) ");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (124, 8, 51, 1, 1) ");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (125, 11, 51, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (126, 7, 52, 1, 1) ");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (127, 8, 52, 1, 1) ");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (128, 7, 53, 1, 1) ");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (129, 11, 53, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (130, 7, 54, 1, 1) ");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (131, 11, 54, 1, 1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActionDefinitionForActivity] OFF ");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActorDefinitionIsInRole] ON");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActorDefinitionIsInRole] ([ID], [RoleId], [WorkflowDefinitionID], [Name]) VALUES (8, N'1', 2, N'Administrator')  ");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActorDefinitionIsInRole] ([ID], [RoleId], [WorkflowDefinitionID], [Name]) VALUES (9, N'2', 2, N'Borrower')		  ");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActorDefinitionIsInRole] ([ID], [RoleId], [WorkflowDefinitionID], [Name]) VALUES (10, N'3', 2, N'UnderWriter')	  ");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActorDefinitionIsInRole] ([ID], [RoleId], [WorkflowDefinitionID], [Name]) VALUES (11, N'4', 2, N'NULTreasury')	  ");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActorDefinitionIsInRole] ([ID], [RoleId], [WorkflowDefinitionID], [Name]) VALUES (12, N'5', 2, N'LoanProcessor') ");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActorDefinitionIsInRole] ([ID], [RoleId], [WorkflowDefinitionID], [Name]) VALUES (13, N'6', 2, N'Controller')	  ");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActorDefinitionIsInRole] ([ID], [RoleId], [WorkflowDefinitionID], [Name]) VALUES (14, N'7', 2, N'SiteAdmin')	  ");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActorDefinitionIsInRole] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[CommandDefinition] ON ");
            migrationBuilder.Sql("INSERT [WorkFlow].[CommandDefinition] ([ID], [CommandIconClass], [WorkflowDefinitionID], [Name]) VALUES (15, N'fa fa-check ', 2, N'Apply')");
            migrationBuilder.Sql("INSERT [WorkFlow].[CommandDefinition] ([ID], [CommandIconClass], [WorkflowDefinitionID], [Name]) VALUES (16, N'fa fa-floppy-o', 2, N'Save')");
            migrationBuilder.Sql("INSERT [WorkFlow].[CommandDefinition] ([ID], [CommandIconClass], [WorkflowDefinitionID], [Name]) VALUES (17, N'fa fa-check-square-o', 2, N'Submit')");
            migrationBuilder.Sql("INSERT [WorkFlow].[CommandDefinition] ([ID], [CommandIconClass], [WorkflowDefinitionID], [Name]) VALUES (18, N'fa fa-caret-square-o-right', 2, N'Accept')");
            migrationBuilder.Sql("INSERT [WorkFlow].[CommandDefinition] ([ID], [CommandIconClass], [WorkflowDefinitionID], [Name]) VALUES (19, N'fa fa-check-square-o', 2, N'Request More Info')");
            migrationBuilder.Sql("INSERT [WorkFlow].[CommandDefinition] ([ID], [CommandIconClass], [WorkflowDefinitionID], [Name]) VALUES (20, N'fa fa-share-square', 2, N'Approve')");
            migrationBuilder.Sql("INSERT [WorkFlow].[CommandDefinition] ([ID], [CommandIconClass], [WorkflowDefinitionID], [Name]) VALUES (21, N'fa fa-check-square', 2, N'Reject')");
            migrationBuilder.Sql("INSERT [WorkFlow].[CommandDefinition] ([ID], [CommandIconClass], [WorkflowDefinitionID], [Name]) VALUES (22, N'fa fa-check-square', 2, N'Accept Agreement')");
            migrationBuilder.Sql("INSERT [WorkFlow].[CommandDefinition] ([ID], [CommandIconClass], [WorkflowDefinitionID], [Name]) VALUES (23, N'fa fa-check-square', 2, N'Fund Initiate')");
            migrationBuilder.Sql("INSERT [WorkFlow].[CommandDefinition] ([ID], [CommandIconClass], [WorkflowDefinitionID], [Name]) VALUES (24, N'fa fa-check-square', 2, N'Disburse')");
            migrationBuilder.Sql("INSERT [WorkFlow].[CommandDefinition] ([ID], [CommandIconClass], [WorkflowDefinitionID], [Name]) VALUES (25, N'fa fa-check-square', 2, N'Submit Aggrement')");
            migrationBuilder.Sql("INSERT [WorkFlow].[CommandDefinition] ([ID], [CommandIconClass], [WorkflowDefinitionID], [Name]) VALUES (26, N'fa fa-check-square', 2, N'Read Agreement')");
            migrationBuilder.Sql("INSERT [WorkFlow].[CommandDefinition] ([ID], [CommandIconClass], [WorkflowDefinitionID], [Name]) VALUES (27, N'fa fa-check-square', 2, N'Disagree')");
            migrationBuilder.Sql("INSERT [WorkFlow].[CommandDefinition] ([ID], [CommandIconClass], [WorkflowDefinitionID], [Name]) VALUES (28, N'fa fa-check-square', 2, N'ReInitiate')");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[CommandDefinition] OFF");


            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TransitionDefinition] ON ");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (38, 1, 2, 3, 2, 28, 31, N'InitializedToSubmitted')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (39, 1, 2, 2, 2, 28, 30, N'InitializedToDrafted')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (40, 1, 2, 2, 2, 30, 30, N'DraftedToDrafted')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (41, 1, 3, 3, 2, 30, 31, N'DraftedToSubmitted	')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (42, 1, 2, 4, 2, 31, 34, N'SubmittedToAccepted')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (43, 1, 3, 5, 2, 31, 32, N'SubmittedToRequestedMoreInfo')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (44, 1, 2, 3, 2, 32, 33, N'RequestedMoreInfoToRequestCompleted')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (45, 1, 3, 5, 2, 33, 32, N'RequestCompletedToRequestedMoreInfo')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (46, 1, 2, 4, 2, 33, 34, N'RequestCompletedToAccepted')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (47, 1, 2, 6, 2, 34, 35, N'AcceptedToApproved')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (48, 1, 2, 7, 2, 34, 36, N'AcceptedToRejected')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (49, 1, 2, 12, 2, 35, 41, N'ApprovedToAgreementSubmitted')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (50, 1, 2, 8, 2, 37, 38, N'AgreementUploadedToAgreementAccepted')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (51, 1, 2, 6, 2, 41, 39, N'AgreementSubmittedToCFOApproved')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (52, 1, 2, 10, 2, 39, 40, N'CFOApprovedToAccountDisbursed')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (53, 1, 2, 13, 2, 35, 42, N'ApprovedToAgreementRejected')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (54, 1, 3, 5, 2, 41, 43, N'AgreementSubmittedToRequestedMoreInfo')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (55, 1, 3, 2, 2, 43, 52, N'RequestedMoreInfoToSave')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (56, 1, 2, 4, 2, 43, 45, N'RequestMoreInfoToAccepted')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (57, 1, 2, 3, 2, 43, 47, N'RequestMoreInfoToSubmitted')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (58, 1, 2, 6, 2, 45, 46, N'UWReviewToUWApproved')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (59, 1, 2, 6, 2, 46, 39, N'UWApprovedToCFOApproved')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (60, 1, 3, 5, 2, 45, 48, N'UWReviewToRequestedMoreDetails')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (61, 1, 2, 4, 2, 47, 45, N'RequestMoreInfoSbumittedByBorrowerToUWReview')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (62, 1, 3, 5, 2, 47, 43, N'RequestMoreDeatailsCompletedByBorrowerToRequestedMoreDetails')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (63, 1, 3, 2, 2, 48, 53, N'UWReviewRequestedMoreDetailsToSave')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (64, 1, 2, 4, 2, 48, 49, N'UWReviewToRequestedMoreDetailsToCompletedByCfoController')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (65, 1, 2, 6, 2, 49, 46, N'UWReviewRequestedMoreDetailsCompletedByCfoControllerToUWApproved')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (66, 1, 3, 5, 2, 49, 48, N'UWReviewRequestedMoreDetailsCompletedByCfoControllerToRequestedMoreDetails')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (67, 1, 2, 3, 2, 48, 50, N'UWReviewToRequestedMoreDetailsToCompletedByBorrower')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (68, 1, 2, 6, 2, 50, 46, N'UWReviewRequestedMoreDetailsCompletedByBorrowerToUWApproved')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (69, 1, 3, 5, 2, 50, 48, N'RequestCompletedByBorrowerToRequestedMoreDetails')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (70, 1, 3, 2, 2, 32, 54, N'RequestedMoreInformationToSave')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (71, 1, 2, 14, 2, 42, 51, N'AgreementRejectedToReInitiate')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (72, 1, 2, 6, 2, 51, 39, N'AgreementReInitiateSubmittedToCFOApproved')");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [FromID], [ToID], [Name]) VALUES (73, 1, 3, 5, 2, 51, 43, N'AgreementSubmittedByReInitiateToRequestedMoreInfo')");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TransitionDefinition] OFF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
