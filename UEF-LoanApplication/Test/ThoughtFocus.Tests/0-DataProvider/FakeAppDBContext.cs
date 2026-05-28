using System;
using Microsoft.EntityFrameworkCore;
using ThoughtFocus.DataAccess;
using ThoughtFocus.DataAccess.Models;
using ThoughtFocus.DataAccess.Models.Contact;
using ThoughtFocus.DataAccess.Models.FundingSource;
using ThoughtFocus.DataAccess.Models.Master;
using ThoughtFocus.DataAccess.Models.Application;
using System.Collections.Generic;
using ThoughtFocus.DataAccess.Models.Notification;
using ThoughtFocus.DataAccess.Models.Admin;
using System.Linq;

namespace ThoughtFocus.UnitTests.DataProvider
{
    public class FakeAppDBContext : IDisposable
    {

        #region IDisposable Support  
       // private bool disposedValue = false; // To detect redundant calls  
        private readonly ApplicationDBContext context;

        public FakeAppDBContext()
        {
            if (context != null)
            {

                var options = new DbContextOptionsBuilder<ApplicationDBContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).UseLazyLoadingProxies(true).Options;

                context = new ApplicationDBContext(options);
                if (context != null)
                {
                    MockFundingSourceData mockFundingSourceData = new MockFundingSourceData();
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                    context.Contacts.Add(getContactFakeData());
                    context.BusinessUsers.Add(getBusinessContactFakeData());
                    context.Notifications.Add(getNotificationFakeData());
                    context.ActivityNotifications.Add(getActivityNotificationFakeData());
                    context.WorkflowNotificationTypes.Add(getWorkflowNotificationTypeFakeData());
                    context.NotificationRecipients.Add(getNotificationRecipientsFakeData(1, 1, 35));
                    context.NotificationRecipients.Add(getProgramNotificationRecipientsFakeData());
                    context.BusinessRoles.Add(getBusinessRoleFakeData());
                    context.FundingSources.Add(getFundingSourceFakeData());
                    context.FundingEntities.Add(getFundingEntityFakeData());
                    context.FundTransactions.Add(GetFundTransaction());
                    context.TransactionTypes.Add(GetTransactionType());
                    context.TransactionTypes.Add(GetTransactionType1());
                    context.FundUtilizations.Add(mockFundingSourceData.GetMockedFundUtilizationData());
                    context.Notifications.Add(getProgramNotificationFakeData());
                    context.ActivityNotifications.Add(getProgramActivityNotificationFakeData());


                    context.SaveChanges();


                }
            }
        }
        public ApplicationDBContext CreateContextForInMemory()
        {
            return this.context;
        }


        public void Dispose()
        {
            context.Dispose();
        }

        private BusinessType GetBusinessType()
        {
            BusinessType businessType = new BusinessType();
            businessType.BusinessTypeID = 1;
            businessType.Description = "test";
            businessType.DisplayOrder = 1;
            businessType.IsActive = true;
            businessType.Type = "test";

            return businessType;
        }

        private UrbanLeagueAffiliate GetAffiliate()
        {
            UrbanLeagueAffiliate affiliate = new UrbanLeagueAffiliate();
            affiliate.AffiliateID = 1;
            affiliate.AffiliateAddress = "test";
            affiliate.DisplayOrder = 1;
            affiliate.IsActive = true;
            affiliate.AffiliateName = "test";

            return affiliate;
        }

        private Contact getContactFakeData()
        {
            Contact contact = new Contact();

            contact.ContactID = 1;
            contact.FirstName = "Archan";
            contact.MiddleName = "";
            contact.LastName = "S Y";
            contact.SalutationID = 1;
            contact.AccountStatusID = 1;
            contact.PhoneNo = "9845984055";
            contact.EmailAddress = "Archanyogeesh@yopmail.com";
            contact.IsActive = true;

            return contact;

        }

