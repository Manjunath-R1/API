using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class Add_ScheduleofPayment_WorkFlow_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[WorkFlow] ON");
            migrationBuilder.Sql("INSERT [Master].[WorkFlow] ([WorkFlowID], [IsActive], [Name], [Description]) VALUES (2, 1, N'SPA', N'Schedule of Payment')");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[WorkFlow] OFF");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
