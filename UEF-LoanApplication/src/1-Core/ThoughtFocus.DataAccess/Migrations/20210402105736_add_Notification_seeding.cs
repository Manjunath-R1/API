using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class add_Notification_seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[EmailTemplatePlaceholderType] ON ");
            migrationBuilder.Sql("INSERT [Master].[EmailTemplatePlaceholderType] ([PlaceHolderTypeID], [PlaceHolderType], IsActive) VALUES (1,  N'User Management', 1)");
            migrationBuilder.Sql("INSERT [Master].[EmailTemplatePlaceholderType] ([PlaceHolderTypeID], [PlaceHolderType], IsActive) VALUES (2,  N'Email Recipient', 1)");
            migrationBuilder.Sql("INSERT [Master].[EmailTemplatePlaceholderType] ([PlaceHolderTypeID], [PlaceHolderType], IsActive) VALUES (3,  N'Email Signature', 1)");
            migrationBuilder.Sql("INSERT [Master].[EmailTemplatePlaceholderType] ([PlaceHolderTypeID], [PlaceHolderType], IsActive) VALUES (4,  N'Email Footer', 1)");
            migrationBuilder.Sql("INSERT [Master].[EmailTemplatePlaceholderType] ([PlaceHolderTypeID], [PlaceHolderType], IsActive) VALUES (5,  N'URL', 1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[EmailTemplatePlaceholderType] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[NotificationStatus] ON ");
            migrationBuilder.Sql("INSERT [Master].[NotificationStatus] ([NotificationStatusID], [Name], [Description]) VALUES (1,  N'Sent', N'Email Notification Sent Successfully')");
            migrationBuilder.Sql("INSERT [Master].[NotificationStatus] ([NotificationStatusID], [Name], [Description]) VALUES (2,  N'NoPreference', N'Email Notification Preference is disabled')");
            migrationBuilder.Sql("INSERT [Master].[NotificationStatus] ([NotificationStatusID], [Name], [Description]) VALUES (3,  N'Error', N'Error Occurred while sending the Email')");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[NotificationStatus] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[WorkflowNotificationType] ON ");
            migrationBuilder.Sql("INSERT [Master].[WorkflowNotificationType] ([WorkflowNotificationTypeID],[CreatedDateTime],[CreatedByUserID],[LastModifiedDateTime],[LastModifiedByUserID],[IsActive],[WorkflowNotificationTypeName]) VALUES (1, N'2021-04-02 12:49:45.760',1,N'2021-04-02 12:49:45.760',1,1,N'BindToActivity')");
            migrationBuilder.Sql("INSERT [Master].[WorkflowNotificationType] ([WorkflowNotificationTypeID],[CreatedDateTime],[CreatedByUserID],[LastModifiedDateTime],[LastModifiedByUserID],[IsActive],[WorkflowNotificationTypeName]) VALUES (2, N'2021-04-02 12:49:45.760',1,N'2021-04-02 12:49:45.760',1,1,N'NotBindToActivity')");
            migrationBuilder.Sql("INSERT [Master].[WorkflowNotificationType] ([WorkflowNotificationTypeID],[CreatedDateTime],[CreatedByUserID],[LastModifiedDateTime],[LastModifiedByUserID],[IsActive],[WorkflowNotificationTypeName]) VALUES (3, N'2021-04-02 12:49:45.760',1,N'2021-04-02 12:49:45.760',1,1,N'Other')");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[WorkflowNotificationType] OFF");

            
              migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Notification] ON ");
            migrationBuilder.Sql("INSERT [Master].[Notification] ([NotificationID], [NotificationType], [EmailFormat],[MessageSubject], [TemplateName], [NotificationTypeDescription],[Head],[Salutation],[Body],[Footer],[RecipientType],[IsActive])VALUES (1,  N'CONTACTINVITATION', N'EMAIL-HTML',N'Invitation for account activation',N'CONTACTINVITATION',N'Contact Invitation',N'<!DOCTYPE HTML PUBLIC ''-//W3C//DTD XHTML 1.0 Transitional//EN'' ''http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd''> <html xmlns=''http://www.w3.org/1999/xhtml''> <head> <meta http-equiv=''Content-Type'' content=''text/html;'' charset=''UTF-8'' /> <title>Demo Accreditation Agency</title> <style type=''text/css''> body { margin: 0; padding: 0; min-width: 100% !important; } img { height: auto; } .content { width: 100%; max-width: 600px; } .header { padding: 40px 30px 20px 30px; } .innerpadding { padding: 30px 30px 30px 30px; } .borderbottom { border-bottom: 1px solid #999999; } .hrcolor { border-bottom: 1px solid #4cff00; } .borderrightbottomleft { border-bottom: 1px solid #44525f; border-right: 1px solid #44525f; border-left: 1px solid #44525f; } .subhead { font-size: 15px; color: #ffffff; font-family: sans-serif; letter-spacing: 10px; } .h1, .h2, .bodycopy { color: #153643; font-family: sans-serif; } .h1 { font-size: 33px; line-height: 38px; font-weight: bold; } .h2 { padding: 0 0 15px 0; font-size: 24px; line-height: 28px; font-weight: bold; } .bodycopy { font-size: 14px; line-height: 22px; } .button { text-align: center; font-size: 18px; font-family: sans-serif; font-weight: bold; padding: 0 30px 0 30px; } .button a { color: #ffffff; text-decoration: none; } .Emailfooter { padding: 20px 30px 15px 30px; background-color:#44525f; } .footercopy { font-family: sans-serif; font-size: 14px; color: #ffffff; } .footercopy a { color: #ffffff; text-decoration: underline; } .btn-primary a:hover { background-color: #34495e !important; border-color: #34495e !important; } @@media only screen and (max-width: 550px), screen and (max-device-width: 550px) { body[yahoo] .hide { display: none !important; } body[yahoo] .buttonwrapper { background-color: transparent !important; } body[yahoo] .button { padding: 0px !important; } body[yahoo] .button a { background-color: #e05443; padding: 15px 15px 13px !important; } body[yahoo] .unsubscribe { display: block; margin-top: 20px; padding: 10px 50px; background: #2f3942; border-radius: 5px; text-decoration: none !important; font-weight: bold; } } </style> </head> <body yahoo bgcolor=''#ffffff''> <table width=''100%'' bgcolor=''#ffffff'' border=''0'' cellpadding=''0'' cellspacing=''0''> <tr> <td> <table bgcolor=''#ffffff'' class=''content'' align=''center'' cellpadding=''0'' cellspacing=''0'' border=''0''> <tr> <td class=''header'' style=''background-color: #64B54D''> <table width=''70'' align=''center'' border=''0'' cellpadding=''0'' cellspacing=''0''> <tr> <td height=''44'' style=''padding: 0 0 10px 0;''> <img class=''fix'' src=''[[ImageContent]]'' width=''229'' height=''44'' border=''0'' alt='''' /> </td> </tr> </table> </td> </tr> </table> <table bgcolor=''#ffffff'' class=''content'' align=''center'' cellpadding=''0'' cellspacing=''0'' border=''0''> <tr>',N'<td class=''innerpadding borderbottom''> <table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''> <tr> <td class=''bodycopy''> Dear @RecipientFullName, <br /><br /> </td> </tr> </table>',N'<table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''> <tr> <td class=''bodycopy''> Welcome to the NUL ! <br /><br /> </td> </tr> <tr> <td class=''bodycopy''> You are receiving this email because NUL staff member has created an account in your name. Please click on ''Activate Account'' to complete your account information. You will be directed to a page where you will be shown your username, (your email address) , enter your password and confirm password . After submission your account will be created. <br /><br /> </td> </tr> <tr> <td style=''padding: 20px 0 20px 180px;''> <table class=''buttonwrapper'' border=''0'' cellspacing=''0'' cellpadding=''0''> <tr> <td style=''font-family: sans-serif; font-size: 14px; vertical-align: top; background-color: #277812; border-radius: 5px; text-align: center;'' class=''btn-primary''> <a href= @CallBackURL target=''_blank'' style=''display: inline-block; color: #ffffff; background-color: #277812; border: solid 1px #277812; border-radius: 5px; box-sizing: border-box; cursor: pointer; text-decoration: none; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-transform: capitalize; border-color: #277812;''>Activate Account</a> </td> </tr> </table> </td> </tr> <tr> <td class=''bodycopy''><br /> @AdditionalMessage </td> </tr> <tr> <td class=''bodycopy''><br /> Please direct any questions or problems regarding this email to staff@yopmail.com or newstaff@yopmail.com for assistance. <br /> </td> </tr> <tr> <td><br /> </td> </tr> <tr> <td class=''bodycopy''> Sincerely,<br /> . NUL </td> </tr> </table> </td> </tr> <tr>',N'<td class=''Emailfooter''> <table width=''100%'' border=''0'' cellspacing=''0'' cellpadding=''0''> <tr> <td align=''center'' style=''background:#44525f;padding:15.0pt 22.5pt 11.25pt 22.5pt;color:#ffffff'' class=''footercopy''>© @CurrentYear, NUL. All rights reserved.<br /></td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </body> </html>','',1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Notification] OFF");


            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[ActivityNotification] ON ");
            migrationBuilder.Sql("INSERT [Notification].[ActivityNotification] ([ActivityNotificationID], [ActivityID], [NotificationID],[WorkflowNotificationTypeID],[IsActive],[CreatedDateTime],[CreatedByUserID],[LastModifiedDateTime],[LastModifiedByUserID]) VALUES (1, 0, 1,3,1,N'2021-04-02 12:49:45.760',1,N'2021-04-02 12:49:45.760',1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[ActivityNotification] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[EmailTemplatePlaceholders] ON ");
            migrationBuilder.Sql("INSERT [Notification].[EmailTemplatePlaceholders] ([PlaceholderID],[DisplayName],[Placeholder],[Description],[PlaceHolderTypeID],[CreatedDateTime],[CreatedByUserID],[LastModifiedDateTime],[LastModifiedByUserID],[IsActive]) 	VALUES (1, N'@Contact',N'Contact',N'Send Email to Contact',4,N'2021-04-02 15:46:41.080',1,N'2021-04-02 15:46:41.080',1,1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[EmailTemplatePlaceholders] OFF");
            
            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[NotificationRecipients] ON ");
                 migrationBuilder.Sql("INSERT [Notification].[NotificationRecipients] ([NotificationRecipientID],[ActivityNotificationID],[PlaceholderID],[IsTo],[IsCC],[IsActive],[CreatedDateTime],[CreatedByUserID],[LastModifiedDateTime],[LastModifiedByUserID]) VALUES ( 1,1,1,1,0,1, N'2021-04-02 12:49:45.760',1,N'2021-04-02 12:49:45.760',1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[NotificationRecipients] OFF");
 
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

             migrationBuilder.Sql("delete from [Master].[EmailTemplatePlaceholderType] where PlaceHolderTypeID between 1 and 5");
            migrationBuilder.Sql("delete from [Master].[NotificationStatus] where NotificationStatusID between 1 and 3");
            migrationBuilder.Sql("delete from [Master].[Notification] where NotificationID =1");
            
              migrationBuilder.Sql("delete from [Master].[WorkflowNotificationType] where WorkflowNotificationTypeID < 4");
            migrationBuilder.Sql("delete from [Notification].[ActivityNotification] where ActivityNotificationID =1");
            migrationBuilder.Sql("delete from [Notification].[EmailTemplatePlaceholders]  where PlaceholderID =1");
            migrationBuilder.Sql("delete from [Notification].[NotificationRecipients]  where NotificationRecipientID =1");

        }
    }
}
