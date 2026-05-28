using System;
using System.Collections.Generic;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using ThoughtFocus.DocumentRepository.Domain;
using ThoughtFocus.DocumentRepository.Domain.Response;

namespace ThoughtFocus.DocumentRepository.Core.Interfaces
{
    public interface IDocumentInformationProvider
    {
        List<FileExtensionType> GetSupportedFileExtensions();

        DocumentEntity GetDocumentByID(Guid documentID);
 
        string GetTheNextDocumentNumber();

        string GetStorageKey(DocumentUploadRequest documentUploadRequest, Document document);

        string GetDocumentPhysicalPath(DocumentRequest documentRequest, string storageKey);

        string GetVersionSalt(DocumentVersionHistory documentVersionHistory);

        string GetFolderPath(DocumentVersionHistory documentVersionHistory);

        ValidationResponse ValidateFileExtension(string fileName);

        ValidationResponse CanModifyDocumentState(Guid documentID, long userID);

        ValidationResponse IsDocumentLocked(Guid documentID);

        ValidationResponse ValidateTagType(long tagType);

        ValidationResponse IsDuplicateTag(string tagName);

        string GetExtensionImagePath(Guid FileExtensionTypeID);

        List<FileExtensionEntity> GetDocumentExtension();

        string GetLastDocument();

        DocumentResponse GetDocumentsByProjectId(Guid parentId,long userID);

        string GetFileExtensionById(Guid fileExtensionTypeID);

        DocumentResponse GetDocument(Guid documentId);

        string GetDocumentVersionkeyById(Guid documentId);

        List<DocumentVersionHistoryEntity> GetDocumentVersionHistoryById(Guid documentId);

        Properties GetProperties(Guid iD, string requestType);

        DocumentEntity GetDocumentByNameAndProjectID(string FileName, Guid ProjectId);
    }
}
