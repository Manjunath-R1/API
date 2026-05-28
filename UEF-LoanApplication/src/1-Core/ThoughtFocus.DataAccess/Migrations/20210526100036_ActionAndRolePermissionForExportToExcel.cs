using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class ActionAndRolePermissionForExportToExcel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT Master.Action ON ");
            migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (33, 'ExportLoanApplications', 'Export Loan Applications', 1, 33)");
            migrationBuilder.Sql("SET IDENTITY_INSERT Master.Action OFF ");

            migrationBuilder.Sql("SET IDENTITY_INSERT Master.RolePermission ON ");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (87, 1, 33, 'LoanApplication', 1, 1, 87)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (88, 3, 33, 'LoanApplication', 1, 1, 88)");
            migrationBuilder.Sql("SET IDENTITY_INSERT Master.RolePermission OFF ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.Sql("delete from Master.RolePermission where RolePermissionID between 87 and 88");
            migrationBuilder.Sql("delete from Master.Action where ActionID = 33");
        }
    }
}
