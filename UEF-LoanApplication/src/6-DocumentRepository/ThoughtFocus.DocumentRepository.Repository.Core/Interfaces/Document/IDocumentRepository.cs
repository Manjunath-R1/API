using System;
using System.Collections.Generic;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using ThoughtFocus.DocumentRepository.Domain.Request;
using ThoughtFocus.DocumentRepository.Domain.Response;

namespace ThoughtFocus.DocumentRepository.Repository.Core
{
    public interface IDocumentRepository : IBaseRepository<Document>
    {
        void SaveDocuments(List<Document> documents);
        void SaveDocument(Document document);
        List<Document> GetDocumentsByParentId(Guid ParentId, Guid userID);       
        FilterResponse SearchDocuments(FilterRequest filterRequest);
        Document GetDocumentById(Guid documentID);
        Document GetLastDocument();
        void SaveDocumentProjectMapping(Document document);
        Document GetDocumentByNameAndProjectID(string FileName, Guid ProjectId);
        Document GetDocument(Guid documentID, Guid ProjectId);
        Document GetDocumentPrimaryProject(Guid documentID);
        Document GetDocumentVersionById(Guid documentVersionID);
    }
}
