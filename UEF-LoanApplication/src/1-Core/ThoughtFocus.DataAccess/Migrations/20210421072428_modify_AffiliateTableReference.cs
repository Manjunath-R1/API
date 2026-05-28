using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class modify_AffiliateTableReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessEntity_Affiliate_AffiliateID",
                schema: "Admin",
                table: "BusinessEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanBusinessDetail_Affiliate_AffiliateID",
                schema: "Application",
                table: "LoanBusinessDetail");

            migrationBuilder.DropTable(
                name: "Affiliate",
                schema: "Master");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessEntity_UrbanLeagueAffiliate_AffiliateID",
                schema: "Admin",
                table: "BusinessEntity",
                column: "AffiliateID",
                principalSchema: "Master",
                principalTable: "UrbanLeagueAffiliate",
                principalColumn: "AffiliateID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanBusinessDetail_UrbanLeagueAffiliate_AffiliateID",
                schema: "Application",
                table: "LoanBusinessDetail",
                column: "AffiliateID",
                principalSchema: "Master",
                principalTable: "UrbanLeagueAffiliate",
                principalColumn: "AffiliateID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessEntity_UrbanLeagueAffiliate_AffiliateID",
                schema: "Admin",
                table: "BusinessEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanBusinessDetail_UrbanLeagueAffiliate_AffiliateID",
                schema: "Application",
                table: "LoanBusinessDetail");

            migrationBuilder.CreateTable(
                name: "Affiliate",
                schema: "Master",
                columns: table => new
                {
                    AffiliateID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Affiliate", x => x.AffiliateID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessEntity_Affiliate_AffiliateID",
                schema: "Admin",
                table: "BusinessEntity",
                column: "AffiliateID",
                principalSchema: "Master",
                principalTable: "Affiliate",
                principalColumn: "AffiliateID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanBusinessDetail_Affiliate_AffiliateID",
                schema: "Application",
                table: "LoanBusinessDetail",
                column: "AffiliateID",
                principalSchema: "Master",
                principalTable: "Affiliate",
                principalColumn: "AffiliateID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
