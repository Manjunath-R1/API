using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class rolepermission_QuestionsDocuments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Action] ON ");

            migrationBuilder.Sql("Insert [Master].[Action] ([ActionID], [Name], [Description], [IsActive], [DisplayOrder]) values (55, 'GetAllQuestions', 'Get AllQuestions', 1, 55)");
            migrationBuilder.Sql("Insert [Master].[Action] ([ActionID], [Name], [Description], [IsActive], [DisplayOrder]) values (56, 'GetQuestion', 'Get Question', 1, 56)");
            migrationBuilder.Sql("Insert [Master].[Action] ([ActionID], [Name], [Description], [IsActive], [DisplayOrder]) values (57, 'SaveOrUpdateQuestions', 'Save/UpdateQuestions', 1, 57)");

            migrationBuilder.Sql("Insert [Master].[Action] ([ActionID], [Name], [Description], [IsActive], [DisplayOrder]) values (58, 'GetAllDocumentTypes', 'Get AllDocumentTypes', 1, 58)");
            migrationBuilder.Sql("Insert [Master].[Action] ([ActionID], [Name], [Description], [IsActive], [DisplayOrder]) values (59, 'GetDocument', 'Get Document', 1, 59)");
            migrationBuilder.Sql("Insert [Master].[Action] ([ActionID], [Name], [Description], [IsActive], [DisplayOrder]) values (60, 'SaveOrUpdateDocuments', 'Save/UpdateDocuments', 1, 60)");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Action] OFF ");


            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[RolePermission] ON ");

            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (237, 1, 55, 'Admin', 1, 1, 237)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (238, 3, 55, 'Admin', 1, 1, 238)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (239, 7, 55, 'Admin', 1, 1, 239)");

            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (240, 1, 56, 'Admin', 1, 1, 240)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (241, 3, 56, 'Admin', 1, 1, 241)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (242, 7, 56, 'Admin', 1, 1, 242)");

            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (243, 1, 57, 'Admin', 1, 1, 243)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (244, 3, 57, 'Admin', 1, 1, 244)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (245, 7, 57, 'Admin', 1, 1, 245)");

            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (246, 1, 58, 'Admin', 1, 1, 246)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (247, 3, 58, 'Admin', 1, 1, 247)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (248, 7, 58, 'Admin', 1, 1, 248)");

            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (249, 1, 59, 'Admin', 1, 1, 249)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (250, 3, 59, 'Admin', 1, 1, 250)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (251, 7, 59, 'Admin', 1, 1, 251)");

            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (252, 1, 60, 'Admin', 1, 1, 252)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (253, 3, 60, 'Admin', 1, 1, 253)");
            migrationBuilder.Sql("Insert [Master].[RolePermission] ([RolePermissionID], [RoleID], [ActionID], [Subject], [IsAllowed], [IsActive], [DisplayOrder])  values (254, 7, 60, 'Admin', 1, 1, 254)");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[RolePermission] OFF ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[Action]  where ActionID between 55 and 60");
            migrationBuilder.Sql("delete from [Master].[RolePermission]  where RolePermissionID between 237 and 254");

        }
    }
}
