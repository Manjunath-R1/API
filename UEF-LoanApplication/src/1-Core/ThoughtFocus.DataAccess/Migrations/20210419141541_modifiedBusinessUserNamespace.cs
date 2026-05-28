using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class modifiedBusinessUserNamespace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "BusinessUser",
                schema: "Contact",
                newName: "BusinessUser",
                newSchema: "Admin");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "BusinessUser",
                schema: "Admin",
                newName: "BusinessUser",
                newSchema: "Contact");
        }
    }
}
