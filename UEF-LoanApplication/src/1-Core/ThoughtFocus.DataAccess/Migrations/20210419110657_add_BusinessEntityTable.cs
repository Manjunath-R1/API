using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class add_BusinessEntityTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Admin");

            migrationBuilder.CreateTable(
                name: "BusinessEntity",
                schema: "Admin",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessTypeID = table.Column<long>(type: "bigint", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DBA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SIC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NAICS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IndustryTypeID = table.Column<long>(type: "bigint", nullable: true),
                    EmployeeStrength = table.Column<long>(type: "bigint", nullable: false),
                    NumberOfYearsInBusiness = table.Column<long>(type: "bigint", nullable: false),
                    AverageMonthlyPayroll = table.Column<long>(type: "bigint", nullable: false),
                    DUNS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AffiliateID = table.Column<long>(type: "bigint", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateID = table.Column<long>(type: "bigint", nullable: true),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNumber = table.Column<long>(type: "bigint", nullable: false),
                    BankRoutingNumber = table.Column<long>(type: "bigint", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessEntity", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BusinessEntity_Affiliate_AffiliateID",
                        column: x => x.AffiliateID,
                        principalSchema: "Master",
                        principalTable: "Affiliate",
                        principalColumn: "AffiliateID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessEntity_BusinessType_BusinessTypeID",
                        column: x => x.BusinessTypeID,
                        principalSchema: "Master",
                        principalTable: "BusinessType",
                        principalColumn: "BusinessTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessEntity_IndustryType_IndustryTypeID",
                        column: x => x.IndustryTypeID,
                        principalSchema: "Master",
                        principalTable: "IndustryType",
                        principalColumn: "IndustryTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BusinessEntity_State_StateID",
                        column: x => x.StateID,
                        principalSchema: "Master",
                        principalTable: "State",
                        principalColumn: "StateID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessEntity_AffiliateID",
                schema: "Admin",
                table: "BusinessEntity",
                column: "AffiliateID");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessEntity_BusinessTypeID",
                schema: "Admin",
                table: "BusinessEntity",
                column: "BusinessTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessEntity_IndustryTypeID",
                schema: "Admin",
                table: "BusinessEntity",
                column: "IndustryTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessEntity_StateID",
                schema: "Admin",
                table: "BusinessEntity",
                column: "StateID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessEntity",
                schema: "Admin");
        }
    }
}
