using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class updateSIPSpace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.Sql("update [Master].[SIC] set  [IndustryTitle] = 'Transportation, Communications, Electric, Gas, And Sanitary Services' where ID = 5");
            migrationBuilder.Sql("update [Master].[SIC] set  [IndustryTitle] = 'Wholesale Trade' where ID = 6");
            migrationBuilder.Sql("update [Master].[SIC] set  [IndustryTitle] = 'Retail Trade' where ID = 7");
            migrationBuilder.Sql("update [Master].[SIC] set  [IndustryTitle] = 'Public Administration' where ID = 10");
            migrationBuilder.Sql("update [Master].[SIC] set  [IndustryTitle] = 'Finance, Insurance, And Real Estate' where ID = 8");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
