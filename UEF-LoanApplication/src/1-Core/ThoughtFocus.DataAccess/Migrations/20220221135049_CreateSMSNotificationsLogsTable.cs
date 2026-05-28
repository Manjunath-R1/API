using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class CreateSMSNotificationsLogsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "SMSNotificationLog",
               schema: "Notification",
               columns: table => new
               {
                   ID = table.Column<long>(type: "bigint", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   To = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   FROM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   UserID = table.Column<long>(type: "bigint", nullable: false),
                   TemplateID = table.Column<long>(type: "bigint", nullable: false),
                   Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   IsActive = table.Column<bool>(type: "bit", nullable: false),
                   IsSuccess = table.Column<bool>(type: "bit", nullable: false),
                   ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                   CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                   LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                   LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_SMSNotificationLog", x => x.ID);
                   table.ForeignKey(
                       name: "FK_SMSNotificationLog_Users_UserID",
                       column: x => x.UserID,
                       principalSchema: "User",
                       principalTable: "Users",
                       principalColumn: "UserID",
                       onDelete: ReferentialAction.Cascade);
                   table.ForeignKey(
                       name: "FK_SMSNotificationLog_SMSNotificationTemplate_TemplateId",
                       column: x => x.TemplateID,
                       principalSchema: "Master",
                       principalTable: "SMSNotificationTemplate",
                       principalColumn: "ID",
                       onDelete: ReferentialAction.Cascade);
               });

            migrationBuilder.CreateIndex(
                name: "IX_SMSNotificationLog_UserID",
                schema: "Notification",
                table: "SMSNotificationLog",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_SMSNotificationLog_TemplateId",
                schema: "Notification",
                table: "SMSNotificationLog",
                column: "TemplateID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SMSNotificationLog",
                schema: "Notification");
        }
    }
}
