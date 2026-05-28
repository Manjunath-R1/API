using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class addDocumentTypeAndCategorySeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[DocumentCategorys] ON");
            migrationBuilder.Sql("INSERT [Master].[DocumentCategorys] ([DocumentCategoryID],[IsActive], [Name], [Description]) VALUES (1,1, N'Application', N'Application Documents')");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[DocumentCategorys] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[DocumentTypes] ON");
            migrationBuilder.Sql("INSERT [Master].[DocumentTypes] ([DocumentTypeID], [IsActive], [Name], [Description], [DocumentCategoryID], [DisplayOrder]) VALUES (1, 1, N'DRIVER LICENSE', N'DRIVER''s LICENSE', 1, 1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[DocumentTypes] OFF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[DocumentTypes] where DocumentTypeID = 1");
            migrationBuilder.Sql("delete from [Master].[DocumentCategorys] where DocumentCategoryID = 1");
        }
    }
}
