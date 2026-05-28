using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThoughtFocus.DataAccess.Models;
using ThoughtFocus.DocumentManager.Interfaces;
using ThoughtFocus.Domain;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.User;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.Service.Interfaces;
using ThoughtFocus.Common.Exceptions.BusinessException;
using ThoughtFocus.AccrediO.Accreditation.Domain.Response;
using AutoMapper;

namespace ThoughtFocus.Service.Impl
{
    public class DocumentServiceImpl : IDocumentService
    {
        #region Fields
        private  readonly ILogger<DocumentServiceImpl> _logger;

        private IApplicationDocumentFacade _applicationDocumentFacade;
        private IFileManager _fileManager;
        private IDocumentUploadManager _documentUploadManager;
        //private readonly IMapper _mapper;

        private IDocFileUploader _docFileUploader;

        #endregion Fields

        #region Constructors

        public DocumentServiceImpl(IApplicationDocumentFacade applicationDocumentFacade,
            IFileManager fileManager, IDocumentUploadManager documentUploadManager,
            IDocFileUploader docFileUploader,
            ILogger<DocumentServiceImpl> logger)
        {
            _applicationDocumentFacade = applicationDocumentFacade;
            _fileManager = fileManager;
            _documentUploadManager = documentUploadManager;
            _docFileUploader = docFileUploader;
            _logger = logger;
        }
        #endregion Constructors

        #region Methods

