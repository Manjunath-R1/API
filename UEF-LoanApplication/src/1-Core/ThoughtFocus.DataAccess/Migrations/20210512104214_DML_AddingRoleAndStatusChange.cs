using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class DML_AddingRoleAndStatusChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update MAster.ApplicationStatus set ApplicationStatusName = 'CFOApproved', Description = 'CFO Approved' where ApplicationStatusName = 'FundingInitiated'");
            migrationBuilder.Sql("update MAster.ApplicationStatus set ApplicationStatusName = 'AccountDisbursed', Description = 'Account Disbursed' where ApplicationStatusName = 'FundingCompleted'");

            migrationBuilder.Sql("update Master.Role set RoleName = 'LoanProcessor' where RoleName = 'Loan Processor'");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Role] ON ");
            migrationBuilder.Sql("INSERT [Master].[Role] ([RoleID], [CreatedDateTime], [CreatedByUserID], [LastModifiedDateTime], [LastModifiedByUserID], [IsActive], [RoleName], [RoleDescription], [DisplayOrder], [IsLoginRole]) VALUES (6, CAST(N'2017-10-27 00:00:00.000' AS DateTime), 7, CAST(N'2017-10-27 00:00:00.000' AS DateTime), 7, 1, N'Controller', N'Controller', 1, 0)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Role] OFF ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[Role] where RoleID = 6");
            migrationBuilder.Sql("update Master.Role set RoleName = 'Loan Processor' where RoleName = 'LoanProcessor'");
            migrationBuilder.Sql("update MAster.ApplicationStatus set ApplicationStatusName = 'FundingInitiated', Description = 'Funding Initiated' where ApplicationStatusName = 'CFOApproved'");
            migrationBuilder.Sql("update MAster.ApplicationStatus set ApplicationStatusName = 'FundingCompleted', Description = 'FundingCompleted' where ApplicationStatusName = 'AccountDisbursed'");

        }
    }
}
