using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class Update_SIC_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update [Master].[SIC] set  [IndustryTitle] = 'Agriculture, Forestry, And Fishing' where ID = 1");
            migrationBuilder.Sql("update [Master].[SIC] set  [IndustryTitle] = 'Transportation, Communications, Electric, Gas, And SanitaryServices' where ID = 5");
            migrationBuilder.Sql("update [Master].[SIC] set  [IndustryTitle] = 'Finance, Insurance, And RealEstate' where ID = 8");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
