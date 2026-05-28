using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class DML_RolePermissionForSiteAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT Master.Action ON ");
            migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (34, 'UploadApplicationDocument', 'Upload Application Document', 1, 34)");
            migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (35, 'Download', 'Download Document', 1, 35)");
            migrationBuilder.Sql("SET IDENTITY_INSERT Master.Action OFF ");

            migrationBuilder.Sql("SET IDENTITY_INSERT Master.RolePermission ON ");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (89,7, 1, 'LoanApplication', 1, 1, 89)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (90,7, 2, 'LoanApplication', 1, 1, 90)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (91,7, 3, 'LoanApplication', 1, 1, 91)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (92,7, 5, 'LoanApplication', 1, 1, 92)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (93,7, 6, 'LoanApplication', 1, 1, 93)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (94,7, 11, 'FundingSource', 1, 1, 94)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (95,7, 12, 'FundingSource', 1, 1, 95)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (96,7, 13, 'FundingSource', 1, 1, 96)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (97,7, 14, 'FundingSource', 1, 1, 97)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (98,7, 15, 'FundingSource', 1, 1, 98)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (99,7, 16, 'FundingSource', 1, 1, 99)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (100,7, 17, 'FundingSource', 1, 1, 100)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (101,7, 18, 'FundingSource', 1, 1, 101)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (102,7, 19, 'FundingSource', 1, 1, 102)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (103,7, 4, 'LoanApplication', 1, 1, 103)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (104,7, 7, 'Admin', 1, 1, 104)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (105,7, 8, 'Admin', 1, 1, 105)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (106,7, 9, 'Admin', 1, 1, 106)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (107,7, 10, 'Admin', 1, 1, 107)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (108,7, 20, 'Master', 1, 1, 108)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (109,7, 21, 'Admin', 1, 1, 109)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (110,7, 22, 'Admin', 1, 1, 110)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (111,7, 23, 'Admin', 1, 1, 111)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (112,7, 24, 'Admin', 1, 1, 112)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (113,7, 25, 'Contact', 1, 1, 113)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (114,7, 26, 'Contact', 1, 1, 114)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (115,7, 27, 'Contact', 1, 1, 115)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (116,7, 28, 'Contact', 1, 1, 116)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (117,7, 30, 'Contact', 1, 1, 117)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (118,7, 31, 'Contact', 1, 1, 118)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (119,7, 33, 'LoanApplication', 1, 1, 119)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (120,1, 34, 'Upload', 1, 1, 120)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (121,2, 34, 'Upload', 1, 1, 121)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (122,3, 34, 'Upload', 1, 1, 122)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (123,4, 34, 'Upload', 1, 1, 123)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (124,5, 34, 'Upload', 1, 1, 124)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (125,6, 34, 'Upload', 1, 1, 125)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (126,7, 34, 'Upload', 1, 1, 126)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (127,1, 35, 'Upload', 1, 1, 127)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (128,2, 35, 'Upload', 1, 1, 128)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (129,3, 35, 'Upload', 1, 1, 129)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (130,4, 35, 'Upload', 1, 1, 130)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (131,5, 35, 'Upload', 1, 1, 131)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (132,6, 35, 'Upload', 1, 1, 132)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (133,7, 35, 'Upload', 1, 1, 133)");
            migrationBuilder.Sql("SET IDENTITY_INSERT Master.RolePermission OFF ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from Master.RolePermission where RolePermissionID between 89 and 133");
            migrationBuilder.Sql("delete from Master.Action where ActionID IN (34,35)");
        }
    }
}
