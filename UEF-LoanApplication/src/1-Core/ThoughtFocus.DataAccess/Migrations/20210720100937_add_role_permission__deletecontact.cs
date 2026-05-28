using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class add_role_permission__deletecontact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {   
             migrationBuilder.Sql("SET IDENTITY_INSERT Master.Action ON ");
            migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (39, 'DeleteContact', 'Delete Contact', 1, 39)");
            migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (40, 'ExportConsolidatedReport', 'Export ConsolidatedReport', 1, 40)");
            migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (41, 'GetConsolidatedReportData', 'Get ConsolidatedReportData', 1, 41)");

            migrationBuilder.Sql("SET IDENTITY_INSERT Master.Action OFF ");
            
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].RolePermission ON ");
			migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (143, 1, 39, 'Contact', 1, 1, 143)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (144, 3, 39, 'Contact', 1, 1, 144)");
		    migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (145, 7, 39, 'Contact', 1, 1, 145)");

            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (146, 1, 40, 'Admin', 1, 1, 146)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (147, 3, 40, 'Admin', 1, 1, 147)");
		    migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (148, 4, 40, 'Admin', 1, 1, 148)");
		    migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (149, 6, 40, 'Admin', 1, 1, 149)");
		    migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (150, 7, 40, 'Admin', 1, 1, 150)");


            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (151, 1, 41, 'Admin', 1, 1, 151)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (152, 3, 41, 'Admin', 1, 1, 152)");
		    migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (153, 4, 41, 'Admin', 1, 1, 153)");
		    migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (154, 6, 41, 'Admin', 1, 1, 154)");
		    migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (155, 7, 41, 'Admin', 1, 1, 155)");
            migrationBuilder.Sql("SET IDENTITY_INSERT Master.RolePermission OFF ");



        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
	         migrationBuilder.Sql("delete from [Master].[Action]  where ActionID in (39,40,41)");

			 migrationBuilder.Sql("delete from [Master].[RolePermission]  where RolePermissionID between 143 and 155");

        }
    }
}
