using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.Common.WorkFlowDataAccess.Migrations
{
    public partial class DMLWorkFlowAddingControllerRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update [WorkFlow].[ActivityDefinition] set Name = 'CFOApproved', State = 'CFOApproved' where Name = 'FundingInitiated'");
            migrationBuilder.Sql("update [WorkFlow].[ActivityDefinition] set Name = 'AccountDisbursed', State = 'AccountDisbursed' where Name = 'FundingCompleted'");

            migrationBuilder.Sql("update [WorkFlow].[TransitionDefinition] set Name = 'AgreementSubmittedToCFOApproved', FromID = 14, ToID = 12, TriggerID = 6  where Name = 'AgreementAcceptedToFundingInitiated'");
            migrationBuilder.Sql("update [WorkFlow].[TransitionDefinition] set Name = 'CFOApprovedToAccountDisbursed', FromID = 12, ToID = 13  where Name = 'AgreementSubmittedToFundingCompleted'");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActorDefinitionIsInRole] ON");
            migrationBuilder.Sql("Insert [WorkFlow].[ActorDefinitionIsInRole] ([ID], [RoleId], [WorkflowDefinitionID], [Name]) values (6, 6, 1, 'Controller')");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActorDefinitionIsInRole] OFF");
          
            migrationBuilder.Sql("update WorkFlow.RestrictionDefinition set ActorDefinitionIsInRole_ID = 3 where Transition_ID IN (6,7,9,10,14)");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[RestrictionDefinition] ON");
            migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (17, 1, 16, 6)");
            migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (18, 1, 6, 5)");
            migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (19, 1, 7, 5)");
            migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (20, 1, 9, 5)");
            migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (21, 1, 10, 5)");
            migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (22, 1, 11, 5)");
            migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (23, 1, 12, 5)");
            migrationBuilder.Sql("insert [WorkFlow].[RestrictionDefinition] ([ID], [RestrictionType_Id], [Transition_ID], [ActorDefinitionIsInRole_ID]) values (24, 1, 14, 5)");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[RestrictionDefinition] OFF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [WorkFlow].[RestrictionDefinition] where ID between 17 and 24");
            migrationBuilder.Sql("update WorkFlow.RestrictionDefinition set ActorDefinitionIsInRole_ID = 1 where Transition_ID IN (6,7,9,10,14)");
            migrationBuilder.Sql("delete from [WorkFlow].[ActorDefinitionIsInRole] where ID = 6"); 
            
            migrationBuilder.Sql("update [WorkFlow].[TransitionDefinition] set Name = 'AgreementAcceptedToFundingInitiated', FromID = 11, ToID = 12, TriggerID = 9  where Name = 'AgreementSubmittedToCFOApproved'");
            migrationBuilder.Sql("update [WorkFlow].[TransitionDefinition] set Name = 'AgreementSubmittedToFundingCompleted', FromID = 14, ToID = 13  where Name = 'CFOApprovedToAccountDisbursed'");

            migrationBuilder.Sql("update [WorkFlow].[ActivityDefinition] set Name = 'FundingInitiated', State = 'FundingInitiated' where Name = 'CFOApproved'");
            migrationBuilder.Sql("update [WorkFlow].[ActivityDefinition] set Name = 'FundingCompleted', State = 'FundingCompleted' where Name = 'AccountDisbursed'"); 
        }
    }
}
