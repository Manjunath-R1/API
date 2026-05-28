using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Service.Interfaces;
using ThoughtFocus.RoleProvider.Interfaces;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Domain;
using Microsoft.Extensions.Logging;
using ThoughtFocus.CustomView;
using ThoughtFocus.App.ViewModels;
using Microsoft.AspNetCore.Authorization;
using ThoughtFocus.App.Utilities;
using ThoughtFocus.Domain.Master;
using System.Net;
using System.IO;

namespace ThoughtFocus.Web.Areas.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MasterController : ControllerBase
    {

        #region Fields

        private IListRole listRole;
        private IMasterService _masterService;
        private readonly ILogger<MasterController> _logger;

        #endregion Fields

        #region Constructor

        public MasterController(IListRole _listRole, IMasterService masterService,
        ILogger<MasterController> logger)
        {
            this.listRole = _listRole;
            _masterService = masterService;
             _logger = logger;
        }

        #endregion Constructor

        #region Methods

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("FetchGlobalRolePermissions")]
        public RolePermission FetchGlobalRolePermissions(GlobalRoleRequest request)
        {
            try
            {
                
                ThoughtFocus.Domain.User.UserSessionEntity userSession = LoginUserInformation.getLoggedInUser(HttpContext);                
               
                return new RolePermission
                {
                    IsSuccess = true,
                    RolePermissions = this._masterService.GetAccessControlRolePermissions(userSession.UserID)

                };
            }
            catch (Exception)
            {
                return new RolePermission
                {
                    IsSuccess = false,
                    Message = "Error while loading Data."
                };
            }
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetAllMasterEntity")]
        public IActionResult GetAllMasterEntity()
        {
            MasterDataEntity MasterDataEntity = new MasterDataEntity();
            try
            {
                MasterDataEntity = _masterService.GetAllMasterEntity();
                return Ok(MasterDataEntity);
            }
            catch (Exception)
            {
                return BadRequest("Failed to fetch master data");
            }
            
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("GetAgreementData")]
        public IActionResult GetAgreementData(long ApplicationId)
        {
            try
            {
                string agreementData = this._masterService.GetAgreementData(ApplicationId);
                          
                return Ok(agreementData);
            }
            catch (Exception)
            {
                return BadRequest("Failed to fetch Agreement Data");
            }

        }

        [HttpGet]
        [Route("GetMasterOption")]
        public IActionResult GetMasterOption(string category)
        {
            try
            {
                var masterOptionResponse = this._masterService.GetMasterOption(category);

                return Ok(masterOptionResponse);
            }
            catch (Exception)
            {
                return BadRequest("Failed to fetch Master Option Data");
            }

        }       
        [HttpGet]
        [Route("GetSPAData")]
        public IActionResult GetSPAData(long ApplicationId)
        {
            try
            {
                var agreementData = this._masterService.GetSPAData(ApplicationId);
                var webClient = new WebClient();
                byte[] files = webClient.DownloadData(agreementData.FileUploadedSourceUrl);               
                var url= new Uri(agreementData.FileUploadedSourceUrl);              
                string fileType = System.IO.Path.GetExtension(url.LocalPath);

                MemoryStream ms = new MemoryStream(files);             


                if (files != null)
                {
                    if (fileType == ".pdf")
                    {
                        //return File(files, ".pdf");
                        return new FileStreamResult(ms, "application/pdf");
                    }
                    else if (fileType == ".doc")
                    {
                        return new FileStreamResult(ms, "application/msword");
                    }
                    else if (fileType == ".docx")
                    {
                        return new FileStreamResult(ms, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
                    }
                    else if (fileType == ".xls")
                    {
                        return new FileStreamResult(ms, "application/vnd.ms-excel");
                    }
                    else if (fileType == ".xlsx")
                    {
                        return new FileStreamResult(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    }
                    else if(fileType == ".jpeg" || fileType == ".jpg")
                    {
                        return new FileStreamResult(ms, "image/jpeg");
                        //return File(files, "image/jpg");

                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return BadRequest("Failed to fetch SPA Data");
            }

        }

        [HttpGet]
        [Route("GetSPADocument")]
        public IActionResult GetSPADocument(long ApplicationId)
        {
            try
            {
                var agreementData = this._masterService.GetSPAData(ApplicationId);

                var agrementDocument = new PaymentScheduleSummaryDocument();

                agrementDocument.DocumentID = agreementData.DocumentID;
                agrementDocument.DocumentGUID = agreementData.DocumentGUID.ToString();
                agrementDocument.DocumentTypeID = agreementData.DocumentTypeID;
                agrementDocument.DocumentName = agreementData.DocumentName;
                agrementDocument.FileName = agreementData.FileName;
                agrementDocument.PhysicalFileStorageKey = agreementData.PhysicalFileStorageKey;
                agrementDocument.FileSize = agreementData.FileSize;
                agrementDocument.FileUploadedSourceUrl = agreementData.FileUploadedSourceUrl;
              
                return Ok(agrementDocument);                
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to fetch SPA Data");
            }       

        }

        [HttpGet]
        [Route("GetThresholdRequestAmount")]
        public IActionResult GetThresholdRequestAmount()
        {
            try
            {
                var masterOptionResponse = this._masterService.GetThresholdRequestAmount();

                return Ok(masterOptionResponse);
            }
            catch (Exception)
            {
                return BadRequest("Failed to fetch Get Threshold Request Amount Data");
            }

        }
        #endregion Methods

    }
}
