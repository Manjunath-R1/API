using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class seeding_HelpfulGuide_Table_DocumentTemplate_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(" update [Master].[HelpfulGuideTemplates] set body='<p><h4 style=''box-sizing: border-box; margin: 0px 0px 1.5rem; padding: 0px; font-weight: 400; line-height: 1.2; font-size: 2.4rem; text-align: left; text-indent: 0px;''>Helpful Guide and FAQs</h4></p><p>Proof of Ownership can include tax return with IRS Schedule C; copy of DBA Certificate; articles of incorporation; or articles of organization.</p><p>Please click the link to download Vendor ACH form.</p><p><a style=''font-size: 1.6rem;'' href =''https://uefdocumentsdev.blob.core.windows.net/applicationassets/NULVENDORACHFORM-I-Updated(11-2-17).xlsx''> Download </a ></p ><p><a style=''font-size: 1.6rem;'' href=''mailto:contactus@buildbackblack.org'' >Contact us</a></p>' where  ID =3");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
