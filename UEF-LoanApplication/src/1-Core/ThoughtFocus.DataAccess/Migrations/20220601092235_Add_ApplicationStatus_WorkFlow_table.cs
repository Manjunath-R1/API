using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class Add_ApplicationStatus_WorkFlow_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[ApplicationStatus] ON");
            migrationBuilder.Sql("INSERT [Master].[ApplicationStatus] ([ApplicationStatusID], [ApplicationStatusName], [Description], [IsActive], [DisplayOrder], [WorkFlowID]) VALUES (40, N'FinalDisbursed', N'Final Disbursed', 1, 0, 2)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[ApplicationStatus] OFF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
