using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class addBusinessContactAndProgramInvitation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_BusinessRole_BusinessRoleID",
                schema: "Contact",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_UrbanLeagueAffiliate_AffiliateID",
                schema: "Contact",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_AffiliateID",
                schema: "Contact",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_BusinessRoleID",
                schema: "Contact",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "AffiliateID",
                schema: "Contact",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "BusinessID",
                schema: "Contact",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "BusinessRoleID",
                schema: "Contact",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "Contact",
                table: "Contacts");

            migrationBuilder.CreateTable(
                name: "BusinessUser",
                schema: "Contact",
                columns: table => new
                {
                    BusinessUserID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessID = table.Column<long>(type: "bigint", nullable: false),
                    ContactID = table.Column<long>(type: "bigint", nullable: false),
                    BusinessRoleID = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessUser", x => x.BusinessUserID);
                    table.ForeignKey(
                        name: "FK_BusinessUser_BusinessEntity_BusinessID",
                        column: x => x.BusinessID,
                        principalSchema: "Admin",
                        principalTable: "BusinessEntity",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessUser_BusinessRole_BusinessRoleID",
                        column: x => x.BusinessRoleID,
                        principalSchema: "Master",
                        principalTable: "BusinessRole",
                        principalColumn: "BusinessRoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessUser_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalSchema: "Contact",
                        principalTable: "Contacts",
                        principalColumn: "ContactID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramInvitation",
                schema: "Admin",
                columns: table => new
                {
                    ProgramInvitationID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessID = table.Column<long>(type: "bigint", nullable: false),
                    ProgramID = table.Column<long>(type: "bigint", nullable: false),
                    ProgramStatusID = table.Column<long>(type: "bigint", nullable: false),
                    InvitationSentDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramInvitation", x => x.ProgramInvitationID);
                    table.ForeignKey(
                        name: "FK_ProgramInvitation_BusinessEntity_BusinessID",
                        column: x => x.BusinessID,
                        principalSchema: "Admin",
                        principalTable: "BusinessEntity",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramInvitation_FundingSources_ProgramID",
                        column: x => x.ProgramID,
                        principalSchema: "FundingSource",
                        principalTable: "FundingSources",
                        principalColumn: "FundingSourceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessUser_BusinessID",
                schema: "Contact",
                table: "BusinessUser",
                column: "BusinessID");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessUser_BusinessRoleID",
                schema: "Contact",
                table: "BusinessUser",
                column: "BusinessRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessUser_ContactID",
                schema: "Contact",
                table: "BusinessUser",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramInvitation_BusinessID",
                schema: "Admin",
                table: "ProgramInvitation",
                column: "BusinessID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramInvitation_ProgramID",
                schema: "Admin",
                table: "ProgramInvitation",
                column: "ProgramID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessUser",
                schema: "Contact");

            migrationBuilder.DropTable(
                name: "ProgramInvitation",
                schema: "Admin");

            migrationBuilder.AddColumn<long>(
                name: "AffiliateID",
                schema: "Contact",
                table: "Contacts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BusinessID",
                schema: "Contact",
                table: "Contacts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BusinessRoleID",
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
                name: "IX_Contacts_AffiliateID",
                schema: "Contact",
                table: "Contacts",
                column: "AffiliateID");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_BusinessRoleID",
                schema: "Contact",
                table: "Contacts",
                column: "BusinessRoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_BusinessRole_BusinessRoleID",
                schema: "Contact",
                table: "Contacts",
                column: "BusinessRoleID",
                principalSchema: "Master",
                principalTable: "BusinessRole",
                principalColumn: "BusinessRoleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_UrbanLeagueAffiliate_AffiliateID",
                schema: "Contact",
                table: "Contacts",
                column: "AffiliateID",
                principalSchema: "Master",
                principalTable: "UrbanLeagueAffiliate",
                principalColumn: "AffiliateID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
