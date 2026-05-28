using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class add_Question_Seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Question].[Questions] ON");
            migrationBuilder.Sql("INSERT [Question].[Questions] ([QuestionID], [QuestionText], [Version], [IsActive], [ResponseTypeID]) VALUES (7, N'Is Business at least 51% Black or Hispanic-owned, operated and controlled?', 1, 1, 1)");
            migrationBuilder.Sql("INSERT [Question].[Questions] ([QuestionID], [QuestionText], [Version], [IsActive], [ResponseTypeID]) VALUES (8, N'Is applicant at least 25% Black or Hispanic?', 1, 1, 1)");
            migrationBuilder.Sql("INSERT [Question].[Questions] ([QuestionID], [QuestionText], [Version], [IsActive], [ResponseTypeID]) VALUES (9, N'Has business been in existence for at least 9 months? ', 1, 1, 1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Question].[Questions] OFF");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.Sql("delete from [Question].[Questions] where QuestionID between 7 and 9");

        }
    }
}
