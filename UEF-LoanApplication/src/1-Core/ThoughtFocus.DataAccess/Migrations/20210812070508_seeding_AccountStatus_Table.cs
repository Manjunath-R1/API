using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class seeding_AccountStatus_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[AccountStatus] ON ");
            migrationBuilder.Sql("INSERT [Master].[AccountStatus] ([AccountStatusID], [AccountStatusName], [Description], IsActive) VALUES (5, N'Locked', N'Locked', 1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[AccountStatus] OFF");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[AccountStatus] where AccountStatusID  = 5");
        }
    }
}
