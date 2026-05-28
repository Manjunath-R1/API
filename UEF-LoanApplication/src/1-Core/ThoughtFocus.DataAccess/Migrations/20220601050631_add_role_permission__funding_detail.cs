using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class add_role_permission__funding_detail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT Master.Action ON ");
            migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (79, 'SaveOrUpdatePaymentScheduleTransaction', 'Add / Update Payment Schedule', 1, 79)");
            migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (80, 'RemovePaymentScheduleTransaction', 'Remove Payment Schedule', 1, 80)");
            migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (81, 'GetPaymentScheduleTransaction', 'Get Transaction Payment Schedule', 1, 81)");
            migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (82, 'GetPaymentScheduleSummary', 'Get Transaction Payment Schedule', 1, 82)");
            migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (83, 'UploadFundAgreementDocument', 'Upload Fund Agreement Document', 1, 83)");
            migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (84, 'SaveorUpdatePaymentSchedule', 'Save or Update Payment Schedule', 1, 84)");
            migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (85, 'GetPaymentScheduleTransactionById', 'Get Payment Schedule Transaction By Id', 1, 85)");            
            migrationBuilder.Sql("Insert Master.Action (ActionID, Name, Description, IsActive, DisplayOrder) values (87, 'SaveorUpdatePaymentScheduleAndTransaction', 'Save or Update Payment Schedule And Transaction', 1, 87)");

            migrationBuilder.Sql("SET IDENTITY_INSERT Master.Action OFF ");

    

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].RolePermission ON ");


            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (386, 1, 79, 'FundingSource', 1, 1, 386)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (411, 3, 79, 'FundingSource', 1, 1, 411)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (412, 4, 79, 'FundingSource', 1, 1, 412)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (413, 6, 79, 'FundingSource', 1, 1, 413)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (418, 7, 79, 'FundingSource', 1, 1, 418)");

            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (387, 1, 80, 'FundingSource', 1, 1, 387)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (408, 3, 80, 'FundingSource', 1, 1, 408)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (409, 4, 80, 'FundingSource', 1, 1, 409)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (410, 6, 80, 'FundingSource', 1, 1, 410)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (419, 7, 80, 'FundingSource', 1, 1, 419)");

            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (388, 1, 81, 'FundingSource', 1, 1, 388)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (405, 3, 81, 'FundingSource', 1, 1, 405)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (406, 4, 81, 'FundingSource', 1, 1, 406)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (407, 6, 81, 'FundingSource', 1, 1, 407)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (420, 7, 81, 'FundingSource', 1, 1, 420)");

            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (389, 1, 82, 'FundingSource', 1, 1, 389)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (393, 3, 82, 'FundingSource', 1, 1, 393)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (394, 4, 82, 'FundingSource', 1, 1, 394)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (395, 6, 82, 'FundingSource', 1, 1, 395)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (421, 7, 82, 'FundingSource', 1, 1, 421)");

            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (390, 1, 83, 'FundingSource', 1, 1, 390)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (396, 3, 83, 'FundingSource', 1, 1, 396)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (397, 4, 83, 'FundingSource', 1, 1, 397)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (398, 6, 83, 'FundingSource', 1, 1, 398)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (422, 7, 83, 'FundingSource', 1, 1, 422)");

            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (391, 1, 84, 'FundingSource', 1, 1, 391)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (399, 3, 84, 'FundingSource', 1, 1, 399)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (400, 4, 84, 'FundingSource', 1, 1, 400)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (401, 6, 84, 'FundingSource', 1, 1, 401)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (423, 7, 84, 'FundingSource', 1, 1, 423)");

            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (392, 1, 85, 'FundingSource', 1, 1, 392)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (402, 3, 85, 'FundingSource', 1, 1, 402)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (403, 4, 85, 'FundingSource', 1, 1, 403)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (404, 6, 85, 'FundingSource', 1, 1, 404)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (424, 7, 85, 'FundingSource', 1, 1, 424)");

            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (432, 1, 87, 'FundingSource', 1, 1, 432)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (433, 3, 87, 'FundingSource', 1, 1, 433)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (434, 4, 87, 'FundingSource', 1, 1, 434)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (435, 6, 87, 'FundingSource', 1, 1, 435)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (436, 7, 87, 'FundingSource', 1, 1, 436)");



            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (425, 4, 22, 'Admin', 1, 1, 425)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (426, 6, 22, 'Admin', 1, 1, 426)");

            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (427, 4, 24, 'Admin', 1, 1, 427)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (428, 6, 24, 'Admin', 1, 1, 428)");

            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (429, 4, 26, 'Contact', 1, 1, 429)");
            migrationBuilder.Sql("Insert Master.RolePermission (RolePermissionID, RoleID, ActionID, Subject, IsAllowed, IsActive, DisplayOrder)  values (430, 6, 26, 'Contact', 1, 1, 430)");


            migrationBuilder.Sql("SET IDENTITY_INSERT Master.RolePermission OFF ");

            migrationBuilder.Sql("SET IDENTITY_INSERT Master.GenaralOption ON ");

            migrationBuilder.Sql("insert into Master.GenaralOption (optionID,OptionValue,OptionCategory,OptionDescription,IsActive,DisplayOrder) Values(1,'Pending','PaymentSchedule','PaymentSchedule',1,1)");
            migrationBuilder.Sql("insert into Master.GenaralOption (optionID,OptionValue,OptionCategory,OptionDescription,IsActive,DisplayOrder) Values(2,'Disbursed','PaymentSchedule','PaymentSchedule',1,2)");
            migrationBuilder.Sql("insert into Master.GenaralOption (optionID,OptionValue,OptionCategory,OptionDescription,IsActive,DisplayOrder) Values(3,'FundAgreementDocumentType','FundAgreementDocumentType','Fund Agreement Document Type if fund is more than $250K',1,1)");
            
            migrationBuilder.Sql("SET IDENTITY_INSERT Master.GenaralOption OFF ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[RolePermission]  where ActionID in (79,80,81,82,83,84,85,87)");
            migrationBuilder.Sql("delete from [Master].[Action]  where ActionID in (79,80,81,82,83,84,85,87)");

            

            migrationBuilder.Sql("delete from [Master].[RolePermission]  where RolePermissionID in (425,426,427,428,429,430)");

            migrationBuilder.Sql("delete from  Master.GenaralOption  where optionID in (1,2,3)");
        }
    }
}