        private BusinessUser getBusinessContactFakeData()
        {
            BusinessUser contact = new BusinessUser();

            contact.BusinessUserID = 1;
            contact.ContactID = 1;
            contact.BusinessID = 1;
            contact.BusinessRoleID = 1;
            contact.IsActive = true;

            return contact;

        }


        private BusinessRole getBusinessRoleFakeData()
        {
            BusinessRole role = new BusinessRole();

            role.BusinessRoleID = 1;
            role.BusinessRoleName = "Owner";
            role.Description = "Owner";
            role.IsActive = true;

            return role;

        }
        // private FundUtilization getFundutilizationFakeData()
        // {
        //     FundUtilization fundUtilization = new FundUtilization();

        //     fundUtilization.ID = 1;
        //     fundUtilization.TransactionAmount = 100000;
        //     fundUtilization.TransactionTypeID = 3;
        //     fundUtilization.DateofDisbursement = DateTime.Now;
        //     fundUtilization.FundingSource = new FundingSource();
        //     fundUtilization.FundingSource.FundingType = new FundingType();
        //     fundUtilization.FundingSource.FundingType.Type = "Loan";

        //     fundUtilization.LoanApplication = new LoanApplication();
        //     fundUtilization.LoanApplication.LoanBusinessDetail = new LoanBusinessDetail();
        //     fundUtilization.LoanApplication.LoanBusinessDetail.BusinessName = "Textile Industry";
        //     fundUtilization.LoanApplication.LoanBusinessDetail.BusinessType = new BusinessType();
        //     fundUtilization.LoanApplication.LoanBusinessDetail.BusinessType.Type = "Textile";
        //     fundUtilization.ApplicationID = 1;
        //     fundUtilization.IsActive=true;

        //     return fundUtilization;
        // }

        private FundingSource getFundingSourceFakeData()
        {
            FundingSource fundingSource = new FundingSource();
            fundingSource.FundingSourceID = 1;
            fundingSource.ProgramName = "test";
            fundingSource.FundingEntityID = 1;
            fundingSource.FundingTypeID = 1;
            fundingSource.IsActive = true;
            return fundingSource;
        }

        private FundingEntity getFundingEntityFakeData()
        {
            FundingEntity fundingEntity = new FundingEntity();
            fundingEntity.FundingEntityID = 1;
            fundingEntity.FundingEntityName = "PepsiCo";
            fundingEntity.Address = "New York";
            fundingEntity.EIN = "CMh2877833";
            fundingEntity.TIN = "CMh28778333434";
            return fundingEntity;
        }

        private TransactionType GetTransactionType()
        {
            TransactionType transactionType = new TransactionType();
            transactionType.TransactionTypeID = 1;
            transactionType.Type = "Add";
            transactionType.DisplayOrder = 1;
            transactionType.Description = "Add amount to fund";
            return transactionType;
        }
        private TransactionType GetTransactionType1()
        {
            TransactionType transactionType = new TransactionType();
            transactionType.TransactionTypeID = 2;
            transactionType.Type = "Remove";
            transactionType.DisplayOrder = 1;
            transactionType.Description = "Remove amount";
            return transactionType;
        }

        private FundTransaction GetFundTransaction()
        {
            FundTransaction FundTransaction1 = new FundTransaction();
            FundTransaction1.ID = 1;
            FundTransaction1.Comment = "Added";
            FundTransaction1.FundingSourceID = 1;
            FundTransaction1.OriginatingBankAccount = "5675846578436";
            FundTransaction1.TransactionTypeID = 1;
            FundTransaction1.TransactionAmount = 80000;
            return FundTransaction1;
        }

