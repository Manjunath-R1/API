using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class Add_GetAllPrograms_To_ActionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Action] ON ");
            migrationBuilder.Sql("Insert [Master].[Action] ([ActionID], [Name], [Description], [IsActive], [DisplayOrder]) values (78, 'GetAllPrograms', 'Get ProgramInvitation Email', 1, 78)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Action] OFF ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[Action]  where ActionID = 78");
        }
    }
}
