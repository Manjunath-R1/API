using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using ThoughtFocus.DocumentManager.Interfaces;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DocumentRepository.Core.Interfaces;
using ThoughtFocus.Repository.Interfaces.Master;
using ThoughtFocus.DataAccess.Models;
using ThoughtFocus.Domain;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Common.Exceptions.BusinessException;
using ThoughtFocus.Repository.Interfaces.Application;
using ThoughtFocus.DocumentRepository.Repository.Core;

namespace ThoughtFocus.DocumentManager.Impl
{
    public class FileManager : IFileManager
    {
        private readonly ILogger<FileManager> _logger;
        private IDocumentInformationProvider _documentInformationProvider;
        private IDocumentTypeRepository _documentTypeRepository;
        private IProjectManager _projectManager;
        private IProjectRepository _projectRepository;

        private ILoanApplicationRepository _loanApplicationRepository;

        public FileManager(IDocumentInformationProvider documentInformationProvider,
            IDocumentTypeRepository documentTypeRepository,
            IProjectManager projectManager, 
            ILoanApplicationRepository loanApplicationRepository,
            IProjectRepository projectRepository,
            ILogger<FileManager> logger)
        {
            _documentInformationProvider = documentInformationProvider;
            _documentTypeRepository = documentTypeRepository;
            _projectManager = projectManager;
            _loanApplicationRepository = loanApplicationRepository;
            _projectRepository = projectRepository;
            _logger = logger;

        }

        public Guid FetchFolderID(Domain.CustomView.DocumentEntity documentEntity)
        {
            Guid folderID = default(Guid);
            try
            {
                if (documentEntity.DocumentTypeID != 0)
                {
                    var project = _projectManager.GetProjectByDocumentTypeID(documentEntity.DocumentTypeID, documentEntity.ProgramInvitationID, documentEntity.DocumentEntityType.ToString());

                    if (project != null)
                    {
                        folderID = project.ProjectID;
                    }
                    else
                    {
                        if (documentEntity.DocumentEntityType == Domain.Enumeration.DocumentEntityTypeEnumeration.ApplicationDocument)
                        {
                            var parentProject = _projectManager.GetParentProjectByName(Domain.Enumeration.DocumentEntityTypeEnumeration.ApplicationDocument.ToString());
                            ///---Add once the Repository Method for ProgramInvitation is done----///
                            //var loanApplicationDetails = _loanApplicationRepository.GetLoanApplicationByApplicationID(documentEntity.LoanApplicationID);

                            var request = new DocumentRepository.Domain.ProjectRequest { Name = "BusinessPlan", ID = documentEntity.ProgramInvitationID, ParentId = parentProject.ProjectID, UserID = 1 };

                            var responseTask = _projectManager.CreateProjectAsync(request);

                            if (responseTask.Result.IsSuccess)
                            {
                                var documentTypeDetails = _documentTypeRepository.GetDocumentTypeByID(documentEntity.DocumentTypeID);
                                if (documentTypeDetails != null)
                                {
                                    var documentTypeRequest = new DocumentRepository.Domain.ProjectRequest { Name = documentTypeDetails.Name, ID = documentEntity.DocumentTypeID,ParentId = responseTask.Result.ProjectID, UserID = 1 };

                                    var documentTypeResponseTask = _projectManager.CreateProjectAsync(documentTypeRequest);
                                    folderID = documentTypeResponseTask.Result.ProjectID;
                                }
                            }

                        }
                    }
                    if (folderID == Guid.Empty)
                    {
                        var workSpaceDetails = _projectManager.GetParentProjectByName(documentEntity.DocumentEntityType.ToString());
                        var documentTypeDetails = _documentTypeRepository.GetDocumentTypeByID(documentEntity.DocumentTypeID);
                        if (workSpaceDetails.ProjectID != Guid.Empty && documentTypeDetails !=null && workSpaceDetails !=null)
                        {
                            var parentProjects = _projectRepository.GetProjectsByParentIdForDirectoryKey(workSpaceDetails.ProjectID).ToList();
                            var parentProject = parentProjects.Where(x => x.ID == documentEntity.ProgramInvitationID).FirstOrDefault();
                            
                            if (parentProject != null)
                            {
                                var documentTypeRequest = new DocumentRepository.Domain.ProjectRequest { Name = documentTypeDetails.Name, ID = documentEntity.DocumentTypeID, ParentId = parentProject.ProjectID, UserID = 1 };
                                var documentTypeResponseTask = _projectManager.CreateProjectAsync(documentTypeRequest);
                                folderID = documentTypeResponseTask.Result.ProjectID;
                            }
                        }
                    }

                }
            }
            catch (BusinessException ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message, ex);

            }
            return folderID;
        }

