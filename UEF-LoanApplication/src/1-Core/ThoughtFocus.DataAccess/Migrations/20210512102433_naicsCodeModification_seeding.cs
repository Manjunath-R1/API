using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class naicsCodeModification_seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.Sql("update master.naics set code='722511',IndustryTitle='722511:Full-Service Restaurants' where id=1");
                migrationBuilder.Sql("update master.naics set code='722513',IndustryTitle='722513:Limited-Service Restaurants' where id=2");
                migrationBuilder.Sql("update master.naics set code='722330',IndustryTitle='722330:Mobile Food Services' where id=3");
                migrationBuilder.Sql("update master.naics set code='722320',IndustryTitle='722320:Caterers' where id=4");
                migrationBuilder.Sql("delete master.naics where id>4 ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.Sql("update master.naics set code=N'72',IndustryTitle=N'AccommodationandFoodServices' where id=1");
migrationBuilder.Sql("update master.naics set code=N'56',IndustryTitle=N'AdministrativeandSupportandWasteManagementandRemediationServices' where id=2");
migrationBuilder.Sql("update master.naics set code=N'11',IndustryTitle=N'Agriculture, Forestry, FishingandHunting' where id=3");
migrationBuilder.Sql("update master.naics set code=N'71',IndustryTitle=N'Arts, Entertainment, andRecreation' where id=4");
 migrationBuilder.Sql("INSERT[Master].[NAICS]([ID], [Code], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(5, N'23', N'Construction', 1, 5)");
migrationBuilder.Sql("INSERT[Master].[NAICS]([ID], [Code], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(6, N'61', N'EducationalServices', 1, 6)");
migrationBuilder.Sql("INSERT[Master].[NAICS]([ID], [Code], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(7, N'52', N'FinanceandInsurance', 1, 7)");
migrationBuilder.Sql("INSERT[Master].[NAICS]([ID], [Code], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(8, N'62', N'HealthCareandSocialAssistance', 1, 8)");
migrationBuilder.Sql("INSERT[Master].[NAICS]([ID], [Code], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(9, N'51', N'Information', 1, 9)");
migrationBuilder.Sql("INSERT[Master].[NAICS]([ID], [Code], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(10, N'55', N'ManagementofCompaniesandEnterprises', 1, 10)");
migrationBuilder.Sql("INSERT[Master].[NAICS]([ID], [Code], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(11, N'31-33', N'Manufacturing', 1, 11)");
migrationBuilder.Sql("INSERT[Master].[NAICS]([ID], [Code], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(12, N'21', N'Mining', 1, 12)");
migrationBuilder.Sql("INSERT[Master].[NAICS]([ID], [Code], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(13, N'81', N'OtherServices(exceptPublicAdministration)', 1, 13)");
migrationBuilder.Sql("INSERT[Master].[NAICS]([ID], [Code], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(14, N'54', N'Professional, Scientific, andTechnicalServices', 1, 14)");
migrationBuilder.Sql("INSERT[Master].[NAICS]([ID], [Code], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(15, N'92', N'PublicAdministration', 1, 15)");
migrationBuilder.Sql("INSERT[Master].[NAICS]([ID], [Code], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(16, N'53', N'RealEstateRentalandLeasing', 1, 16)");
migrationBuilder.Sql("INSERT[Master].[NAICS]([ID], [Code], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(17, N'44-45', N'RetailTrade', 1, 17)");
migrationBuilder.Sql("INSERT[Master].[NAICS]([ID], [Code], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(18, N'48-49', N'TransportationandWarehousing', 1, 18)");
migrationBuilder.Sql("INSERT[Master].[NAICS]([ID], [Code], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(19, N'22', N'Utilities', 1, 19)");
migrationBuilder.Sql("INSERT[Master].[NAICS]([ID], [Code], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(20, N'42', N'WholesaleTrade', 1, 20)");

        }
    }
}
