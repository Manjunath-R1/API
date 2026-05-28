using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class AddBusinessProfileMasterTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
               name: "Application");

            migrationBuilder.CreateTable(
                name: "BusinessOwnerMaster",
                schema: "Application",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessID = table.Column<long>(type: "bigint", nullable: false),
                    BusinessOwnerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnedPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VeteranID = table.Column<long>(type: "bigint", nullable: true),
                    GenderID = table.Column<long>(type: "bigint", nullable: true),
                    RaceID = table.Column<long>(type: "bigint", nullable: true),
                    EthnicityID = table.Column<long>(type: "bigint", nullable: true),
                    Demographic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessOwnerMaster", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BusinessOwnerMaster_BusinessEntity_BusinessID",
                        column: x => x.BusinessID,
                        principalSchema: "Admin",
                        principalTable: "BusinessEntity",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessOwnerMaster_Ethnicity_EthnicityID",
                        column: x => x.EthnicityID,
                        principalSchema: "Master",
                        principalTable: "Ethnicity",
                        principalColumn: "EthnicityID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessOwnerMaster_Gender_GenderID",
                        column: x => x.GenderID,
                        principalSchema: "Master",
                        principalTable: "Gender",
                        principalColumn: "GenderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessOwnerMaster_Race_RaceID",
                        column: x => x.RaceID,
                        principalSchema: "Master",
                        principalTable: "Race",
                        principalColumn: "RaceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessOwnerMaster_Veteran_VeteranID",
                        column: x => x.VeteranID,
                        principalSchema: "Master",
                        principalTable: "Veteran",
                        principalColumn: "VeteranID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanBusinessDetailMaster",
                schema: "Application",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessID = table.Column<long>(type: "bigint", nullable: false),
                    BusinessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessTypeID = table.Column<long>(type: "bigint", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DBA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacebookUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SIC_ID = table.Column<long>(type: "bigint", nullable: true),
                    NAICS_ID = table.Column<long>(type: "bigint", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IndustryTypeID = table.Column<long>(type: "bigint", nullable: false),
                    EmployeeStrength = table.Column<long>(type: "bigint", nullable: false),
                    NumberOfYearsInBusiness = table.Column<long>(type: "bigint", nullable: false),
                    AverageMonthlyPayroll = table.Column<long>(type: "bigint", nullable: false),
                    DUNS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AffiliateID = table.Column<long>(type: "bigint", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateID = table.Column<long>(type: "bigint", nullable: false),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankRoutingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NaicsCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanBusinessDetailMaster", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LoanBusinessDetailMaster_BusinessEntity_BusinessID",
                        column: x => x.BusinessID,
                        principalSchema: "Admin",
                        principalTable: "BusinessEntity",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanBusinessDetailMaster_BusinessType_BusinessTypeID",
                        column: x => x.BusinessTypeID,
                        principalSchema: "Master",
                        principalTable: "BusinessType",
                        principalColumn: "BusinessTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanBusinessDetailMaster_IndustryType_IndustryTypeID",
                        column: x => x.IndustryTypeID,
                        principalSchema: "Master",
                        principalTable: "IndustryType",
                        principalColumn: "IndustryTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanBusinessDetailMaster_NAICS_NAICS_ID",
                        column: x => x.NAICS_ID,
                        principalSchema: "Master",
                        principalTable: "NAICS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanBusinessDetailMaster_SIC_SIC_ID",
                        column: x => x.SIC_ID,
                        principalSchema: "Master",
                        principalTable: "SIC",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanBusinessDetailMaster_State_StateID",
                        column: x => x.StateID,
                        principalSchema: "Master",
                        principalTable: "State",
                        principalColumn: "StateID",
                        onDelete: ReferentialAction.Cascade);
                    
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessOwnerMaster_BusinessID",
                schema: "Application",
                table: "BusinessOwnerMaster",
                column: "BusinessID");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessOwnerMaster_EthnicityID",
                schema: "Application",
                table: "BusinessOwnerMaster",
                column: "EthnicityID");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessOwnerMaster_GenderID",
                schema: "Application",
                table: "BusinessOwnerMaster",
                column: "GenderID");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessOwnerMaster_RaceID",
                schema: "Application",
                table: "BusinessOwnerMaster",
                column: "RaceID");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessOwnerMaster_VeteranID",
                schema: "Application",
                table: "BusinessOwnerMaster",
                column: "VeteranID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanBusinessDetailMaster_AffiliateID",
                schema: "Application",
                table: "LoanBusinessDetailMaster",
                column: "AffiliateID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanBusinessDetailMaster_BusinessID",
                schema: "Application",
                table: "LoanBusinessDetailMaster",
                column: "BusinessID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanBusinessDetailMaster_BusinessTypeID",
                schema: "Application",
                table: "LoanBusinessDetailMaster",
                column: "BusinessTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanBusinessDetailMaster_IndustryTypeID",
                schema: "Application",
                table: "LoanBusinessDetailMaster",
                column: "IndustryTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanBusinessDetailMaster_NAICS_ID",
                schema: "Application",
                table: "LoanBusinessDetailMaster",
                column: "NAICS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanBusinessDetailMaster_SIC_ID",
                schema: "Application",
                table: "LoanBusinessDetailMaster",
                column: "SIC_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanBusinessDetailMaster_StateID",
                schema: "Application",
                table: "LoanBusinessDetailMaster",
                column: "StateID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessOwnerMaster",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "LoanBusinessDetailMaster",
                schema: "Application");
        }
    }
}
