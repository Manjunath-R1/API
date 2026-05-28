using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Service.Interfaces;
using ThoughtFocus.App.Utilities;
using Microsoft.AspNetCore.Http;
using ThoughtFocus.Domain.Request;
using System.IO;
using ThoughtFocus.DocumentManager.Impl.CleanUpService;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Linq;

namespace ThoughtFocus.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly ILogger<UploadController> _logger;
        private IDocumentService _documentServiceImpl;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CleanUpProcessingService _cleanUpProcessingService;
        public UploadController(IDocumentService documentServiceImpl,IHttpContextAccessor httpContextAccessor,
            ILogger<UploadController> logger, CleanUpProcessingService cleanUpProcessingService
            )
        {
            this._documentServiceImpl = documentServiceImpl;
            this._cleanUpProcessingService = cleanUpProcessingService;
              
            this._httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpPost]
        [Route("UploadApplicationDocument")]
        public async Task<DocumentResponse> UploadApplicationDocument(IFormFile file)
        {
            ApplicationDocumentEntity applicationDocumentEntity = new ApplicationDocumentEntity();
            DocumentResponse documentUploadResponse = new DocumentResponse();
            FileEntity fileEntity = new FileEntity();
            

            #region InputValidation

            #region FileInputStreamValidation
            if (file == null)
            {
                _logger.LogError("HttpRequest is null.");
                documentUploadResponse.IsSuccess = false;
                documentUploadResponse.Message = "File Upload failed.";
                return documentUploadResponse;
            }
            else if (file.OpenReadStream() == null)
            {
                _logger.LogError("Input Stream is missing - httpRequest.Files['file'].InputStream is null.");
                documentUploadResponse.IsSuccess = false;
                documentUploadResponse.Message = "File format is not correct.";
                return documentUploadResponse;
            }
            else if (String.IsNullOrEmpty(file.FileName))
            {
                _logger.LogError("FileName is missing fileName is Empty.");
                documentUploadResponse.IsSuccess = false;
                documentUploadResponse.Message = "No File found.";
                return documentUploadResponse;
            }
            #endregion


            #region DocumentTypeValidation
            
            if (file.Length == 0)
            {
                _logger.LogError("ContentLength is Empty.");
                documentUploadResponse.IsSuccess = false;
                documentUploadResponse.Message = "Unable to upload file at this moment. Try after sometime.";
                return documentUploadResponse;
            }
            #endregion

            #region FileSizeValidation
            #endregion

            #endregion

            try
            {
                if (file != null)
                {
                    fileEntity.FileName = file.FileName;
                    fileEntity.ContentType = file.ContentType;
                    fileEntity.ContentLength = file.Length;
                    fileEntity.InputStream = file.OpenReadStream();
                    fileEntity.FileSize = file.Length;
                    applicationDocumentEntity.FileEntity = fileEntity;
                }

                applicationDocumentEntity.UserID = 1;
                 applicationDocumentEntity.DocumentTypeID = 1;

                documentUploadResponse = await this._documentServiceImpl.UploadDocument(applicationDocumentEntity);
                return documentUploadResponse;

            }
            catch (Exception)
            {
                return new DocumentResponse
                {
                    IsSuccess = false,
                    Message = "Unable to upload file at this moment. Try after sometime."
                };
            }

        }

        [TypeFilter(typeof(AuthorizeAttribute))]
        [HttpGet]
        [Route("Download")]
        public async Task<FileStreamResult> Download(string storageKey)
        {
            FileStreamResult response = null;
            #region InputValidation
            if (string.IsNullOrEmpty(storageKey))
            {
                _logger.LogError("Document Identifier is Empty");
                
                return response;
            }
            #endregion

            var result = await  _documentServiceImpl.DownloadDocument(LoginUserInformation.getLoggedInUser(HttpContext), storageKey);
            if (!result.IsSuccess)
            {
                return response;
            }

            return File(result.Document.FileStream, "application/octet-stream", result.Document.FileName);  
        }

       
        [HttpPost]
        [Route("UploadLogo")]
        public async Task<DocumentResponse> UploadLogo(IFormFile file)
        {
            ApplicationDocumentEntity applicationDocumentEntity = new ApplicationDocumentEntity();
            DocumentResponse documentUploadResponse = new DocumentResponse();
            FileEntity fileEntity = new FileEntity();
            

            #region InputValidation

            #region FileInputStreamValidation
            if (file == null)
            {
                _logger.LogError("HttpRequest is null.");
                documentUploadResponse.IsSuccess = false;
                documentUploadResponse.Message = "File Upload failed.";
                return documentUploadResponse;
            }
            else if (file.OpenReadStream() == null)
            {
                _logger.LogError("Input Stream is missing - httpRequest.Files['file'].InputStream is null.");
                documentUploadResponse.IsSuccess = false;
                documentUploadResponse.Message = "File format is not correct.";
                return documentUploadResponse;
            }
            else if (String.IsNullOrEmpty(file.FileName))
            {
                _logger.LogError("FileName is missing fileName is Empty.");
                documentUploadResponse.IsSuccess = false;
                documentUploadResponse.Message = "No File found.";
                return documentUploadResponse;
            }
            #endregion


            #region DocumentTypeValidation
            
            if (file.Length == 0)
            {
                _logger.LogError("ContentLength is Empty.");
                documentUploadResponse.IsSuccess = false;
                documentUploadResponse.Message = "Unable to upload file at this moment. Try after sometime.";
                return documentUploadResponse;
            }
            #endregion

            #region FileSizeValidation
            #endregion

            #endregion

            try
            {
                if (file != null)
                {
                    fileEntity.FileName = file.FileName;
                    fileEntity.ContentType = file.ContentType;
                    fileEntity.ContentLength = file.Length;
                    fileEntity.InputStream = file.OpenReadStream();
                    fileEntity.FileSize = file.Length;
                    applicationDocumentEntity.FileEntity = fileEntity;
                }

                applicationDocumentEntity.UserID = 1;
                applicationDocumentEntity.DocumentTypeID = 0;

                documentUploadResponse = await this._documentServiceImpl.UploadDocument(applicationDocumentEntity);
                return documentUploadResponse;

            }
            catch (Exception)
            {
                return new DocumentResponse
                {
                    IsSuccess = false,
                    Message = "Unable to upload file at this moment. Try after sometime."
                };
            }

        }
        [HttpPost]
        [Route("CleanUpApplicationDocument")]
        public async Task<DocumentResponse> CleanUpApplicationDocument(IFormFile file)
        {

            using (var memoryStream = new MemoryStream())
            {
                try
                {
                    file.CopyToAsync(memoryStream);

                    // ✅ Ensure position is reset
                    memoryStream.Position = 0;
                    using (SpreadsheetDocument document = SpreadsheetDocument.Open(memoryStream, false))
                    {
                        WorkbookPart workbookPart = document.WorkbookPart;

                        // Get the first sheet in the workbook
                        Sheet sheet = workbookPart.Workbook.Sheets.Elements<Sheet>().FirstOrDefault();
                        if (sheet == null)
                        {
                            return new DocumentResponse { IsSuccess = false, Message = "No sheets found in the document" };
                        }
                        WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                        SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().FirstOrDefault();
                        if (sheetData != null)
                        {
                            Row headerRow = sheetData.Elements<Row>().FirstOrDefault();
                            if (headerRow == null)
                            {
                                return new DocumentResponse { IsSuccess = false, Message = "No headers found in the document" };
                            }
                            var fileHeaders = headerRow.Elements<Cell>().Select(c => GetCellValue(c, workbookPart)).ToList();

                            // Define your expected headers
                            var expectedHeaders = new[] { "Grant Number", "Date Applied", "Business Name", "Affiliate Name", "Grant Program Name", "Funds Allocated", "Disbursed Amount", "Application Status" };
                            bool isMatch = expectedHeaders.SequenceEqual(fileHeaders);
                            if (!isMatch)
                            {
                                return new DocumentResponse
                                {
                                    IsSuccess = false,
                                    Message = $"Header mismatch!\nExpected: {string.Join(", ", expectedHeaders)}\nGot: {string.Join(", ", fileHeaders)}"
                                };
                            }
                        }
                    }
                }
               catch(Exception ex)
                {
                    return new DocumentResponse
                    {
                        IsSuccess = false,
                        Message = $"Some thing went wrong"
                    };
                }
                var rslt = await _cleanUpProcessingService.ProcessExcelDataAsync(file);

                return new DocumentResponse
                {
                    IsSuccess = true,
                };
            }
        }
        private string GetCellValue(Cell cell, WorkbookPart workbookPart)
        {
            string value = cell.CellValue?.Text;

            if (cell.DataType != null && cell.DataType == CellValues.SharedString)
            {
                var sharedStringTablePart = workbookPart.SharedStringTablePart;
                value = sharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(int.Parse(value)).Text.Text;
            }

            return value;
        }
    }

}
