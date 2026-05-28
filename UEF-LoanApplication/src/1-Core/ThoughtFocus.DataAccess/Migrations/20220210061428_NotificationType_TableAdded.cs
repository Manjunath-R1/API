using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class NotificationType_TableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationModes",
                schema: "Master",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationModes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NotificationModesTypes",
                schema: "Notification",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationModesID = table.Column<long>(type: "bigint", nullable: false),
                    UserID = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationModesTypes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NotificationModesTypes_NotificationModes_NotificationModesID",
                        column: x => x.NotificationModesID,
                        principalSchema: "Master",
                        principalTable: "NotificationModes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationModesTypes_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "User",
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationModesTypes_NotificationModesID",
                schema: "Notification",
                table: "NotificationModesTypes",
                column: "NotificationModesID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationModesTypes_UserID",
                schema: "Notification",
                table: "NotificationModesTypes",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationModesTypes",
                schema: "Notification");

            migrationBuilder.DropTable(
                name: "NotificationModes",
                schema: "Master");
        }
    }
}
