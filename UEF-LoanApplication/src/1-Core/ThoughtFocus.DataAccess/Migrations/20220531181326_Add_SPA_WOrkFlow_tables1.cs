using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class Add_SPA_WOrkFlow_tables1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[WorkflowDefinition] ON ");
            migrationBuilder.Sql("Insert [WorkFlow].[WorkflowDefinition] ([ID], [DesignerModel], [Name]) values (2, 'Schedule of Payment', 'Schedule of Payment')");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[WorkflowDefinition] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActivityDefinition] ON");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (28, N'Initialized', 1, 0, 1, 1, 2, N'Initialized')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (29, N'Created', 0, 0, 1, 1, 2, N'Created')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (30, N'Drafted', 0, 0, 1, 1, 2, N'Drafted')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (31, N'Submitted', 0, 0, 1, 1, 2, N'Submitted')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (32, N'RequestedMoreInfo', 0, 0, 1, 1, 2, N'RequestedMoreInfo')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (33, N'RequestCompleted', 0, 0, 1, 1, 2, N'RequestCompleted')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (34, N'Accepted', 0, 0, 1, 1, 2, N'Accepted')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (35, N'Approved', 0, 0, 1, 1, 2, N'Approved')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (36, N'Rejected', 0, 0, 1, 1, 2, N'Rejected')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (37, N'AgreementUploaded', 0, 0, 1, 1, 2, N'AgreementUploaded')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (38, N'AgreementAccepted', 0, 1, 1, 1, 2, N'AgreementAccepted')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (39, N'CFOApproved', 0, 1, 1, 1, 2, N'CFOApproved')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (40, N'FinalDisbursed', 0, 1, 1, 1, 2, N'FinalDisbursed')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (41, N'AgreementSubmitted', 0, 0, 1, 1, 2, N'AgreementSubmitted')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (42, N'AgreementRejected', 0, 0, 1, 1, 2, N'AgreementRejected')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (43, N'RequestedMoreDetails', 0, 0, 1, 1, 2, N'RequestedMoreDetails')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (44, N'RequestMoreDeatailsCompleted', 0, 0, 1, 1, 2, N'RequestMoreDeatailsCompleted')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (45, N'UWReview', 0, 0, 1, 1, 2, N'UWReview')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (46, N'UWApproved', 0, 0, 1, 1, 2, N'UWApproved')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (47, N'RequestMoreDeatailsCompletedByBorrower', 0, 0, 1, 1, 2, N'RequestMoreDeatailsCompletedByBorrower')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (48, N'UWReviewToRequestedMoreDetails', 0, 0, 1, 1, 2, N'UWReviewToRequestedMoreDetails')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (49, N'UWReviewRequestedMoreDetailsCompletedByCfoController', 0, 0, 1, 1, 2, N'UWReviewRequestedMoreDetailsCompletedByCfoController')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (50, N'UWReviewRequestedMoreDetailsCompletedByBorrower', 0, 0, 1, 1, 2, N'UWReviewRequestedMoreDetailsCompletedByBorrower')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (51, N'AgreementSubmittedByReInitiate', 0, 0, 1, 1, 2, N'AgreementSubmittedByReInitiate')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (52, N'RequestedMoreInfoToSave', 0, 0, 1, 1, 2, N'RequestedMoreInfoToSave')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (53, N'UWReviewRequestedMoreDetailsToSave', 0, 0, 1, 1, 2, N'RequestedMoreInformationToSave')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (54, N'RequestedMoreInformationToSave', 0, 0, 1, 1, 2, N'RequestedMoreInformationToSave')");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActivityDefinition] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActionDefinition] ON");

            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinition] ([ID], [TypeAsString], [FullTypeName], [MethodName], [WorkflowDefinitionID], [Name]) VALUES (7, N'ThoughtFocus.Workflow.WorkflowActions,ThoughtFocus.Workflow', N'UpdateLoanApplicationStatus', N'UpdateLoanApplicationStatus', 2, N'UpdateLoanApplicationStatus')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinition] ([ID], [TypeAsString], [FullTypeName], [MethodName], [WorkflowDefinitionID], [Name]) VALUES (8, N'ThoughtFocus.Workflow.WorkflowActions,ThoughtFocus.Workflow', N'SendNotification', N'SendNotification', 2, N'SendNotification')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinition] ([ID], [TypeAsString], [FullTypeName], [MethodName], [WorkflowDefinitionID], [Name]) VALUES (9, N'ThoughtFocus.Workflow.WorkflowActions,ThoughtFocus.Workflow', N'UpdateProgramInvitationStatus', N'UpdateProgramInvitationStatus', 2, N'UpdateProgramInvitationStatus')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinition] ([ID], [TypeAsString], [FullTypeName], [MethodName], [WorkflowDefinitionID], [Name]) VALUES (10, N'ThoughtFocus.Workflow.WorkflowActions,ThoughtFocus.Workflow', N'UpdateFundUtilization', N'UpdateFundUtilization', 2, N'UpdateFundUtilization')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinition] ([ID], [TypeAsString], [FullTypeName], [MethodName], [WorkflowDefinitionID], [Name]) VALUES (11, N'ThoughtFocus.Workflow.WorkflowActions,ThoughtFocus.Workflow', N'SaveTransitionComments', N'SaveTransitionComments', 2, N'SaveTransitionComments')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinition] ([ID], [TypeAsString], [FullTypeName], [MethodName], [WorkflowDefinitionID], [Name]) VALUES (12, N'ThoughtFocus.Workflow.WorkflowActions,ThoughtFocus.Workflow', N'SaveAgreementDetails', N'SaveAgreementDetails', 2, N'SaveAgreementDetails')");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActionDefinition] OFF");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
