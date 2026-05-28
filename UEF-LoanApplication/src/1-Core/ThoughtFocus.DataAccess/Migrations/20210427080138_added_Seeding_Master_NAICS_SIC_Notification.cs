using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class added_Seeding_Master_NAICS_SIC_Notification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[SIC] ON ");
            migrationBuilder.Sql("INSERT[Master].[SIC]([ID], [Code], [Division], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(1, N'44205', N'A', N'Agriculture, Forestry, AndFishing', 1, 1)");
            migrationBuilder.Sql("INSERT[Master].[SIC]([ID], [Code], [Division], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(2, N'44483', N'B', N'Mining', 1, 2)");
            migrationBuilder.Sql("INSERT[Master].[SIC]([ID], [Code], [Division], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(3, N'15-17', N'C', N'Construction', 1, 3)");
            migrationBuilder.Sql("INSERT[Master].[SIC]([ID], [Code], [Division], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(4, N'20-39', N'D', N'Manufacturing', 1, 4)");
            migrationBuilder.Sql("INSERT[Master].[SIC]([ID], [Code], [Division], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(5, N'40-49', N'E', N'Transportation, Communications, Electric, Gas, AndSanitaryServices', 1, 5)");
            migrationBuilder.Sql("INSERT[Master].[SIC]([ID], [Code], [Division], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(6, N'50-51', N'F', N'WholesaleTrade', 1, 6)");
            migrationBuilder.Sql("INSERT[Master].[SIC]([ID], [Code], [Division], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(7, N'52-59', N'G', N'RetailTrade', 1, 7)");
            migrationBuilder.Sql("INSERT[Master].[SIC]([ID], [Code], [Division], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(8, N'60-67', N'H', N'Finance, Insurance, AndRealEstate', 1, 8)");
            migrationBuilder.Sql("INSERT[Master].[SIC]([ID], [Code], [Division], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(9, N'70-89', N'I', N'Services', 1, 9)");
            migrationBuilder.Sql("INSERT[Master].[SIC]([ID], [Code], [Division], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(10, N'90-99', N'J', N'PublicAdministration', 1, 10)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[SIC] OFF ");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[NAICS] ON ");
            migrationBuilder.Sql("INSERT[Master].[NAICS]([ID], [Code], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(1, N'72', N'AccommodationandFoodServices', 1, 1)");
            migrationBuilder.Sql("INSERT[Master].[NAICS]([ID], [Code], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(2, N'56', N'AdministrativeandSupportandWasteManagementandRemediationServices', 1, 2)");
            migrationBuilder.Sql("INSERT[Master].[NAICS]([ID], [Code], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(3, N'11', N'Agriculture, Forestry, FishingandHunting', 1, 3)");
            migrationBuilder.Sql("INSERT[Master].[NAICS]([ID], [Code], [IndustryTitle], [IsActive], [DisplayOrder])VALUES(4, N'71', N'Arts, Entertainment, andRecreation', 1, 4)");
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
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[NAICS] OFF ");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[DocumentTypes] ON");
            migrationBuilder.Sql("INSERT [Master].[DocumentTypes] ([DocumentTypeID], [IsActive], [Name], [Description], [DocumentCategoryID], [DisplayOrder]) VALUES (2, 1, N'Form W-9 - Request for Taxpayer Identification Number and Certification', N'Form W-9 - Request for Taxpayer Identification Number and Certification', 1, 2)");
            migrationBuilder.Sql("INSERT [Master].[DocumentTypes] ([DocumentTypeID], [IsActive], [Name], [Description], [DocumentCategoryID], [DisplayOrder]) VALUES (3, 1, N'Proof of Ownership', N'Proof of Ownership', 1, 3)");
            migrationBuilder.Sql("INSERT [Master].[DocumentTypes] ([DocumentTypeID], [IsActive], [Name], [Description], [DocumentCategoryID], [DisplayOrder]) VALUES (4, 1, N'ACH Vendor Form', N'ACH Vendor Form', 1, 4)");
            migrationBuilder.Sql("INSERT [Master].[DocumentTypes] ([DocumentTypeID], [IsActive], [Name], [Description], [DocumentCategoryID], [DisplayOrder]) VALUES (5, 1, N'Invoice or Estimate on Letterhead for Product or Service to Be Purchased with [Grant/Loan] Proceeds', N'Invoice or Estimate on Letterhead for Product or Service to Be Purchased with [Grant/Loan] Proceeds', 1, 5)");
            migrationBuilder.Sql("INSERT [Master].[DocumentTypes] ([DocumentTypeID], [IsActive], [Name], [Description], [DocumentCategoryID], [DisplayOrder]) VALUES (6, 1, N'Others', N'Others', 1, 6)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[DocumentTypes] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[ResponseType] ON");
            migrationBuilder.Sql("INSERT [Master].[ResponseType] ([TypeID], [QuestionType], [Description], [IsActive], [DisplayOrder]) VALUES (1, N'boolean', N'True or False Question', 1, 1)");
            migrationBuilder.Sql("INSERT [Master].[ResponseType] ([TypeID], [QuestionType], [Description], [IsActive], [DisplayOrder]) VALUES (2, N'Text', N'Text Answer', 1, 2)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[ResponseType] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Notification] ON ");
            migrationBuilder.Sql("INSERT [Master].[Notification] ([NotificationID], [NotificationType], [EmailFormat],[MessageSubject], [TemplateName], [NotificationTypeDescription],[Head],[Salutation],[Body],[Footer],[RecipientType],[IsActive])VALUES (2,  N'PROGRAMINVITATION', N'EMAIL-HTML',N'Invitation for program assignment',N'PROGRAMINVITATION',N'Program Invitation',N'<!DOCTYPE HTML PUBLIC ''-//W3C//DTD XHTML 1.0 Transitional//EN'' ''http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd''> <html xmlns=''http://www.w3.org/1999/xhtml''> <head> <meta http-equiv=''Content-Type'' content=''text/html;'' charset=''UTF-8'' /> <title>Program Invitation</title> <style type=''text/css''> body { margin: 0; padding: 0; min-width: 100% !important; } img { height: auto; } .content { width: 100%; max-width: 600px; } .header { padding: 40px 30px 20px 30px; } .innerpadding { padding: 30px 30px 30px 30px; } .borderbottom { border-bottom: 1px solid #999999; } .hrcolor { border-bottom: 1px solid #4cff00; } .borderrightbottomleft { border-bottom: 1px solid #44525f; border-right: 1px solid #44525f; border-left: 1px solid #44525f; } .subhead { font-size: 15px; color: #ffffff; font-family: sans-serif; letter-spacing: 10px; } .h1, .h2, .bodycopy { color: #153643; font-family: sans-serif; } .h1 { font-size: 33px; line-height: 38px; font-weight: bold; } .h2 { padding: 0 0 15px 0; font-size: 24px; line-height: 28px; font-weight: bold; } .bodycopy { font-size: 14px; line-height: 22px; } .button { text-align: center; font-size: 18px; font-family: sans-serif; font-weight: bold; padding: 0 30px 0 30px; } .button a { color: #ffffff; text-decoration: none; } .Emailfooter { padding: 20px 30px 15px 30px; background-color:#44525f; } .footercopy { font-family: sans-serif; font-size: 14px; color: #ffffff; } .footercopy a { color: #ffffff; text-decoration: underline; } .btn-primary a:hover { background-color: #34495e !important; border-color: #34495e !important; } @@media only screen and (max-width: 550px), screen and (max-device-width: 550px) { body[yahoo] .hide { display: none !important; } body[yahoo] .buttonwrapper { background-color: transparent !important; } body[yahoo] .button { padding: 0px !important; } body[yahoo] .button a { background-color: #e05443; padding: 15px 15px 13px !important; } body[yahoo] .unsubscribe { display: block; margin-top: 20px; padding: 10px 50px; background: #2f3942; border-radius: 5px; text-decoration: none !important; font-weight: bold; } } </style> </head> <body yahoo bgcolor=''#ffffff''> <table width=''100%'' bgcolor=''#ffffff'' border=''0'' cellpadding=''0'' cellspacing=''0''> <tr> <td> <table bgcolor=''#ffffff'' class=''content'' align=''center'' cellpadding=''0'' cellspacing=''0'' border=''0''> <tr> <td class=''header'' style=''background-color: #64B54D''> <table width=''70'' align=''center'' border=''0'' cellpadding=''0'' cellspacing=''0''> <tr> <td height=''44'' style=''padding: 0 0 10px 0;''> <img class=''fix'' src=''[[ImageContent]]'' width=''229'' height=''44'' border=''0'' alt='''' /> </td> </tr> </table> </td> </tr> </table> <table bgcolor=''#ffffff'' class=''content'' align=''center'' cellpadding=''0'' cellspacing=''0'' border=''0''> <tr>',N'<td class=''innerpadding borderbottom''> <table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''> <tr> <td class=''bodycopy''> Dear @RecipientFullName, <br /><br /> </td> </tr> </table>',N'<table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''> <tr> <td class=''bodycopy''> The National Urban League invites @BusinessName to submit a [grant/loan] application for consideration. This request is confidential and is prepared for the sole use of your business and should not be distributed to other businesses. Several items will need to be attached during the application submission process. Required attachments will include an electronic or scanned copy of your: <br /> </td> </tr> <tr> <td class=''bodycopy''> <ul> <li> Form W-9 - Request for Taxpayer Identification Number and Certification </li> <li> Certificate of Good Standing (*not required for sole proprietorships) </li> <li> Proof of Ownership </li> <li> ACH Vendor Form </li> <li> Invoice or Estimate on Letterhead for Product or Service to Be Purchased with [Grant/Loan] Proceeds </li> <li> If this is for loans and grants, there will likely be other documents required such as financial statements </li> </ul> </td> </tr> <tr> <td class=''bodycopy''> <br /> You may begin a NEW application at the following link: @CallBackURL </td> </tr> <tr> <td class=''bodycopy''> <br /> Your request will be reviewed promptly, and you can expect a formal response to your application within @TimeFrame. If you need additional information or have any questions, please contact Stephanie DeVane via phone (212) 558-5378 or @EmailAddress. We look forward to receiving your application. <br /> </td> </tr> <tr> <td><br /> </td> </tr>',N'<td class=''Emailfooter''> <table width=''100%'' border=''0'' cellspacing=''0'' cellpadding=''0''> <tr> <td align=''center'' style=''background:#44525f;padding:15.0pt 22.5pt 11.25pt 22.5pt;color:#ffffff'' class=''footercopy''>© @CurrentYear, NUL. All rights reserved.<br /></td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </body> </html>','',1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Notification] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[ActivityNotification] ON ");
            migrationBuilder.Sql("INSERT [Notification].[ActivityNotification] ([ActivityNotificationID], [ActivityID], [NotificationID],[WorkflowNotificationTypeID],[IsActive],[CreatedDateTime],[CreatedByUserID],[LastModifiedDateTime],[LastModifiedByUserID]) VALUES (2, 0, 2,3,1,N'2021-04-02 12:49:45.760',1,N'2021-04-02 12:49:45.760',1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[ActivityNotification] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[NotificationRecipients] ON ");
            migrationBuilder.Sql("INSERT [Notification].[NotificationRecipients] ([NotificationRecipientID],[ActivityNotificationID],[PlaceholderID],[IsTo],[IsCC],[IsActive],[CreatedDateTime],[CreatedByUserID],[LastModifiedDateTime],[LastModifiedByUserID]) VALUES ( 2,2,1,1,0,1, N'2021-04-02 12:49:45.760',1,N'2021-04-02 12:49:45.760',1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[NotificationRecipients] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Question].[Questions] ON");
            migrationBuilder.Sql("INSERT [Question].[Questions] ([QuestionID], [QuestionText], [Version], [IsActive], [ResponseTypeID]) VALUES (1, N'Has the business ever filed for bankruptcy protection?', 1, 1, 1)");
            migrationBuilder.Sql("INSERT [Question].[Questions] ([QuestionID], [QuestionText], [Version], [IsActive], [ResponseTypeID]) VALUES (2, N'Has the business applied to a bank for a loan?', 1, 1, 1)");
            migrationBuilder.Sql("INSERT [Question].[Questions] ([QuestionID], [QuestionText], [Version], [IsActive], [ResponseTypeID]) VALUES (3, N'Have any owners of the business ever field for bankruptcy?', 1, 1, 1)");
            migrationBuilder.Sql("INSERT [Question].[Questions] ([QuestionID], [QuestionText], [Version], [IsActive], [ResponseTypeID]) VALUES (4, N'Is Business at least 51% Black-owned, operated and controlled?', 1, 1, 1)");
            migrationBuilder.Sql("INSERT [Question].[Questions] ([QuestionID], [QuestionText], [Version], [IsActive], [ResponseTypeID]) VALUES (5, N'Is applicant at least 25% Black?', 1, 1, 1)");
            migrationBuilder.Sql("INSERT [Question].[Questions] ([QuestionID], [QuestionText], [Version], [IsActive], [ResponseTypeID]) VALUES (6, N'Is applicant a U.S. citizen?', 1, 1, 1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Question].[Questions] OFF");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("delete from [Master].[SIC] where ID between 1 and 10");
            migrationBuilder.Sql("delete from [Master].[NAICS] where ID between 1 and 20");

            migrationBuilder.Sql("delete from [Notification].[ActivityNotification] where ActivityNotificationID =2");
            migrationBuilder.Sql("delete from [Notification].[NotificationRecipients]  where NotificationRecipientID =2");
            migrationBuilder.Sql("delete from [Master].[Notification] where NotificationID =2");

            migrationBuilder.Sql("delete from [Master].[DocumentTypes] where DocumentTypeID between 2 and 6");
            migrationBuilder.Sql("delete from [Master].[ResponseType] where TypeID between 1 and 2");
            migrationBuilder.Sql("delete from [Question].[Questions] where QuestionID between 1 and 6");

        }
    }
}
