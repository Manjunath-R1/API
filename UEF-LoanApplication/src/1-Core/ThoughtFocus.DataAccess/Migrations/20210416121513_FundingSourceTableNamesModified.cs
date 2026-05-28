using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class FundingSourceTableNamesModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "FundingSourceAndBusinessTypeMapping",
                schema: "FundingSource");

            migrationBuilder.DropTable(
                name: "FundingSourceAndStateMapping",
                schema: "FundingSource");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "User",
                table: "UserProfileLoginInfo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "User",
                table: "UserPasswordResetInfo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "FundingSourceBusinessTypes",
                schema: "FundingSource",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FundingSourceID = table.Column<long>(type: "bigint", nullable: false),
                    BusinessTypeID = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundingSourceBusinessTypes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FundingSourceBusinessTypes_BusinessType_BusinessTypeID",
                        column: x => x.BusinessTypeID,
                        principalSchema: "Master",
                        principalTable: "BusinessType",
                        principalColumn: "BusinessTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FundingSourceBusinessTypes_FundingSources_FundingSourceID",
                        column: x => x.FundingSourceID,
                        principalSchema: "FundingSource",
                        principalTable: "FundingSources",
                        principalColumn: "FundingSourceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FundingSourceStates",
                schema: "FundingSource",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FundingSourceID = table.Column<long>(type: "bigint", nullable: false),
                    StateID = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundingSourceStates", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FundingSourceStates_FundingSources_FundingSourceID",
                        column: x => x.FundingSourceID,
                        principalSchema: "FundingSource",
                        principalTable: "FundingSources",
                        principalColumn: "FundingSourceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FundingSourceStates_State_StateID",
                        column: x => x.StateID,
                        principalSchema: "Master",
                        principalTable: "State",
                        principalColumn: "StateID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FundingSourceBusinessTypes_BusinessTypeID",
                schema: "FundingSource",
                table: "FundingSourceBusinessTypes",
                column: "BusinessTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_FundingSourceBusinessTypes_FundingSourceID",
                schema: "FundingSource",
                table: "FundingSourceBusinessTypes",
                column: "FundingSourceID");

            migrationBuilder.CreateIndex(
                name: "IX_FundingSourceStates_FundingSourceID",
                schema: "FundingSource",
                table: "FundingSourceStates",
                column: "FundingSourceID");

            migrationBuilder.CreateIndex(
                name: "IX_FundingSourceStates_StateID",
                schema: "FundingSource",
                table: "FundingSourceStates",
                column: "StateID");

            

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessOwner_Gender_GenderID",
                schema: "Application",
                table: "BusinessOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessOwner_LoanApplication_LoanApplicationID",
                schema: "Application",
                table: "BusinessOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessOwner_Race_RaceID",
                schema: "Application",
                table: "BusinessOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessOwner_Veteran_VeteranID",
                schema: "Application",
                table: "BusinessOwner");

            migrationBuilder.DropTable(
                name: "FundingSourceBusinessTypes",
                schema: "FundingSource");

            migrationBuilder.DropTable(
                name: "FundingSourceStates",
                schema: "FundingSource");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "User",
                table: "UserProfileLoginInfo");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "User",
                table: "UserPasswordResetInfo");

            

            migrationBuilder.CreateTable(
                name: "FundingSourceAndBusinessTypeMapping",
                schema: "FundingSource",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessTypeID = table.Column<long>(type: "bigint", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FundingSourceID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundingSourceAndBusinessTypeMapping", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FundingSourceAndBusinessTypeMapping_BusinessType_BusinessTypeID",
                        column: x => x.BusinessTypeID,
                        principalSchema: "Master",
                        principalTable: "BusinessType",
                        principalColumn: "BusinessTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FundingSourceAndBusinessTypeMapping_FundingSources_FundingSourceID",
                        column: x => x.FundingSourceID,
                        principalSchema: "FundingSource",
                        principalTable: "FundingSources",
                        principalColumn: "FundingSourceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FundingSourceAndStateMapping",
                schema: "FundingSource",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FundingSourceID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StateID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundingSourceAndStateMapping", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FundingSourceAndStateMapping_FundingSources_FundingSourceID",
                        column: x => x.FundingSourceID,
                        principalSchema: "FundingSource",
                        principalTable: "FundingSources",
                        principalColumn: "FundingSourceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FundingSourceAndStateMapping_State_StateID",
                        column: x => x.StateID,
                        principalSchema: "Master",
                        principalTable: "State",
                        principalColumn: "StateID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FundingSourceAndBusinessTypeMapping_BusinessTypeID",
                schema: "FundingSource",
                table: "FundingSourceAndBusinessTypeMapping",
                column: "BusinessTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_FundingSourceAndBusinessTypeMapping_FundingSourceID",
                schema: "FundingSource",
                table: "FundingSourceAndBusinessTypeMapping",
                column: "FundingSourceID");

            migrationBuilder.CreateIndex(
                name: "IX_FundingSourceAndStateMapping_FundingSourceID",
                schema: "FundingSource",
                table: "FundingSourceAndStateMapping",
                column: "FundingSourceID");

            migrationBuilder.CreateIndex(
                name: "IX_FundingSourceAndStateMapping_StateID",
                schema: "FundingSource",
                table: "FundingSourceAndStateMapping",
                column: "StateID");
        }
    }
}
