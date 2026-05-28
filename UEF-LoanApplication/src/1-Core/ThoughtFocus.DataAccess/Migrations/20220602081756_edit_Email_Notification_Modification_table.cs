using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class edit_Email_Notification_Modification_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE [Master].[Notification] SET [Head] =N'<!DOCTYPE HTML PUBLIC ''-//W3C//DTD XHTML 1.0 Transitional//EN'' ''http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd''> <html xmlns=''http://www.w3.org/1999/xhtml''> <head> <meta http-equiv=''Content-Type'' content=''text/html;'' charset=''UTF-8'' /> <title>Notification</title> <style type=''text/css''> body { margin: 0; padding: 0; min-width: 100% !important; } img { height: auto; margin-right: 1rem;} .content { width: 100%; max-width: 600px; } .header { padding: 40px 30px 20px 30px; } .innerpadding { padding: 30px 30px 30px 30px; } .borderbottom { border-bottom: 1px solid #999999; } .hrcolor { border-bottom: 1px solid #4cff00; } .borderrightbottomleft { border-bottom: 1px solid #44525f; border-right: 1px solid #44525f; border-left: 1px solid #44525f; } .subhead { font-size: 15px; color: #ffffff; font-family: sans-serif; letter-spacing: 10px; } .h1, .h2, .bodycopy { color: #153643; font-family: sans-serif; } .h1 { font-size: 33px; line-height: 38px; font-weight: bold; } .h2 { padding: 0 0 15px 0; font-size: 24px; line-height: 28px; font-weight: bold; } .bodycopy { font-size: 14px; line-height: 22px; } .button { text-align: center; font-size: 18px; font-family: sans-serif; font-weight: bold; padding: 0 30px 0 30px; } .button a { color: #ffffff; text-decoration: none; } .Emailfooter { padding: 20px 30px 15px 30px; background-color:#44525f; } .footercopy { font-family: sans-serif; font-size: 14px; color: #ffffff; } .footercopy a { color: #ffffff; text-decoration: underline; } .btn-primary a:hover { background-color: #34495e !important; border-color: #34495e !important; } @@media only screen and (max-width: 550px), screen and (max-device-width: 550px) { body[yahoo] .hide { display: none !important; } body[yahoo] .buttonwrapper { background-color: transparent !important; } body[yahoo] .button { padding: 0px !important; } body[yahoo] .button a { background-color: #e05443; padding: 15px 15px 13px !important; } body[yahoo] .unsubscribe { display: block; margin-top: 20px; padding: 10px 50px; background: #2f3942; border-radius: 5px; text-decoration: none !important; font-weight: bold; } } </style> </head> <body yahoo bgcolor=''#ffffff''> <table width=''100%'' bgcolor=''#ffffff'' border=''0'' cellpadding=''0'' cellspacing=''0''> <tr> <td> <table bgcolor=''#ffffff'' class=''content'' align=''center'' cellpadding=''0'' cellspacing=''0'' border=''0''> <tr> <td class=''header''> <table> <tr><td><img class=''fix'' src=''[[NUL_Standalone_Logo]]'' width=''120'' height=''30'' border=''0'' alt='''' /></td><td style=''padding-right: 60px''> <img class=''fix'' src=''[[Urban_Empowerment_Fund_Logo]]'' width=''120'' height=''30'' border=''0'' alt=''''/></td></tr> </table> </td> </tr> </table> <table bgcolor=''#ffffff'' class=''content'' align=''center'' cellpadding=''0'' cellspacing=''0'' border=''0''> <tr> <td class=''innerpadding borderbottom''>' WHERE [NotificationID] = 22");
            
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
