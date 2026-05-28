using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class AddSeeding_DocumentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[DocumentTypes] ON");
            migrationBuilder.Sql("INSERT [Master].[DocumentTypes] ([DocumentTypeID], [IsActive], [Name], [Description], [DocumentCategoryID], [DisplayOrder]) VALUES (7, 1, N'Funding Detail Document', N'Funding Detail Document', 1, 7)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[DocumentTypes] OFF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[DocumentTypes] where DocumentTypeID =7");
        }
    }
}
