using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class added_Program_status_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProgramStatus",
                schema: "Master",
                columns: table => new
                {
                    ProgramStatusID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramStatusName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramStatus", x => x.ProgramStatusID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramInvitation_ProgramStatusID",
                schema: "Admin",
                table: "ProgramInvitation",
                column: "ProgramStatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramInvitation_ProgramStatus_ProgramStatusID",
                schema: "Admin",
                table: "ProgramInvitation",
                column: "ProgramStatusID",
                principalSchema: "Master",
                principalTable: "ProgramStatus",
                principalColumn: "ProgramStatusID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramInvitation_ProgramStatus_ProgramStatusID",
                schema: "Admin",
                table: "ProgramInvitation");

            migrationBuilder.DropTable(
                name: "ProgramStatus",
                schema: "Master");

            migrationBuilder.DropIndex(
                name: "IX_ProgramInvitation_ProgramStatusID",
                schema: "Admin",
                table: "ProgramInvitation");
        }
    }
}
