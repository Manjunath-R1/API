using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class add_ContactInvitation_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactInvitationInfo",
                schema: "Contact",
                columns: table => new
                {
                    ContactInvitationInfoID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactID = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ContactInvitationStatusID = table.Column<long>(type: "bigint", nullable: false),
                    ContactInvitedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContactActionDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InvitationDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvitationEmailAddreess = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TokenID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInvitationInfo", x => x.ContactInvitationInfoID);
                    table.ForeignKey(
                        name: "FK_ContactInvitationInfo_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalSchema: "Contact",
                        principalTable: "Contacts",
                        principalColumn: "ContactID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactInvitationQueue",
                schema: "Contact",
                columns: table => new
                {
                    ContactInvitationQueueID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    ContactInvitationInfoID = table.Column<long>(type: "bigint", nullable: false),
                    QueueStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttemptCount = table.Column<long>(type: "bigint", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInvitationQueue", x => x.ContactInvitationQueueID);
                    table.ForeignKey(
                        name: "FK_ContactInvitationQueue_ContactInvitationInfo_ContactInvitationInfoID",
                        column: x => x.ContactInvitationInfoID,
                        principalSchema: "Contact",
                        principalTable: "ContactInvitationInfo",
                        principalColumn: "ContactInvitationInfoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactInvitationInfo_ContactID",
                schema: "Contact",
                table: "ContactInvitationInfo",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactInvitationQueue_ContactInvitationInfoID",
                schema: "Contact",
                table: "ContactInvitationQueue",
                column: "ContactInvitationInfoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactInvitationQueue",
                schema: "Contact");

            migrationBuilder.DropTable(
                name: "ContactInvitationInfo",
                schema: "Contact");
        }
    }
}
