using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class AlterTable_FundTransactionDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                schema: "FundingSource",
                table: "FundTransactionDocuments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FileSize",
                schema: "FundingSource",
                table: "FundTransactionDocuments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "PhysicalFileStorageKey",
                schema: "FundingSource",
                table: "FundTransactionDocuments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[DocumentCategorys] ON");
               migrationBuilder.Sql("INSERT [Master].[DocumentCategorys] ([DocumentCategoryID],[IsActive], [Name], [Description]) VALUES (2,1, N'FundingSource', N'Funding Source')");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[DocumentCategorys] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[DocumentTypes] ON");
               migrationBuilder.Sql("INSERT [Master].[DocumentTypes] ([DocumentTypeID], [IsActive], [Name], [Description], [DocumentCategoryID], [DisplayOrder]) VALUES (8, 1, N'Fund Transaction Document', N'Fund Transaction Document', 2, 8)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[DocumentTypes] OFF");    
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[DocumentTypes] where DocumentTypeID = 8");
            migrationBuilder.Sql("delete from [Master].[DocumentCategorys] where DocumentCategoryID = 2");
        
            migrationBuilder.DropColumn(
                name: "FileName",
                schema: "FundingSource",
                table: "FundTransactionDocuments");

            migrationBuilder.DropColumn(
                name: "FileSize",
                schema: "FundingSource",
                table: "FundTransactionDocuments");

            migrationBuilder.DropColumn(
                name: "PhysicalFileStorageKey",
                schema: "FundingSource",
                table: "FundTransactionDocuments");
        }
    }
}