        public DocumentResponse GetDocumentInfo(Guid documentID, long userID)
        {
            DocumentResponse documentResponse = new DocumentResponse();
            DocumentViewModel documentViewModel = new DocumentViewModel();
            DocumentRepository.Domain.DocumentEntity document = this._documentInformationProvider.GetDocumentByID(documentID);
            var DocumentVersionHistory = this._documentInformationProvider.GetDocumentVersionHistoryById(documentID);

            if (document != null)
            {
                documentViewModel.FileName = document.Name;
                documentViewModel.DocumentID = document.DocumentID;
                documentViewModel.CreatedDateTime = document.CreatedDateTime;
                documentViewModel.DocumentVersionkey = DocumentVersionHistory.Where(a => a.DocumentID == document.DocumentID) == null ? String.Empty : (DocumentVersionHistory.Where(a => a.DocumentID == document.DocumentID).OrderByDescending(a => a.CreatedDateTime).FirstOrDefault() == null ? String.Empty : DocumentVersionHistory.Where(a => a.DocumentID == document.DocumentID).OrderByDescending(a => a.CreatedDateTime).FirstOrDefault().VersionSalt);
                documentViewModel.VersionNumber = String.Format("Ver_{0}.{1}", document.MajorVersion, document.MinorVersion);
                documentViewModel.UploadDate = document.LastUploadedDate == null ? document.LastModifiedDateTime.ToString("MM/dd/yyyy") : document.LastUploadedDate.Value.ToString("MM/dd/yyyy");
                documentViewModel.DocumentKey = document.Key;
                documentViewModel.DocumentVersionID = DocumentVersionHistory.Where(a => a.DocumentID == document.DocumentID) == null ? Guid.Empty : (DocumentVersionHistory.Where(a => a.DocumentID == document.DocumentID).OrderByDescending(a => a.CreatedDateTime).FirstOrDefault() == null ? Guid.Empty : DocumentVersionHistory.Where(a => a.DocumentID == document.DocumentID).OrderByDescending(a => a.CreatedDateTime).FirstOrDefault().DocumentVersionHistoryID);
                documentViewModel.FileImagePath = this._documentInformationProvider.GetExtensionImagePath(document.FileExtensionTypeID);
                documentViewModel.IsLocked = document.IsLocked;
                documentViewModel.LockedByUserID = document.LockedByUserID == null ? 0 : document.LockedByUserID.Value;
                documentViewModel.LoggedInUserID = userID;
                documentViewModel.FileSize = document.FileSize == null ? 0 : document.FileSize.Value;

                documentResponse.IsSuccess = true;
                documentResponse.Document = documentViewModel;
            }
            else
            {
                documentResponse.IsSuccess = false;
                documentResponse.Message = "Unable to fetch document at this moment. Please contact administrator.";
            }
            return documentResponse;
        }

        public FileExtensionResponse GetDocumentExtensions()
        {
            FileExtensionResponse fileExtensionResponse = new FileExtensionResponse();
            List<FileExtensionModel> extensionModels = new List<FileExtensionModel>();
            try
            {
                List<DocumentRepository.Domain.FileExtensionEntity> fileEntensionCategories = this._documentInformationProvider.GetDocumentExtension();
                if (fileEntensionCategories != null)
                {
                    foreach (var extension in fileEntensionCategories)
                    {
                        FileExtensionModel extensionModel = new FileExtensionModel();
                        extensionModel.Value = extension.Description;
                        extensionModel.Key = extension.FileExtensionCategoryID;
                        extensionModel.Selected = false;
                        extensionModels.Add(extensionModel);
                    }
                    fileExtensionResponse.IsSuccess = true;
                    fileExtensionResponse.FileExtensions = extensionModels;
                }
                else
                {
                    fileExtensionResponse.IsSuccess = false;
                    fileExtensionResponse.Message = "Unable to fetch document extensions at this moment. Please contact administrator.";
                    return fileExtensionResponse;
                }
            }
            catch (BusinessException ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message, ex);

            }
            return fileExtensionResponse;
        }

        public string GetNextDocumentNumber()
        {
            long documentNumber = 1;
            try
            {
                var docNumber = this._documentInformationProvider.GetLastDocument();
                if (docNumber != null)
                {
                    documentNumber = docNumber == null ? documentNumber : Convert.ToInt64(docNumber);
                    documentNumber = documentNumber + 1;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Error while fetching NextDocumentNumber {0}", ex));
                throw new BusinessException("", ex);
            }

            return documentNumber.ToString();
        }
    }
}
