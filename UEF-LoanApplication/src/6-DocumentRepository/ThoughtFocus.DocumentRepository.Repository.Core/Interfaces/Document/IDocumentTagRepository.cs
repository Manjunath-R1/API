using System;
using System.Collections.Generic;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;

namespace ThoughtFocus.DocumentRepository.Repository.Core
{
    public interface IDocumentTagRepository : IBaseRepository<DocumentTag>
    {
        void SaveDocumentTags(List<DocumentTag> documentTags);
        List<DocumentTag> GetTagListByDocumentId(Guid documentId);
        DocumentTag GetTagById(Guid documentId, Guid tagID);
    }
}
