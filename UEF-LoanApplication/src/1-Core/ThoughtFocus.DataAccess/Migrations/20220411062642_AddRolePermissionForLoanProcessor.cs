using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class AddRolePermissionForLoanProcessor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT[Master].RolePermission ON");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(315, 5, 49, 'Dashboard', 1, 1, 207)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(316, 5, 50, 'Dashboard', 1, 1, 212)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(317, 5, 51, 'Dashboard', 1, 1, 217)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(318, 5, 52, 'Dashboard', 1, 1, 222)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(319, 5, 53, 'Dashboard', 1, 1, 227)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(320, 5, 54, 'Dashboard', 1, 1, 232)");

            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(321, 5, 33, 'LoanApplication', 1, 1, 88)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(322, 5, 11, 'FundingSource', 1, 1, 31)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(323, 5, 12, 'FundingSource', 1, 1, 33)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(324, 5, 13, 'FundingSource', 1, 1, 35)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(325, 5, 14, 'FundingSource', 1, 1, 37)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(326, 5, 15, 'FundingSource', 1, 1, 39)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(327, 5, 16, 'FundingSource', 1, 1, 41)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(328, 5, 17, 'FundingSource', 1, 1, 43)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(329, 5, 18, 'FundingSource', 1, 1, 45)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(330, 5, 19, 'FundingSource', 1, 1, 47)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(331, 5, 63, 'FundingSource', 1, 1, 262)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(332, 5, 64, 'FundingSource', 1, 1, 265)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(333, 5, 65, 'FundingSource', 1, 1, 268)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(334, 5, 66, 'FundingSource', 1, 1, 271)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(335, 5, 67, 'FundingSource', 1, 1, 274)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(336, 5, 68, 'FundingSource', 1, 1, 277)");

            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(337, 5, 7, 'Admin', 1, 1, 56)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(338, 5, 8, 'Admin', 1, 1, 58)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(339, 5, 10, 'Admin', 1, 1, 62)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(340, 5, 21, 'Admin', 1, 1, 66)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(341, 5, 22, 'Admin', 1, 1, 68)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(342, 5, 23, 'Admin', 1, 1, 70)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(343, 5, 24, 'Admin', 1, 1, 72)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(344, 5, 40, 'Admin', 1, 1, 147)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(345, 5, 41, 'Admin', 1, 1, 152)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(346, 5, 42, 'Admin', 1, 1, 157)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(347, 5, 43, 'Admin', 1, 1, 160)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(348, 5, 44, 'Admin', 1, 1, 163)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(349, 5, 45, 'Admin', 1, 1, 166)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(350, 5, 55, 'Admin', 1, 1, 238)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(351, 5, 56, 'Admin', 1, 1, 241)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(352, 5, 57, 'Admin', 1, 1, 244)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(353, 5, 58, 'Admin', 1, 1, 247)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(354, 5, 59, 'Admin', 1, 1, 250)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(355, 5, 60, 'Admin', 1, 1, 253)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(356, 5, 61, 'Admin', 1, 1, 256)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(357, 5, 62, 'Admin', 1, 1, 259)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(358, 5, 69, 'Admin', 1, 1, 280)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(359, 5, 70, 'Admin', 1, 1, 283)");

            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(360, 5, 25, 'Contact', 1, 1, 74)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(361, 5, 26, 'Contact', 1, 1, 76)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(362, 5, 27, 'Contact', 1, 1, 78)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(363, 5, 28, 'Contact', 1, 1, 80)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(364, 5, 30, 'Contact', 1, 1, 83)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(365, 5, 31, 'Contact', 1, 1, 85)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(366, 5, 36, 'Contact', 1, 1, 135)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(367, 5, 37, 'Contact', 1, 1, 138)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(368, 5, 38, 'Contact', 1, 1, 141)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(369, 5, 39, 'Contact', 1, 1, 144)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(370, 5, 46, 'Contact', 1, 1, 169)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(371, 5, 47, 'Contact', 1, 1, 172)");
            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(372, 5, 48, 'Contact', 1, 1, 175)");

            migrationBuilder.Sql("Insert Master.RolePermission(RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values(373, 5, 20, 'Master', 1, 1, 64)");


            migrationBuilder.Sql("SET IDENTITY_INSERT[Master].RolePermission OFF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[RolePermission]  where RolePermissionID between 315 and 320");
        }
    }
}
