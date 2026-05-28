using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class add_seeding_resetPassword_email_template : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Notification] ON ");
            migrationBuilder.Sql("INSERT [Master].[Notification] ([NotificationID], [NotificationType], [EmailFormat],[MessageSubject], [TemplateName], [NotificationTypeDescription],[Head],[Salutation],[Body],[Footer],[RecipientType],[IsActive]) VALUES (17,  N'RESETBUSINESSCONTACT', N'EMAIL-HTML',N'Password Reset Access',N'RESETBUSINESSCONTACT',N'Reset BusinessContact EMAIL', N'<!DOCTYPE HTML PUBLIC ''-//W3C//DTD XHTML 1.0 Transitional//EN'' ''http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd''> <html xmlns=''http://www.w3.org/1999/xhtml''> <head> <meta http-equiv=''Content-Type'' content=''text/html;'' charset=''UTF-8'' /> <title>Notification</title> <style type=''text/css''> body { margin: 0; padding: 0; min-width: 100% !important; } img { height: auto; margin-right: 1rem;} .content { width: 100%; max-width: 600px; } .header { padding: 40px 30px 20px 30px; } .innerpadding { padding: 30px 30px 30px 30px; } .borderbottom { border-bottom: 1px solid #999999; } .hrcolor { border-bottom: 1px solid #4cff00; } .borderrightbottomleft { border-bottom: 1px solid #44525f; border-right: 1px solid #44525f; border-left: 1px solid #44525f; } .subhead { font-size: 15px; color: #ffffff; font-family: sans-serif; letter-spacing: 10px; } .h1, .h2, .bodycopy { color: #153643; font-family: sans-serif; } .h1 { font-size: 33px; line-height: 38px; font-weight: bold; } .h2 { padding: 0 0 15px 0; font-size: 24px; line-height: 28px; font-weight: bold; } .bodycopy { font-size: 14px; line-height: 22px; } .button { text-align: center; font-size: 18px; font-family: sans-serif; font-weight: bold; padding: 0 30px 0 30px; } .button a { color: #ffffff; text-decoration: none; } .Emailfooter { padding: 20px 30px 15px 30px; background-color:#44525f; } .footercopy { font-family: sans-serif; font-size: 14px; color: #ffffff; } .footercopy a { color: #ffffff; text-decoration: underline; } .btn-primary a:hover { background-color: #34495e !important; border-color: #34495e !important; } @@media only screen and (max-width: 550px), screen and (max-device-width: 550px) { body[yahoo] .hide { display: none !important; } body[yahoo] .buttonwrapper { background-color: transparent !important; } body[yahoo] .button { padding: 0px !important; } body[yahoo] .button a { background-color: #e05443; padding: 15px 15px 13px !important; } body[yahoo] .unsubscribe { display: block; margin-top: 20px; padding: 10px 50px; background: #2f3942; border-radius: 5px; text-decoration: none !important; font-weight: bold; } } </style> </head> <body yahoo bgcolor=''#ffffff''> <table width=''100%'' bgcolor=''#ffffff'' border=''0'' cellpadding=''0'' cellspacing=''0''> <tr> <td> <table bgcolor=''#ffffff'' class=''content'' align=''center'' cellpadding=''0'' cellspacing=''0'' border=''0''> <tr> <td class=''header''> <table> <tr><td><img class=''fix'' src=''[[NUL_Standalone_Logo]]'' width=''120'' height=''30'' border=''0'' alt='''' /></td><td style=''padding-right: 60px''> <img class=''fix'' src=''[[Urban_Empowerment_Fund_Logo]]'' width=''120'' height=''30'' border=''0'' alt=''''/></td></tr> </table> </td> </tr> </table> <table bgcolor=''#ffffff'' class=''content'' align=''center'' cellpadding=''0'' cellspacing=''0'' border=''0''> <tr> <td class=''innerpadding borderbottom''>',N'<table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''> <tr> <td class=''bodycopy''> Dear @RecipientFullName, <br /><br /> </td> </tr> </table>',N'<table width=''100%'' border=''0'' cellspacing=''10'' cellpadding=''0''><tr> <td class=''bodycopy''> Admin has initiated request to reset the access for this account. <br /> Please click on the button below to proceed<br /><br /></td></tr><tr>@CallBackURL <br /><br /></tr><tr><td class=''bodycopy''>Sincerely,<br />NUL</td></tr>',N'<td class=''Emailfooter''> <table width=''100%'' border=''0'' cellspacing=''0'' cellpadding=''0''> <tr> <td align=''center'' style=''background:#44525f;padding:15.0pt 22.5pt 11.25pt 22.5pt;color:#ffffff'' class=''footercopy''>© @CurrentYear, NUL. All rights reserved.<br /></td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </body> </html>','',1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Master].[Notification] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[ActivityNotification] ON ");
            migrationBuilder.Sql("INSERT [Notification].[ActivityNotification] ([ActivityNotificationID], [ActivityID], [NotificationID],[WorkflowNotificationTypeID],[IsActive],[CreatedDateTime],[CreatedByUserID],[LastModifiedDateTime],[LastModifiedByUserID]) VALUES (28, 0, 17,3,1,N'2021-04-02 12:49:45.760',1,N'2021-04-02 12:49:45.760',1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[ActivityNotification] OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[NotificationRecipients] ON ");
            migrationBuilder.Sql("INSERT [Notification].[NotificationRecipients] ([NotificationRecipientID],[ActivityNotificationID],[PlaceholderID],[IsTo],[IsCC],[IsActive],[CreatedDateTime],[CreatedByUserID],[LastModifiedDateTime],[LastModifiedByUserID]) VALUES ( 59,28,1,1,0,1, N'2021-04-02 12:49:45.760',1,N'2021-04-02 12:49:45.760',1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Notification].[NotificationRecipients] OFF");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("delete from [Notification].[NotificationRecipients]  where NotificationRecipientID =59");
            migrationBuilder.Sql("delete from [Notification].[ActivityNotification] where ActivityNotificationID =28");
            migrationBuilder.Sql("delete from [Master].[Notification] where NotificationID =17");


        }
    }
}
