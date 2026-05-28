using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class InitialApplicationMasterData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LoanBusinessDetail_LoanApplicationID",
                schema: "Application",
                table: "LoanBusinessDetail");

            migrationBuilder.DropIndex(
                name: "IX_LoanApplicantDetails_LoanApplicationID",
                schema: "Application",
                table: "LoanApplicantDetails");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateApplied",
                schema: "Application",
                table: "LoanApplication",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_LoanBusinessDetail_LoanApplicationID",
                schema: "Application",
                table: "LoanBusinessDetail",
                column: "LoanApplicationID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplicantDetails_LoanApplicationID",
                schema: "Application",
                table: "LoanApplicantDetails",
                column: "LoanApplicationID",
                unique: true);

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[BusinessType] ON ");
            migrationBuilder.Sql("INSERT [Master].[BusinessType] ([BusinessTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (1, N'SoleProprietor', N'Sole Proprietor', 1,1)");
            migrationBuilder.Sql("INSERT [Master].[BusinessType] ([BusinessTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (2, N'Partnership', N'Partnership', 1,2)");
            migrationBuilder.Sql("INSERT [Master].[BusinessType] ([BusinessTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (3, N'C-Corp', N'C-Corp', 1,3)");
            migrationBuilder.Sql("INSERT [Master].[BusinessType] ([BusinessTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (4, N'S-Corp', N'S-Corp', 1,4)");
            migrationBuilder.Sql("INSERT [Master].[BusinessType] ([BusinessTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (5, N'LLC', N'LLC', 1,5)");
            migrationBuilder.Sql("INSERT [Master].[BusinessType] ([BusinessTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (6, N'Independent Contractor', N'Independent Contractor', 1,6)");
            migrationBuilder.Sql("INSERT [Master].[BusinessType] ([BusinessTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (7, N'Eligible Self-Employed Individual', N'Eligible Self-Employed Individual', 1,7)");
            migrationBuilder.Sql("INSERT [Master].[BusinessType] ([BusinessTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (8, N'501 (c) (3) Nonprofit', N'501 (c) (3) Nonprofit', 1,8)");
            migrationBuilder.Sql("INSERT [Master].[BusinessType] ([BusinessTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (9, N'501 (c) (19) veterans organization', N'501 (c) (19) veterans organization', 1,9)");
            migrationBuilder.Sql("INSERT [Master].[BusinessType] ([BusinessTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (10, N'Tribal Business (sec. 31 (b) (2) (c) of Small Business Act)', N'Tribal Business (sec. 31 (b) (2) (c) of Small Business Act)', 1,10)");
            migrationBuilder.Sql("INSERT [Master].[BusinessType] ([BusinessTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (11, N'Other', N'Other', 1,11)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[BusinessType] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[IndustryType] ON ");
            migrationBuilder.Sql("INSERT [Master].[IndustryType] ([IndustryTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (1, N'Agriculture, Forestry, Fishing and Hunting', N'Agriculture, Forestry, Fishing and Hunting', 1,1)");
            migrationBuilder.Sql("INSERT [Master].[IndustryType] ([IndustryTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (2, N'Arts, Entertainment, and Recreation', N'Arts, Entertainment, and Recreation', 1,2)");
            migrationBuilder.Sql("INSERT [Master].[IndustryType] ([IndustryTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (3, N'Automobile Dealers & Parts', N'Automobile Dealers & Parts', 1,3)");
            migrationBuilder.Sql("INSERT [Master].[IndustryType] ([IndustryTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (4, N'Construction', N'Construction', 1,4)");
            migrationBuilder.Sql("INSERT [Master].[IndustryType] ([IndustryTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (5, N'Education', N'Education', 1,5)");
            migrationBuilder.Sql("INSERT [Master].[IndustryType] ([IndustryTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (6, N'Finance and Insurance', N'Finance and Insurance', 1,6)");
            migrationBuilder.Sql("INSERT [Master].[IndustryType] ([IndustryTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (7, N'Healthcare', N'Healthcare', 1,7)");
            migrationBuilder.Sql("INSERT [Master].[IndustryType] ([IndustryTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (8, N'Social Assistance', N'Social Assistance', 1,8)");
            migrationBuilder.Sql("INSERT [Master].[IndustryType] ([IndustryTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (9, N'IT, media, or Publishing', N'IT, media, or Publishing', 1,9)");
            migrationBuilder.Sql("INSERT [Master].[IndustryType] ([IndustryTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (10, N'Legal Services', N'Legal Services', 1,10)");
            migrationBuilder.Sql("INSERT [Master].[IndustryType] ([IndustryTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (11, N'Mining (Oil and Gas Extraction)', N'Mining (Oil and Gas Extraction)', 1,11)");
            migrationBuilder.Sql("INSERT [Master].[IndustryType] ([IndustryTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (12, N'Manufacturing', N'Manufacturing', 1,12)");
            migrationBuilder.Sql("INSERT [Master].[IndustryType] ([IndustryTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (13, N'Political, Governmental, or Public Organizations', N'Political, Governmental, or Public Organizations', 1,13)");
            migrationBuilder.Sql("INSERT [Master].[IndustryType] ([IndustryTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (14, N'Real Estate', N'Real Estate', 1,14)");
            migrationBuilder.Sql("INSERT [Master].[IndustryType] ([IndustryTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (15, N'Religious Organizations', N'Religious Organizations', 1,15)");
            migrationBuilder.Sql("INSERT [Master].[IndustryType] ([IndustryTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (16, N'Restaurants and Food Services', N'Restaurants and Food Services', 1,16)");
            migrationBuilder.Sql("INSERT [Master].[IndustryType] ([IndustryTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (17, N'Retail Stores', N'Retail Stores', 1,17)");
            migrationBuilder.Sql("INSERT [Master].[IndustryType] ([IndustryTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (18, N'Firearm Sales', N'Firearm Sales', 1,18)");
            migrationBuilder.Sql("INSERT [Master].[IndustryType] ([IndustryTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (19, N'Gas Stations', N'Gas Stations', 1,19)");
            migrationBuilder.Sql("INSERT [Master].[IndustryType] ([IndustryTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (20, N'Transportation and Warehousing', N'Transportation and Warehousing', 1,20)");
            migrationBuilder.Sql("INSERT [Master].[IndustryType] ([IndustryTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (21, N'Freight Trucking', N'Freight Trucking', 1,21)");
            migrationBuilder.Sql("INSERT [Master].[IndustryType] ([IndustryTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (22, N'Travel Agencies', N'Travel Agencies', 1,22)");
            migrationBuilder.Sql("INSERT [Master].[IndustryType] ([IndustryTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (23, N'Utilities', N'Utilities', 1,23)");
            migrationBuilder.Sql("INSERT [Master].[IndustryType] ([IndustryTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (24, N'Wholesale Trade', N'Wholesale Trade', 1,24)");
            migrationBuilder.Sql("INSERT [Master].[IndustryType] ([IndustryTypeID],[Type], [Description], [IsActive], DisplayOrder) VALUES (25, N'Others', N'Others', 1,25)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[IndustryType] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Veteran] ON ");
            migrationBuilder.Sql("INSERT [Master].[Veteran] ([VeteranID],[VeteranType], [Description], [IsActive], DisplayOrder) VALUES (1, N'Non-Veteran', N'Non-Veteran', 1,1)");
            migrationBuilder.Sql("INSERT [Master].[Veteran] ([VeteranID],[VeteranType], [Description], [IsActive], DisplayOrder) VALUES (2, N'Veteran-Other', N'Veteran-Other', 1,2)");
            migrationBuilder.Sql("INSERT [Master].[Veteran] ([VeteranID],[VeteranType], [Description], [IsActive], DisplayOrder) VALUES (3, N'Service-Disabled Veteran', N'Service-Disabled Veteran', 1,3)");
            migrationBuilder.Sql("INSERT [Master].[Veteran] ([VeteranID],[VeteranType], [Description], [IsActive], DisplayOrder) VALUES (4, N'Not Disclosed', N'Not Disclosed', 1,4)");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Veteran] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Race] ON ");
            migrationBuilder.Sql("INSERT [Master].[Race] ([RaceID],[RaceName], [Description], [IsActive], DisplayOrder) VALUES (1, N'American Indian or Alaska Native', N'American Indian or Alaska Native', 1,1)");
            migrationBuilder.Sql("INSERT [Master].[Race] ([RaceID],[RaceName], [Description], [IsActive], DisplayOrder) VALUES (2, N'Asian', N'Asian', 1,2)");
            migrationBuilder.Sql("INSERT [Master].[Race] ([RaceID],[RaceName], [Description], [IsActive], DisplayOrder) VALUES (3, N'Black or African-American', N'Black or African-American', 1,3)");
            migrationBuilder.Sql("INSERT [Master].[Race] ([RaceID],[RaceName], [Description], [IsActive], DisplayOrder) VALUES (4, N'Native Hawaiian or Pacific Islander', N'Native Hawaiian or Pacific Islander', 1,4)");
            migrationBuilder.Sql("INSERT [Master].[Race] ([RaceID],[RaceName], [Description], [IsActive], DisplayOrder) VALUES (5, N'White', N'White', 1,5)");
            migrationBuilder.Sql("INSERT [Master].[Race] ([RaceID],[RaceName], [Description], [IsActive], DisplayOrder) VALUES (6, N'Not Disclosed', N'Not Disclosed', 1,6)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Race] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[ApplicationType] ON ");
            migrationBuilder.Sql("INSERT [Master].[ApplicationType] ([ApplicationTypeID],[ApplicationTypeName], [Description], [IsActive], DisplayOrder) VALUES (1, N'Loan', N'Loan', 1,1)");
            migrationBuilder.Sql("INSERT [Master].[ApplicationType] ([ApplicationTypeID],[ApplicationTypeName], [Description], [IsActive], DisplayOrder) VALUES (2, N'Fund', N'Fund', 1,1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[ApplicationType] OFF");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LoanBusinessDetail_LoanApplicationID",
                schema: "Application",
                table: "LoanBusinessDetail");

            migrationBuilder.DropIndex(
                name: "IX_LoanApplicantDetails_LoanApplicationID",
                schema: "Application",
                table: "LoanApplicantDetails");

            migrationBuilder.DropColumn(
                name: "DateApplied",
                schema: "Application",
                table: "LoanApplication");

            migrationBuilder.CreateIndex(
                name: "IX_LoanBusinessDetail_LoanApplicationID",
                schema: "Application",
                table: "LoanBusinessDetail",
                column: "LoanApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplicantDetails_LoanApplicationID",
                schema: "Application",
                table: "LoanApplicantDetails",
                column: "LoanApplicationID");

            migrationBuilder.Sql("delete from [Master].[BusinessType] where BusinessTypeID between 1 and 11");
            migrationBuilder.Sql("delete from [Master].[IndustryType] where IndustryTypeID between 1 and 25");
            migrationBuilder.Sql("delete from [Master].[Veteran] where VeteranID between 1 and 4");
            migrationBuilder.Sql("delete from [Master].[Race] where RaceID between 1 and 6");
            migrationBuilder.Sql("delete from [Master].[ApplicationType] where ApplicationTypeID between 1 and 2");
        }
    }
}
