using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class AddSeeding_AccountStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE [Master].[AccountStatus] SET  [AccountStatusName]='NotInvited', [Description] = 'Not Invited' WHERE [AccountStatusID] = 1 ");
            migrationBuilder.Sql("UPDATE [Master].[AccountStatus] SET  [AccountStatusName]='Invited', [Description] = 'Invited' WHERE [AccountStatusID] = 2 ");
            migrationBuilder.Sql("UPDATE [Master].[AccountStatus] SET  [AccountStatusName]='Active', [Description] = 'Active' WHERE [AccountStatusID] = 3 ");
            migrationBuilder.Sql("UPDATE [Master].[AccountStatus] SET  [AccountStatusName]='Deactivated', [Description] = 'Deactivated' WHERE [AccountStatusID] = 4 ");
            migrationBuilder.Sql("DELETE FROM [Master].[AccountStatus] WHERE [AccountStatusID] = 5 ");
            migrationBuilder.Sql("DELETE FROM [Master].[AccountStatus] WHERE [AccountStatusID] = 6 ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[AccountStatus] ON ");
            migrationBuilder.Sql("INSERT [Master].[AccountStatus] ([AccountStatusID], [AccountStatusName], [Description], IsActive) VALUES (5, N'Activated', N'Activated', 1)");
            migrationBuilder.Sql("INSERT [Master].[AccountStatus] ([AccountStatusID], [AccountStatusName], [Description], IsActive) VALUES (6, N'Deactivated', N'Deactivated', 1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[AccountStatus] OFF");

            migrationBuilder.Sql("UPDATE [Master].[AccountStatus] SET  [AccountStatusName]='NotCreated', [Description] = 'Not Created' WHERE [AccountStatusID] = 1 ");
            migrationBuilder.Sql("UPDATE [Master].[AccountStatus] SET  [AccountStatusName]='RequestPending', [Description] = 'Request Pending' WHERE [AccountStatusID] = 2 ");
            migrationBuilder.Sql("UPDATE [Master].[AccountStatus] SET  [AccountStatusName]='RequestRejected', [Description] = 'Request Rejected' WHERE [AccountStatusID] = 3 ");
            migrationBuilder.Sql("UPDATE [Master].[AccountStatus] SET  [AccountStatusName]='RequestAccepted', [Description] = 'Request Accepted' WHERE [AccountStatusID] = 4 ");
        }
    }
}
