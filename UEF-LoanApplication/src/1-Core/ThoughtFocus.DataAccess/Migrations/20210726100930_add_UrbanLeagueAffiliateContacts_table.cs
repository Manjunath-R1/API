using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class add_UrbanLeagueAffiliateContacts_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "Master",
                table: "UrbanLeagueAffiliate");

            migrationBuilder.RenameColumn(
                name: "EmailAddress",
                schema: "Master",
                table: "UrbanLeagueAffiliate",
                newName: "AffiliateAddress");

            migrationBuilder.CreateTable(
                name: "UrbanLeagueAffiliateContacts",
                schema: "Admin",
                columns: table => new
                {
                    AffiliateContactID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AffiliateID = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrbanLeagueAffiliateContacts", x => x.AffiliateContactID);
                    table.ForeignKey(
                        name: "FK_UrbanLeagueAffiliateContacts_UrbanLeagueAffiliate_AffiliateID",
                        column: x => x.AffiliateID,
                        principalSchema: "Master",
                        principalTable: "UrbanLeagueAffiliate",
                        principalColumn: "AffiliateID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UrbanLeagueAffiliateContacts_AffiliateID",
                schema: "Admin",
                table: "UrbanLeagueAffiliateContacts",
                column: "AffiliateID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrbanLeagueAffiliateContacts",
                schema: "Admin");

            migrationBuilder.RenameColumn(
                name: "AffiliateAddress",
                schema: "Master",
                table: "UrbanLeagueAffiliate",
                newName: "EmailAddress");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "Master",
                table: "UrbanLeagueAffiliate",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