        private Notification getNotificationFakeData()
        {
            Notification notification = new Notification();
            notification.NotificationID = 1;
            notification.NotificationType = "CONTACTINVITATION";
            notification.EmailFormat = "EMAIL-HTML";
            notification.MessageSubject = "Invitation for account activation";
            notification.TemplateName = "CONTACTINVITATION";
            notification.NotificationTypeDescription = "Contact Invitation";
            notification.Head = "<!DOCTYPE HTML PUBLIC ''-//W3C//DTD XHTML 1.0 Transitional//EN'' ''http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd''> <html xmlns=''http://www.w3.org/1999/xhtml''> <head> <meta http-equiv=''Content-Type'' content=''text/html;'' charset=''UTF-8'' /> <title>Demo Accreditation Agency</title> <style type=''text/css''> body { margin: 0; padding: 0; min-width: 100% !important; } img { height: auto; } .content { width: 100%; max-width: 600px; } .header { padding: 40px 30px 20px 30px; } .innerpadding { padding: 30px 30px 30px 30px; } .borderbottom { border-bottom: 1px solid #999999; } .hrcolor { border-bottom: 1px solid #4cff00; } .borderrightbottomleft { border-bottom: 1px solid #44525f; border-right: 1px solid #44525f; border-left: 1px solid #44525f; } .subhead { font-size: 15px; color: #ffffff; font-family: sans-serif; letter-spacing: 10px; } .h1, .h2, .bodycopy { color: #153643; font-family: sans-serif; } .h1 { font-size: 33px; line-height: 38px; font-weight: bold; } .h2 { padding: 0 0 15px 0; font-size: 24px; line-height: 28px; font-weight: bold; } .bodycopy { font-size: 14px; line-height: 22px; } .button { text-align: center; font-size: 18px; font-family: sans-serif; font-weight: bold; padding: 0 30px 0 30px; } .button a { color: #ffffff; text-decoration: none; } .Emailfooter { padding: 20px 30px 15px 30px; background-color:#44525f; } .footercopy { font-family: sans-serif; font-size: 14px; color: #ffffff; } .footercopy a { color: #ffffff; text-decoration: underline; } .btn-primary a:hover { background-color: #34495e !important; border-color: #34495e !important; } @@media only screen and (max-width: 550px), screen and (max-device-width: 550px) { body[yahoo] .hide { display: none !important; } body[yahoo] .buttonwrapper { background-color: transparent !important; } body[yahoo] .button { padding: 0px !important; } body[yahoo] .button a { background-color: #e05443; padding: 15px 15px 13px !important; } body[yahoo] .unsubscribe { display: block; margin-top: 20px; padding: 10px 50px; background: #2f3942; border-radius: 5px; text-decoration: none !important; font-weight: bold; } } </style> </head> <body yahoo bgcolor=''#ffffff''> <table width=''100%'' bgcolor=''#ffffff'' border=''0'' cellpadding=''0'' cellspacing=''0''> <tr> <td> <table bgcolor=''#ffffff'' class=''content'' align=''center'' cellpadding=''0'' cellspacing=''0'' border=''0''> <tr> <td class=''header'' style=''background-color: #64B54D''> <table width=''70'' align=''center'' border=''0'' cellpadding=''0'' cellspacing=''0''> <tr> <td height=''44'' style=''padding: 0 0 10px 0;''> <img class=''fix'' src=''[[ImageContent]]'' width=''229'' height=''44'' border=''0'' alt='''' /> </td> </tr> </table> </td> </tr> </table> <table bgcolor=''#ffffff'' class=''content'' align=''center'' cellpadding=''0'' cellspacing=''0'' border=''0''> <tr>";
            notification.Salutation = "<td class=''innerpadding borderbottom''> <table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''> <tr> <td class=''bodycopy''> Dear @RecipientFullName, <br /><br /> </td> </tr> </table>";
            notification.Body = "<table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''> <tr> <td class=''bodycopy''> Welcome to the NUL ! <br /><br /> </td> </tr> <tr> <td class=''bodycopy''> You are receiving this email because NUL staff member has created an account in your name. Please click on ''Activate Account'' to complete your account information. You will be directed to a page where you will be shown your username, (your email address) , enter your password and confirm password . After submission your account will be created. <br /><br /> </td> </tr> <tr> <td style=''padding: 20px 0 20px 180px;''> <table class=''buttonwrapper'' border=''0'' cellspacing=''0'' cellpadding=''0''> <tr> <td style=''font-family: sans-serif; font-size: 14px; vertical-align: top; background-color: #277812; border-radius: 5px; text-align: center;'' class=''btn-primary''> <a href= @CallBackURL target=''_blank'' style=''display: inline-block; color: #ffffff; background-color: #277812; border: solid 1px #277812; border-radius: 5px; box-sizing: border-box; cursor: pointer; text-decoration: none; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-transform: capitalize; border-color: #277812;''>Activate Account</a> </td> </tr> </table> </td> </tr> <tr> <td class=''bodycopy''><br /> @AdditionalMessage </td> </tr> <tr> <td class=''bodycopy''><br /> Please direct any questions or problems regarding this email to staff@yopmail.com or newstaff@yopmail.com for assistance. <br /> </td> </tr> <tr> <td><br /> </td> </tr> <tr> <td class=''bodycopy''> Sincerely,<br /> . NUL </td> </tr> </table> </td> </tr> <tr>";
            notification.Footer = "<td class=''Emailfooter''> <table width=''100%'' border=''0'' cellspacing=''0'' cellpadding=''0''> <tr> <td align=''center'' style=''background:#44525f;padding:15.0pt 22.5pt 11.25pt 22.5pt;color:#ffffff'' class=''footercopy''>© @CurrentYear, NUL. All rights reserved.<br /></td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </body> </html>";
            notification.RecipientType = 0;
            notification.IsActive = true;
            return notification;

        }

