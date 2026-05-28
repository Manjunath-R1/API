using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class seeding_LogoType_Logo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[LogoTypes] ON ");
            migrationBuilder.Sql("Insert [Master].[LogoTypes] (LogoTypeID,LogoTypeName,IsActive,DisplayOrder) values (1, 'Funding Entity Logo',1,1)");
            migrationBuilder.Sql("Insert [Master].[LogoTypes] (LogoTypeID,LogoTypeName,IsActive,DisplayOrder) values(2, 'Program Logo',1,2)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[LogoTypes] OFF ");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Logos] ON ");
            migrationBuilder.Sql("Insert [Master].[Logos] (ID, Name,Source, LogoTypeId,CreatedDateTime,CreatedByUserID,LastModifiedDateTime,LastModifiedByUserID,IsActive) values(1, 'PepsicoLogo', 'https://uefdocumentsdev.blob.core.windows.net/applicationassets/Pepsico.png', 1,N'2021-04-02 12:49:45.760',1,N'2021-04-02 12:49:45.760',1,1)");
            migrationBuilder.Sql("Insert [Master].[Logos] (ID, Name,Source,LogoTypeId,CreatedDateTime,CreatedByUserID,LastModifiedDateTime,LastModifiedByUserID,IsActive) values(2, 'BlackRestaurantProgram', 'https://uefdocumentsdev.blob.core.windows.net/applicationassets/BRA.png', 2,N'2021-04-02 12:49:45.760',1,N'2021-04-02 12:49:45.760',1,1)");
            migrationBuilder.Sql("Insert [Master].[Logos] (ID, Name,Source,LogoTypeId,CreatedDateTime,CreatedByUserID,LastModifiedDateTime,LastModifiedByUserID,IsActive) values(3, 'RoundUpLogo', 'https://uefdocumentsdev.blob.core.windows.net//applicationassets/RoundItUp.png', 1,N'2021-04-02 12:49:45.760',1,N'2021-04-02 12:49:45.760',1,1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Logos] OFF ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Master].[Logos]  where LogoID in (1,3)");
            migrationBuilder.Sql("delete from [Master].[LogoTypes]  where LogoTypeID between 1 and 2");

        }
    }
}
