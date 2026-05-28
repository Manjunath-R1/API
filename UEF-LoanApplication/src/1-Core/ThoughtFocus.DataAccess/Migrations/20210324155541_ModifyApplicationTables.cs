using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class ModifyApplicationTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuisnessOwner",
                schema: "Application");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "Application",
                table: "LoanBusinessDetail");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                schema: "Application",
                table: "LoanBusinessDetail");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Application",
                table: "LoanBusinessDetail");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserID",
                schema: "Application",
                table: "LoanBusinessDetail");

            migrationBuilder.DropColumn(
                name: "LastModifiedDateTime",
                schema: "Application",
                table: "LoanBusinessDetail");

            migrationBuilder.RenameTable(
                name: "LoanApplication",
                schema: "Application",
                newName: "LoanApplication",
                newSchema: "Application");

            migrationBuilder.RenameTable(
                name: "LoanApplicantDetails",
                schema: "Application",
                newName: "LoanApplicantDetails",
                newSchema: "Application");

            migrationBuilder.RenameColumn(
                name: "NumberOfYearsInBuisness",
                schema: "Application",
                table: "LoanBusinessDetail",
                newName: "NumberOfYearsInBusiness");

            migrationBuilder.RenameColumn(
                name: "BuisnessName",
                schema: "Application",
                table: "LoanBusinessDetail",
                newName: "BusinessName");

            migrationBuilder.AddColumn<long>(
                name: "CreatedByUserID",
                schema: "Application",
                table: "LoanApplication",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                schema: "Application",
                table: "LoanApplication",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Application",
                table: "LoanApplication",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "LastModifiedByUserID",
                schema: "Application",
                table: "LoanApplication",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDateTime",
                schema: "Application",
                table: "LoanApplication",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "BusinessOwner",
                schema: "Application",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanApplicationID = table.Column<long>(type: "bigint", nullable: false),
                    BusinessOwnerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnedPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VeteranID = table.Column<long>(type: "bigint", nullable: false),
                    GenderID = table.Column<long>(type: "bigint", nullable: false),
                    RaceID = table.Column<long>(type: "bigint", nullable: false),
                    EthnicityID = table.Column<long>(type: "bigint", nullable: false),
                    Demographic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessOwner", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BusinessOwner_Ethnicity_EthnicityID",
                        column: x => x.EthnicityID,
                        principalSchema: "Master",
                        principalTable: "Ethnicity",
                        principalColumn: "EthnicityID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessOwner_Gender_GenderID",
                        column: x => x.GenderID,
                        principalSchema: "Master",
                        principalTable: "Gender",
                        principalColumn: "GenderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessOwner_LoanApplication_LoanApplicationID",
                        column: x => x.LoanApplicationID,
                        principalSchema: "Application",
                        principalTable: "LoanApplication",
                        principalColumn: "LoanApplicationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessOwner_Race_RaceID",
                        column: x => x.RaceID,
                        principalSchema: "Master",
                        principalTable: "Race",
                        principalColumn: "RaceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessOwner_Veteran_VeteranID",
                        column: x => x.VeteranID,
                        principalSchema: "Master",
                        principalTable: "Veteran",
                        principalColumn: "VeteranID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessOwner_EthnicityID",
                schema: "Application",
                table: "BusinessOwner",
                column: "EthnicityID");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessOwner_GenderID",
                schema: "Application",
                table: "BusinessOwner",
                column: "GenderID");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessOwner_LoanApplicationID",
                schema: "Application",
                table: "BusinessOwner",
                column: "LoanApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessOwner_RaceID",
                schema: "Application",
                table: "BusinessOwner",
                column: "RaceID");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessOwner_VeteranID",
                schema: "Application",
                table: "BusinessOwner",
                column: "VeteranID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessOwner",
                schema: "Application");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                schema: "Application",
                table: "LoanApplication");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                schema: "Application",
                table: "LoanApplication");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Application",
                table: "LoanApplication");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserID",
                schema: "Application",
                table: "LoanApplication");

            migrationBuilder.DropColumn(
                name: "LastModifiedDateTime",
                schema: "Application",
                table: "LoanApplication");

            migrationBuilder.EnsureSchema(
                name: "Application");

            migrationBuilder.RenameTable(
                name: "LoanApplication",
                schema: "Application",
                newName: "LoanApplication",
                newSchema: "Application");

            migrationBuilder.RenameTable(
                name: "LoanApplicantDetails",
                schema: "Application",
                newName: "LoanApplicantDetails",
                newSchema: "Application");

            migrationBuilder.RenameColumn(
                name: "NumberOfYearsInBusiness",
                schema: "Application",
                table: "LoanBusinessDetail",
                newName: "NumberOfYearsInBuisness");

            migrationBuilder.RenameColumn(
                name: "BusinessName",
                schema: "Application",
                table: "LoanBusinessDetail",
                newName: "BuisnessName");

            migrationBuilder.AddColumn<long>(
                name: "CreatedByUserID",
                schema: "Application",
                table: "LoanBusinessDetail",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                schema: "Application",
                table: "LoanBusinessDetail",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Application",
                table: "LoanBusinessDetail",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "LastModifiedByUserID",
                schema: "Application",
                table: "LoanBusinessDetail",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDateTime",
                schema: "Application",
                table: "LoanBusinessDetail",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "BuisnessOwner",
                schema: "Application",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuisnessOwnerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Demographic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EthnicityID = table.Column<long>(type: "bigint", nullable: false),
                    GenderID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoanApplicationID = table.Column<long>(type: "bigint", nullable: false),
                    OwnedPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RaceID = table.Column<long>(type: "bigint", nullable: false),
                    VeteranID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuisnessOwner", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BuisnessOwner_Ethnicity_EthnicityID",
                        column: x => x.EthnicityID,
                        principalSchema: "Master",
                        principalTable: "Ethnicity",
                        principalColumn: "EthnicityID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuisnessOwner_Gender_GenderID",
                        column: x => x.GenderID,
                        principalSchema: "Master",
                        principalTable: "Gender",
                        principalColumn: "GenderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuisnessOwner_LoanApplication_LoanApplicationID",
                        column: x => x.LoanApplicationID,
                        principalSchema: "Application",
                        principalTable: "LoanApplication",
                        principalColumn: "LoanApplicationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuisnessOwner_Race_RaceID",
                        column: x => x.RaceID,
                        principalSchema: "Master",
                        principalTable: "Race",
                        principalColumn: "RaceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuisnessOwner_Veteran_VeteranID",
                        column: x => x.VeteranID,
                        principalSchema: "Master",
                        principalTable: "Veteran",
                        principalColumn: "VeteranID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuisnessOwner_EthnicityID",
                schema: "Application",
                table: "BuisnessOwner",
                column: "EthnicityID");

            migrationBuilder.CreateIndex(
                name: "IX_BuisnessOwner_GenderID",
                schema: "Application",
                table: "BuisnessOwner",
                column: "GenderID");

            migrationBuilder.CreateIndex(
                name: "IX_BuisnessOwner_LoanApplicationID",
                schema: "Application",
                table: "BuisnessOwner",
                column: "LoanApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_BuisnessOwner_RaceID",
                schema: "Application",
                table: "BuisnessOwner",
                column: "RaceID");

            migrationBuilder.CreateIndex(
                name: "IX_BuisnessOwner_VeteranID",
                schema: "Application",
                table: "BuisnessOwner",
                column: "VeteranID");
        }
    }
}
