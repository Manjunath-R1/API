using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class civicrm_tables_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CiviCRMExportFlag",
                schema: "Admin",
                table: "BusinessEntity",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CiviCRMDataExportLogs",
                schema: "Admin",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExportedBy = table.Column<long>(type: "bigint", nullable: false),
                    ExportedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExportType = table.Column<int>(type: "int", nullable: false),
                    Recordscount = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CiviCRMDataExportLogs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CiviCRMContactExportDetails",
                schema: "Admin",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogID = table.Column<long>(type: "bigint", nullable: true),
                    ContactID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CiviCRMContactExportDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CiviCRMContactExportDetails_CiviCRMDataExportLogs_LogID",
                        column: x => x.LogID,
                        principalSchema: "Admin",
                        principalTable: "CiviCRMDataExportLogs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CiviCRMContactExportDetails_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalSchema: "Contact",
                        principalTable: "Contacts",
                        principalColumn: "ContactID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CiviCRMOrganizationExportDetails",
                schema: "Admin",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogID = table.Column<long>(type: "bigint", nullable: true),
                    BusinessEntityID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CiviCRMOrganizationExportDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CiviCRMOrganizationExportDetails_BusinessEntity_BusinessEntityID",
                        column: x => x.BusinessEntityID,
                        principalSchema: "Admin",
                        principalTable: "BusinessEntity",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CiviCRMOrganizationExportDetails_CiviCRMDataExportLogs_LogID",
                        column: x => x.LogID,
                        principalSchema: "Admin",
                        principalTable: "CiviCRMDataExportLogs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CiviCRMContactExportDetails_ContactID",
                schema: "Admin",
                table: "CiviCRMContactExportDetails",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_CiviCRMContactExportDetails_LogID",
                schema: "Admin",
                table: "CiviCRMContactExportDetails",
                column: "LogID");

            migrationBuilder.CreateIndex(
                name: "IX_CiviCRMOrganizationExportDetails_BusinessEntityID",
                schema: "Admin",
                table: "CiviCRMOrganizationExportDetails",
                column: "BusinessEntityID");

            migrationBuilder.CreateIndex(
                name: "IX_CiviCRMOrganizationExportDetails_LogID",
                schema: "Admin",
                table: "CiviCRMOrganizationExportDetails",
                column: "LogID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CiviCRMContactExportDetails",
                schema: "Admin");

            migrationBuilder.DropTable(
                name: "CiviCRMOrganizationExportDetails",
                schema: "Admin");

            migrationBuilder.DropTable(
                name: "CiviCRMDataExportLogs",
                schema: "Admin");

            migrationBuilder.DropColumn(
                name: "CiviCRMExportFlag",
                schema: "Admin",
                table: "BusinessEntity");
        }
    }
}
