using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class DML_RolePermissionWithAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT Master.Action ON ");
            migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (27, 'GetContact', 'Get Contact', 1, 27)");
            migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (28, 'GetAllInternalContacts', 'Get All Internal Contacts', 1, 28)");
            migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (29, 'GetAgreementData', 'Get Agreement Data', 1, 29)");
            migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (30, 'ResetPassword', 'Reset Password', 1, 30)");
            migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (31, 'DeactivateContact', 'Deactivate Contact', 1, 31)");
            migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (32, 'SaveConsent', 'Save Consent', 1, 32)");

            migrationBuilder.Sql("SET IDENTITY_INSERT Master.Action OFF ");

            migrationBuilder.Sql("SET IDENTITY_INSERT Master.RolePermission ON ");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (77, 1, 27, 'Contact', 1, 1, 77)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (78, 3, 27, 'Contact', 1, 1, 78)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (79, 1, 28, 'Contact', 1, 1, 79)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (80, 3, 28, 'Contact', 1, 1, 80)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (81, 2, 29, 'Master', 1, 1, 81)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (82, 1, 30, 'Contact', 1, 1, 82)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (83, 3, 30, 'Contact', 1, 1, 83)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (84, 1, 31, 'Contact', 1, 1, 84)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (85, 3, 31, 'Contact', 1, 1, 85)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (86, 2, 32, 'Account', 1, 1, 86)");

            
            migrationBuilder.Sql("SET IDENTITY_INSERT Master.RolePermission OFF ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from Master.RolePermission where RolePermissionID between 77 and 86");
            migrationBuilder.Sql("delete from MAster.Action where ActionID between 27 and 32");
        }
    
    }
}