        private ActivityNotification getActivityNotificationFakeData()
        {
            ActivityNotification activityNotification = new ActivityNotification();
            activityNotification.ActivityNotificationID = 1;
            activityNotification.ActivityID = 0;
            activityNotification.NotificationID = 1;
            activityNotification.WorkflowNotificationTypeID = 3;
            activityNotification.IsActive = true;
            return activityNotification;
        }

        private WorkflowNotificationType getWorkflowNotificationTypeFakeData()
        {
            WorkflowNotificationType workflowNotificationType = new WorkflowNotificationType();

            workflowNotificationType.WorkflowNotificationTypeID = 1;
            workflowNotificationType.WorkflowNotificationTypeName = "Other";
            workflowNotificationType.IsActive = true;
            return workflowNotificationType;

        }

        private NotificationRecipients getNotificationRecipientsFakeData(long NotificationRecipientID, long ActivityNotificationID, long PlaceholderID)
        {
            NotificationRecipients notificationRecipients = new NotificationRecipients();

            notificationRecipients.NotificationRecipientID = NotificationRecipientID;
            notificationRecipients.ActivityNotificationID = ActivityNotificationID;
            notificationRecipients.PlaceholderID = PlaceholderID;
            notificationRecipients.IsTo = true;
            notificationRecipients.IsCC = false;
            notificationRecipients.IsActive = true;

            // if((context.EmailTemplatePlaceholders.Where(x => x.PlaceholderID == PlaceholderID).Count())==0){
            //     notificationRecipients.Placeholders=getEmailTemplatePlaceholdersFakeData(35, "@Contact", "Contact", 4);
            // }

            return notificationRecipients;

        }

        private EmailTemplatePlaceholders getEmailTemplatePlaceholdersFakeData(long PlaceholderID, string DisplayName, string Placeholder, long PlaceHolderTypeID)
        {
            EmailTemplatePlaceholders emailTemplatePlaceholders = new EmailTemplatePlaceholders();

            emailTemplatePlaceholders.PlaceholderID = PlaceholderID;
            emailTemplatePlaceholders.DisplayName = DisplayName;
            emailTemplatePlaceholders.Placeholder = Placeholder;
            emailTemplatePlaceholders.PlaceHolderTypeID = PlaceHolderTypeID;
            emailTemplatePlaceholders.IsActive = true;


            return emailTemplatePlaceholders;

        }

