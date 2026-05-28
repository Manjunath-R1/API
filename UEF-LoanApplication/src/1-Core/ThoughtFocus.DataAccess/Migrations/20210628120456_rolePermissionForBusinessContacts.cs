using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class rolePermissionForBusinessContacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT Master.Action ON ");
            migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (36, 'GetAllBusinessContacts', 'Fetch all Business Contacts', 1, 36)");
            migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (37, 'AddRoleForExistingContact', 'Add role for existing contact', 1, 37)");
            migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (38, 'SaveorUpdateBusinessContact', 'Save/update Business Contacts', 1, 38)");

            migrationBuilder.Sql("SET IDENTITY_INSERT Master.Action OFF ");
            
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].RolePermission ON ");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (134,1, 36, 'Contact', 1, 1, 134)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (135,3, 36, 'Contact', 1, 1, 135)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (136,7, 36, 'Contact', 1, 1, 136)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (137,1, 37, 'Contact', 1, 1, 137)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (138,3, 37, 'Contact', 1, 1, 138)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (139,7, 37, 'Contact', 1, 1, 139)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (140,1, 38, 'Contact', 1, 1, 140)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (141,3, 38, 'Contact', 1, 1, 141)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (142,7, 38, 'Contact', 1, 1, 142)");
            migrationBuilder.Sql("SET IDENTITY_INSERT Master.RolePermission OFF ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.Sql("delete from Master.Action where ActionID IN (36,37,38)");
            migrationBuilder.Sql("delete from Master.RolePermission where RolePermissionID between 134 and 142");
        }
    }
}
