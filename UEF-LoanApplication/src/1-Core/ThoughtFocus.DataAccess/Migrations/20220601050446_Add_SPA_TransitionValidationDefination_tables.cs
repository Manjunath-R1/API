using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class Add_SPA_TransitionValidationDefination_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TransitionValidationDefination] ON");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) VALUES (17, N'FundUtilizationValidation', 1, 1, 52)");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) VALUES (18, N'CommentRequiredValidation', 1, 2, 43)");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) VALUES (19, N'CommentRequiredValidation', 1, 2, 44)");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) VALUES (20, N'CommentRequiredValidation', 1, 2, 45)");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) VALUES (21, N'CommentRequiredValidation', 1, 3, 49)");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) VALUES (22, N'CommentRequiredValidation', 1, 2, 54)");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) VALUES (23, N'CommentRequiredValidation', 1, 2, 55)");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) VALUES (24, N'CommentRequiredValidation', 1, 2, 57)");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) VALUES (25, N'CommentRequiredValidation', 1, 2, 60)");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) VALUES (26, N'CommentRequiredValidation', 1, 2, 62)");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) VALUES (27, N'CommentRequiredValidation', 1, 2, 63)");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) VALUES (28, N'CommentRequiredValidation', 1, 2, 66)");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) VALUES (29, N'CommentRequiredValidation', 1, 2, 67)");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) VALUES (30, N'CommentRequiredValidation', 1, 2, 69)");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) VALUES (31, N'CommentRequiredValidation', 1, 2, 71)");
            migrationBuilder.Sql("INSERT [WorkFlow].[TransitionValidationDefination] ([TransitionValidationDefinationID], [TransitionValidationName], [IsEnabled], [ValidationDefinationID], [TransitionDefinitionID]) VALUES (32, N'CommentRequiredValidation', 1, 2, 73)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [WorkFlow].[TransitionValidationDefination] OFF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
