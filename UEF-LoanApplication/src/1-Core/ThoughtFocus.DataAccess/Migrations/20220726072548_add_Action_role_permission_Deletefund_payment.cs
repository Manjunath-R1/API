using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class add_Action_role_permission_Deletefund_payment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT Master.Action ON ");
            migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (89,'DeleteAllPaymentScheduleTransactionByLoan','Delete All pending Payment Schedule Transaction By Loan ',1,89)");
    
            migrationBuilder.Sql("SET IDENTITY_INSERT Master.Action OFF ");



            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].RolePermission ON ");


            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (446, 1, 89,'FundingSource',1,1,446)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (447, 3,89,'FundingSource',1,1,447)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (448, 4,89,'FundingSource',1,1,448)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (449, 6,89,'FundingSource',1,1,449)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (450,7,89,'FundingSource',1,1,450)");

          

            migrationBuilder.Sql("SET IDENTITY_INSERT Master.RolePermission OFF ");

          
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[RolePermission]  where ActionID = 89");
            migrationBuilder.Sql("delete from [Master].[Action]  where ActionID = 89");
        }
    }
}