        private Notification getProgramNotificationFakeData()
        {
            Notification notification = new Notification();
            notification.NotificationID = 2;
            notification.NotificationType = "PROGRAMINVITATION";
            notification.EmailFormat = "EMAIL-HTML";
            notification.MessageSubject = "Invitation for program assigned";
            notification.TemplateName = "PROGRAMINVITATION";
            notification.NotificationTypeDescription = "Program Invitation";
            notification.Head = "<!DOCTYPE HTML PUBLIC ''-//W3C//DTD XHTML 1.0 Transitional//EN'' ''http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd''> <html xmlns=''http://www.w3.org/1999/xhtml''> <head> <meta http-equiv=''Content-Type'' content=''text/html;'' charset=''UTF-8'' /> <title>Demo Accreditation Agency</title> <style type=''text/css''> body { margin: 0; padding: 0; min-width: 100% !important; } img { height: auto; } .content { width: 100%; max-width: 600px; } .header { padding: 40px 30px 20px 30px; } .innerpadding { padding: 30px 30px 30px 30px; } .borderbottom { border-bottom: 1px solid #999999; } .hrcolor { border-bottom: 1px solid #4cff00; } .borderrightbottomleft { border-bottom: 1px solid #44525f; border-right: 1px solid #44525f; border-left: 1px solid #44525f; } .subhead { font-size: 15px; color: #ffffff; font-family: sans-serif; letter-spacing: 10px; } .h1, .h2, .bodycopy { color: #153643; font-family: sans-serif; } .h1 { font-size: 33px; line-height: 38px; font-weight: bold; } .h2 { padding: 0 0 15px 0; font-size: 24px; line-height: 28px; font-weight: bold; } .bodycopy { font-size: 14px; line-height: 22px; } .button { text-align: center; font-size: 18px; font-family: sans-serif; font-weight: bold; padding: 0 30px 0 30px; } .button a { color: #ffffff; text-decoration: none; } .Emailfooter { padding: 20px 30px 15px 30px; background-color:#44525f; } .footercopy { font-family: sans-serif; font-size: 14px; color: #ffffff; } .footercopy a { color: #ffffff; text-decoration: underline; } .btn-primary a:hover { background-color: #34495e !important; border-color: #34495e !important; } @@media only screen and (max-width: 550px), screen and (max-device-width: 550px) { body[yahoo] .hide { display: none !important; } body[yahoo] .buttonwrapper { background-color: transparent !important; } body[yahoo] .button { padding: 0px !important; } body[yahoo] .button a { background-color: #e05443; padding: 15px 15px 13px !important; } body[yahoo] .unsubscribe { display: block; margin-top: 20px; padding: 10px 50px; background: #2f3942; border-radius: 5px; text-decoration: none !important; font-weight: bold; } } </style> </head> <body yahoo bgcolor=''#ffffff''> <table width=''100%'' bgcolor=''#ffffff'' border=''0'' cellpadding=''0'' cellspacing=''0''> <tr> <td> <table bgcolor=''#ffffff'' class=''content'' align=''center'' cellpadding=''0'' cellspacing=''0'' border=''0''> <tr> <td class=''header'' style=''background-color: #64B54D''> <table width=''70'' align=''center'' border=''0'' cellpadding=''0'' cellspacing=''0''> <tr> <td height=''44'' style=''padding: 0 0 10px 0;''> <img class=''fix'' src=''[[ImageContent]]'' width=''229'' height=''44'' border=''0'' alt='''' /> </td> </tr> </table> </td> </tr> </table> <table bgcolor=''#ffffff'' class=''content'' align=''center'' cellpadding=''0'' cellspacing=''0'' border=''0''> <tr>";
            notification.Salutation = "<td class=''innerpadding borderbottom''> <table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''> <tr> <td class=''bodycopy''> Dear @RecipientFullName, <br /><br /> </td> </tr> </table>";
            notification.Body = "<table width='100%' border='0' cellspacing='10' cellpadding='0'> <tr> <td class='bodycopy'> The National Urban League invites @BusinessName to submit a [grant/loan] application for consideration. This request is confidential and is prepared for the sole use of your business and should not be distributed to other businesses. Several items will need to be attached during the application submission process. Required attachments will include an electronic or scanned copy of your: <br /> </td> </tr> <tr> <td class='bodycopy'> <ul> <li> Form W-9 - Request for Taxpayer Identification Number and Certification </li> <li> Certificate of Good Standing (*not required for sole proprietorships) </li> <li> Proof of Ownership </li> <li> ACH Vendor Form </li> <li> Invoice or Estimate on Letterhead for Product or Service to Be Purchased with [Grant/Loan] Proceeds </li> <li> If this is for loans and grants, there will likely be other documents required such as financial statements </li> </ul> </td> </tr> <tr> <td class='bodycopy'> <br /> You may begin a NEW application at the following link: @CallBackURL </td> </tr> <tr> <td class='bodycopy'> <br /> Your request will be reviewed promptly, and you can expect a formal response to your application within @TimeFrame. If you need additional information or have any questions, please contact Stephanie DeVane via phone (212) 558-5378 or @EmailAddress. We look forward to receiving your application. <br /> </td> </tr> <tr> <td><br /> </td> </tr>";
            notification.Footer = "<td class=''Emailfooter''> <table width=''100%'' border=''0'' cellspacing=''0'' cellpadding=''0''> <tr> <td align=''center'' style=''background:#44525f;padding:15.0pt 22.5pt 11.25pt 22.5pt;color:#ffffff'' class=''footercopy''>© @CurrentYear, NUL. All rights reserved.<br /></td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </body> </html>";
            notification.RecipientType = 0;
            notification.IsActive = true;
            return notification;

        }

