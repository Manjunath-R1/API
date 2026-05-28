using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class DML_BusinessOwners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessEntity_BusinessType_BusinessTypeID",
                schema: "Admin",
                table: "BusinessEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessOwner_Ethnicity_EthnicityID",
                schema: "Application",
                table: "BusinessOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessOwner_Gender_GenderID",
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

            migrationBuilder.AlterColumn<string>(
                name: "BankRoutingNumber",
                schema: "Application",
                table: "LoanBusinessDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "BankAccountNumber",
                schema: "Application",
                table: "LoanBusinessDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "VeteranID",
                schema: "Application",
                table: "BusinessOwner",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "RaceID",
                schema: "Application",
                table: "BusinessOwner",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<decimal>(
                name: "OwnedPercentage",
                schema: "Application",
                table: "BusinessOwner",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<long>(
                name: "GenderID",
                schema: "Application",
                table: "BusinessOwner",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "EthnicityID",
                schema: "Application",
                table: "BusinessOwner",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "BusinessTypeID",
                schema: "Admin",
                table: "BusinessEntity",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessEntity_BusinessType_BusinessTypeID",
                schema: "Admin",
                table: "BusinessEntity",
                column: "BusinessTypeID",
                principalSchema: "Master",
                principalTable: "BusinessType",
                principalColumn: "BusinessTypeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessOwner_Ethnicity_EthnicityID",
                schema: "Application",
                table: "BusinessOwner",
                column: "EthnicityID",
                principalSchema: "Master",
                principalTable: "Ethnicity",
                principalColumn: "EthnicityID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessOwner_Gender_GenderID",
                schema: "Application",
                table: "BusinessOwner",
                column: "GenderID",
                principalSchema: "Master",
                principalTable: "Gender",
                principalColumn: "GenderID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessOwner_Race_RaceID",
                schema: "Application",
                table: "BusinessOwner",
                column: "RaceID",
                principalSchema: "Master",
                principalTable: "Race",
                principalColumn: "RaceID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessOwner_Veteran_VeteranID",
                schema: "Application",
                table: "BusinessOwner",
                column: "VeteranID",
                principalSchema: "Master",
                principalTable: "Veteran",
                principalColumn: "VeteranID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessEntity_BusinessType_BusinessTypeID",
                schema: "Admin",
                table: "BusinessEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessOwner_Ethnicity_EthnicityID",
                schema: "Application",
                table: "BusinessOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessOwner_Gender_GenderID",
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

            migrationBuilder.AlterColumn<long>(
                name: "BankRoutingNumber",
                schema: "Application",
                table: "LoanBusinessDetail",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "BankAccountNumber",
                schema: "Application",
                table: "LoanBusinessDetail",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "VeteranID",
                schema: "Application",
                table: "BusinessOwner",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "RaceID",
                schema: "Application",
                table: "BusinessOwner",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OwnedPercentage",
                schema: "Application",
                table: "BusinessOwner",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "GenderID",
                schema: "Application",
                table: "BusinessOwner",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "EthnicityID",
                schema: "Application",
                table: "BusinessOwner",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "BusinessTypeID",
                schema: "Admin",
                table: "BusinessEntity",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessEntity_BusinessType_BusinessTypeID",
                schema: "Admin",
                table: "BusinessEntity",
                column: "BusinessTypeID",
                principalSchema: "Master",
                principalTable: "BusinessType",
                principalColumn: "BusinessTypeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessOwner_Ethnicity_EthnicityID",
                schema: "Application",
                table: "BusinessOwner",
                column: "EthnicityID",
                principalSchema: "Master",
                principalTable: "Ethnicity",
                principalColumn: "EthnicityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessOwner_Gender_GenderID",
                schema: "Application",
                table: "BusinessOwner",
                column: "GenderID",
                principalSchema: "Master",
                principalTable: "Gender",
                principalColumn: "GenderID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessOwner_Race_RaceID",
                schema: "Application",
                table: "BusinessOwner",
                column: "RaceID",
                principalSchema: "Master",
                principalTable: "Race",
                principalColumn: "RaceID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessOwner_Veteran_VeteranID",
                schema: "Application",
                table: "BusinessOwner",
                column: "VeteranID",
                principalSchema: "Master",
                principalTable: "Veteran",
                principalColumn: "VeteranID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
