using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class add_ThresholdRequestAmount_GenaralOption_ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT Master.GenaralOption ON ");

            migrationBuilder.Sql("insert into Master.GenaralOption (optionID,OptionValue,OptionCategory,OptionDescription,IsActive,DisplayOrder) Values(4,250000,'ThresholdRequestAmount','Threshold Request Amount PaymentSchedule',1,1)");
           
            migrationBuilder.Sql("SET IDENTITY_INSERT Master.GenaralOption OFF ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from  Master.GenaralOption  where optionID = 4");
        }
    }
}
