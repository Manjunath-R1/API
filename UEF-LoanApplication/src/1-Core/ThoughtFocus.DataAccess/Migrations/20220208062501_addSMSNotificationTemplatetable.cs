using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class addSMSNotificationTemplatetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SMSNotificationTemplate",
                schema: "Master",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Template = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ApplicationStatusID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSNotificationTemplate", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SMSNotificationTemplate_ApplicationStatus_ApplicationStatusID",
                        column: x => x.ApplicationStatusID,
                        principalSchema: "Master",
                        principalTable: "ApplicationStatus",
                        principalColumn: "ApplicationStatusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SMSNotificationTemplate_ApplicationStatusID",
                schema: "Master",
                table: "SMSNotificationTemplate",
                column: "ApplicationStatusID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SMSNotificationTemplate",
                schema: "Master");
        }
    }
}
