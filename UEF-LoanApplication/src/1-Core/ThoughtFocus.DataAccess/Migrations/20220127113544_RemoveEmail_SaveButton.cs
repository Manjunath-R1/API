using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class RemoveEmail_SaveButton : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[ApplicationStatus] ON");
            migrationBuilder.Sql("INSERT [Master].[ApplicationStatus] ([ApplicationStatusID], [ApplicationStatusName], [Description], [IsActive], [DisplayOrder], [WorkFlowID]) VALUES (25, N'RequestedMoreInfoToSave', N'Requested More Information', 1, 0, 1)");
            migrationBuilder.Sql("INSERT [Master].[ApplicationStatus] ([ApplicationStatusID], [ApplicationStatusName], [Description], [IsActive], [DisplayOrder], [WorkFlowID]) VALUES (26, N'UWReviewRequestedMoreDetailsToSave', N'Requested More Information', 1, 0, 1)");
            migrationBuilder.Sql("INSERT [Master].[ApplicationStatus] ([ApplicationStatusID], [ApplicationStatusName], [Description], [IsActive], [DisplayOrder], [WorkFlowID]) VALUES (27, N'RequestedMoreInformationToSave', N'Requested More Information', 1, 0, 1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[ApplicationStatus] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActivityDefinition] ON");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (25, N'RequestedMoreInfoToSave', 0, 0, 1, 1, 1, N'RequestedMoreInfoToSave')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (26, N'UWReviewRequestedMoreDetailsToSave', 0, 0, 1, 1, 1, N'UWReviewRequestedMoreDetailsToSave')");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActivityDefinition] ([ID], [State], [IsInitial], [IsFinal], [IsForSetState], [IsAutoSchemeUpdate], [WorkflowDefinitionID], [Name]) VALUES (27, N'RequestedMoreInformationToSave', 0, 0, 1, 1, 1, N'RequestedMoreInformationToSave')");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActivityDefinition] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActionDefinitionForActivity] ON");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (60, 1, 25, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (61, 5, 25, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (62, 1, 26, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (63, 5, 26, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (64, 1, 27, 1, 1)");
            migrationBuilder.Sql("INSERT [WorkFlow].[ActionDefinitionForActivity] ([ID], [ActionDefinitionID], [ActivityDefinitionID], [IsPostExecution], [Order]) VALUES (65, 5, 27, 1, 1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[ActionDefinitionForActivity] OFF");

            migrationBuilder.Sql("update WorkFlow.TransitionDefinition  set ToID = 25 where id = 19");
            migrationBuilder.Sql("update WorkFlow.TransitionDefinition  set ToID = 26 where id = 27");
            migrationBuilder.Sql("update WorkFlow.TransitionDefinition  set ToID = 27 where id = 34");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[ApplicationStatus]  where [ApplicationStatusID] between 25 and 27");
            migrationBuilder.Sql("delete from [WorkFlow].[ActionDefinitionForActivity]  where [ID] between 60 and 65");
            migrationBuilder.Sql("delete from [WorkFlow].[ActivityDefinition]  where [ID] between 25 and 27");

            migrationBuilder.Sql("update WorkFlow.TransitionDefinition  set ToID = 16 where id = 19");
            migrationBuilder.Sql("update WorkFlow.TransitionDefinition  set ToID = 21 where id = 27");
            migrationBuilder.Sql("update WorkFlow.TransitionDefinition  set ToID = 5 where id = 34");


        }
    }
}
