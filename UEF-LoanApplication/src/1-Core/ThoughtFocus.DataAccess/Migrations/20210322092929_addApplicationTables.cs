using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class addApplicationTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Application");

            migrationBuilder.EnsureSchema(
                name: "Application");

            migrationBuilder.CreateTable(
                name: "Affiliate",
                schema: "Master",
                columns: table => new
                {
                    AffiliateID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Affiliate", x => x.AffiliateID);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationStatus",
                schema: "Master",
                columns: table => new
                {
                    ApplicationStatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationStatusName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationStatus", x => x.ApplicationStatusID);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationType",
                schema: "Master",
                columns: table => new
                {
                    ApplicationTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationType", x => x.ApplicationTypeID);
                });

            migrationBuilder.CreateTable(
                name: "BusinessType",
                schema: "Master",
                columns: table => new
                {
                    BusinessTypeID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessType", x => x.BusinessTypeID);
                });

            migrationBuilder.CreateTable(
                name: "IndustryType",
                schema: "Master",
                columns: table => new
                {
                    IndustryTypeID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndustryType", x => x.IndustryTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Race",
                schema: "Master",
                columns: table => new
                {
                    RaceID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RaceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Race", x => x.RaceID);
                });

            migrationBuilder.CreateTable(
                name: "Veteran",
                schema: "Master",
                columns: table => new
                {
                    VeteranID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VeteranType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veteran", x => x.VeteranID);
                });

            migrationBuilder.CreateTable(
                name: "LoanApplication",
                schema: "Application",
                columns: table => new
                {
                    LoanApplicationID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsConcentAccepted = table.Column<bool>(type: "bit", nullable: false),
                    ConcentAcceptedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationStatusID = table.Column<int>(type: "int", nullable: false),
                    ApplicationTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanApplication", x => x.LoanApplicationID);
                    table.ForeignKey(
                        name: "FK_LoanApplication_ApplicationStatus_ApplicationStatusID",
                        column: x => x.ApplicationStatusID,
                        principalSchema: "Master",
                        principalTable: "ApplicationStatus",
                        principalColumn: "ApplicationStatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanApplication_ApplicationType_ApplicationTypeID",
                        column: x => x.ApplicationTypeID,
                        principalSchema: "Master",
                        principalTable: "ApplicationType",
                        principalColumn: "ApplicationTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuisnessOwner",
                schema: "Application",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanApplicationID = table.Column<long>(type: "bigint", nullable: false),
                    BuisnessOwnerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnedPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VeteranID = table.Column<long>(type: "bigint", nullable: false),
                    GenderID = table.Column<long>(type: "bigint", nullable: false),
                    RaceID = table.Column<long>(type: "bigint", nullable: false),
                    EthnicityID = table.Column<long>(type: "bigint", nullable: false),
                    Demographic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "LoanApplicantDetails",
                schema: "Application",
                columns: table => new
                {
                    LoanApplicantID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanApplicationID = table.Column<long>(type: "bigint", nullable: false),
                    BusinessRoleID = table.Column<long>(type: "bigint", nullable: false),
                    SSN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalutationID = table.Column<long>(type: "bigint", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<long>(type: "bigint", nullable: false),
                    CurrentAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateID = table.Column<long>(type: "bigint", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanApplicantDetails", x => x.LoanApplicantID);
                    table.ForeignKey(
                        name: "FK_LoanApplicantDetails_BusinessRole_BusinessRoleID",
                        column: x => x.BusinessRoleID,
                        principalSchema: "Master",
                        principalTable: "BusinessRole",
                        principalColumn: "BusinessRoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanApplicantDetails_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalSchema: "Contact",
                        principalTable: "Contacts",
                        principalColumn: "ContactID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LoanApplicantDetails_LoanApplication_LoanApplicationID",
                        column: x => x.LoanApplicationID,
                        principalSchema: "Application",
                        principalTable: "LoanApplication",
                        principalColumn: "LoanApplicationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanApplicantDetails_Salutation_SalutationID",
                        column: x => x.SalutationID,
                        principalSchema: "Master",
                        principalTable: "Salutation",
                        principalColumn: "SalutationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanApplicantDetails_State_StateID",
                        column: x => x.StateID,
                        principalSchema: "Master",
                        principalTable: "State",
                        principalColumn: "StateID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanBusinessDetail",
                schema: "Application",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanApplicationID = table.Column<long>(type: "bigint", nullable: false),
                    BuisnessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessTypeID = table.Column<long>(type: "bigint", nullable: false),
                    EIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DBA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IndustryTypeID = table.Column<long>(type: "bigint", nullable: false),
                    EmployeeStrength = table.Column<long>(type: "bigint", nullable: false),
                    NumberOfYearsInBuisness = table.Column<long>(type: "bigint", nullable: false),
                    AverageMonthlyPayroll = table.Column<long>(type: "bigint", nullable: false),
                    DUNS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AffiliateID = table.Column<long>(type: "bigint", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateID = table.Column<long>(type: "bigint", nullable: false),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNumber = table.Column<long>(type: "bigint", nullable: false),
                    BankRoutingNumber = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanBusinessDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LoanBusinessDetail_Affiliate_AffiliateID",
                        column: x => x.AffiliateID,
                        principalSchema: "Master",
                        principalTable: "Affiliate",
                        principalColumn: "AffiliateID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanBusinessDetail_BusinessType_BusinessTypeID",
                        column: x => x.BusinessTypeID,
                        principalSchema: "Master",
                        principalTable: "BusinessType",
                        principalColumn: "BusinessTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanBusinessDetail_IndustryType_IndustryTypeID",
                        column: x => x.IndustryTypeID,
                        principalSchema: "Master",
                        principalTable: "IndustryType",
                        principalColumn: "IndustryTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanBusinessDetail_LoanApplication_LoanApplicationID",
                        column: x => x.LoanApplicationID,
                        principalSchema: "Application",
                        principalTable: "LoanApplication",
                        principalColumn: "LoanApplicationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanBusinessDetail_State_StateID",
                        column: x => x.StateID,
                        principalSchema: "Master",
                        principalTable: "State",
                        principalColumn: "StateID",
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

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplicantDetails_BusinessRoleID",
                schema: "Application",
                table: "LoanApplicantDetails",
                column: "BusinessRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplicantDetails_ContactID",
                schema: "Application",
                table: "LoanApplicantDetails",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplicantDetails_LoanApplicationID",
                schema: "Application",
                table: "LoanApplicantDetails",
                column: "LoanApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplicantDetails_SalutationID",
                schema: "Application",
                table: "LoanApplicantDetails",
                column: "SalutationID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplicantDetails_StateID",
                schema: "Application",
                table: "LoanApplicantDetails",
                column: "StateID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplication_ApplicationStatusID",
                schema: "Application",
                table: "LoanApplication",
                column: "ApplicationStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplication_ApplicationTypeID",
                schema: "Application",
                table: "LoanApplication",
                column: "ApplicationTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanBusinessDetail_AffiliateID",
                schema: "Application",
                table: "LoanBusinessDetail",
                column: "AffiliateID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanBusinessDetail_BusinessTypeID",
                schema: "Application",
                table: "LoanBusinessDetail",
                column: "BusinessTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanBusinessDetail_IndustryTypeID",
                schema: "Application",
                table: "LoanBusinessDetail",
                column: "IndustryTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanBusinessDetail_LoanApplicationID",
                schema: "Application",
                table: "LoanBusinessDetail",
                column: "LoanApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanBusinessDetail_StateID",
                schema: "Application",
                table: "LoanBusinessDetail",
                column: "StateID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuisnessOwner",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "LoanApplicantDetails",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "LoanBusinessDetail",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "Race",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "Veteran",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "Affiliate",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "BusinessType",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "IndustryType",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "LoanApplication",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "ApplicationStatus",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "ApplicationType",
                schema: "Master");
        }
    }
}
