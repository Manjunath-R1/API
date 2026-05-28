using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.Common.WorkFlowDataAccess.Migrations
{
    public partial class DML_InitialDataSeedingWorkflow : Migration
    {
         protected override void Up(MigrationBuilder migrationBuilder)
        {
           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[WorkflowDefinition] ON");
           migrationBuilder.Sql("Insert [WorkFlow].[WorkflowDefinition] ([ID], [DesignerModel], [Name]) values (1, 'LoanApplication', 'LoanApplication')");
           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[WorkflowDefinition] OFF");

           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActivityDefinition] ON");
           migrationBuilder.Sql("Insert [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) values (1, 'Initialized', 1, 0, 1, 1, 1, 'Initialized')");
           migrationBuilder.Sql("Insert [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) values (2, 'Created', 0, 0, 1, 1, 1, 'Created')");
           migrationBuilder.Sql("Insert [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) values (3, 'Drafted', 0, 0, 1, 1, 1, 'Drafted')");
           migrationBuilder.Sql("Insert [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) values (4, 'Submitted', 0, 0, 1, 1, 1, 'Submitted')");
           migrationBuilder.Sql("Insert [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) values (5, 'RequestedMoreInfo', 0, 0, 1, 1, 1, 'RequestedMoreInfo')");
           migrationBuilder.Sql("Insert [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) values (6, 'RequestCompleted', 0, 0, 1, 1, 1, 'RequestCompleted')");
           migrationBuilder.Sql("Insert [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) values (7, 'Accepted', 0, 0, 1, 1, 1, 'Accepted')");
           migrationBuilder.Sql("Insert [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) values (8, 'Approved', 0, 0, 1, 1, 1, 'Approved')");
           migrationBuilder.Sql("Insert [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) values (9, 'Rejected', 0, 0, 1, 1, 1, 'Rejected')");
           migrationBuilder.Sql("Insert [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) values (10, 'AgreementUploaded', 0, 0, 1, 1, 1, 'AgreementUploaded')");
           migrationBuilder.Sql("Insert [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) values (11, 'AgreementAccepted', 0, 1, 1, 1, 1, 'AgreementAccepted')");
           migrationBuilder.Sql("Insert [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) values (12, 'FundingInitiated', 0, 1, 1, 1, 1, 'FundingInitiated')");
           migrationBuilder.Sql("Insert [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) values (13, 'FundingCompleted', 0, 1, 1, 1, 1, 'FundingCompleted')");
           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActivityDefinition] OFF");

           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActionDefinition] ON");
           migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinition] ([ID], [TypeAsString], [FullTypeName], [MethodName], [WorkflowDefinitionID], [Name]) values(1, 'ThoughtFocus.Workflow.WorkflowActions,ThoughtFocus.Workflow', 'UpdateLoanApplicationStatus', 'UpdateLoanApplicationStatus', 1, 'UpdateLoanApplicationStatus')");
	       migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinition] ([ID], [TypeAsString], [FullTypeName], [MethodName], [WorkflowDefinitionID], [Name]) values(2, 'ThoughtFocus.Workflow.WorkflowActions,ThoughtFocus.Workflow', 'SendNotification', 'SendNotification', 1, 'SendNotification')");
           migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinition] ([ID], [TypeAsString], [FullTypeName], [MethodName], [WorkflowDefinitionID], [Name]) values(3, 'ThoughtFocus.Workflow.WorkflowActions,ThoughtFocus.Workflow', 'UpdateProgramInvitationStatus', 'UpdateProgramInvitationStatus', 1, 'UpdateProgramInvitationStatus')");
           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActionDefinition] OFF");
       
	       migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActionDefinitionForActivity] ON");
           migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(1, 1, 1, 1, 4)");
           migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(2, 1, 1, 1, 5)");
           migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(3, 1, 1, 1, 6)");
           migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(4, 1, 1, 1, 7)");
           migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(5, 1, 1, 1, 8)");
           migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(6, 1, 1, 1, 9)");
           migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(7, 1, 1, 1, 10)");
           migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(8, 1, 1, 1, 11)");
           migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(9, 1, 1, 1, 12)");
           migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(10, 1, 1, 1, 13)");
	       migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(11, 1, 1, 2, 4)");
           migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(12, 1, 1, 2, 5)");
           migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(13, 1, 1, 2, 6)");
           migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(14, 1, 1, 2, 7)");
           migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(15, 1, 1, 2, 8)");
           migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(16, 1, 1, 2, 9)");
           migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(17, 1, 1, 2, 10)");
           migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(18, 1, 1, 2, 11)");
           migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(19, 1, 1, 2, 12)");
           migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(20, 1, 1, 2, 13)");
           migrationBuilder.Sql("Insert [WorkFlow].[ActionDefinitionForActivity] ([ID], [IsPostExecution], [Order], [ActionDefinitionID], [ActivityDefinitionID]) values(21, 1, 1, 3, 4)");
           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActionDefinitionForActivity] OFF");

           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActorDefinitionIsInRole] ON");
           migrationBuilder.Sql("Insert [WorkFlow].[ActorDefinitionIsInRole] ([ID], [RoleId], [WorkflowDefinitionID], [Name]) values (1, 1, 1, 'Administrator')");
           migrationBuilder.Sql("Insert [WorkFlow].[ActorDefinitionIsInRole] ([ID], [RoleId], [WorkflowDefinitionID], [Name]) values (2, 2, 1, 'Borrower')");
           migrationBuilder.Sql("Insert [WorkFlow].[ActorDefinitionIsInRole] ([ID], [RoleId], [WorkflowDefinitionID], [Name]) values (3, 3, 1, 'UnderWriter')");
           migrationBuilder.Sql("Insert [WorkFlow].[ActorDefinitionIsInRole] ([ID], [RoleId], [WorkflowDefinitionID], [Name]) values (4, 4, 1, 'NULTreasury')");
           migrationBuilder.Sql("Insert [WorkFlow].[ActorDefinitionIsInRole] ([ID], [RoleId], [WorkflowDefinitionID], [Name]) values (5, 5, 1, 'LoanProcessor')");
           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActorDefinitionIsInRole] OFF");
           
           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ConditionType] ON");
           migrationBuilder.Sql("Insert [WorkFlow].[ConditionType] ([ID], [Name], [Description]) values (1, 'Action', 'Action')");
           migrationBuilder.Sql("Insert [WorkFlow].[ConditionType] ([ID], [Name], [Description]) values (2, 'Always', 'Always')");
           migrationBuilder.Sql("Insert [WorkFlow].[ConditionType] ([ID], [Name], [Description]) values (3, 'Otherwise', 'Otherwise')");
           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ConditionType] OFF");


           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ConditionDefinition] ON");
           migrationBuilder.Sql("Insert [WorkFlow].[ConditionDefinition] ([ID], [Name], [ConditionTypeID], [ResultOnPreExecution], [Action_ID]) values (1, 'Default', 2, NULL, NULL)");
           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ConditionDefinition] OFF");

           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[CommandDefinition] ON");
           migrationBuilder.Sql("Insert [WorkFlow].[CommandDefinition] ([ID], [CommandIconClass], [WorkflowDefinitionID], [Name]) values (1, 'fa fa-check ', 1, 'Apply')");
           migrationBuilder.Sql("Insert [WorkFlow].[CommandDefinition] ([ID], [CommandIconClass], [WorkflowDefinitionID], [Name]) values (2, 'fa fa-floppy-o', 1, 'Save')");
           migrationBuilder.Sql("Insert [WorkFlow].[CommandDefinition] ([ID], [CommandIconClass], [WorkflowDefinitionID], [Name]) values (3, 'fa fa-check-square-o', 1, 'Submit')");
           migrationBuilder.Sql("Insert [WorkFlow].[CommandDefinition] ([ID], [CommandIconClass], [WorkflowDefinitionID], [Name]) values (4, 'fa fa-caret-square-o-right', 1, 'Accept')");
           migrationBuilder.Sql("Insert [WorkFlow].[CommandDefinition] ([ID], [CommandIconClass], [WorkflowDefinitionID], [Name]) values (5, 'fa fa-check-square-o', 1, 'Request More Info')");
           migrationBuilder.Sql("Insert [WorkFlow].[CommandDefinition] ([ID], [CommandIconClass], [WorkflowDefinitionID], [Name]) values (6, 'fa fa-share-square', 1, 'Approve')");
           migrationBuilder.Sql("Insert [WorkFlow].[CommandDefinition] ([ID], [CommandIconClass], [WorkflowDefinitionID], [Name]) values (7, 'fa fa-check-square', 1, 'Reject')");
           migrationBuilder.Sql("Insert [WorkFlow].[CommandDefinition] ([ID], [CommandIconClass], [WorkflowDefinitionID], [Name]) values (8, 'fa fa-check-square', 1, 'Accept Agreement')");
           migrationBuilder.Sql("Insert [WorkFlow].[CommandDefinition] ([ID], [CommandIconClass], [WorkflowDefinitionID], [Name]) values (9, 'fa fa-check-square', 1, 'Fund Initiate')");
           migrationBuilder.Sql("Insert [WorkFlow].[CommandDefinition] ([ID], [CommandIconClass], [WorkflowDefinitionID], [Name]) values (10, 'fa fa-check-square', 1, 'Disburse')");
           migrationBuilder.Sql("Insert [WorkFlow].[CommandDefinition] ([ID], [CommandIconClass], [WorkflowDefinitionID], [Name]) values (11, 'fa fa-check-square', 1, 'Submit Aggrement')");
           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[CommandDefinition] OFF");

           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TransitionClassifier] ON");
           migrationBuilder.Sql("Insert [WorkFlow].[TransitionClassifier] ([ID], [Name], [Description]) values (1, 'NotSpecified', 'NotSpecified')");
           migrationBuilder.Sql("Insert [WorkFlow].[TransitionClassifier] ([ID], [Name], [Description]) values (2, 'Direct', 'Direct')");
           migrationBuilder.Sql("Insert [WorkFlow].[TransitionClassifier] ([ID], [Name], [Description]) values (3, 'Reverse', 'Reverse')");
           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TransitionClassifier] OFF");

           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TriggerType] ON");
           migrationBuilder.Sql("Insert [WorkFlow].[TriggerType] ([ID], [Name], [Description]) values (1, 'Command', 'Command')");
           migrationBuilder.Sql("Insert [WorkFlow].[TriggerType] ([ID], [Name], [Description]) values (2, 'Auto', 'Auto')");
           migrationBuilder.Sql("Insert [WorkFlow].[TriggerType] ([ID], [Name], [Description]) values (3, 'Timer', 'Timer')");
           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TriggerType] OFF");

           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TriggerDefinition] ON");
           migrationBuilder.Sql("Insert [WorkFlow].[TriggerDefinition] ([ID], [Name], [CommandID], [TypeId]) values (1, 'ApplyTrigger', 1, 1)");
           migrationBuilder.Sql("Insert [WorkFlow].[TriggerDefinition] ([ID], [Name], [CommandID], [TypeId]) values (2, 'SaveTrigger', 2, 1)");
           migrationBuilder.Sql("Insert [WorkFlow].[TriggerDefinition] ([ID], [Name], [CommandID], [TypeId]) values (3, 'SubmitTrigger', 3, 1)");
           migrationBuilder.Sql("Insert [WorkFlow].[TriggerDefinition] ([ID], [Name], [CommandID], [TypeId]) values (4, 'AcceptTrigger', 4, 1)");
           migrationBuilder.Sql("Insert [WorkFlow].[TriggerDefinition] ([ID], [Name], [CommandID], [TypeId]) values (5, 'RequestMoreInfoTrigger', 5, 1)");
           migrationBuilder.Sql("Insert [WorkFlow].[TriggerDefinition] ([ID], [Name], [CommandID], [TypeId]) values (6, 'ApproveTrigger', 6, 1)");
           migrationBuilder.Sql("Insert [WorkFlow].[TriggerDefinition] ([ID], [Name], [CommandID], [TypeId]) values (7, 'RejectTrigger', 7, 1)");
           migrationBuilder.Sql("Insert [WorkFlow].[TriggerDefinition] ([ID], [Name], [CommandID], [TypeId]) values (8, 'AcceptAgreementTrigger', 8, 1)");
           migrationBuilder.Sql("Insert [WorkFlow].[TriggerDefinition] ([ID], [Name], [CommandID], [TypeId]) values (9, 'FundInitiateTrigger', 9, 1)");
           migrationBuilder.Sql("Insert [WorkFlow].[TriggerDefinition] ([ID], [Name], [CommandID], [TypeId]) values (10, 'DisburseTrigger', 10, 1)");
           migrationBuilder.Sql("Insert [WorkFlow].[TriggerDefinition] ([ID], [Name], [CommandID], [TypeId]) values (11, 'SubmitAggrementTrigger', 11, 1)");
           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TriggerDefinition] OFF");

           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TransitionDefinition] ON");
           migrationBuilder.Sql("Insert [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [Name], [FromID], ToID) values (1, 1, 2, 3, 1, 'InitializedToSubmitted', 1, 4)");
           migrationBuilder.Sql("Insert [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [Name], [FromID], ToID) values (6, 1, 2, 4, 1, 'SubmittedToAccepted', 4, 7)");
           migrationBuilder.Sql("Insert [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [Name], [FromID], ToID) values (7, 1, 3, 5, 1, 'SubmittedToRequestedMoreInfo', 4, 5)");
           migrationBuilder.Sql("Insert [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [Name], [FromID], ToID) values (8, 1, 2, 3, 1, 'RequestedMoreInfoToRequestCompleted', 5, 6)");
           migrationBuilder.Sql("Insert [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [Name], [FromID], ToID) values (9, 1, 3, 5, 1, 'RequestCompletedToRequestedMoreInfo', 6, 5)");
           migrationBuilder.Sql("Insert [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [Name], [FromID], ToID) values (10, 1, 2, 4, 1, 'RequestCompletedToAccepted', 6, 7)");
           migrationBuilder.Sql("Insert [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [Name], [FromID], ToID) values (11, 1, 2, 6, 1, 'AcceptedToApproved', 7, 8)");
           migrationBuilder.Sql("Insert [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [Name], [FromID], ToID) values (12, 1, 2, 7, 1, 'AcceptedToRejected', 7, 9)");
           migrationBuilder.Sql("Insert [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [Name], [FromID], ToID) values (13, 1, 2, 11, 1, 'ApprovedToAgreementUploaded', 8, 10)");
           migrationBuilder.Sql("Insert [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [Name], [FromID], ToID) values (14, 1, 2, 8, 1, 'AgreementUploadedToAgreementAccepted', 10, 11)");
           migrationBuilder.Sql("Insert [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [Name], [FromID], ToID) values (15, 1, 2, 9, 1, 'AgreementAcceptedToFundingInitiated', 11, 12)");
           migrationBuilder.Sql("Insert [WorkFlow].[TransitionDefinition] ([ID], [ConditionID], [TransitionClassifierID], [TriggerID], [WorkflowDefinitionID], [Name], [FromID], ToID) values (16, 1, 2, 10, 1, 'FundingInitiatedToFundingCompleted', 12, 13)");


           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TransitionDefinition] OFF");

           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ValidationType] ON");
           migrationBuilder.Sql("Insert [WorkFlow].[ValidationType] ([ValidationTypeID], [ValidationTypeName], [ValidationTypeDescription]) values (1, 'Required', 'Required Field')");
           migrationBuilder.Sql("Insert [WorkFlow].[ValidationType] ([ValidationTypeID], [ValidationTypeName], [ValidationTypeDescription]) values (2, 'Confirmation', 'Confirm Action')");
           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ValidationType] OFF");

           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[RestrictionType] ON");
           migrationBuilder.Sql("Insert [WorkFlow].[RestrictionType] ([ID], [Name], [Description]) values (1, 'Allow', 'Allow')");
           migrationBuilder.Sql("Insert [WorkFlow].[RestrictionType] ([ID], [Name], [Description]) values (2, 'Restrict', 'Restrict')");
           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[RestrictionType] OFF");

           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[RestrictionDefinition] ON");
           migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (1, 1, 1, 2)");
           migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (2, 1, 6, 1)");
           migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (3, 1, 7, 1)");
           migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (4, 1, 8, 2)");
           migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (5, 1, 9, 1)");
           migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (6, 1, 10, 1)");
           migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (7, 1, 11, 1)");
           migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (8, 1, 12, 1)");
           migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (9, 1, 13, 2)");
           migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (10, 1, 14, 1)");
           migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (11, 1, 15, 4)");
           migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (12, 1, 16, 4)");
           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[RestrictionDefinition] OFF");

           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ParameterPurpose] ON");
           migrationBuilder.Sql("Insert [WorkFlow].[ParameterPurpose] ([ID], [Name], [Description]) values (1, 'Temporary', 'Temporary')");
           migrationBuilder.Sql("Insert [WorkFlow].[ParameterPurpose] ([ID], [Name], [Description]) values (2, 'Persistence', 'Persistence')");
           migrationBuilder.Sql("Insert [WorkFlow].[ParameterPurpose] ([ID], [Name], [Description]) values (3, 'System', 'System')");
           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ParameterPurpose] OFF");

           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ParameterDefinition] ON");
           migrationBuilder.Sql("Insert [WorkFlow].[ParameterDefinition] ([ID], [TypeAsString], [PurposeID], [WorkflowDefinitionID], [Name]) values(1, 'System.Int64', 3, 1, 'ProcessId')");
           migrationBuilder.Sql("Insert [WorkFlow].[ParameterDefinition] ([ID], [TypeAsString], [PurposeID], [WorkflowDefinitionID], [Name]) values(2, 'System.String', 3, 1, 'PreviousState')");
           migrationBuilder.Sql("Insert [WorkFlow].[ParameterDefinition] ([ID], [TypeAsString], [PurposeID], [WorkflowDefinitionID], [Name]) values(3, 'System.String', 3, 1, 'CurrentState')");
           migrationBuilder.Sql("Insert [WorkFlow].[ParameterDefinition] ([ID], [TypeAsString], [PurposeID], [WorkflowDefinitionID], [Name]) values(4, 'System.String', 3, 1, 'PreviousStateForDirect')");
           migrationBuilder.Sql("Insert [WorkFlow].[ParameterDefinition] ([ID], [TypeAsString], [PurposeID], [WorkflowDefinitionID], [Name]) values(5, 'System.String', 3, 1, 'PreviousStateForReverse')");
           migrationBuilder.Sql("Insert [WorkFlow].[ParameterDefinition] ([ID], [TypeAsString], [PurposeID], [WorkflowDefinitionID], [Name]) values(6, 'System.String', 3, 1, 'PreviousActivity')");
           migrationBuilder.Sql("Insert [WorkFlow].[ParameterDefinition] ([ID], [TypeAsString], [PurposeID], [WorkflowDefinitionID], [Name]) values(7, 'System.String', 3, 1, 'CurrentActivity')");
           migrationBuilder.Sql("Insert [WorkFlow].[ParameterDefinition] ([ID], [TypeAsString], [PurposeID], [WorkflowDefinitionID], [Name]) values(8, 'System.String', 3, 1, 'PreviousActivityForDirect')");
           migrationBuilder.Sql("Insert [WorkFlow].[ParameterDefinition] ([ID], [TypeAsString], [PurposeID], [WorkflowDefinitionID], [Name]) values(9, 'System.String', 3, 1, 'PreviousActivityForReverse')");
           migrationBuilder.Sql("Insert [WorkFlow].[ParameterDefinition] ([ID], [TypeAsString], [PurposeID], [WorkflowDefinitionID], [Name]) values(10, 'System.String', 3, 1, 'CurrentCommand')");
           migrationBuilder.Sql("Insert [WorkFlow].[ParameterDefinition] ([ID], [TypeAsString], [PurposeID], [WorkflowDefinitionID], [Name]) values(11, 'System.String', 3, 1, 'ExecutedActivityState')");
           migrationBuilder.Sql("Insert [WorkFlow].[ParameterDefinition] ([ID], [TypeAsString], [PurposeID], [WorkflowDefinitionID], [Name]) values(12, 'System.Guid', 3, 1, 'IdentityId')");
           migrationBuilder.Sql("Insert [WorkFlow].[ParameterDefinition] ([ID], [TypeAsString], [PurposeID], [WorkflowDefinitionID], [Name]) values(13, 'System.Guid', 3, 1, 'ImpersonatedIdentityId')");
           migrationBuilder.Sql("Insert [WorkFlow].[ParameterDefinition] ([ID], [TypeAsString], [PurposeID], [WorkflowDefinitionID], [Name]) values(14, 'System.Guid', 3, 1, 'SchemeId')");
           migrationBuilder.Sql("Insert [WorkFlow].[ParameterDefinition] ([ID], [TypeAsString], [PurposeID], [WorkflowDefinitionID], [Name]) values(15, 'System.String', 3, 1, 'ProcessName')");
           migrationBuilder.Sql("Insert [WorkFlow].[ParameterDefinition] ([ID], [TypeAsString], [PurposeID], [WorkflowDefinitionID], [Name]) values(16, 'ThoughtFocus.Domain.User.UserSessionEntity,ThoughtFocus.Domain', 3, 1, 'UserSessionModel')");
           migrationBuilder.Sql("Insert [WorkFlow].[ParameterDefinition] ([ID], [TypeAsString], [PurposeID], [WorkflowDefinitionID], [Name]) values(17, 'System.String', 3, 1, 'Command')");
           migrationBuilder.Sql("Insert [WorkFlow].[ParameterDefinition] ([ID], [TypeAsString], [PurposeID], [WorkflowDefinitionID], [Name]) values(18, 'ThoughtFocus.Common.Workflow.Core.Model.ProcessInstance,ThoughtFocus.Common.Workflow.Core', 3, 1, 'ProcessInstance')");
           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ParameterDefinition] OFF");
         
           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ParameterDefinitionForAction] ON");
           migrationBuilder.Sql("Insert [WorkFlow].[ParameterDefinitionForAction] ([ID], [IsInputParameter], [ParameterDefinitionID], [ActionDefinitionID], [Order]) values (1, 1, 18, 1, 1)");
           migrationBuilder.Sql("Insert [WorkFlow].[ParameterDefinitionForAction] ([ID], [IsInputParameter], [ParameterDefinitionID], [ActionDefinitionID], [Order]) values (2, 1, 18, 2, 1)");
           migrationBuilder.Sql("Insert [WorkFlow].[ParameterDefinitionForAction] ([ID], [IsInputParameter], [ParameterDefinitionID], [ActionDefinitionID], [Order]) values (3, 1, 18, 3, 1)");
           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ParameterDefinitionForAction] OFF");

           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[LocalizeType] ON");
           migrationBuilder.Sql("Insert [WorkFlow].[LocalizeType] ([ID], [Name], [Description]) values (1, 'Command', 'Command')");
           migrationBuilder.Sql("Insert [WorkFlow].[LocalizeType] ([ID], [Name], [Description]) values (2, 'State', 'State')");
           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[LocalizeType] OFF");

           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[LocalizeDefinition] ON");
           migrationBuilder.Sql("Insert [WorkFlow].[LocalizeDefinition] ([ID], [LocalizeTypeID], [IsDefault], [Objectname], [Culture], [Value], [WorkflowDefinitionID]) values (1, 2, 1, 'Initialized', 'EN-US', 'Initialized', 1)");
           migrationBuilder.Sql("Insert [WorkFlow].[LocalizeDefinition] ([ID], [LocalizeTypeID], [IsDefault], [Objectname], [Culture], [Value], [WorkflowDefinitionID]) values (2, 2, 1, 'Drafted', 'EN-US', 'Drafted', 1)");
           migrationBuilder.Sql("Insert [WorkFlow].[LocalizeDefinition] ([ID], [LocalizeTypeID], [IsDefault], [Objectname], [Culture], [Value], [WorkflowDefinitionID]) values (3, 2, 1, 'Submitted', 'EN-US', 'Submitted', 1)");
           migrationBuilder.Sql("Insert [WorkFlow].[LocalizeDefinition] ([ID], [LocalizeTypeID], [IsDefault], [Objectname], [Culture], [Value], [WorkflowDefinitionID]) values (4, 2, 1, 'RequestedAddlInfo', 'EN-US', 'Requested Additional Information', 1)");
           migrationBuilder.Sql("Insert [WorkFlow].[LocalizeDefinition] ([ID], [LocalizeTypeID], [IsDefault], [Objectname], [Culture], [Value], [WorkflowDefinitionID]) values (5, 2, 1, 'SentForConditionalApproval', 'EN-US', 'Sent For Conditional Approval', 1)");
           migrationBuilder.Sql("Insert [WorkFlow].[LocalizeDefinition] ([ID], [LocalizeTypeID], [IsDefault], [Objectname], [Culture], [Value], [WorkflowDefinitionID]) values (6, 2, 1, 'ApprovedConditional', 'EN-US', 'Approved Conditional', 1)");
           migrationBuilder.Sql("Insert [WorkFlow].[LocalizeDefinition] ([ID], [LocalizeTypeID], [IsDefault], [Objectname], [Culture], [Value], [WorkflowDefinitionID]) values (7, 2, 1, 'RequestedFinalApproval', 'EN-US', 'Requested Final Approval', 1)");
           migrationBuilder.Sql("Insert [WorkFlow].[LocalizeDefinition] ([ID], [LocalizeTypeID], [IsDefault], [Objectname], [Culture], [Value], [WorkflowDefinitionID]) values (8, 2, 1, 'LoanApproved', 'EN-US', 'Loan Approved', 1)");
           migrationBuilder.Sql("Insert [WorkFlow].[LocalizeDefinition] ([ID], [LocalizeTypeID], [IsDefault], [Objectname], [Culture], [Value], [WorkflowDefinitionID]) values (9, 2, 1, 'FundingInitiated', 'EN-US', 'Funding Initiated', 1)");
           migrationBuilder.Sql("Insert [WorkFlow].[LocalizeDefinition] ([ID], [LocalizeTypeID], [IsDefault], [Objectname], [Culture], [Value], [WorkflowDefinitionID]) values (10, 2, 1, 'LoanAmountFunded', 'EN-US', 'Loan Amount Funded', 1)");
           migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[LocalizeDefinition] OFF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [WorkFlow].[LocalizeDefinition]"); 
            migrationBuilder.Sql("delete from [WorkFlow].[LocalizeType]"); 
            migrationBuilder.Sql("delete from [WorkFlow].[ParameterDefinitionForAction]");
            migrationBuilder.Sql("delete from [WorkFlow].[ParameterDefinition]"); 
            migrationBuilder.Sql("delete from [WorkFlow].[ParameterPurpose]"); 
            migrationBuilder.Sql("delete from [WorkFlow].[RestrictionDefinition]"); 
            migrationBuilder.Sql("delete from [WorkFlow].[RestrictionType]"); 
            migrationBuilder.Sql("delete from [WorkFlow].[ValidationType]"); 
            migrationBuilder.Sql("delete from [WorkFlow].[TransitionDefinition]"); 
            migrationBuilder.Sql("delete from [WorkFlow].[TriggerDefinition] "); 
            migrationBuilder.Sql("delete from [WorkFlow].[TriggerType] "); 
            migrationBuilder.Sql("delete from [WorkFlow].[TransitionClassifier] "); 
            migrationBuilder.Sql("delete from [WorkFlow].[CommandDefinition]"); 
            migrationBuilder.Sql("delete from [WorkFlow].[ConditionDefinition]"); 
            migrationBuilder.Sql("delete from [WorkFlow].[ConditionType]"); 
            migrationBuilder.Sql("delete from [WorkFlow].[ActorDefinitionIsInRole]"); 
            migrationBuilder.Sql("delete from [WorkFlow].[ActionDefinitionForActivity]");
            migrationBuilder.Sql("delete from [WorkFlow].[ActionDefinition]");
            migrationBuilder.Sql("delete from [WorkFlow].[ActivityDefinition]"); 
            migrationBuilder.Sql("delete from [WorkFlow].[WorkflowDefinition]"); 
        }
    }
}