        public ApplicationDocumentResponse GetApplicationDocumentByVersionID(string documentVersionkey, UserSessionEntity userSessionEntity)
        {
            ApplicationDocumentResponse applicationDocumentResponse = new ApplicationDocumentResponse
            ();
            try
            {
                applicationDocumentResponse = this._applicationDocumentFacade.GetApplicationDocumentByVersionID(documentVersionkey, userSessionEntity);
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_logger,null, ex);
                applicationDocumentResponse.IsSuccess = false;
                applicationDocumentResponse.Message = "Unable to download document. Please try after sometime.";

            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_logger,null, ex);
                applicationDocumentResponse.IsSuccess = false;
                applicationDocumentResponse.Message = "Unable to download document. Please try after sometime.";
            }
            return applicationDocumentResponse;
        }

        public ApplicationDocumentResponse DeleteApplicationDocument(ApplicationDocumentRequest request)
        {
            return null;//this._applicationDocumentFacade.DeleteApplicationDocument(request);
        }

        public ApplicationDocumentResponse DeleteApplicationDocumentVersion(ApplicationDocumentRequest request)
        {
            return null;//this._applicationDocumentFacade.DeleteApplicationDocumentVersion(request);
        }

        public DocumentResponse GetDocumentIDByTypeAndApplication(long documentTypeID, long applicationID)
        {
            var baseResponse = new DocumentResponse();
            try
            {
                baseResponse = this._applicationDocumentFacade.GetDocumentIDByTypeAndApplication(documentTypeID, applicationID);
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_logger, null, ex);
                baseResponse.IsSuccess = false;
            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_logger, null, ex);
                baseResponse.IsSuccess = false;
            }
            return baseResponse;
        }

                       
        public async Task<DocumentResponse> UploadDocument(DocumentEntity documentEntity)
        {
            DocumentResponse uploadResponse = new DocumentResponse();

            #region Validation

            #region InputNullValidation

            if (documentEntity == null)
            {
                _logger.LogError("documentEntity is null");
                uploadResponse.IsSuccess = false;
                uploadResponse.Message = "Unable to upload document. Please try after sometime.";
                return uploadResponse;

            }
            if (documentEntity.FileEntity == null)
            {
                _logger.LogError("FileEntity is null");
                uploadResponse.IsSuccess = false;
                uploadResponse.Message = "Unable to upload document. Please try after sometime.";
                return uploadResponse;
            }
            #endregion

            #endregion

            try
            {
                
                uploadResponse = await this._documentUploadManager.UploadDocument(documentEntity);

            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_logger, null, ex);
                uploadResponse.IsSuccess = false;
                uploadResponse.Message = "Unable to upload document. Please try after sometime.";

            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_logger, null, ex);
                uploadResponse.IsSuccess = false;
                uploadResponse.Message = "Unable to upload document. Please try after sometime.";
            }
            return uploadResponse;
        }

        public FileExtensionResponse GetAllDocumentExtension()
        {
            FileExtensionResponse fileExtensionResponse = new FileExtensionResponse();
            try
            {
                fileExtensionResponse = this._fileManager.GetDocumentExtensions();
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_logger, null, ex);
                fileExtensionResponse.IsSuccess = false;
                fileExtensionResponse.Message = "Unable to fetch document extensions at this moment. Please contact administrator.";

            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_logger, null, ex);
                fileExtensionResponse.IsSuccess = false;
                fileExtensionResponse.Message = "Unable to fetch document extensions at this moment. Please contact administrator.";
            }
            return fileExtensionResponse;
        }

        public DocumentResponse GetDocumentByID(Guid documentID, UserSessionEntity userSessionEntity)
        {
            DocumentResponse documentresponse = new DocumentResponse();

            #region Validation
            if (documentID == null)
            {
                _logger.LogError("Input Parameter documentID is 0");
                documentresponse.IsSuccess = false;
                documentresponse.Message = "Unable to fetch document at this moment. Please contact administrator.";
                return documentresponse;
            }
            if (userSessionEntity == null)
            {
                _logger.LogError("Input Parameter userSessionEntity is null");
                documentresponse.IsSuccess = false;
                documentresponse.Message = "Unable to fetch document at this moment. Please contact administrator.";
                return documentresponse;
            }
            if (userSessionEntity.UserID == 0)
            {
                _logger.LogError("Input Parameter UserID is 0");
                documentresponse.IsSuccess = false;
                documentresponse.Message = "Unable to fetch document at this moment. Please contact administrator.";
                return documentresponse;
            }

            #endregion Validation
            try
            {
                documentresponse = this._fileManager.GetDocumentInfo(documentID, userSessionEntity.UserID);
            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_logger, null, ex);
                documentresponse.IsSuccess = false;
                documentresponse.Message = "Unable to fetch document  at this moment. Please contact administrator.";

            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_logger, null, ex);
                documentresponse.IsSuccess = false;
                documentresponse.Message = "Unable to fetch document  at this moment. Please contact administrator.";
            }
            return documentresponse;
        }

        public async Task<DocumentResponse> DownloadDocument(UserSessionEntity userSessionEntity, string storageKey)
        {
            try
            {
                return await _docFileUploader.DownloadDocument(storageKey, userSessionEntity);
            }
            catch (BusinessException ex)
            {
                _logger.LogError("Exception in DownloadDocument", ex);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in DownloadDocument", ex);
                return null;
            }
        }
        public async Task<DocumentResponse> CleanUpApplicationDocument(DocumentEntity documentEntity)
        {
            DocumentResponse uploadResponse = new DocumentResponse();

            #region Validation

            #region InputNullValidation

            if (documentEntity == null)
            {
                _logger.LogError("documentEntity is null");
                uploadResponse.IsSuccess = false;
                uploadResponse.Message = "Unable to upload document. Please try after sometime.";
                return uploadResponse;

            }
            if (documentEntity.FileEntity == null)
            {
                _logger.LogError("FileEntity is null");
                uploadResponse.IsSuccess = false;
                uploadResponse.Message = "Unable to upload document. Please try after sometime.";
                return uploadResponse;
            }
            #endregion

            #endregion

            try
            {

                uploadResponse = await this._documentUploadManager.UploadDocument(documentEntity);

            }
            catch (BusinessException ex)
            {
                LoggerExtensions.LogInformation(_logger, null, ex);
                uploadResponse.IsSuccess = false;
                uploadResponse.Message = "Unable to upload document. Please try after sometime.";

            }
            catch (Exception ex)
            {
                LoggerExtensions.LogInformation(_logger, null, ex);
                uploadResponse.IsSuccess = false;
                uploadResponse.Message = "Unable to upload document. Please try after sometime.";
            }
            return uploadResponse;
        }

        #endregion

    }
}
