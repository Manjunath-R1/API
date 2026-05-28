using System;
using System.Collections.Generic;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;

namespace ThoughtFocus.DocumentRepository.Repository.Core
{
    public interface IDocumentVersionHistoryRepository : IBaseRepository<DocumentVersionHistory>
    {
        void SaveDocumentVersionHistories(List<DataAccess.Neo4j.DocumentVersionHistory> documentVersions);
        void SaveDocumentVersionHistory(DocumentVersionHistory documentVersions);
        List<DocumentVersionHistory> GetDocumentVersionHistoryId(Guid guid);
        DocumentVersionHistory GetDocumentVersionHistoryById(Guid guid);
        DocumentVersionHistory GetDocumentVersionHistoryByVersionKey(string documentVersionKey);
    }
}
