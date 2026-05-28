using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class alterTableBusinessEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessEntity_BusinessType_BusinessTypeID",
                schema: "Admin",
                table: "BusinessEntity");

            migrationBuilder.AlterColumn<long>(
                name: "BusinessTypeID",
                schema: "Admin",
                table: "BusinessEntity",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessEntity_BusinessType_BusinessTypeID",
                schema: "Admin",
                table: "BusinessEntity",
                column: "BusinessTypeID",
                principalSchema: "Master",
                principalTable: "BusinessType",
                principalColumn: "BusinessTypeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessEntity_BusinessType_BusinessTypeID",
                schema: "Admin",
                table: "BusinessEntity");

            migrationBuilder.AlterColumn<long>(
                name: "BusinessTypeID",
                schema: "Admin",
                table: "BusinessEntity",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessEntity_BusinessType_BusinessTypeID",
                schema: "Admin",
                table: "BusinessEntity",
                column: "BusinessTypeID",
                principalSchema: "Master",
                principalTable: "BusinessType",
                principalColumn: "BusinessTypeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
