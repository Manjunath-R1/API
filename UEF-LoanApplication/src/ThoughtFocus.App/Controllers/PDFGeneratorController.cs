
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 
using ThoughtFocus.DataAccess.Models;
using Microsoft.Extensions.Options; 
using Microsoft.AspNetCore.Cors; 
using System;
using Api2PdfLibrary;
using System.Collections.Generic;
using System.Text;
using ThoughtFocus.Service.Interfaces;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Repository.Interfaces.FundingSource;
using System.Linq;
using ThoughtFocus.Repository.Interfaces.Application;

namespace ThoughtFocus.App.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PDFGeneratorController : ControllerBase
    {
        #region Fields
        private readonly ILogger<ContactController> _logger;
        private readonly AppSettings _appSettings;

        private readonly IMasterService _masterService;
        private readonly IFundingSourceRepository _fundingSourceRepository;
        private readonly ILoanApplicationRepository _LoanApplicationRepository;
        #endregion Fields
        #region Constructors
        public PDFGeneratorController(ILogger<ContactController> logger, IOptions<AppSettings> appSettings,IMasterService masterService, IFundingSourceRepository fundingSourceRepository, ILoanApplicationRepository loanApplicationRepository)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            _masterService = masterService;
            this._fundingSourceRepository = fundingSourceRepository;
            this._LoanApplicationRepository = loanApplicationRepository;
    }
        #endregion Constructors       

        [HttpGet("ApplicationSummary")]
        [EnableCors("AllowAllOrigins")]
        public IActionResult ApplicationSummary(long applicationID)
        {
            try
            {
                if (applicationID > 0)
                {
                    string PdfContentUrl = _appSettings.BaseUrl + "/summarypage/" + applicationID;
 
                    //Page Setting
                    Dictionary<string, string> PrintOptions = new Dictionary<string, string>();

                    //Footer settings
                    PrintOptions.Add("displayHeaderFooter", "true");
                    //PrintOptions.Add("headerTemplate ", "<div style=\"font-size:12px;\">Header Content Here</div>");

                            PrintOptions.Add("footerTemplate", "<div class=\"page-footer\" style=\"width:100%; padding-left: 60px; text-align:left;font-family: Verdana; font-size:8px;\">Page <span class=\"pageNumber\"></span> of <span class=\"totalPages\"></span></div><div class=\"page-footer\" style=\"width:100%; text-align:right; font-size:8px;font-family: Verdana;display: inline; padding-right: 60px;\"> ©" + DateTime.Now.Year.ToString() + " National Urban League");

                    //Page margin settings 
                   PrintOptions.Add("marginBottom", "0.75");
                    PrintOptions.Add("marginLeft", "0.75");
                    PrintOptions.Add("marginRight", "0.75");
                    PrintOptions.Add("marginTop", "0.75");
                    PrintOptions.Add("paperWidth", "8.5");
                    PrintOptions.Add("paperHeight", "11");

                    //Delay to load all the images
                    PrintOptions.Add("delay", "3000");

                    PrintOptions.Add("printBackground", "true");

                    Api2Pdf a2pClient;
                    //Assign API key which is defined in appSetting                
                    a2pClient = new Api2Pdf(_appSettings.Api2PDFKey);
                    //API Call              
                    var pdf = a2pClient.HeadlessChrome.FromUrl(PdfContentUrl, true, "ApplicationSummary", PrintOptions);
                    return File(pdf.GetPdfBytes(), "application/pdf;");
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                return BadRequest();
            }

        }

   
        [HttpGet]
        [Route("GetAgreement")]
        public IActionResult GetAgreement(long ApplicationId)
        {
            try
            {
                if (ApplicationId > 0)
                {
                    string AgreementData = this._masterService.GetAgreementData(ApplicationId);
                    
                    string AgreementTemplateText = this._masterService.GetAgreementText(AgreementData);

                    //Page Setting
                    Dictionary<string, string> PrintOptions = new Dictionary<string, string>();

                    //Footer settings
                    PrintOptions.Add("displayHeaderFooter", "true");

                    PrintOptions.Add("footerTemplate", "<div class=\"page-footer\" style=\"width:100%; padding-left: 60px; text-align:left;font-family: Verdana; font-size:8px;\">Page <span class=\"pageNumber\"></span> of <span class=\"totalPages\"></span></div><div class=\"page-footer\" style=\"width:100%; text-align:right; font-size:8px;font-family: Verdana;display: inline; padding-right: 60px;\"> ©" + DateTime.Now.Year.ToString() + " National Urban League");

                    //Page margin settings 
                    PrintOptions.Add("marginBottom", "0.75");
                    PrintOptions.Add("marginLeft", "0.75");
                    PrintOptions.Add("marginRight", "0.75");
                    PrintOptions.Add("marginTop", "0.75");
                    PrintOptions.Add("paperWidth", "8.5");
                    PrintOptions.Add("paperHeight", "11");

                    //Delay to load all the images
                    PrintOptions.Add("delay", "3000");

                    PrintOptions.Add("printBackground", "true");

                    Api2Pdf a2pClient;
                    //Assign API key which is defined in appSetting                
                    a2pClient = new Api2Pdf(_appSettings.Api2PDFKey);
                    //API Call              
                    var pdf = a2pClient.HeadlessChrome.FromHtml(AgreementTemplateText, true, "GetAgreement", PrintOptions);
                    return File(pdf.GetPdfBytes(), "application/pdf;");
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("GetPaymentScheduleTransactionAgreement")]
        [EnableCors("AllowAllOrigins")]
        public IActionResult GetPaymentScheduleTransactionAgreement(long applicationId)
        {
            try
            {
                if (applicationId > 0)
                {
                   
                    string AgreementData = this._masterService.GetSPAHTMLText(applicationId);
                    string AgreementTemplateText = this._masterService.GetAgreementText(AgreementData);
                    //Page Setting
                    Dictionary<string, string> PrintOptions = new Dictionary<string, string>();

                    //Footer settings
                    PrintOptions.Add("displayHeaderFooter", "true");
                 //   PrintOptions.Add("headerTemplate ", "<div class=\"page-header\" style=\"width:100%; padding-left: 60px; text-align:left;font-family: Verdana; font-size:8px;\">Page <span class=\"pageNumber\"></span> of <span class=\"totalPages\"></span></div><div class=\"page-footer\" style=\"width:100%; text-align:right; font-size:8px;font-family: Verdana;display: inline; padding-right: 60px;\"> ©" + DateTime.Now.Year.ToString() + " National Urban League");

                    PrintOptions.Add("footerTemplate", "<div class=\"page-footer\" style=\"width:100%; padding-left: 60px; text-align:left;font-family: Verdana; font-size:8px;\">Page <span class=\"pageNumber\"></span> of <span class=\"totalPages\"></span></div><div class=\"page-footer\" style=\"width:100%; text-align:right; font-size:8px;font-family: Verdana;display: inline; padding-right: 60px;\"> ©" + DateTime.Now.Year.ToString() + " National Urban League");

                    //Page margin settings 
                    PrintOptions.Add("marginBottom", "0.75");
                    PrintOptions.Add("marginLeft", "0.75");
                    PrintOptions.Add("marginRight", "0.75");
                    PrintOptions.Add("marginTop", "0.75");
                    PrintOptions.Add("paperWidth", "8.5");
                    PrintOptions.Add("paperHeight", "11");

                    //Delay to load all the images
                    PrintOptions.Add("delay", "3000");

                    PrintOptions.Add("printBackground", "true");

                    Api2Pdf a2pClient;
                    //Assign API key which is defined in appSetting                
                    a2pClient = new Api2Pdf(_appSettings.Api2PDFKey);
                    //API Call              
                    var pdf = a2pClient.HeadlessChrome.FromHtml(AgreementTemplateText, true, "GetPaymentScheduleTransactionAgreement", PrintOptions);
                    return File(pdf.GetPdfBytes(), "application/pdf;");
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                return BadRequest();
            }
        }


    }
}