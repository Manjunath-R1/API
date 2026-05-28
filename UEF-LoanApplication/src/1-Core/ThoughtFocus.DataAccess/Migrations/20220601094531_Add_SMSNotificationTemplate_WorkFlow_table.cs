using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class Add_SMSNotificationTemplate_WorkFlow_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[SMSNotificationTemplate] ON");
            migrationBuilder.Sql("insert into [master].[SMSNotificationTemplate](ID, Template, IsActive, ApplicationStatusID) values(9, N'Congratulations! The <Disbursement> of funds for the application <#REF No> for <Program Name> has been disbursed to your account', 1, 40)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[SMSNotificationTemplate] off");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
