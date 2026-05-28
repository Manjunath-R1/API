using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThoughtFocus.AccrediO.Accreditation.Domain.Response;
using ThoughtFocus.DataAccess.Models;
using ThoughtFocus.Domain;
using ThoughtFocus.Domain.CustomView;
using ThoughtFocus.Domain.Response;
using ThoughtFocus.Domain.User;

namespace ThoughtFocus.Service.Interfaces
{
    public interface IDocumentService
    {
        ApplicationDocumentResponse GetApplicationDocumentByVersionID(string documentVersionkey, UserSessionEntity userSessionEntity);

        ApplicationDocumentResponse DeleteApplicationDocument(ApplicationDocumentRequest request);

        ApplicationDocumentResponse DeleteApplicationDocumentVersion(ApplicationDocumentRequest request);

        DocumentResponse GetDocumentIDByTypeAndApplication(long documentTypeID, long applicationID);

        Task<DocumentResponse> UploadDocument(DocumentEntity documentEntity);

        FileExtensionResponse GetAllDocumentExtension();

        DocumentResponse GetDocumentByID(Guid documentID, UserSessionEntity userSessionEntity);
        Task<DocumentResponse> DownloadDocument(UserSessionEntity userSessionEntity,string storageKey);

    }
}
