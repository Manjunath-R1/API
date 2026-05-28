using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class AddNewColumnsApplicationDocuments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DocumentID",
                schema: "Application",
                table: "ApplicationDocuments",
                newName: "FileSize");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                schema: "Application",
                table: "ApplicationDocuments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhysicalFileStorageKey",
                schema: "Application",
                table: "ApplicationDocuments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                schema: "Application",
                table: "ApplicationDocuments");

            migrationBuilder.DropColumn(
                name: "PhysicalFileStorageKey",
                schema: "Application",
                table: "ApplicationDocuments");

            migrationBuilder.RenameColumn(
                name: "FileSize",
                schema: "Application",
                table: "ApplicationDocuments",
                newName: "DocumentID");
        }
    }
}
