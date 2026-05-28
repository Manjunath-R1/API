using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class program_specific_tables_mapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AgreementID",
                schema: "FundingSource",
                table: "FundingSources",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasDefaultFundingAmount",
                schema: "FundingSource",
                table: "FundingSources",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "LogoID",
                schema: "FundingSource",
                table: "FundingSources",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LogoID",
                schema: "FundingSource",
                table: "FundingEntities",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LogoTypes",
                schema: "Master",
                columns: table => new
                {
                    LogoTypeID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogoTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogoTypes", x => x.LogoTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Logos",
                schema: "Master",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoTypeID = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Logos_LogoTypes_LogoTypeID",
                        column: x => x.LogoTypeID,
                        principalSchema: "Master",
                        principalTable: "LogoTypes",
                        principalColumn: "LogoTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FundingSources_AgreementID",
                schema: "FundingSource",
                table: "FundingSources",
                column: "AgreementID");

            migrationBuilder.CreateIndex(
                name: "IX_FundingSources_LogoID",
                schema: "FundingSource",
                table: "FundingSources",
                column: "LogoID");

            migrationBuilder.CreateIndex(
                name: "IX_FundingEntities_LogoID",
                schema: "FundingSource",
                table: "FundingEntities",
                column: "LogoID");

            migrationBuilder.CreateIndex(
                name: "IX_Logos_LogoTypeID",
                schema: "Master",
                table: "Logos",
                column: "LogoTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_FundingEntities_Logos_LogoID",
                schema: "FundingSource",
                table: "FundingEntities",
                column: "LogoID",
                principalSchema: "Master",
                principalTable: "Logos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FundingSources_Agreement_AgreementID",
                schema: "FundingSource",
                table: "FundingSources",
                column: "AgreementID",
                principalSchema: "Master",
                principalTable: "Agreement",
                principalColumn: "AgreementID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FundingSources_Logos_LogoID",
                schema: "FundingSource",
                table: "FundingSources",
                column: "LogoID",
                principalSchema: "Master",
                principalTable: "Logos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FundingEntities_Logos_LogoID",
                schema: "FundingSource",
                table: "FundingEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_FundingSources_Agreement_AgreementID",
                schema: "FundingSource",
                table: "FundingSources");

            migrationBuilder.DropForeignKey(
                name: "FK_FundingSources_Logos_LogoID",
                schema: "FundingSource",
                table: "FundingSources");

            migrationBuilder.DropTable(
                name: "Logos",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "LogoTypes",
                schema: "Master");

            migrationBuilder.DropIndex(
                name: "IX_FundingSources_AgreementID",
                schema: "FundingSource",
                table: "FundingSources");

            migrationBuilder.DropIndex(
                name: "IX_FundingSources_LogoID",
                schema: "FundingSource",
                table: "FundingSources");

            migrationBuilder.DropIndex(
                name: "IX_FundingEntities_LogoID",
                schema: "FundingSource",
                table: "FundingEntities");

            migrationBuilder.DropColumn(
                name: "AgreementID",
                schema: "FundingSource",
                table: "FundingSources");

            migrationBuilder.DropColumn(
                name: "HasDefaultFundingAmount",
                schema: "FundingSource",
                table: "FundingSources");

            migrationBuilder.DropColumn(
                name: "LogoID",
                schema: "FundingSource",
                table: "FundingSources");

            migrationBuilder.DropColumn(
                name: "LogoID",
                schema: "FundingSource",
                table: "FundingEntities");
        }
    }
}
