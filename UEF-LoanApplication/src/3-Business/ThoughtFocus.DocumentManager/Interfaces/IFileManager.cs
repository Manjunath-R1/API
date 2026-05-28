using System;
using ThoughtFocus.Domain;
using ThoughtFocus.Domain.Response;

namespace ThoughtFocus.DocumentManager.Interfaces
{
    public interface IFileManager
    {
       
        Guid FetchFolderID(Domain.CustomView.DocumentEntity documentEntity);

        DocumentResponse GetDocumentInfo(Guid documentID, long userID);

        FileExtensionResponse GetDocumentExtensions();

        string GetNextDocumentNumber();

    }
}
