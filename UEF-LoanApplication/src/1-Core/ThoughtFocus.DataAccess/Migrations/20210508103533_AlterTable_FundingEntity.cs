using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class AlterTable_FundingEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                schema: "FundingSource",
                table: "FundingEntities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StateID",
                schema: "FundingSource",
                table: "FundingEntities",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                schema: "FundingSource",
                table: "FundingEntities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FundingEntities_StateID",
                schema: "FundingSource",
                table: "FundingEntities",
                column: "StateID");

            migrationBuilder.AddForeignKey(
                name: "FK_FundingEntities_State_StateID",
                schema: "FundingSource",
                table: "FundingEntities",
                column: "StateID",
                principalSchema: "Master",
                principalTable: "State",
                principalColumn: "StateID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FundingEntities_State_StateID",
                schema: "FundingSource",
                table: "FundingEntities");

            migrationBuilder.DropIndex(
                name: "IX_FundingEntities_StateID",
                schema: "FundingSource",
                table: "FundingEntities");

            migrationBuilder.DropColumn(
                name: "City",
                schema: "FundingSource",
                table: "FundingEntities");

            migrationBuilder.DropColumn(
                name: "StateID",
                schema: "FundingSource",
                table: "FundingEntities");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                schema: "FundingSource",
                table: "FundingEntities");
        }
    }
}
