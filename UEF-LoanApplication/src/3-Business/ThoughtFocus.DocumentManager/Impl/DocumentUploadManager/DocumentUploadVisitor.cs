using Microsoft.Extensions.Logging;
using System;
using ThoughtFocus.DataAccess.Models;
using ThoughtFocus.Domain;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Enumeration;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Repository.Interfaces;
using ThoughtFocus.Common.Exceptions;
using ThoughtFocus.DocumentRepository.Core.Interfaces;
using ThoughtFocus.Repository.Interfaces.Application;
using ThoughtFocus.DocumentManager.Interfaces;

namespace ThoughtFocus.DocumentManager.Impl
{
    public class DocumentUploadVisitor : IDocumentVisitor
    {
        #region Fields
        //private static readonly ILogger<DocumentUploadVisitor> Logger;
        private IApplicationDocumentRepository _applicationDocumentRepository;

        #endregion

        #region Constructor
        public DocumentUploadVisitor(IApplicationDocumentRepository applicationDocumentRepository,
            IDocumentUploader documentUploader, IFileManager fileManager)
        {
            _applicationDocumentRepository = applicationDocumentRepository;
        }

        #endregion Constructor

        #region Methods


        public DocumentResponse HandleApplicationDocument(DocumentEntity documentEntity)
        {
            return this.UpdateApplicationDocument(documentEntity);
        }
  
        #endregion
        private DocumentResponse UpdateApplicationDocument(DocumentEntity documentEntity)
        {
            DocumentResponse uploadResponse = new DocumentResponse();
            try
            {
                ApplicationDocument applicationDocument = this._applicationDocumentRepository.FirstOrDefault(a => a.DocumentGUID == documentEntity.DocumentID && a.IsActive == true);

                if (applicationDocument == null)
                {
                    applicationDocument = new ApplicationDocument
                    {
                        CreatedByUserID = documentEntity.UserID,
                        CreatedDateTime = DateTime.Now,
                        DocumentGUID = documentEntity.DocumentID,
                        DocumentName = documentEntity.DocumentName,
                        DocumentTypeID = documentEntity.DocumentTypeID,
                        IsActive = true,
                        LoanApplicationID = (documentEntity as ApplicationDocumentEntity).ProgramInvitationID
                    };
                }

                applicationDocument.LastModifiedByUserID = documentEntity.UserID;
                applicationDocument.LastModifiedDateTime = DateTime.Now;
                applicationDocument.DocumentGUID = documentEntity.DocumentID;
                _applicationDocumentRepository.SaveApplicationDocument(applicationDocument);
                uploadResponse.IsSuccess = true;

                //if DocumentTypeID is member or chair document then insert to SiteVisitMemberDocument
                // if (documentEntity.DocumentTypeID == (int)DocumentTypeEnumeration.MemberReport || documentEntity.DocumentTypeID == (int)DocumentTypeEnumeration.ChairReport)
                //     this.UpdateSiteVisitMemberDocument(documentEntity, siteVisitDocument);

                return uploadResponse;
            }
            catch (RepositoryException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    }
}
