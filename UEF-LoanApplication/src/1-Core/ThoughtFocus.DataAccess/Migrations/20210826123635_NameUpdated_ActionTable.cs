using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class NameUpdated_ActionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.Sql("update [Master].[Action] set  [Name] = 'GetFoundAllocation', [Description] ='Get FoundAllocation' where ActionID = 49");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
