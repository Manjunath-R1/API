using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class added_Program_status_seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[ProgramStatus] ON ");
            migrationBuilder.Sql("INSERT [Master].[ProgramStatus] ([ProgramStatusID], [ProgramStatusName], [Description], IsActive) VALUES (1, N'Invited', N'Invited', 1)");
            migrationBuilder.Sql("INSERT [Master].[ProgramStatus] ([ProgramStatusID], [ProgramStatusName], [Description], IsActive) VALUES (2, N'Applied', N'Applied', 1)");
            migrationBuilder.Sql("INSERT [Master].[ProgramStatus] ([ProgramStatusID], [ProgramStatusName], [Description], IsActive) VALUES (3, N'Expired', N'Expired', 1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[ProgramStatus] OFF");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[ProgramStatus] where ProgramStatusID between 1 and 3");
        }
    }
}
