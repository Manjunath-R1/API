using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using ThoughtFocus.DocumentRepository.Repository.Neo4j;
using ThoughtFocus.Common.Exceptions;

using ThoughtFocus.DocumentRepository.Repository.Core;
using ThoughtFocus.DocumentRepository.DataAccess.Neo4j;
using Neo4jClient;

namespace ThoughtFocus.DocumentRepository.Repository.Neo4j.Impl
{
    public class DocumentTagRepository : AbstractNeo4jBaseRepository<DocumentTag>, IDocumentTagRepository
    {
        private readonly ILogger<DocumentTagRepository> _logger;
        private readonly IGraphClient _graphClient;

        #region Constructors

        public DocumentTagRepository(IGraphClient graphClient,ILogger<DocumentTagRepository> logger)
        {
            _graphClient = graphClient;
            _logger = logger;
        }

        #endregion Constructors


        public void SaveDocumentTags(List<DocumentTag> documentTags)
        {
            try
            {
                if (!_graphClient.IsConnected)
                    _graphClient.ConnectAsync().Wait();

                var query = _graphClient.Cypher
                    .Unwind(documentTags, "documentTag")
                    .Match("(document:Document)")
                    .Where("document.DocumentID=documentTag.DocumentID")
                    .Merge("(Tag:DocumentTag {DocumentTagID : documentTag.DocumentTagID})")
                    .CreateUnique("(document)-[r:HAS]->(Tag)")
                    .Set("Tag = documentTag");

                query.ExecuteWithoutResultsAsync();
                _graphClient.Cypher.CreateUniqueConstraint("Tag:DocumentTag", "Tag.DocumentTagID").ExecuteWithoutResultsAsync();
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException in SaveTag repository", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Exception in SaveTag repository", ex);
            }
        }

        public List<DocumentTag> GetTagListByDocumentId(Guid documentId)
        {
            List<DocumentTag> documentTagList = new List<DocumentTag>();
            try
            {

                var Query = _graphClient.Cypher
                    .Match("(documentTag:DocumentTag)")
                    .Where((DocumentTag documentTag) => documentTag.DocumentID.ToString() == documentId.ToString() && documentTag.IsActive == true)
                    .Return((documentTag) => new
                    {
                        documentTag = documentTag.As<DocumentTag>()
                    });

                documentTagList = Query.ResultsAsync.Result.Select(d =>
                    new DocumentTag
                    {
                        DocumentTagID = d.documentTag.DocumentTagID,
                        CreatedDateTime = d.documentTag.CreatedDateTime,
                        LastModifiedDateTime = d.documentTag.LastModifiedDateTime,
                        CreatedByUserID = d.documentTag.CreatedByUserID,
                        LastModifiedByUserID = d.documentTag.LastModifiedByUserID,
                        IsActive = d.documentTag.IsActive,
                        DocumentID = d.documentTag.DocumentID,
                        TagID = d.documentTag.TagID,
                        Value = d.documentTag.Value,
                        IsDefault = d.documentTag.IsDefault,
                        Tag = _graphClient.Cypher.Match("(tag:Tag)").Where((Tag tag) => tag.TagID.ToString() == d.documentTag.TagID.ToString()).Return((tag) => new { tag = tag.As<Tag>() })
                        .ResultsAsync.Result.Select(t =>
                            new Tag 
                            {
                                Name = t.tag.Name,
                                TagTypeID = t.tag.TagTypeID,
                                TagID = t.tag.TagID
                            }).FirstOrDefault(),
                    }).ToList();

                return documentTagList;
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException exception-while Getting the GetTagListByDocumentId", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException-while Getting the GetTagListByDocumentId", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error while Getting the GetTagListByDocumentId ", ex);
            }
        }


        public DocumentTag GetTagById(Guid documentId, Guid tagID)
        {
            DocumentTag documentTag = new DocumentTag();
            try
            {

                var Query = _graphClient.Cypher
                    .Match("(d:DocumentTag)")
                    .Where((DocumentTag d) => d.DocumentID.ToString() == documentId.ToString() && d.TagID.ToString() == tagID.ToString() && d.IsActive == true)
                    .Return(d => d.As<DocumentTag>());

                documentTag = Query.ResultsAsync.Result.FirstOrDefault();

                return documentTag;
            }
            catch (NeoException ex)
            {
                throw new RepositoryException("NeoException exception-while Getting the GetTagById", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new RepositoryException("ObjectDisposedException-while Getting the GetTagById", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error while Getting the GetTagById", ex);
            }
        }
    }
}
