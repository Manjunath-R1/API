using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class ApplicationStatusSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkFlowID",
                schema: "Master",
                table: "ApplicationStatus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "WorkFlow",
                schema: "Master",
                columns: table => new
                {
                    WorkFlowID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFlow", x => x.WorkFlowID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationStatus_WorkFlowID",
                schema: "Master",
                table: "ApplicationStatus",
                column: "WorkFlowID");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationStatus_WorkFlow_WorkFlowID",
                schema: "Master",
                table: "ApplicationStatus",
                column: "WorkFlowID",
                principalSchema: "Master",
                principalTable: "WorkFlow",
                principalColumn: "WorkFlowID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[WorkFlow] ON");
                    migrationBuilder.Sql("Insert [Master].[WorkFlow] ([WorkFlowID], [IsActive], [Name], [Description]) values (1, 1, 'LoanApplication','Loan Application')");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[WorkFlow] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[ApplicationStatus] ON");
            migrationBuilder.Sql("Insert [Master].[ApplicationStatus] ([ApplicationStatusID], [ApplicationStatusName], [Description], [IsActive], [DisplayOrder], [WorkFlowID]) values (1, 'Initialized','Initialized', 1, 0, 1)");
            migrationBuilder.Sql("Insert [Master].[ApplicationStatus] ([ApplicationStatusID], [ApplicationStatusName], [Description], [IsActive], [DisplayOrder], [WorkFlowID]) values (2, 'Created', 'Created', 1, 0, 1)");
            migrationBuilder.Sql("Insert [Master].[ApplicationStatus] ([ApplicationStatusID], [ApplicationStatusName], [Description], [IsActive], [DisplayOrder], [WorkFlowID]) values (3, 'Drafted', 'Drafted', 1, 0, 1)");
            migrationBuilder.Sql("Insert [Master].[ApplicationStatus] ([ApplicationStatusID], [ApplicationStatusName], [Description], [IsActive], [DisplayOrder], [WorkFlowID]) values (4, 'Submitted', 'Submitted', 1, 0, 1)");
            migrationBuilder.Sql("Insert [Master].[ApplicationStatus] ([ApplicationStatusID], [ApplicationStatusName], [Description], [IsActive], [DisplayOrder], [WorkFlowID]) values (5, 'RequestedMoreInfo', 'Requested More Information', 1, 0, 1)");
            migrationBuilder.Sql("Insert [Master].[ApplicationStatus] ([ApplicationStatusID], [ApplicationStatusName], [Description], [IsActive], [DisplayOrder], [WorkFlowID]) values (6, 'RequestCompleted', 'Request Completed', 1, 0, 1)");
            migrationBuilder.Sql("Insert [Master].[ApplicationStatus] ([ApplicationStatusID], [ApplicationStatusName], [Description], [IsActive], [DisplayOrder], [WorkFlowID]) values (7, 'Accepted', 'Accepted', 1, 0, 1)");
            migrationBuilder.Sql("Insert [Master].[ApplicationStatus] ([ApplicationStatusID], [ApplicationStatusName], [Description], [IsActive], [DisplayOrder], [WorkFlowID]) values (8, 'Approved', 'Approved', 1, 0, 1)");
            migrationBuilder.Sql("Insert [Master].[ApplicationStatus] ([ApplicationStatusID], [ApplicationStatusName], [Description], [IsActive], [DisplayOrder], [WorkFlowID]) values (9, 'Rejected', 'Rejected', 1, 0, 1)");
            migrationBuilder.Sql("Insert [Master].[ApplicationStatus] ([ApplicationStatusID], [ApplicationStatusName], [Description], [IsActive], [DisplayOrder], [WorkFlowID]) values (10, 'AgreementUploaded', 'Agreement Uploaded', 1, 0, 1)");
            migrationBuilder.Sql("Insert [Master].[ApplicationStatus] ([ApplicationStatusID], [ApplicationStatusName], [Description], [IsActive], [DisplayOrder], [WorkFlowID]) values (11, 'AgreementAccepted', 'Agreement Accepted', 1, 0, 1)");
            migrationBuilder.Sql("Insert [Master].[ApplicationStatus] ([ApplicationStatusID], [ApplicationStatusName], [Description], [IsActive], [DisplayOrder], [WorkFlowID]) values (12, 'FundingInitiated', 'Funding Initiated', 1, 0, 1)");
            migrationBuilder.Sql("Insert [Master].[ApplicationStatus] ([ApplicationStatusID], [ApplicationStatusName], [Description], [IsActive], [DisplayOrder], [WorkFlowID]) values (13, 'FundingCompleted', 'Funding Completed', 1, 0, 1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[ApplicationStatus] OFF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationStatus_WorkFlow_WorkFlowID",
                schema: "Master",
                table: "ApplicationStatus");

            migrationBuilder.DropTable(
                name: "WorkFlow",
                schema: "Master");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationStatus_WorkFlowID",
                schema: "Master",
                table: "ApplicationStatus");

            migrationBuilder.DropColumn(
                name: "WorkFlowID",
                schema: "Master",
                table: "ApplicationStatus");

            migrationBuilder.Sql("delete from [Master].[WorkFlow] where WorkFlowID = 1 and");
            migrationBuilder.Sql("delete from [Master].[ApplicationStatus] where ApplicationStatusID between 1 and 13");
        }
    }
}
