using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class addContactAndBusinessContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_ContactID",
                schema: "User",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BusinessName",
                schema: "Contact",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ProgramName",
                schema: "Contact",
                table: "Contacts");

            migrationBuilder.AlterColumn<long>(
                name: "BusinessRoleID",
                schema: "Contact",
                table: "Contacts",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "AffiliateID",
                schema: "Contact",
                table: "Contacts",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "BusinessID",
                schema: "Contact",
                table: "Contacts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                schema: "Contact",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ContactID",
                schema: "User",
                table: "Users",
                column: "ContactID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_ContactID",
                schema: "User",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BusinessID",
                schema: "Contact",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "Contact",
                table: "Contacts");

            migrationBuilder.AlterColumn<long>(
                name: "BusinessRoleID",
                schema: "Contact",
                table: "Contacts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AffiliateID",
                schema: "Contact",
                table: "Contacts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessName",
                schema: "Contact",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProgramName",
                schema: "Contact",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ContactID",
                schema: "User",
                table: "Users",
                column: "ContactID");
        }
    }
}