        private ActivityNotification getProgramActivityNotificationFakeData()
        {
            ActivityNotification activityNotification = new ActivityNotification();
            activityNotification.ActivityNotificationID = 2;
            activityNotification.ActivityID = 0;
            activityNotification.NotificationID = 2;
            activityNotification.WorkflowNotificationTypeID = 3;
            activityNotification.IsActive = true;
            return activityNotification;
        }


        private NotificationRecipients getProgramNotificationRecipientsFakeData()
        {
            NotificationRecipients notificationRecipients = new NotificationRecipients();

            notificationRecipients.NotificationRecipientID = 2;
            notificationRecipients.ActivityNotificationID = 2;
            notificationRecipients.PlaceholderID = 1;
            notificationRecipients.IsTo = true;
            notificationRecipients.IsCC = false;
            notificationRecipients.IsActive = true;
            notificationRecipients.Placeholders = getEmailTemplatePlaceholdersFakeData(35, "@Contact", "Contact", 4);
            return notificationRecipients;

        }

        public BusinessEntity getBusinessEntity()
        {
            BusinessEntity businessEntity = new BusinessEntity();

            businessEntity.AffiliateID = 1;
            businessEntity.BusinessName = "Test";
            businessEntity.EIN = "12-8764435";
            businessEntity.BusinessTypeID = 1;
            return businessEntity;
        }

        public ProgramInvitation getProgramInvitation()
        {
            ProgramInvitation programInvitation = new ProgramInvitation();
            programInvitation.ProgramInvitationID = 1;
            programInvitation.BusinessID = 1;
            programInvitation.ProgramID = 10;
            programInvitation.ProgramStatusID = 1;
            programInvitation.InvitationSentDateTime = DateTime.Now;
            return programInvitation;
        }


        #endregion
    }
}

