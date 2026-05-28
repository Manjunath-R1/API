using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class addMasterTablesSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[FundingTypes] ON ");
            migrationBuilder.Sql("INSERT [Master].[FundingTypes] ([FundingTypeID],[Type],[Description],[IsActive],[DisplayOrder]) VALUES (1, N'Loan', N'Loan', 1, 1)");
            migrationBuilder.Sql("INSERT [Master].[FundingTypes] ([FundingTypeID],[Type],[Description],[IsActive],[DisplayOrder]) VALUES (2, N'Grant', N'Grant', 1, 2)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[FundingTypes] OFF ");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[TransactionTypes] ON ");
            migrationBuilder.Sql("INSERT [Master].[TransactionTypes] ([TransactionTypeID],[Type],[Description],[IsActive],[DisplayOrder]) VALUES (1, N'Add', N'Add', 1, 1)");
            migrationBuilder.Sql("INSERT [Master].[TransactionTypes] ([TransactionTypeID],[Type],[Description],[IsActive],[DisplayOrder]) VALUES (2, N'Remove', N'Remove', 1, 2)");
            migrationBuilder.Sql("INSERT [Master].[TransactionTypes] ([TransactionTypeID],[Type],[Description],[IsActive],[DisplayOrder]) VALUES (3, N'Allocate', N'Allocate', 1, 3)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[TransactionTypes] OFF ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[FundingTypes] where FundingTypeID between 1 and 2");
            migrationBuilder.Sql("delete from [Master].[TransactionTypes] where TransactionTypeID between 1 and 3");

        }
    }
}
